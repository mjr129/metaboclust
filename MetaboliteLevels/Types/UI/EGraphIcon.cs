using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGui.Helpers;
namespace MetaboliteLevels.Types.UI
{
    enum EGraphIcon
    {
        [Name( "(Default)" )]
        Default,

        [Name( "●" )]
        Circle,

        [Name( "╳" )]
        Cross,

        [Name( "◆" )]
        Diamond,

        [Name( "─" )]
        HLine,

        [Name( "+" )]
        Plus,

        [Name( "■" )]
        Square,

        [Name( "✴" )]
        Asterisk,

        [Name( "▲" )]
        Triangle,

        [Name( "▼" )]    
        InvertedTriangle,

        [Name( "|" )]
        VLine,
    }

    enum EHatchStyle
    {          
        Solid                  =-1,
        None                   =-2,
        Horizontal             = HatchStyle.Horizontal,
        Min                    = HatchStyle.Min,
        Vertical               = HatchStyle.Vertical,
        ForwardDiagonal        = HatchStyle.ForwardDiagonal,
        BackwardDiagonal       = HatchStyle.BackwardDiagonal,
        Cross                  = HatchStyle.Cross,
        LargeGrid              = HatchStyle.LargeGrid,
        Max                    = HatchStyle.Max,
        DiagonalCross          = HatchStyle.DiagonalCross,
        Percent05              = HatchStyle.Percent05,
        Percent10              = HatchStyle.Percent10,
        Percent20              = HatchStyle.Percent20,
        Percent25              = HatchStyle.Percent25,
        Percent30              = HatchStyle.Percent30,
        Percent40              = HatchStyle.Percent40,
        Percent50              = HatchStyle.Percent50,
        Percent60              = HatchStyle.Percent60,
        Percent70              = HatchStyle.Percent70,
        Percent75              = HatchStyle.Percent75,
        Percent80              = HatchStyle.Percent80,
        Percent90              = HatchStyle.Percent90,
        LightDownwardDiagonal  = HatchStyle.LightDownwardDiagonal,
        LightUpwardDiagonal    = HatchStyle.LightUpwardDiagonal,
        DarkDownwardDiagonal   = HatchStyle.DarkDownwardDiagonal,
        DarkUpwardDiagonal     = HatchStyle.DarkUpwardDiagonal,
        WideDownwardDiagonal   = HatchStyle.WideDownwardDiagonal,
        WideUpwardDiagonal     = HatchStyle.WideUpwardDiagonal,
        LightVertical          = HatchStyle.LightVertical,
        LightHorizontal        = HatchStyle.LightHorizontal,
        NarrowVertical         = HatchStyle.NarrowVertical,
        NarrowHorizontal       = HatchStyle.NarrowHorizontal,
        DarkVertical           = HatchStyle.DarkVertical,
        DarkHorizontal         = HatchStyle.DarkHorizontal,
        DashedDownwardDiagonal = HatchStyle.DashedDownwardDiagonal,
        DashedUpwardDiagonal   = HatchStyle.DashedUpwardDiagonal,
        DashedHorizontal       = HatchStyle.DashedHorizontal,
        DashedVertical         = HatchStyle.DashedVertical,
        SmallConfetti          = HatchStyle.SmallConfetti,
        LargeConfetti          = HatchStyle.LargeConfetti,
        ZigZag                 = HatchStyle.ZigZag,
        Wave                   = HatchStyle.Wave,
        DiagonalBrick          = HatchStyle.DiagonalBrick,
        HorizontalBrick        = HatchStyle.HorizontalBrick,
        Weave                  = HatchStyle.Weave,
        Plaid                  = HatchStyle.Plaid,
        Divot                  = HatchStyle.Divot,
        DottedGrid             = HatchStyle.DottedGrid,
        DottedDiamond          = HatchStyle.DottedDiamond,
        Shingle                = HatchStyle.Shingle,
        Trellis                = HatchStyle.Trellis,
        Sphere                 = HatchStyle.Sphere,
        SmallGrid              = HatchStyle.SmallGrid,
        SmallCheckerBoard      = HatchStyle.SmallCheckerBoard,
        LargeCheckerBoard      = HatchStyle.LargeCheckerBoard,
        OutlinedDiamond        = HatchStyle.OutlinedDiamond,
        SolidDiamond           = HatchStyle.SolidDiamond,
    }
}
