using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetaboliteLevels.Controls.Lists
{
    partial class CtlAutoList
    {
        class ColumnFilter
        {
            private readonly CtlAutoList _owner;
            public readonly Column _column;
            public readonly ListVieweHelper.EOperator Operator;
            public readonly double NumValue;
            public readonly string TextValue;

            public ColumnFilter(CtlAutoList owner, Column column, ListVieweHelper.EOperator op, string va)
            {
                this._owner = owner;
                this.Operator = op;
                this._column = column;
                this.TextValue = va.ToUpper();

                if (!double.TryParse(va, out this.NumValue))
                {
                    this.NumValue = double.NaN;
                }
            }

            public bool FilterRemove(object value)
            {
                object v = _column.GetRow(value);

                switch (Operator)
                {
                    case ListVieweHelper.EOperator.TextContains:
                        return !AsBString(v).Contains(TextValue);

                    case ListVieweHelper.EOperator.TextDoesNotContain:
                        return AsBString(v).Contains(TextValue);

                    case ListVieweHelper.EOperator.Regex:
                        return !Regex.IsMatch(AsBString(v), TextValue);

                    case ListVieweHelper.EOperator.EqualTo:
                        return ToDouble(v) != NumValue;

                    case ListVieweHelper.EOperator.NotEqualTo:
                        return ToDouble(v) == NumValue;

                    case ListVieweHelper.EOperator.LessThan:
                        return ToDouble(v) >= NumValue;

                    case ListVieweHelper.EOperator.LessThanEq:
                        return ToDouble(v) > NumValue;

                    case ListVieweHelper.EOperator.MoreThan:
                        return ToDouble(v) <= NumValue;

                    case ListVieweHelper.EOperator.MoreThanEq:
                        return ToDouble(v) < NumValue;

                    default:
                        throw new InvalidOperationException("Invalid switch: " + Operator);
                }
            }

            private string AsBString(object v)
            {
                return Column.AsString(v, EListDisplayMode.Content).ToUpper();
            }

            private double ToDouble(object v)
            {
                if (v is ICollection)
                {
                    return ((ICollection)v).Count;
                }

                return Convert.ToDouble(v);
            }
        }
    }
}
