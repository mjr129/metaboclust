using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Controls
{
    internal class EnumComboBox<T>
        where T : struct
    {
        public ComboBox ComboBox;
        private T? _fallback;

        public EnumComboBox(ComboBox box, T? def, T? fallback, bool useFirst)
        {
            this.ComboBox = box;
            this._fallback = fallback;
            EnumComboBox.Populate<T>(box, def, useFirst);
        }

        public bool HasSelection
        {
            get { return this.ComboBox.SelectedItem != null; }
        }

        public T SelectedItemOrDefault
        {
            get { return EnumComboBox.Get(this.ComboBox, this._fallback); }
            set { EnumComboBox.Set(this.ComboBox, (T?)value); }
        }

        public T? SelectedItem
        {
            get
            {
                if (this.HasSelection)
                {
                    return EnumComboBox.Get(this.ComboBox, this._fallback);
                }
                else
                {
                    return null;
                }
            }
            set { EnumComboBox.Set(this.ComboBox, (T?)value); }
        }

        public void ClearSelection()
        {
            EnumComboBox.Set(this.ComboBox, (T?)null);
        }

        public bool Visible
        {
            get { return this.ComboBox.Visible; }
            set { this.ComboBox.Visible = value; }
        }
    }

    /// <summary>
    /// Handles a ComboBox containing a list of the members of the enum.
    /// </summary>
    static class EnumComboBox
    {
        private class EnumAsText
        {
            public readonly object Value;
            public readonly string Text;

            public EnumAsText(object v, string p)
            {
                this.Value = v;
                this.Text = p;
            }

            public override string ToString()
            {
                return this.Text;
            }
        }

        public static EnumComboBox<T> Create<T>(ComboBox box) where T : struct
        {
            return new EnumComboBox<T>(box, (T?)null, (T?)null, false);
        }

        public static EnumComboBox<T> Create<T>(ComboBox box, T def) where T : struct
        {
            return new EnumComboBox<T>(box, (T?)def, (T?)def, false);
        }

        public static EnumComboBox<T> Create<T>(ComboBox box, T def, bool useFirst) where T : struct
        {
            return new EnumComboBox<T>(box, (T?)def, (T?)def, useFirst);
        }

        public static EnumComboBox<T> Create<T>(ComboBox box, T def, T fallback, bool useFirst) where T : struct
        {
            return new EnumComboBox<T>(box, (T?)def, (T?)fallback, useFirst);
        }

        /// <summary>
        /// Create an EnumComboBox with the contents of an enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <param name="default">Initially selected value (if this is null or not a member of the enum then this selects the "none" selection)</param>
        /// <param name="useFirstForNone">If [default] is not in the list then use the first member of the enum.</param>
        internal static void Populate<T>(ComboBox comboBox, T? @default, bool useFirstForNone) where T : struct
        {
            comboBox.Items.Clear();

            foreach (Enum v in Enum.GetValues(typeof(T)))
            {
                string txt = v.ToUiString();

                if (!txt.StartsWith("_"))
                {
                    comboBox.Items.Add(new EnumAsText(v, txt));
                }
            }

            Set(comboBox, @default, useFirstForNone);
        }

        /// <summary>
        /// Create an EnumComboBox with the contents of an enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <param name="default">Initially selected value (if this is null or not a member of the enum then this selects the "none" selection)</param>
        internal static void Populate<T>(ComboBox comboBox, T @default) where T : struct
        {
            Populate<T>(comboBox, (T?)@default, false);
        }

        /// <summary>
        /// Create an EnumComboBox with the contents of an enum.
        /// The default value is the "none" selection.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>                                                                                                
        internal static void Populate<T>( ComboBox comboBox ) where T : struct
        {
            Populate<T>( comboBox, (T?)null, false );
        }

        /// <summary>
        /// Sets the value of an EnumComboBox.
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <param name="value">Selected value</param>
        internal static void Set<T>(ComboBox comboBox, T? value) where T : struct
        {
            Set(comboBox, value, false);
        }

        internal static void Set<T>(ComboBox comboBox, T val, bool useFirst) where T : struct
        {
            Set<T>(comboBox, (T?) val, useFirst);
        }

        /// <summary>
        /// Sets the value of an EnumComboBox.
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <param name="val">Initially selected value (if not a member of the enum then selects nothing)</param>
        /// <param name="forceDefault">If [def] is not in the list then use the first member of the enum.</param>
        internal static void Set<T>(ComboBox comboBox, T? val, bool useFirst) where T : struct
        {
            if (val.HasValue)
            {
                foreach (EnumAsText et in comboBox.Items)
                {
                    if (((T)et.Value).Equals(val.Value))
                    {
                        comboBox.SelectedItem = et;
                        return;
                    }
                }
            }

            if (useFirst)
            {
                comboBox.SelectedIndex = 0;
            }
            else
            {
                comboBox.SelectedItem = null;
            }
        }

        /// <summary>
        /// Clears the selection.
        /// </summary>
        internal static void Clear(ComboBox comboBox)
        {
            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Gets the value of combobox populated with enums using CreateEnumComboBox. 
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <param name="ifNotSet">If the combobox is not set what to return</param>
        /// <returns>Selection as T</returns>
        internal static T Get<T>(ComboBox comboBox, T? ifNotSet) where T : struct
        {
            if (comboBox.SelectedItem == null)
            {
                if (ifNotSet.HasValue)
                {
                    return ifNotSet.Value;
                }
                else
                {
                    throw new InvalidOperationException("Item not selected in list of type " + typeof(T).Name + ".");
                }
            }

            return (T)((EnumAsText)comboBox.SelectedItem).Value;
        }

        internal static T Get<T>(ComboBox comboBox, T ifNotSet) where T : struct
        {
            return Get(comboBox, (T?)ifNotSet);
        }

        /// <summary>
        /// Gets the value of combobox populated with enums using CreateEnumComboBox. 
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="comboBox">The combobox</param>
        /// <exception cref="InvalidOperationException">If no selection.</exception>
        /// <returns>Selection as T</returns>
        internal static T Get<T>(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                throw new InvalidOperationException("No item selected from list.");
            }

            return (T)((EnumAsText)comboBox.SelectedItem).Value;
        }
    }
}
