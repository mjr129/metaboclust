using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaboliteLevels.Controls
{
    class CtlBinder<T>
    {
        private ToolTip toolTip1;
        private IContainer components;
        Dictionary<Control, PropertyPath> _properties = new Dictionary<Control, PropertyPath>();
        private T _target;

        public CtlBinder()
        {
            InitializeComponent();
        }

        class PropertyPath
        {                                                     
            PropertyInfo[] _properties;

            public PropertyPath( Expression<Converter<T, object>> property )
            {
                List<PropertyInfo> properties = new List<PropertyInfo>();

                var body = property.Body;

                while (!(body is ParameterExpression))
                {
                    var bodyUe = body as UnaryExpression;

                    if (bodyUe != null)
                    {
                        MemberExpression memEx = bodyUe.Operand as MemberExpression;
                        properties.Add( (PropertyInfo)memEx.Member );

                        body = memEx.Expression;
                        continue;
                    }

                    var bodyMe = body as MemberExpression;

                    if (bodyMe != null)
                    {
                        properties.Add( (PropertyInfo)bodyMe.Member );

                        body = bodyMe.Expression;
                        continue;
                    }
                }

                _properties = properties.Reverse<PropertyInfo>().ToArray();
            } 

            public PropertyInfo Last
            {
                get
                {
                    return _properties[_properties.Length - 1];
                }
            }

            public object this[object target]
            {
                get
                {
                    PropertyInfo property = null;
                    object value = target;

                    foreach (PropertyInfo property2 in _properties)
                    {
                        value = property2.GetValue( value );
                        property = property2;
                    }

                    return value;
                }
                set
                {
                    for (int n = 0; n < _properties.Length; n++)
                    {
                        PropertyInfo property = _properties[n];

                        if (n == _properties.Length - 1)
                        {
                            if (property.PropertyType == typeof( ParseElementCollection ))
                            {
                                value = new ParseElementCollection( (string)value );
                            }

                            property.SetValue( target, value );
                        }
                        else
                        {
                            target = property.GetValue( target );
                        }
                    }
                }
            }
        }

        public void Bind(Control control, Expression<Converter<T, object>> property)
        {
            // Get property path
            var path = new PropertyPath( property );
            _properties.Add( control, path );

            // Create reset button
            if (!(control is CheckBox))
            {
                CtlButton resetButton = new CtlButton();
                resetButton.Text = string.Empty;
                resetButton.Image = Properties.Resources.MnuUndo;
                resetButton.UseDefaultSize = true;
                resetButton.Visible = true;
                resetButton.Tag = control;
                resetButton.Margin = control.Margin;

                TableLayoutPanel tlp = (TableLayoutPanel)control.Parent;
                tlp.Controls.Add( resetButton, tlp.ColumnCount - 1, tlp.GetRow( control ) );

                resetButton.Enabled = path.Last.GetCustomAttribute<DefaultValueAttribute>() != null;

                resetButton.Click += resetButton_Click;
            }
        }

        private void resetButton_Click( object sender, EventArgs e )
        {
            CtlButton resetButton = (CtlButton)sender;
            Control editControl = (Control )resetButton.Tag;
            PropertyPath property = _properties[editControl];

            var attr = property.Last.GetCustomAttribute<DefaultValueAttribute>();

            property[_target] = attr.Value;

            ControlSetValue( editControl );
        }

        internal void Read(T target)
        {
            _target = target;

            foreach (Control control in _properties.Keys)
            {
                ControlSetValue(control);
            }
        }

        internal void Commit()
        {
            foreach (var kvp in _properties)
            {
                kvp.Value[_target] = ControlGetValue(kvp.Key);
            }
        }

        private object ControlGetValue(Control c)
        {
            if (c is TextBox)
            {
                return ((TextBox)c).Text;
            }
            else if (c is CheckBox)
            {
                return ((CheckBox)c).Checked;
            }
            else if (c is ComboBox)
            {
                return ((ComboBox)c).SelectedIndex != 0;
            }
            else if (c is NumericUpDown)
            {
                return (int)((NumericUpDown)c).Value;
            }
            else if (c is Button)
            {
                return ((Button)c).BackColor;
            }
            else
            {
                throw new SwitchException(c);
            }
        }

        private void ControlSetValue(Control c)
        {
            PropertyPath propertyPath = _properties[c];
            object value = propertyPath[_target];
            var property = propertyPath.Last;

            StringBuilder sb = new StringBuilder();
            CategoryAttribute cat = property.GetCustomAttribute<CategoryAttribute>();
            DisplayNameAttribute namer = property.GetCustomAttribute<DisplayNameAttribute>();
            DescriptionAttribute desc = property.GetCustomAttribute<DescriptionAttribute>();

            if (cat != null)
            {
                sb.Append(cat.Category.ToBold() + ": ");
            }

            if (namer != null)
            {
                sb.Append(namer.DisplayName.ToBold());
            }
            else
            {
                sb.Append(property.Name.ToBold());
            }

            if (desc != null)
            {
                sb.AppendLine();
                sb.Append(desc.Description);
            }

            toolTip1.SetToolTip(c, sb.ToString());

            if (c is TextBox)
            {
                ((TextBox)c).Text = value != null ? value.ToString() : "";
            }
            else if (c is CheckBox)
            {
                ((CheckBox)c).Checked = (bool)value;
            }
            else if (c is ComboBox)
            {
                ((ComboBox)c).SelectedIndex = ((bool)value ? 1 : 0);
            }
            else if (c is NumericUpDown)
            {
                ((NumericUpDown)c).Value = Convert.ToDecimal(value);
            }
            else if (c is Button)
            {
                ((Button)c).BackColor = (Color)value;
            }
            else
            {
                throw new SwitchException(c);
            }
        }   

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 50000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 10;

        }
    }
}
