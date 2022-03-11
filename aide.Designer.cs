namespace GH
{
    partial class Aide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aide));
            this.lb_SVP = new System.Windows.Forms.Label();
            this.Wv_aide = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pb_del_3 = new System.Windows.Forms.PictureBox();
            this.pb_del_2 = new System.Windows.Forms.PictureBox();
            this.pb_del_1 = new System.Windows.Forms.PictureBox();
            this.Timer_animation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Wv_aide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_1)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_SVP
            // 
            this.lb_SVP.AutoSize = true;
            this.lb_SVP.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SVP.Location = new System.Drawing.Point(207, 84);
            this.lb_SVP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_SVP.Name = "lb_SVP";
            this.lb_SVP.Size = new System.Drawing.Size(248, 39);
            this.lb_SVP.TabIndex = 3;
            this.lb_SVP.Text = "Un instant SVP";
            // 
            // Wv_aide
            // 
            this.Wv_aide.BackColor = System.Drawing.Color.Black;
            this.Wv_aide.CreationProperties = null;
            this.Wv_aide.DefaultBackgroundColor = System.Drawing.Color.Black;
            this.Wv_aide.ForeColor = System.Drawing.Color.White;
            this.Wv_aide.Location = new System.Drawing.Point(0, 0);
            this.Wv_aide.Margin = new System.Windows.Forms.Padding(2);
            this.Wv_aide.Name = "Wv_aide";
            this.Wv_aide.Size = new System.Drawing.Size(261, 162);
            this.Wv_aide.TabIndex = 0;
            this.Wv_aide.ZoomFactor = 1D;
            // 
            // pb_del_3
            // 
            this.pb_del_3.ErrorImage = null;
            this.pb_del_3.Location = new System.Drawing.Point(528, 89);
            this.pb_del_3.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_3.Name = "pb_del_3";
            this.pb_del_3.Size = new System.Drawing.Size(30, 30);
            this.pb_del_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_del_3.TabIndex = 150;
            this.pb_del_3.TabStop = false;
            // 
            // pb_del_2
            // 
            this.pb_del_2.ErrorImage = null;
            this.pb_del_2.InitialImage = null;
            this.pb_del_2.Location = new System.Drawing.Point(493, 89);
            this.pb_del_2.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_2.Name = "pb_del_2";
            this.pb_del_2.Size = new System.Drawing.Size(30, 30);
            this.pb_del_2.TabIndex = 149;
            this.pb_del_2.TabStop = false;
            // 
            // pb_del_1
            // 
            this.pb_del_1.ErrorImage = null;
            this.pb_del_1.InitialImage = null;
            this.pb_del_1.Location = new System.Drawing.Point(457, 89);
            this.pb_del_1.Margin = new System.Windows.Forms.Padding(0);
            this.pb_del_1.Name = "pb_del_1";
            this.pb_del_1.Size = new System.Drawing.Size(30, 30);
            this.pb_del_1.TabIndex = 148;
            this.pb_del_1.TabStop = false;
            // 
            // Timer_animation
            // 
            this.Timer_animation.Enabled = true;
            this.Timer_animation.Interval = 10;
            this.Timer_animation.Tick += new System.EventHandler(this.Timer_animation_Tick);
            // 
            // Aide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(814, 461);
            this.Controls.Add(this.Wv_aide);
            this.Controls.Add(this.lb_SVP);
            this.Controls.Add(this.pb_del_3);
            this.Controls.Add(this.pb_del_2);
            this.Controls.Add(this.pb_del_1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(830, 500);
            this.Name = "Aide";
            this.Text = "Aide";
            this.Load += new System.EventHandler(this.Aide_Load);
            this.SizeChanged += new System.EventHandler(this.Aide_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.Wv_aide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lb_SVP;
        private Microsoft.Web.WebView2.WinForms.WebView2 Wv_aide;
        private System.Windows.Forms.PictureBox pb_del_3;
        private System.Windows.Forms.PictureBox pb_del_2;
        private System.Windows.Forms.PictureBox pb_del_1;
        private System.Windows.Forms.Timer Timer_animation;
    }
}