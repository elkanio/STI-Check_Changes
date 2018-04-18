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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
            tokenBox.GotFocus += new EventHandler(this.tokenBox_GotFocus);
            tokenBox.LostFocus += new EventHandler(this.tokenBox_LostFocus);
            uzivatelBox.GotFocus += new EventHandler(this.uzivatelBox_GotFocus);
            uzivatelBox.LostFocus += new EventHandler(this.uzivatelBox_LostFocus);
            repozitarBox.GotFocus += new EventHandler(this.repozitarBox_GotFocus);
            repozitarBox.LostFocus += new EventHandler(this.repozitarBox_LostFocus);
        }

        private void tokenBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void tokenBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void uzivatelBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "Antoninecek";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void uzivatelBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Antoninecek")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        private void repozitarBox_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "TEST";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void repozitarBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "TEST")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string token = tokenBox.Text;
            string uzivatel = uzivatelBox.Text;
            string repozitar = repozitarBox.Text;

            this.Hide();
            Sluzba s = new Sluzba();
            s.NastavDataMiner(repozitar, uzivatel, token);
            MainForm mf = new MainForm();
            mf.Text = uzivatel + "/" + repozitar;
            mf.StartPosition = FormStartPosition.CenterParent;
            mf.ShowDialog();
            this.Close();
        }
    }
}
