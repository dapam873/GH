
namespace GH
{
    partial class Bienvenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bienvenu));
            this.Tb_text = new System.Windows.Forms.TextBox();
            this.Ll_licence = new System.Windows.Forms.LinkLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Btn_fermer = new System.Windows.Forms.Button();
            this.Pb_logo_GH = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_logo_GH)).BeginInit();
            this.SuspendLayout();
            // 
            // Tb_text
            // 
            this.Tb_text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Tb_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_text.Location = new System.Drawing.Point(190, 12);
            this.Tb_text.Multiline = true;
            this.Tb_text.Name = "Tb_text";
            this.Tb_text.Size = new System.Drawing.Size(729, 61);
            this.Tb_text.TabIndex = 0;
            this.Tb_text.TabStop = false;
            this.Tb_text.Text = "GEDCOM-HTML Ce logiciel permet d\'extraire les informations d\'un fichier GEDCOM\r\ns" +
    "ous forme de fichier HTML pour tous les individus et toutes les familles.";
            this.Tb_text.TextChanged += new System.EventHandler(this.Tb_text_TextChanged);
            // 
            // Ll_licence
            // 
            this.Ll_licence.ActiveLinkColor = System.Drawing.Color.Transparent;
            this.Ll_licence.AutoSize = true;
            this.Ll_licence.DisabledLinkColor = System.Drawing.Color.Blue;
            this.Ll_licence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Ll_licence.ForeColor = System.Drawing.Color.Blue;
            this.Ll_licence.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Ll_licence.LinkColor = System.Drawing.Color.Blue;
            this.Ll_licence.LinkVisited = true;
            this.Ll_licence.Location = new System.Drawing.Point(8, 299);
            this.Ll_licence.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Ll_licence.Name = "Ll_licence";
            this.Ll_licence.Size = new System.Drawing.Size(397, 20);
            this.Ll_licence.TabIndex = 208;
            this.Ll_licence.TabStop = true;
            this.Ll_licence.Text = "Cliquez ici pour voir la licence GPLV3 officiel en anglais.";
            this.Ll_licence.VisitedLinkColor = System.Drawing.Color.Blue;
            this.Ll_licence.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Ll_licence_LinkClicked);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(191, 98);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(729, 47);
            this.textBox2.TabIndex = 211;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "     Copyright (C) 2022 Daniel Pambrun\r\n\r\n";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.Location = new System.Drawing.Point(13, 182);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(906, 100);
            this.TextBox1.TabIndex = 210;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = resources.GetString("TextBox1.Text");
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Btn_fermer
            // 
            this.Btn_fermer.AutoSize = true;
            this.Btn_fermer.BackgroundImage = global::GH.Properties.Resources.Btn_ok;
            this.Btn_fermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_fermer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_fermer.FlatAppearance.BorderSize = 0;
            this.Btn_fermer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_fermer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_fermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_fermer.Location = new System.Drawing.Point(870, 274);
            this.Btn_fermer.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_fermer.Name = "Btn_fermer";
            this.Btn_fermer.Size = new System.Drawing.Size(50, 50);
            this.Btn_fermer.TabIndex = 212;
            this.Btn_fermer.UseVisualStyleBackColor = true;
            this.Btn_fermer.Click += new System.EventHandler(this.Btn_fermer_Click_1);
            // 
            // Pb_logo_GH
            // 
            this.Pb_logo_GH.Image = global::GH.Properties.Resources.GH150V2;
            this.Pb_logo_GH.Location = new System.Drawing.Point(13, 12);
            this.Pb_logo_GH.Margin = new System.Windows.Forms.Padding(4);
            this.Pb_logo_GH.Name = "Pb_logo_GH";
            this.Pb_logo_GH.Size = new System.Drawing.Size(171, 158);
            this.Pb_logo_GH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pb_logo_GH.TabIndex = 209;
            this.Pb_logo_GH.TabStop = false;
            // 
            // Bienvenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(931, 336);
            this.Controls.Add(this.Btn_fermer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Ll_licence);
            this.Controls.Add(this.Pb_logo_GH);
            this.Controls.Add(this.Tb_text);
            this.Controls.Add(this.TextBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Bienvenu";
            this.Text = "Bienvenu";
            this.Load += new System.EventHandler(this.Bienvenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pb_logo_GH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tb_text;
        private System.Windows.Forms.LinkLabel Ll_licence;
        private System.Windows.Forms.PictureBox Pb_logo_GH;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Button Btn_fermer;
    }
}