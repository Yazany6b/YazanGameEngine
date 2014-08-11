namespace GameEngin
{
    partial class Form1
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
            this.handler1Panel = new System.Windows.Forms.Panel();
            this.startPanel = new System.Windows.Forms.Panel();
            this.endPanel = new System.Windows.Forms.Panel();
            this.handler2Panel = new System.Windows.Forms.Panel();
            this.lineStartPanel = new System.Windows.Forms.Panel();
            this.lineEndPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // handler1Panel
            // 
            this.handler1Panel.BackColor = System.Drawing.Color.Red;
            this.handler1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handler1Panel.Location = new System.Drawing.Point(446, 273);
            this.handler1Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.handler1Panel.Name = "handler1Panel";
            this.handler1Panel.Size = new System.Drawing.Size(9, 8);
            this.handler1Panel.TabIndex = 2;
            this.handler1Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseDown);
            this.handler1Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.handler1Panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // startPanel
            // 
            this.startPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.startPanel.Location = new System.Drawing.Point(647, 280);
            this.startPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(9, 8);
            this.startPanel.TabIndex = 1;
            this.startPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseDown);
            this.startPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.startPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // endPanel
            // 
            this.endPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.endPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.endPanel.Location = new System.Drawing.Point(417, 370);
            this.endPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.endPanel.Name = "endPanel";
            this.endPanel.Size = new System.Drawing.Size(9, 8);
            this.endPanel.TabIndex = 4;
            this.endPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseDown);
            this.endPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.endPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // handler2Panel
            // 
            this.handler2Panel.BackColor = System.Drawing.Color.Red;
            this.handler2Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handler2Panel.Location = new System.Drawing.Point(487, 150);
            this.handler2Panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.handler2Panel.Name = "handler2Panel";
            this.handler2Panel.Size = new System.Drawing.Size(9, 8);
            this.handler2Panel.TabIndex = 3;
            this.handler2Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseDown);
            this.handler2Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.handler2Panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // lineStartPanel
            // 
            this.lineStartPanel.BackColor = System.Drawing.Color.Goldenrod;
            this.lineStartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineStartPanel.Location = new System.Drawing.Point(602, 236);
            this.lineStartPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lineStartPanel.Name = "lineStartPanel";
            this.lineStartPanel.Size = new System.Drawing.Size(9, 8);
            this.lineStartPanel.TabIndex = 5;
            this.lineStartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.lineStartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // lineEndPanel
            // 
            this.lineEndPanel.BackColor = System.Drawing.Color.Goldenrod;
            this.lineEndPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineEndPanel.Location = new System.Drawing.Point(551, 255);
            this.lineEndPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lineEndPanel.Name = "lineEndPanel";
            this.lineEndPanel.Size = new System.Drawing.Size(9, 8);
            this.lineEndPanel.TabIndex = 6;
            this.lineEndPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.startPanel_MouseMove);
            this.lineEndPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handler2Panel_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1321, 578);
            this.Controls.Add(this.lineEndPanel);
            this.Controls.Add(this.lineStartPanel);
            this.Controls.Add(this.handler2Panel);
            this.Controls.Add(this.endPanel);
            this.Controls.Add(this.startPanel);
            this.Controls.Add(this.handler1Panel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel handler1Panel;
        private System.Windows.Forms.Panel startPanel;
        private System.Windows.Forms.Panel endPanel;
        private System.Windows.Forms.Panel handler2Panel;
        private System.Windows.Forms.Panel lineStartPanel;
        private System.Windows.Forms.Panel lineEndPanel;
    }
}

