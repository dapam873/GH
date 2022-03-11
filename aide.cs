using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Routine;
using Microsoft.Web.WebView2;
using Microsoft.Web.WebView2.Core;
using System.IO;

namespace GH
{
    public partial class Aide : Form
    {
        public Aide()
        {
            
            InitializeComponent();
            
            InitializeBrowser();
            Timer_animation.Stop();
        }

        private void Aide_Load(object sender, EventArgs e)
        {
            Timer_animation.Start();
        }

        private void Aide_SizeChanged(object sender, EventArgs e)
        {
            int largeur = this.Width;
            int hauteur = this.Height;
            Wv_aide.Width =largeur - 18;
            Wv_aide.Height = hauteur - 39;
        }

        private async void InitializeBrowser(string url = null)
        {
            Application.DoEvents(); 
            int largeur = this.Width;
            int hauteur = this.Height;
            Wv_aide.Width = largeur - 18;
            Wv_aide.Height = hauteur - 39;
            //if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH\EBWebView"))
            {
                var env = await CoreWebView2Environment.CreateAsync(null, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH");
                await Wv_aide.EnsureCoreWebView2Async(env);
            }
            Wv_aide.Source = new Uri("file:///" + Application.StartupPath + "\\aide\\aide.html");

        }
          
        private void Timer_animation_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (rnd.Next(1, 5) == 1)
                pb_del_1.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2)
                pb_del_1.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3)
                pb_del_1.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4)
                pb_del_1.Image = Properties.Resources.del_vert;
            if (rnd.Next(1, 5) == 1)
                pb_del_2.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2)
                pb_del_2.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3)
                pb_del_2.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4)
                pb_del_2.Image = Properties.Resources.del_vert;
            if (rnd.Next(1, 5) == 1)
                pb_del_3.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2)
                pb_del_3.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3)
                pb_del_3.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4)
                pb_del_3.Image = Properties.Resources.del_vert;
            Application.DoEvents();
        }
    }
}
