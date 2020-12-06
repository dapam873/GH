using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GH
{
    public partial class Bienvenu : Form
    {
        public Bienvenu()
        {
            InitializeComponent();
        }
        private void Btn_Fermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
