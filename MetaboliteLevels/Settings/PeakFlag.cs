using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// Used to allow the user to "flag" variables with quick comments.
    /// </summary>
    [Serializable]
    public class PeakFlag
    {
        private char _key;

        public string Id { get; set; }
        public char Key { get { return _key; } set { _key = char.ToUpper(value); } }
        public uint BeepFrequency { get; set; }
        public uint BeepDuration { get; set; }
        public string Comments { get; set; }
        public Color Colour { get; set; }

        public PeakFlag()
        {
            Init();
        }

        private void Init()
        {
            Id = "";
            Key = 'X';
            Comments = "";
            BeepFrequency = 3000;
            BeepDuration = 100;
            Colour = UiControls.NextColour();
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Init();
        }

        public PeakFlag(string p1, char p2, string p3, Color colour)
        {
            this.Id = p1;
            this.Key = p2;
            this.Comments = p3;
            this.Colour = colour;
        }

        public override string ToString()
        {
            return "[" + Key + "] " + Id + ": " + Comments;
        }
    }
}
