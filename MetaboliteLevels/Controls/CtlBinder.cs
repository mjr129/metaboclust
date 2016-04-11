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
        Dictionary<Control, PropertyInfo[]> _properties = new Dictionary<Control, PropertyInfo[]>();
        private T _target;

        public CtlBinder()
        {
            InitializeComponent();
        }

        public void Bind(Control control, Expression<Converter<T, object>> property)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            var body = property.Body;

            while (!(body is ParameterExpression))
            {
                var bodyUe = body as UnaryExpression;

                if (bodyUe != null)
                {
                    MemberExpression memEx = bodyUe.Operand as MemberExpression;
                    properties.Add((PropertyInfo)memEx.Member);

                    body = memEx.Expression;
                    continue;
                }

                var bodyMe = body as MemberExpression;

                if (bodyMe != null)
                {
                    properties.Add((PropertyInfo)bodyMe.Member);

                    body = bodyMe.Expression;
                    continue;
                }
            }

            _properties.Add(control, properties.Reverse<PropertyInfo>().ToArray());
        }

        internal void Read(T target)
        {
            _target = target;

            foreach (var kvp in _properties)
            {
                ControlSetValue(kvp.Key, kvp.Value);
            }
        }

        internal void Commit()
        {
            foreach (var kvp in _properties)
            {
                PropertySetValue(kvp.Value, ControlGetValue(kvp.Key));
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

        private void ControlSetValue(Control c, PropertyInfo[] propertyPath)
        {
            PropertyInfo property = null;
            object value = _target;

            foreach (PropertyInfo property2 in propertyPath)
            {
                value = property2.GetValue(value);
                property = property2;
            }

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

        private void PropertySetValue(PropertyInfo[] values, object v)
        {
            object target = _target;

            for (int n = 0; n < values.Length; n++)
            {
                PropertyInfo property = values[n];

                if (n == values.Length - 1)
                {
                    if (property.PropertyType == typeof(ParseElementCollection))
                    {
                        v = new ParseElementCollection((string)v);
                    }

                    property.SetValue(target, v);
                }
                else
                {
                    target = property.GetValue(target);
                }
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
