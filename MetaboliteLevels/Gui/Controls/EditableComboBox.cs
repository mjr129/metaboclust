using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    /// <summary>
    /// Manages a combo-box populated with a <see cref="DataSet{T}"/>.
    /// Optionally possesses a button for editing the dataset.
    /// 
    /// This class manages a pre-created combobox and button, it does not create them.
    /// </summary>
    /// <typeparam name="T">Dataset type</typeparam>
    class EditableComboBox   : IDisposable
    {
        public readonly ComboBox ComboBox;
        private Button _button;
        private IDataSet _config;
        private ENullItemName _includeNull;
        private NamedItem<object> _none;
        private bool IsDisposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="box">Box to manage (mandatory)</param>
        /// <param name="editButton">Edit button (optional)</param>
        /// <param name="items">Items to populate with</param>
        /// <param name="includeNull">Whether to include an extra item for a "null" or empty selection.</param>
        public EditableComboBox( [NotNull] ComboBox box, [CanBeNull] Button editButton, [NotNull] IDataSet items, ENullItemName includeNull )
        {
            this.ComboBox = box;
            this._button = editButton;
            this._includeNull = includeNull;
            this._config = items;

            if (this._button != null)
            {
                this._button.Click += this._button_Click;
            }

            this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComboBox.DrawItem += this.ComboBox_DrawItem;
            this.ComboBox.DropDown += this.ComboBox_DropDown; // Update on every drop-down in case something else modified the list
            this.UpdateItemsNoPreserve(); // Don't do this yet or we'll NRE if the caller has events on the combobox
        }

        /// <summary>
        /// Unhooks event handlers
        /// </summary>
        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                this.IsDisposed = true;

                if (this._button != null)
                {
                    this._button.Click -= this._button_Click;
                }

                this.ComboBox.DrawItem -= this.ComboBox_DrawItem;
                this.ComboBox.DropDown -= this.ComboBox_DropDown;
            }
        }

        private void ComboBox_DrawItem( object sender, DrawItemEventArgs e )
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index == -1)
            {
                return;
            }

            NamedItem<object> n = (NamedItem<object>)this.ComboBox.Items[e.Index];

            Image image = UiControls.GetImage( n.Value, false );
            int offset = e.Bounds.Height / 2 - image.Height / 2;
            Rectangle imageDest = new Rectangle( e.Bounds.Left + offset, e.Bounds.Top + offset, image.Width, image.Height );
            Rectangle imageSrc = new Rectangle( 0, 0, image.Width, image.Height );
            e.Graphics.DrawImage( image, imageDest, imageSrc, GraphicsUnit.Pixel );
            SizeF stringSize = e.Graphics.MeasureString( n.DisplayName, e.Font );                                                                                          
            TextRenderer.DrawText( e.Graphics, n.DisplayName, e.Font, new Point(imageDest.Right + 4, (int)(e.Bounds.Top + e.Bounds.Height / 2 - stringSize.Height / 2)), e.ForeColor );
        }

        private void ComboBox_DropDown( object sender, EventArgs e )
        {
            this.UpdateItems();
        }

        private void UpdateItems()
        {
            object selection = this.SelectedItem;
            this.UpdateItemsNoPreserve();
            this.SelectedItem = selection; // Doesn't seem to care if "selection" isn't in the list (just doesn't do anything)
        }

        private void UpdateItemsNoPreserve()
        {
            this.ComboBox.Items.Clear();

            if (this._includeNull != ENullItemName.NoNullItem)
            {
                this._none = new NamedItem<object>( ReflectionHelper.GetDefault( this._config.DataType ), this._includeNull.ToUiString() );
                this.ComboBox.Items.Add( this._none );
            }

            NamedItem<object>[] source = NamedItem.GetRange<object>( this._config.UntypedGetList( true ).Cast<object>(), this._config.UntypedName ).ToArray();
            Array.Sort<NamedItem<object>>( source ); // For Julie
            this.ComboBox.Items.AddRange( source );
        }

        void _button_Click( object sender, EventArgs e )
        {
            if (this._config.ShowListEditor( this.ComboBox.FindForm() ))
            {
                this.UpdateItems();
            }
        }

        /// <summary>
        /// Returns if an item is selected (including the null item).
        /// </summary>
        public bool HasSelection
        {
            get { return this.ComboBox.SelectedItem != null; }
        }

        /// <summary>
        /// Clears the selection.
        /// </summary>
        public void ClearSelection()
        {
            this.ComboBox.SelectedItem = null;
        }

        /// <summary>
        /// Selected item
        /// </summary>
        /// <remarks>
        /// Setting to NULL when <see cref="_includeNull"/> is set to <see cref="ENullItemName.NoNullItem"/> will clear the selection (since there is no null item).
        /// Use <see cref="ClearSelection"/> to clear the selection in other cases.
        /// 
        /// Since NULL will be returned if the selection is clear OR if the null item is selected, use <see cref="HasSelection"/> to
        /// determine a selection has been made when <see cref="_includeNull"/> is not <see cref="ENullItemName.NoNullItem"/>.
        /// </remarks>
        public object SelectedItem
        {
            get
            {
                return NamedItem<object>.Extract( this.ComboBox.SelectedItem );
            }
            set
            {
                if (value == null)
                {
                    if ((this._includeNull != ENullItemName.NoNullItem))
                    {
                        this.ComboBox.SelectedItem = this._none;
                        return;
                    }
                    else
                    {
                        this.ClearSelection();
                        return;
                    }
                }

                this.ComboBox.SelectedItem = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.ComboBox.Enabled && this._button.Enabled;
            }
            set
            {
                this.ComboBox.Enabled = value;

                if (this._button != null)
                {
                    this._button.Enabled = value;
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.ComboBox.Visible && this._button.Visible;
            }
            set
            {
                this.ComboBox.Visible = value;

                if (this._button != null)
                {
                    this._button.Visible = value;
                }
            }
        }
    }

    
    class EditableComboBox<T> : EditableComboBox
    {
        public EditableComboBox( [NotNull] ComboBox box, [CanBeNull] Button editButton, [NotNull] DataSet<T> items, ENullItemName includeNull )
            : base( box, editButton, items, includeNull )
        {
        }

        public new T SelectedItem
        {
            get { return (T)base.SelectedItem; }
            set { base.SelectedItem = (T)value; }
        }
    }

    public enum ENullItemName
    {
        NoNullItem,

        [Name( "(All)" )]
        RepresentingAll,

        [Name( "(None)" )]
        RepresentingNone,
    }
}
