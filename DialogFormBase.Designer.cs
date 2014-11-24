namespace TEArts.Etc.CollectionLibrary
{
    partial class DialogFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mbrContinaer = new System.Windows.Forms.TableLayoutPanel();
            this.mbrOK = new System.Windows.Forms.Button();
            this.mbrCancel = new System.Windows.Forms.Button();
            this.mbrContinaer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrContinaer
            // 
            this.mbrContinaer.ColumnCount = 4;
            this.mbrContinaer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.19F));
            this.mbrContinaer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.81F));
            this.mbrContinaer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.mbrContinaer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mbrContinaer.Controls.Add(this.mbrOK, 0, 1);
            this.mbrContinaer.Controls.Add(this.mbrCancel, 3, 1);
            this.mbrContinaer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbrContinaer.Location = new System.Drawing.Point(5, 5);
            this.mbrContinaer.Name = "mbrContinaer";
            this.mbrContinaer.RowCount = 2;
            this.mbrContinaer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mbrContinaer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.mbrContinaer.Size = new System.Drawing.Size(282, 263);
            this.mbrContinaer.TabIndex = 0;
            // 
            // mbrOK
            // 
            this.mbrOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbrContinaer.SetColumnSpan(this.mbrOK, 2);
            this.mbrOK.Location = new System.Drawing.Point(60, 239);
            this.mbrOK.Name = "mbrOK";
            this.mbrOK.Size = new System.Drawing.Size(75, 21);
            this.mbrOK.TabIndex = 0;
            this.mbrOK.Text = "OK";
            this.mbrOK.UseVisualStyleBackColor = true;
            this.mbrOK.Click += new System.EventHandler(this.mbrOK_Click);
            // 
            // mbrCancel
            // 
            this.mbrCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbrCancel.Location = new System.Drawing.Point(144, 239);
            this.mbrCancel.Name = "mbrCancel";
            this.mbrCancel.Size = new System.Drawing.Size(75, 21);
            this.mbrCancel.TabIndex = 1;
            this.mbrCancel.Text = "Cancel";
            this.mbrCancel.UseVisualStyleBackColor = true;
            this.mbrCancel.Click += new System.EventHandler(this.mbrCancel_Click);
            // 
            // DialogFormBase
            // 
            this.AcceptButton = this.mbrOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbrCancel;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.mbrContinaer);
            this.Name = "DialogFormBase";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Title:";
            this.mbrContinaer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TableLayoutPanel mbrContinaer;
        protected System.Windows.Forms.Button mbrOK;
        protected System.Windows.Forms.Button mbrCancel;
    }
}