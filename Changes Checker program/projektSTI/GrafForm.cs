using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    public partial class GrafForm : Form
    {
        public GrafForm(String FileName)
        {
            InitializeComponent();
            //GrafForm.ActiveForm.Text = "Graf: " + FileName;
            label1.Text = "Změny počtu řádků souboru " + FileName;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
