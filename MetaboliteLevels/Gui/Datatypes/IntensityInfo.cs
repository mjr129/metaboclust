using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Main;

namespace MetaboliteLevels.Gui.Datatypes
{
    /// <summary>
    /// For graphs.
    /// </summary>
    class IntensityInfo
    {
        public readonly int? Time;
        public readonly int? Rep;
        public readonly GroupInfo Group;
        public readonly double Intensity;

        public IntensityInfo(int? time, int? rep, GroupInfo group, double intensity)
        {
            this.Time = time;
            this.Rep = rep;
            this.Group = group;
            this.Intensity = intensity;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Don't add the group - this is described in the series name already and is random in multigroup mode
            //if (d.Group != null)
            //{
            //    sb.Append(d.Group.Name + " ");
            //}

            if (this.Time.HasValue)
            {
                sb.Append("day " + this.Time.Value + " ");
            }

            if (this.Rep.HasValue)
            {
                sb.Append("rep " + this.Rep.Value + " ");
            }

            sb.Append("= " + this.Intensity.ToString("F02"));

            return sb.ToString();
        }
    }
}
