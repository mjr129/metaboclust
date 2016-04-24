using MetaboliteLevels.Types.UI;
using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    internal partial class CtlBinder<T> : Component
    {
        readonly Dictionary<Control, CtrlInfo> _properties = new Dictionary<Control, CtrlInfo>();
        private Control _revertButtonSelection;

        [DefaultValue( true )]
        public bool GenerateRevertButtons { get; set; } = true;

        [DefaultValue( true )]
        public bool TestOnEdit { get; set; } = true;

        public CtlBinder()
        {
            InitializeComponent();
        }

        public class CtrlInfo
        {
            public PropertyPath<T, object> Path;
            public readonly Control Control;

            private T _target;
            private object _originalValue;
            private Converter<Control, object> _getter;
            private Action<Control, object> _setter;

            public CtrlInfo( Control control, PropertyPath<T, object> path, Converter<Control, object> getter, Action<Control, object> setter )
            {
                this.Control = control;
                this.Path = path;
                this._getter = getter;
                this._setter = setter;
            }

            public T Target
            {
                get
                {
                    return _target;
                }
                set
                {
                    _target = value;
                    _originalValue = TargetValue;
                }
            }

            public object OriginalValue
            {
                get
                {
                    return _originalValue;
                }
            }

            public object ControlValue
            {
                get
                {
                    return _getter( Control );
                }
                set
                {
                    _setter( Control, value );
                }
            }

            public object TargetValue
            {
                get
                {
                    return Path.Get( _target );
                }
                set
                {
                    Path.Set( _target, value );
                }
            }
        }

        public CtlBinder( IContainer container )
        {
            container.Add( this );

            InitializeComponent();

            _mnuSetToDefault.Click += _mnuSetToDefault_Click;
            _mnuUndoChanges.Click += _mnuUndoChanges_Click;
        }

        private void _mnuUndoChanges_Click( object sender, EventArgs e )
        {
            var ctrlInfo = _properties[_revertButtonSelection];
            ctrlInfo.ControlValue = ctrlInfo.OriginalValue;
        }

        private void _mnuSetToDefault_Click( object sender, EventArgs e )
        {
            var ctrlInfo = _properties[_revertButtonSelection];
            ctrlInfo.ControlValue = ctrlInfo.Path.DefaultValue;
        }

        public void Bind( Control control, Expression<PropertyPath<T, object>.Property> property )
        {
            // Get property path
            var path = new PropertyPath<T, object>( property );
            Converter<Control, object> getter = null;
            Action<Control, object> setter = null;
            var propType = path.Last.PropertyType;

            if (typeof( IConvertible ).IsAssignableFrom( propType ))
            {
                // Convertibles
                if (control is TextBox)
                {
                    getter = ( c ) => Convert.ChangeType( ((TextBox)c).Text, propType );
                    setter = ( c, v ) => ((TextBox)c).Text = Convert.ToString( v );
                }
                else if (control is CheckBox)
                {
                    getter = ( c ) => Convert.ChangeType( ((CheckBox)c).Checked, propType );
                    setter = ( c, v ) => ((CheckBox)c).Checked = Convert.ToBoolean( v );
                }
                else if (control is RadioButton)
                {
                    getter = ( c ) => Convert.ChangeType( ((RadioButton)c).Checked, propType );
                    setter = ( c, v ) => ((RadioButton)c).Checked = Convert.ToBoolean( v );
                }
                else if (control is NumericUpDown)
                {
                    getter = ( c ) => Convert.ChangeType( ((NumericUpDown)c).Value, propType );
                    setter = ( c, v ) => ((NumericUpDown)c).Value = Convert.ToDecimal( v );
                }
                else if (propType == typeof( bool ) && control is ComboBox)
                {
                    getter = ( c ) => ((ComboBox)c).SelectedIndex == 1;
                    setter = ( c, v ) => ((ComboBox)c).SelectedIndex = ((bool)v) ? 1 : 0;
                }
            }
            else if (propType == typeof( ParseElementCollection ) && control is TextBox)
            {
                getter = ( c ) => new ParseElementCollection( ((TextBox)control).Text );
                setter = ( c, v ) => ((TextBox)c).Text = ((ParseElementCollection)v).ToStringSafe();
            }
            else if (propType == typeof( Color ) && control is Button)
            {
                getter = ( c ) =>  ((Button)control).BackColor;
                setter = ( c, v ) => ((Button)c).BackColor = ((Color)v);
            }

            if (getter == null)
            {
                throw new InvalidOperationException( $"Cannot edit a property of type {propType.Name} with a control of type {control.GetType().Name}." );
            }

            _properties.Add( control, new CtrlInfo( control, path, getter, setter ) );

            // Track changes
            if (TestOnEdit)
            {
                TrackChanges( control );
            }

            // Create tooltops
            foreach (CtrlInfo ctrlInfo in _properties.Values)
            {
                StringBuilder sb = new StringBuilder();
                CategoryAttribute cat = ctrlInfo.Path.Last.GetCustomAttribute<CategoryAttribute>();
                DisplayNameAttribute namer = ctrlInfo.Path.Last.GetCustomAttribute<DisplayNameAttribute>();
                DescriptionAttribute desc = ctrlInfo.Path.Last.GetCustomAttribute<DescriptionAttribute>();

                if (cat != null)
                {
                    sb.Append( cat.Category.ToBold() + ": " );
                }

                if (namer != null)
                {
                    sb.Append( namer.DisplayName.ToBold() );
                }
                else
                {
                    sb.Append( property.Name.ToBold() );
                }

                if (desc != null)
                {
                    sb.AppendLine();
                    sb.Append( desc.Description );
                }

                toolTip1.SetToolTip( ctrlInfo.Control, sb.ToString() );
            }

            // Create reset button
            if (!(control is CheckBox) && GenerateRevertButtons)
            {
                CtlButton resetButton = new CtlButton
                {
                    Text = string.Empty,
                    Image = Properties.Resources.MnuUndo,
                    UseDefaultSize = true,
                    Visible = true,
                    Tag = control,
                    Margin = control.Margin
                };

                TableLayoutPanel tlp = (TableLayoutPanel)control.Parent;
                tlp.Controls.Add( resetButton, tlp.ColumnCount - 1, tlp.GetRow( control ) );

                resetButton.Enabled = path.Last.GetCustomAttribute<DefaultValueAttribute>() != null;

                resetButton.Click += resetButton_Click;
            }

            control.MouseUp += Control_MouseUp;
        }

        private void Control_MouseUp( object sender, MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Right)
            {
                _revertButtonSelection = (Control)sender;
                _cmsRevertButton.Show( _revertButtonSelection, e.Location );
            }
        }

        private void TrackChanges( Control control )
        {   
            if (control is TextBox)
            {
                ((TextBox)control).TextChanged += Control_SomethingChanged;
            }
            else if (control is CheckBox)
            {
                ((CheckBox)control).CheckedChanged += Control_SomethingChanged;
            }
            else if (control is RadioButton)
            {
                ((RadioButton)control).CheckedChanged += Control_SomethingChanged;
            }
            else if (control is NumericUpDown)
            {
                ((NumericUpDown)control).ValueChanged += Control_SomethingChanged;
            }
            else if (control is ComboBox)
            {
                ((ComboBox)control).SelectedIndexChanged += Control_SomethingChanged;
            }
            else if (control is Button)
            {
                ((Button)control).BackColorChanged += Control_SomethingChanged;
            }
        }

        private void Control_SomethingChanged( object sender, EventArgs e )
        {
            Control control = (Control)sender;
            var x = _properties[control];

            try
            {
                x.TargetValue = x.ControlValue;
                _errorProvider.Remove( control );
            }
            catch (Exception ex)
            {
                _errorProvider.Set( control, ex.Message );
            }
            finally
            {
                x.TargetValue = x.OriginalValue;
            }
        }

        private void resetButton_Click( object sender, EventArgs e )
        {
            CtlButton resetButton = (CtlButton)sender;

            _revertButtonSelection = (Control)resetButton.Tag;
            _cmsRevertButton.Show( resetButton, 0, resetButton.Height );
        }

        public void Read( T target )
        {
            foreach (var kvp in _properties)
            {
                kvp.Value.Target = target;
                kvp.Value.ControlValue = kvp.Value.TargetValue;
            }
        }

        internal void Commit()
        {
            foreach (var kvp in _properties)
            {
                kvp.Value.TargetValue = kvp.Value.ControlValue;
            }
        }     
    }
}
