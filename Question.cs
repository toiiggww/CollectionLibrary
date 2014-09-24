using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CollectionLibrary
{
    public partial class Question : DialogFormBase
    {
        public Question()
        {
        }
        private TableLayoutPanel mbrClientAre;
        public string QuestionText
        {
            get { return mbrQuestion.Text; }
            set
            {
                Graphics g = mbrQuestion.CreateGraphics();
                SizeF s = g.MeasureString(value, mbrQuestion.Font, mbrQuestion.ClientRectangle.Width),c = g.MeasureString("C", mbrQuestion.Font);
                if (s.Height > mbrQuestion.Height || s.Height > mbrQuestion.ClientRectangle.Height)
                {
                    base.IncrementRowHeight(0, (int)(c.Height));
                }
            }
        }
        public string Answer { get { return mbrAnswer.Text.Trim(); } set { mbrAnswer.Text = value; } }
        protected override void InitializeCustomizedComponent()
        {
            InitializeComponent();
            mbrContinaer.Controls.Add(mbrClientAre);
            //base.InitializeCustomizedComponent();
        }

        private void mbrOK_Click(object sender, EventArgs e)
        {
            this.Text = Answer;
        }

        private void mbrAnswer_TextChanged(object sender, EventArgs e)
        {
            Graphics g = mbrAnswer.CreateGraphics();
            SizeF t = g.MeasureString(mbrAnswer.Text, mbrAnswer.Font, mbrAnswer.ClientRectangle.Width), c = g.MeasureString("C", mbrQuestion.Font);
            if (t.Height > mbrAnswer.Height || t.Height > mbrAnswer.ClientRectangle.Height)
            {
                if (!mbrAnswer.Multiline)
                {
                    mbrAnswer.Multiline = true;
                }
                base.IncrementRowHeight(0, (int)(c.Height));
            }
        }
    }
}
