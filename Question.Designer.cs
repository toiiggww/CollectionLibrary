namespace CollectionLibrary
{
    partial class Question
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
            this.mbrClientAre = new System.Windows.Forms.TableLayoutPanel();
            this.mbrQuestion = new System.Windows.Forms.Label();
            this.mbrAnswer = new System.Windows.Forms.TextBox();
            this.mbrClientAre.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrClientAre
            // 
            this.mbrContinaer.SetColumnSpan(this.mbrClientAre, 4);
            this.mbrClientAre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mbrClientAre.Controls.Add(this.mbrQuestion, 0, 0);
            this.mbrClientAre.Controls.Add(this.mbrAnswer, 0, 1);
            this.mbrClientAre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbrClientAre.Location = new System.Drawing.Point(3, 3);
            this.mbrClientAre.Name = "mbrClientAre";
            this.mbrClientAre.RowCount = 2;
            this.mbrClientAre.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.mbrClientAre.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mbrClientAre.Size = new System.Drawing.Size(276, 57);
            this.mbrClientAre.TabIndex = 2;
            // 
            // mbrQuestion
            // 
            this.mbrQuestion.AutoSize = true;
            this.mbrQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbrQuestion.Location = new System.Drawing.Point(3, 0);
            this.mbrQuestion.Name = "mbrQuestion";
            this.mbrQuestion.Size = new System.Drawing.Size(270, 27);
            this.mbrQuestion.TabIndex = 0;
            this.mbrQuestion.Text = "That a Question ...";
            // 
            // mbrAnswer
            // 
            this.mbrAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbrAnswer.Location = new System.Drawing.Point(3, 30);
            this.mbrAnswer.Name = "mbrAnswer";
            this.mbrAnswer.Size = new System.Drawing.Size(270, 21);
            this.mbrAnswer.TabIndex = 1;
            this.mbrAnswer.TextChanged += new System.EventHandler(this.mbrAnswer_TextChanged);
            // 
            // Question
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 100);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Question";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Question";
            this.mbrClientAre.ResumeLayout(false);
            this.mbrClientAre.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label mbrQuestion;
        private System.Windows.Forms.TextBox mbrAnswer;
    }
}