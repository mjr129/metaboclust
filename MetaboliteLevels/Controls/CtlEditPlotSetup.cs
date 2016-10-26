using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MGui.Controls;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Controls
{
    internal partial class CtlEditPlotSetup : UserControl
    {                    
        public CtlEditPlotSetup()
        {
            InitializeComponent();    
        }

        public void BindAll<T>( CtlBinder<T> _binder, PropertyPath<T, PlotSetup> source)
        {                                        
            _binder.BinderCollection.Add( new AxisBinder() );
                                                                                 
            _binder.Bind( _txtClusterInfo    , source.Append<object>( z => z.Information ) );
            _binder.Bind( _txtClusterSubtitle, source.Append<object>( z => z.SubTitle    ) );
            _binder.Bind( _txtClusterTitle   , source.Append<object>( z => z.Title       ) );
            _binder.Bind( _txtClusterXAxis   , source.Append<object>( z => z.AxisX       ) );
            _binder.Bind( _txtClusterYAxis   , source.Append<object>( z => z.AxisY       ) );
            _binder.Bind( _xrMax             , source.Append<object>( z => z.RangeXMax   ) );
            _binder.Bind( _xRmin             , source.Append<object>( z => z.RangeXMin   ) );
            _binder.Bind( _yrMax             , source.Append<object>( z => z.RangeYMax   ) );
            _binder.Bind( _yrMin             , source.Append<object>( z => z.RangeYMin   ) );
        }

        private sealed class AxisBinder : Binder<ComboBox, AxisRange>
        {
            protected override void ConfigureControl( ComboBox control, Type dataType )
            {
                base.ConfigureControl( control, dataType );

                control.Items.Add( EAxisRange.Automatic.ToUiString() );
                control.Items.Add( EAxisRange.General.ToUiString() );
                control.Items.Add( "1.0" );

                control.DrawMode = DrawMode.OwnerDrawFixed;
                control.DrawItem += Control_DrawItem;
            }

            private void Control_DrawItem( object sender, DrawItemEventArgs e )
            {
                var cb = (ComboBox)sender;

                e.DrawBackground();

                if (e.Index != -1)
                {
                    string text = cb.Items[e.Index].ToString();

                    using (SolidBrush b = new SolidBrush( e.ForeColor ))
                    {
                        if (!e.State.Has( DrawItemState.ComboBoxEdit ))
                        {
                            switch (e.Index)
                            {
                                case 0:
                                    text = EnumHelper.ToDescription( EAxisRange.Automatic );
                                    break;

                                case 1:
                                    text = EnumHelper.ToDescription( EAxisRange.General );
                                    break;

                                case 2:
                                    text = EnumHelper.ToDescription( EAxisRange.Fixed );
                                    break;
                            }
                        }

                        e.Graphics.DrawString( text, e.Font, b, e.Bounds );
                    }
                }

                e.DrawFocusRectangle();
            }

            protected override AxisRange GetValue( ComboBox control, Type dataType )
            {
                return new AxisRange( control.Text );
            }

            protected override void SetValue( ComboBox control, AxisRange value, Type dataType )
            {
                control.Text = value.ToString();
            }
        }      
    }
}
