using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Settings;

namespace MetaboliteLevels.Controls
{
    public partial class CtlHelpBar : UserControl
    {
        private string _hash;
        private static readonly Color BgCol = Color.LightYellow;
        private static readonly Color FgCol = Color.Black;

        public CtlHelpBar()
        {
            InitializeComponent();

            BackColor = BgCol;
            ForeColor = FgCol;
            button1.BackColor = BgCol;
            button1.FlatAppearance.BorderColor = BgCol;
            button1.FlatAppearance.CheckedBackColor = Color.SteelBlue;
            button1.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            button1.FlatAppearance.MouseOverBackColor = Color.White;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SetSize();
        }

        private void SetSize()
        {
            label1.MaximumSize = new Size(ClientSize.Width - label1.Margin.Size.Width - button1.Width - button1.Margin.Size.Width, 512);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                // No action
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = FgCol;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = BgCol;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                label1.Text = value;
                base.Text = value;
                System.Security.Cryptography.HashAlgorithm algorithm = System.Security.Cryptography.SHA1.Create();
                byte[] bhash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                _hash = "QuickHelpBar:" + Convert.ToBase64String(bhash);
                ApplyVisible();
                SetSize();
            }
        }

        string _titleText;

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return _titleText;
            }
            set
            {
                _titleText = value;
                label2.Text = value;
            }
        }

        private void ApplyVisible()
        {
            if (MainSettings.Instance != null)
            {
                base.Visible = !MainSettings.Instance.DoNotShowAgain.ContainsKey(_hash);
            }
            else
            {
                base.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MainSettings.Instance != null)
            {
                SetHidden(_hash, true);

                base.Visible = false;
            }
        }

        public static bool GetHidden(string uid)
        {
            return MainSettings.Instance.DoNotShowAgain.ContainsKey(uid);
        }

        public static void SetHidden(string uid, bool inList)
        {
            if (MainSettings.Instance.DoNotShowAgain.ContainsKey(uid) != inList)
            {
                if (inList)
                {
                    MainSettings.Instance.DoNotShowAgain.Add(uid, 0);
                }
                else
                {
                    MainSettings.Instance.DoNotShowAgain.Remove(uid);
                }

                MainSettings.Instance.Save();
            }
        }
    }
}
