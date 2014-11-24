using System;
using System.Windows.Forms;

namespace TEArts.Etc.CollectionLibrary
{
    public partial class DialogFormBase : Form
    {
        public DialogFormBase()
        {
            InitializeComponent();
            InitializeCustomizedComponent();
        }
        protected virtual void InitializeCustomizedComponent() { }
        protected virtual void OnOkClicked() { }
        protected virtual void OnCancelClicked() { }
        private void mbrOK_Click(object sender, EventArgs e)
        {
            OnOkClicked();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        protected void IncrementRowHeight(int r, int h)
        {
            if (r >= mbrContinaer.RowCount)
            {
                return;
            }
            if (mbrContinaer.RowStyles[r].SizeType == SizeType.Absolute)
            {
                mbrContinaer.RowStyles[r].Height += h;
            }
            this.Height += h;
        }
        protected void IncrementColumHeight(int c, int w)
        {
            if (c >= mbrContinaer.ColumnCount)
            {
                return;
            }
            if (mbrContinaer.ColumnStyles[c].SizeType == SizeType.Absolute)
            {
                mbrContinaer.ColumnStyles[c].Width += w;
            }
            this.Width += w;
        }
        private void mbrCancel_Click(object sender, EventArgs e)
        {
            OnCancelClicked();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
