namespace GH
{
    partial class Page
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Page));
            this.Wv_page = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.lb_lien = new System.Windows.Forms.Label();
            this.lb_SVP = new System.Windows.Forms.Label();
            this.Timer_animation = new System.Windows.Forms.Timer(this.components);
            this.pb_del_3 = new System.Windows.Forms.PictureBox();
            this.pb_del_2 = new System.Windows.Forms.PictureBox();
            this.pb_del_1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Wv_page)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_1)).BeginInit();
            this.SuspendLayout();
            // 
            // Wv_page
            // 
            this.Wv_page.CreationProperties = null;
            this.Wv_page.DefaultBackgroundColor = System.Drawing.Color.White;
            this.Wv_page.Location = new System.Drawing.Point(0, 0);
            this.Wv_page.Margin = new System.Windows.Forms.Padding(2);
            this.Wv_page.Name = "Wv_page";
            this.Wv_page.Size = new System.Drawing.Size(364, 218);
            this.Wv_page.TabIndex = 0;
            this.Wv_page.ZoomFactor = 1D;
            // 
            // lb_lien
            // 
            this.lb_lien.AutoSize = true;
            this.lb_lien.Location = new System.Drawing.Point(116, 40);
            this.lb_lien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_lien.Name = "lb_lien";
            this.lb_lien.Size = new System.Drawing.Size(23, 13);
            this.lb_lien.TabIndex = 1;
            this.lb_lien.Text = "lien";
            this.lb_lien.Visible = false;
            // 
            // lb_SVP
            // 
            this.lb_SVP.AutoSize = true;
            this.lb_SVP.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SVP.Location = new System.Drawing.Point(173, 149);
            this.lb_SVP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_SVP.Name = "lb_SVP";
            this.lb_SVP.Size = new System.Drawing.Size(248, 39);
            this.lb_SVP.TabIndex = 2;
            this.lb_SVP.Text = "Un instant SVP";
            // 
            // Timer_animation
            // 
            this.Timer_animation.Enabled = true;
            this.Timer_animation.Interval = 10;
            this.Timer_animation.Tick += new System.EventHandler(this.Timer_animation_Tick);
            // 
            // pb_del_3
            // 
            this.pb_del_3.ErrorImage = null;
            this.pb_del_3.Location = new System.Drawing.Point(492, 153);
            this.pb_del_3.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_3.Name = "pb_del_3";
            this.pb_del_3.Size = new System.Drawing.Size(30, 30);
            this.pb_del_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_del_3.TabIndex = 153;
            this.pb_del_3.TabStop = false;
            // 
            // pb_del_2
            // 
            this.pb_del_2.ErrorImage = null;
            this.pb_del_2.InitialImage = null;
            this.pb_del_2.Location = new System.Drawing.Point(457, 153);
            this.pb_del_2.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_2.Name = "pb_del_2";
            this.pb_del_2.Size = new System.Drawing.Size(30, 30);
            this.pb_del_2.TabIndex = 152;
            this.pb_del_2.TabStop = false;
            // 
            // pb_del_1
            // 
            this.pb_del_1.ErrorImage = null;
            this.pb_del_1.InitialImage = null;
            this.pb_del_1.Location = new System.Drawing.Point(421, 153);
            this.pb_del_1.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_1.Name = "pb_del_1";
            this.pb_del_1.Size = new System.Drawing.Size(30, 30);
            this.pb_del_1.TabIndex = 151;
            this.pb_del_1.TabStop = false;
            // 
            // Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(829, 551);
            this.Controls.Add(this.Wv_page);
            this.Controls.Add(this.lb_lien);
            this.Controls.Add(this.lb_SVP);
            this.Controls.Add(this.pb_del_3);
            this.Controls.Add(this.pb_del_2);
            this.Controls.Add(this.pb_del_1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Page";
            this.Text = "Page";
            this.Load += new System.EventHandler(this.Page_Load);
            this.SizeChanged += new System.EventHandler(this.Page_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.Wv_page)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 Wv_page;
        private System.Windows.Forms.Label lb_lien;
        private System.Windows.Forms.Label lb_SVP;
        private System.Windows.Forms.Timer Timer_animation;
        private System.Windows.Forms.PictureBox pb_del_3;
        private System.Windows.Forms.PictureBox pb_del_2;
        private System.Windows.Forms.PictureBox pb_del_1;
    }
}