namespace MineSweeper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            nouvellePartieToolStripMenuItem = new ToolStripMenuItem();
            separator1 = new ToolStripSeparator();
            débutantToolStripMenuItem = new ToolStripMenuItem();
            intermédiaireToolStripMenuItem = new ToolStripMenuItem();
            expertToolStripMenuItem = new ToolStripMenuItem();
            separator2 = new ToolStripSeparator();
            quitterToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            userControl11 = new UserControl1();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(596, 28);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip3";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { nouvellePartieToolStripMenuItem, separator1, débutantToolStripMenuItem, intermédiaireToolStripMenuItem, expertToolStripMenuItem, separator2, quitterToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(66, 24);
            toolStripMenuItem1.Text = "Fichier";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // nouvellePartieToolStripMenuItem
            // 
            nouvellePartieToolStripMenuItem.Name = "nouvellePartieToolStripMenuItem";
            nouvellePartieToolStripMenuItem.Size = new Size(194, 26);
            nouvellePartieToolStripMenuItem.Text = "Nouvelle partie";
            nouvellePartieToolStripMenuItem.Click += nouvellePartie;
            // 
            // separator1
            // 
            separator1.Name = "separator1";
            separator1.Size = new Size(191, 6);
            // 
            // débutantToolStripMenuItem
            // 
            débutantToolStripMenuItem.Name = "débutantToolStripMenuItem";
            débutantToolStripMenuItem.Size = new Size(194, 26);
            débutantToolStripMenuItem.Text = "Débutant";
            débutantToolStripMenuItem.Click += debutant_Click;
            // 
            // intermédiaireToolStripMenuItem
            // 
            intermédiaireToolStripMenuItem.Name = "intermédiaireToolStripMenuItem";
            intermédiaireToolStripMenuItem.Size = new Size(194, 26);
            intermédiaireToolStripMenuItem.Text = "Intermédiaire";
            intermédiaireToolStripMenuItem.Click += intermediaire_Click;
            // 
            // expertToolStripMenuItem
            // 
            expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            expertToolStripMenuItem.Size = new Size(194, 26);
            expertToolStripMenuItem.Text = "Expert";
            expertToolStripMenuItem.Click += expert_Click;
            // 
            // separator2
            // 
            separator2.Name = "separator2";
            separator2.Size = new Size(191, 6);
            // 
            // quitterToolStripMenuItem
            // 
            quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            quitterToolStripMenuItem.Size = new Size(194, 26);
            quitterToolStripMenuItem.Text = "Quitter";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(30, 24);
            toolStripMenuItem2.Text = "?";
            // 
            // userControl11
            // 
            userControl11.Location = new Point(57, 41);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(462, 435);
            userControl11.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(596, 514);
            Controls.Add(userControl11);
            Controls.Add(menuStrip);
            Name = "Form1";
            Text = "Form1";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void ExpertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem nouvellePartieToolStripMenuItem;
        private ToolStripSeparator separator1;
        private ToolStripMenuItem débutantToolStripMenuItem;
        private ToolStripMenuItem intermédiaireToolStripMenuItem;
        private ToolStripMenuItem expertToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator separator2;
        private ToolStripMenuItem quitterToolStripMenuItem;
        private UserControl1 userControl11;
    }
}
