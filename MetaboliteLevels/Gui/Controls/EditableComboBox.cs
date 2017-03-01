using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Properties;
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
        private readonly Button _button;
        private readonly IDataSet _config;
        private readonly ENullItemName _includeNull;
        private NamedItem<object> _none;
        private bool IsDisposed;
        private EFlags _flags;

        [Flags]
        public enum EFlags
        {
            /// <summary>
            /// Nothing special
            /// </summary>
            None = 0,

            /// <summary>
            /// Include an item representing a "none" selection (a null selection). Incompatible with <see cref="IncludeAll"/>.
            /// </summary>
            IncludeNone = 1,

            /// <summary>
            /// Include an item representing an "all" selection (a null selection). Incompatible with <see cref="IncludeNone"/>.
            /// </summary>
            IncludeAll = 2,

            /// <summary>
            /// If SelectedItem is set to a non-existant item then that item will be added to the list UNTIL the user changes the selection
            /// </summary>
            CreateSelection = 4,
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="box">Box to manage (mandatory)</param>
        /// <param name="editButton">Edit button (optional)</param>
        /// <param name="items">Items to populate with</param>
        /// <param name="includeNull">Whether to include an extra item for a "null" or empty selection.</param>
        public EditableComboBox( [NotNull] ComboBox box, [CanBeNull] Button editButton, [NotNull] IDataSet items, EFlags flags )
        {
            this.ComboBox = box;
            this._button = editButton;
            this._includeNull = flags.Has( EFlags.IncludeNone ) ? ENullItemName.RepresentingNone : flags.Has( EFlags.IncludeAll ) ? ENullItemName.RepresentingAll : ENullItemName.NoNullItem;
            this._flags = flags;
            this._config = items;

            if (this._button != null)
            {
                this._button.Click += this._button_Click;
            }

            this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComboBox.DrawItem += this.ComboBox_DrawItem;
            this.ComboBox.DropDown += this.ComboBox_DropDown; // Update on every drop-down in case something else modified the list
            this.ComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
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

            if (!Enabled)
            {
                image = UiControls.DisabledImage( image );
            }

            Color colour = Enabled ? e.ForeColor : Color.FromKnownColor( KnownColor.GrayText );

            if (Enabled && n.Value is SpecialEditItem)
            {
                if (e.State.Has( DrawItemState.ComboBoxEdit ))
                {
                    return;
                }

                Rectangle rect;
                const int BUTTON_WIDTH = 64;

                if (e.Bounds.Width > BUTTON_WIDTH)
                {
                    rect = new Rectangle( e.Bounds.Right - BUTTON_WIDTH, e.Bounds.Top, BUTTON_WIDTH, e.Bounds.Height );
                }
                else
                {
                    rect = new Rectangle( e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height );
                }

                ButtonRenderer.DrawButton( e.Graphics, rect, n.Value.ToString(), e.Font, e.State.Has( DrawItemState.Focus ), PushButtonState.Normal );
                return;
            }

            int offset = e.Bounds.Height / 2 - image.Height / 2;
            Rectangle imageDest = new Rectangle( e.Bounds.Left + offset, e.Bounds.Top + offset, image.Width, image.Height );
            Rectangle imageSrc = new Rectangle( 0, 0, image.Width, image.Height );
            e.Graphics.DrawImage( image, imageDest, imageSrc, GraphicsUnit.Pixel );
            SizeF stringSize = e.Graphics.MeasureString( n.DisplayName, e.Font );
            TextRenderer.DrawText( e.Graphics, n.DisplayName, e.Font, new Point( imageDest.Right + 4, (int)(e.Bounds.Top + e.Bounds.Height / 2 - stringSize.Height / 2) ), colour );
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

            // Null ("all" or "none") item
            if (this._includeNull != ENullItemName.NoNullItem)
            {
                this._none = new NamedItem<object>( ReflectionHelper.GetDefault( this._config.DataType ), this._includeNull.ToUiString() );
                this.ComboBox.Items.Add( this._none );
            }

            // Regular items
            NamedItem<object>[] source = NamedItem.GetRange<object>( this._config.UntypedGetList( true ).Cast<object>(), this._config.UntypedName ).ToArray();
            Array.Sort<NamedItem<object>>( source ); // For Julie
            this.ComboBox.Items.AddRange( source );

            // Edit button item        
            if (_button != null)
            {
                this.ComboBox.Items.Add( new NamedItem<object>( new SpecialEditItem() ) );
            }
        }

        class SpecialEditItem : IIconProvider
        {
            public override string ToString()
            {
                return "More...";
            }

            public Image Icon => Resources.MnuEnlargeList;
        }

        private object _lastValidSelection;

        private void ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if (InternalSelection is SpecialEditItem)
            {
                ShowEditor();
                return;
            }

            _lastValidSelection = SelectedItem;
        }

        void _button_Click( object sender, EventArgs e )
        {
            ShowEditor();
        }

        private void ShowEditor()
        {
            BigListResult<object> res = this._config.ShowListEditor( this.ComboBox.FindForm() );

            if (res != null)
            {
                this.UpdateItemsNoPreserve();
                this.SelectedItem = res.Selection;
            }
            else
            {
                this.SelectedItem = _lastValidSelection;
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
        /// The item really selected.
        /// </summary>
        private object InternalSelection => NamedItem<object>.Extract( this.ComboBox.SelectedItem );

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
                // The caller might be responding to a SelectedItemChanged event when we get this,
                // so we might not have have had chance to update our own _lastValidSelection, depending on the order the
                // events come through. Instead return the real item, unless it is the "button", in which case stick
                // with whatever was selected last (the caller will think nothing has happened).
                if (InternalSelection is SpecialEditItem)
                {
                    return _lastValidSelection;
                }
                else
                {
                    return InternalSelection;
                }                            
            }
            set
            {
                // Special case for null handling
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

                // Special case for EFlags.CreateSelection
                if (_flags.Has( EFlags.CreateSelection ))
                {
                    if (!this.ComboBox.Items.Contains( value ))
                    {
                        int index = this.ComboBox.Items.Count - 1;

                        if (this.ComboBox.Items[index] is SpecialEditItem)
                        {
                            --index;
                        }           

                        this.ComboBox.Items.Insert( index, new NamedItem<object>( value, this._config.UntypedName ) );
                    }
                }

                this.ComboBox.SelectedItem = value; // Doesn't do anything if value isn't in list
            }
        }

        public bool Enabled
        {
            get
            {
                return this.ComboBox.Enabled;
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
        public EditableComboBox( [NotNull] ComboBox box, [CanBeNull] Button editButton, [NotNull] DataSet<T> items, EFlags flags )
            : base( box, editButton, items, flags )
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
