using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace magazin_vestimentar_adela
{
    public partial class ConfirmareComanda : Form
    {
        public ConfirmareComanda(string fullName, DateTime deliveryDate)
        {
            InitializeComponent();

            label1.Text = $"Multumim, {fullName}! Comanda ta va fi livrata pe {deliveryDate:dd/MM/yyyy}.";
            label1.TextAlign = ContentAlignment.MiddleCenter;

            // Button click event
            button1.Click += (sender, e) =>
            {
                // Assuming Form1 is your main form where the cart and account details are
                Magazin mainForm = (Magazin)Application.OpenForms["Magazin"];
                mainForm.SelectTabForOrderDetails();  // This is a method you'll define to switch the tab
                this.Close();
            };
            /*button2.Click += (sender, e) =>
            {
                // Assuming Form1 is your main form where the cart and account details are
                Form1 mainForm = (Form1)Application.OpenForms["Form1"];
                mainForm.SelectHomeTab();  // This is a method you'll define to switch the tab
                this.Close();
            };*/
        }
        private void ConfirmareComanda_Load(object sender, EventArgs e)
            {

            }
    }
}
