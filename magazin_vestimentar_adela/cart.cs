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
    public partial class cart : Form
    {
        public int Quantity => (int)cantitate.Value;

        public cart(string productName, string productStyle, decimal price)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            numeProdus.Text = $"{productName} {productStyle}";
            pret.Text = $"Pret produs : {price:F2} lei";

            cantitate.Value = 1;
            cantitate.Minimum = 1;

            UpdateTotal(price);
            cantitate.ValueChanged += (s, e) => UpdateTotal(price);

            this.AcceptButton = button1;
            this.CancelButton = button2;

        }

        private void UpdateTotal(decimal price)
        {
            decimal pretTotal = price * cantitate.Value;
            total.Text = $"Total: {(price * cantitate.Value):F2} lei";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Set the dialog result to OK (indicates the user confirmed the action)
            this.DialogResult = DialogResult.OK;
            // Close the form (this will return to the caller)
            this.Close();
        }

        // Cancel Button Click - Closes the form without adding the product to the cart
        private void button2_Click(object sender, EventArgs e)
        {
            // Set the dialog result to Cancel (indicates the user canceled the action)
            this.DialogResult = DialogResult.Cancel;
            // Close the form (this will return to the caller)
            this.Close();
        }

        /*public cart(string tip)
        {
            InitializeComponent();
        }*/

        private void cart_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
