using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using magazin_vestimentar_adela.bazadedate_magazinDataSetTableAdapters;
using static magazin_vestimentar_adela.Magazin;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace magazin_vestimentar_adela
{

    public partial class Magazin : Form
    {
        public Magazin()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            button8.FlatStyle = FlatStyle.Flat;
            button8.BackColor = Color.Transparent;
            button8.FlatAppearance.BorderSize = 0;

            button8.Paint += new PaintEventHandler(button10_Paint);

            button10.FlatStyle = FlatStyle.Flat;
            button10.BackColor = Color.Transparent;
            button10.FlatAppearance.BorderSize = 0;

            button10.Paint += new PaintEventHandler(button10_Paint);
            textBox7.PasswordChar = '•';
        }

        private void btnVerifyPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show(this, "Nu ați completat toate spațiile necesare");
                return;
            }

            string correctPassword = "12345";
            string enteredPassword = textBox7.Text;

            string correctUser = "admin";
            string enteredUser = textBox8.Text;

            if (enteredPassword == correctPassword && enteredUser == correctUser)
            {
                MessageBox.Show(this, "Autentificat cu succes!");
                tabControl1.SelectedTab = tabPage12;
            }
            else
            {
                MessageBox.Show(this, "Datele introduse sunt incorecte.");
            }
        }

        private List<Order> orderHistory = new List<Order>();

        private void UpdateCartUI()
        {
            flowLayoutPanel7.Controls.Clear();

            if (cartItems.Count == 0)
            {
                Label emptyCartLabel = new Label
                {
                    Text = "Coșul dumneavoastră este gol.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.DarkRed,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = flowLayoutPanel7.Width - 10,
                    Height = 40,
                    Margin = new Padding(5)
                };

                flowLayoutPanel7.Controls.Add(emptyCartLabel);
            }
            else
            {
                decimal total = 0;
                
                foreach (var item in cartItems)
                {
                    decimal itemTotal = (decimal)item.Product.pret * item.Quantity;
                    total += itemTotal;

                    decimal itemPrice = (decimal)item.Product.pret;

                    Panel itemPanel = new Panel
                    {
                        Width = flowLayoutPanel7.ClientSize.Width - 20,
                        Height = 35,
                        Padding = new Padding(2),
                        BackColor = Color.LightYellow,
                        Tag = item,
                        Margin = new Padding(5)
                    };

                    Label itemLabel = new Label
                    {
                        Text = $"{item.Product.tip} {item.Product.stil} - {itemPrice:F2} lei",
                        AutoSize = true,
                        Height = 30,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 10),
                        Margin = new Padding(5)
                    };

                    Label quantityLabel = new Label
                    {
                        Text = $"x {item.Quantity}",
                        AutoSize = true,
                        Height = 30,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 10),
                        Margin = new Padding(5)
                    };

                    NumericUpDown quantityUpDown = new NumericUpDown
                    {
                        Value = item.Quantity,
                        Minimum = 1,
                        Maximum = 100,
                        Width = 60,
                        Location = new Point(quantityLabel.Right + 15, 5),
                        Margin = new Padding(5)
                    };

                    quantityUpDown.ValueChanged += (sender, e) =>
                    {
                        item.Quantity = (int)quantityUpDown.Value;
                        quantityLabel.Text = $"x {item.Quantity}";
                        UpdateCartUI();
                    };

                    Button removeButton = new Button
                    {
                        Text = "Eliminați",
                        Width = 80,
                        Height = 30,
                        BackColor = Color.LightCoral,
                        FlatStyle = FlatStyle.Flat,
                        Tag = item,
                        Margin = new Padding(5)
                    };
                    removeButton.Click += RemoveCartItem_Click;

                    itemPanel.Controls.Add(itemLabel);
                    itemPanel.Controls.Add(quantityLabel);
                    itemPanel.Controls.Add(quantityUpDown);
                    itemPanel.Controls.Add(removeButton);

                    itemLabel.Location = new Point(0, 5);
                    quantityLabel.Location = new Point(itemLabel.Right + 15, 5);
                    quantityUpDown.Location = new Point(875, 5);
                    removeButton.Location = new Point(950, 5);

                    flowLayoutPanel7.Controls.Add(itemPanel);
                }

                Panel bottomPanel = new Panel
                {
                    Width = flowLayoutPanel7.Width - 10,
                    Height = 50,
                    Margin = new Padding(5),
                    Dock = DockStyle.Bottom
                };

                Label totalLabel = new Label
                {
                    Text = $"Total: {total:F2} lei",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.DarkRed,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Left, 
                    Padding = new Padding(10) 
                };

                Button placeOrderButton = new Button
                {
                    Text = "Plasați comanda",
                    Width = 170,
                    Height = 30,
                    BackColor = Color.LightGreen,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Dock = DockStyle.Right, 
                    Margin = new Padding(10) 
                };
                placeOrderButton.Click += buttonPlaceOrder_Click;

                bottomPanel.Controls.Add(totalLabel);
                bottomPanel.Controls.Add(placeOrderButton);

                flowLayoutPanel7.Controls.Add(bottomPanel);


            }

            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel7.Refresh();
        }

        public class CartItem
        {
            public bazadedate_magazinDataSet.produseRow Product { get; set; }
            public int Quantity { get; set; }
        }

        private List<CartItem> cartItems = new List<CartItem>();

        private void RemoveCartItem_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            CartItem item = btn?.Tag as CartItem;

            if (item != null)
            {
                var result = MessageBox.Show(this, "Doriți să eliminați acest produs?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cartItems.Remove(item);
                    UpdateCartUI();
                }
            }
        }

        private void ProductName_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;

            if (btn?.Tag is bazadedate_magazinDataSet.produseRow productRow)
            {

                using (var dialog = new cart(productRow.tip, productRow.stil, (decimal)productRow.pret))
                {
                    var result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        int quantity = dialog.Quantity;
                        
                        var existingItem = cartItems.FirstOrDefault(item => item.Product.idProdus == productRow.idProdus);
                        if (existingItem != null)
                        {
                            existingItem.Quantity += quantity;
                        }
                        else
                        {
                            cartItems.Add(new CartItem { Product = productRow, Quantity = quantity });
                        }
                        UpdateCartUI();

                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabPage3":
                    tabControl1.SelectedTab = tabPage3;
                    LoadProducts("sport", flowLayoutPanel2);
                    break;
                case "tabPage4":
                    tabControl1.SelectedTab = tabPage4;
                    LoadProducts("grunge", flowLayoutPanel3);
                    break;
                case "tabPage5":
                    tabControl1.SelectedTab = tabPage5;
                    LoadProducts("y2k", flowLayoutPanel4);
                    break;
                case "tabPage6":
                    tabControl1.SelectedTab = tabPage6;
                    LoadProducts("casual", flowLayoutPanel5);
                    break;
                case "tabPage7":
                    tabControl1.SelectedTab = tabPage7;
                    LoadProducts("business", flowLayoutPanel6);
                    break;
                case "tabPage10":
                    tabControl1.SelectedTab = tabPage10;
                    DisplayOrderDetails();
                    break;
            }
        }

        private void LoadProducts(string style, FlowLayoutPanel targetPanel)
        {
            targetPanel.Controls.Clear();

            var adapter = new bazadedate_magazinDataSetTableAdapters.produseTableAdapter();
            var products = adapter.GetData();

            foreach (bazadedate_magazinDataSet.produseRow row in products)
            {
                if (!string.IsNullOrEmpty(style) && !row.stil.Equals(style, StringComparison.OrdinalIgnoreCase))
                    continue;

                Panel card = new Panel
                {
                    Width = 200,
                    Height = 120,
                    Margin = new Padding(10),
                    BackColor = Color.FromArgb(255, 245, 238),
                    BorderStyle = BorderStyle.FixedSingle
                };

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button
                {
                    Text = row.tip,
                    Dock = DockStyle.Top,
                    Height = 40,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.Pink,
                    FlatStyle = FlatStyle.Flat,
                    Tag = row 
                };
                btn.Click += ProductName_Click;

                Label lblStyle = new Label
                {
                    Text = row.stil,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Height = 40,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };

                Label lblPrice = new Label
                {
                    Text = $"{row.pret:F2} lei",
                    Dock = DockStyle.Bottom,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Height = 30,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };

                card.Controls.Add(lblPrice);
                card.Controls.Add(lblStyle);
                card.Controls.Add(btn); 

                targetPanel.Controls.Add(card);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.dateTop' table. You can move, or remove it, as needed.
            this.dateTopTableAdapter.DateTopAchizitii(this.bazadedate_magazinDataSet.dateTop);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.ProdRecente' table. You can move, or remove it, as needed.
            this.prodRecenteTableAdapter.AchizitiiRecente(this.bazadedate_magazinDataSet.ProdRecente);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.prodsexfm' table. You can move, or remove it, as needed.
            this.prodsexfmTableAdapter.ProdDupaSexFM(this.bazadedate_magazinDataSet.prodsexfm);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.detaliiFull' table. You can move, or remove it, as needed.
            this.detaliiFullTableAdapter.DateClienti(this.bazadedate_magazinDataSet.detaliiFull);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.prodvarsta' table. You can move, or remove it, as needed.
            this.prodvarstaTableAdapter.TotalProdVarsta(this.bazadedate_magazinDataSet.prodvarsta);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.clienti1' table. You can move, or remove it, as needed.
            //this.DataTable1TableAdapter.ProdTotalVarsta(this.bazadedate_magazinDataSet.clienti1);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.produse1' table. You can move, or remove it, as needed.
            this.produse1TableAdapter.TopProduseQuery(this.bazadedate_magazinDataSet.produse1);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.produse2' table. You can move, or remove it, as needed.
            this.produse2TableAdapter.TotalVenitStil(this.bazadedate_magazinDataSet.produse2);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.produse' table. You can move, or remove it, as needed.
            //this.produseTableAdapter.Fill(this.bazadedate_magazinDataSet.produse);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.factura' table. You can move, or remove it, as needed.
            //this.facturaTableAdapter.Fill(this.bazadedate_magazinDataSet.factura);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.detalii_factura' table. You can move, or remove it, as needed.
            //this.detalii_facturaTableAdapter.Fill(this.bazadedate_magazinDataSet.detalii_factura);
            // TODO: This line of code loads data into the 'bazadedate_magazinDataSet.Clienti' table. You can move, or remove it, as needed.
            //this.clientiTableAdapter.Fill(this.bazadedate_magazinDataSet.Clienti);
            cartItems.Clear();
            UpdateCartUI();
            LoadProducts("", flowLayoutPanel1);
            DisplayOrderDetails();
            produse1DataGridView.Visible = false;
            produse2DataGridView.Visible = false;
            prodvarstaDataGridView.Visible = false;
            detaliiFullDataGridView.Visible = false;
            prodsexfmDataGridView.Visible = false;
            prodRecenteDataGridView.Visible = false;
            dateTopDataGridView.Visible = false;
            //clienti1DataGridView.Visible = false;
        }

        private bool IsSignUpComplete()
        {
            return !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrWhiteSpace(textBox6.Text);
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!IsSignUpComplete())
            {
                using (var prompt = new creareCont())
                {
                    var result = prompt.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        tabControl1.SelectedTab = tabPage9; // pagina de sign-up
                    }
                }

                return; 
            }

            string nume = textBox1.Text; 
            string prenume = textBox2.Text;

            DateTime deliveryDate = DateTime.Now.AddDays(14);
            string fullName = $"{textBox1.Text} {textBox2.Text}";

            ConfirmareComanda confirmationForm = new ConfirmareComanda(fullName, deliveryDate);
            confirmationForm.ShowDialog();

            decimal totalPrice = cartItems.Sum(item => (decimal)item.Product.pret * item.Quantity);

            var newOrder = new Order
            {
                CustomerName = $"{textBox1.Text} {textBox2.Text}",
                TotalPrice = totalPrice,
                Email = textBox4.Text,
                PlacedAt = DateTime.Now,
                Items = cartItems.Select(item => new CartItem
                {
                    Product = item.Product,
                    Quantity = item.Quantity
                }).ToList()
            };
            orderHistory.Add(newOrder);

            cartItems.Clear();
            UpdateCartUI();

            SelectTabForOrderDetails();
            DisplayOrderDetails();
        }

        public void SelectTabForOrderDetails()
        {
            tabControl1.SelectedTab = tabPage10;
        }

        public class Order
        {
            public string CustomerName { get; set; }

            public List<CartItem> Items { get; set; }

            public string Email { get; set; }

            public decimal TotalPrice { get; set; }

            public DateTime PlacedAt { get; set; } = DateTime.Now;

            
        }

        private void DisplayOrderDetails()
        {
            listBox1.Items.Clear();
            label8.Visible = false;
            label9.Visible = false;
            label16.Visible = false;
            listBox1.Visible = false;

            if (orderHistory.Count == 0)
            {
                label16.Text = "Nu ați plasat nicio comandă.";
                label16.Visible = true;
            }
            else
            {
                label8.Visible = true;
                label9.Visible = true;
                listBox1.Visible = true;

                var latestOrder = orderHistory.Last();
                label8.Text = latestOrder.CustomerName;
                label9.Text = $"Email: {latestOrder.Email}";

                foreach (var order in orderHistory)
                {
                    listBox1.Items.Add("------------------------------");
                    listBox1.Items.Add($"Comandă plasată: {order.PlacedAt}");
                    listBox1.Items.Add($"Client: {order.CustomerName}");

                    foreach (var item in order.Items)
                    {
                        listBox1.Items.Add($"{item.Product.tip} {item.Product.stil} - {item.Quantity} x {item.Product.pret:F2} lei");
                    }

                    listBox1.Items.Add($"Total: {order.TotalPrice:F2} lei");
                    listBox1.Items.Add("------------------------------\n");
                }
            }
        }

        public class HiddenTabs : TabControl
        {
            public HiddenTabs()
            {
                this.Appearance = TabAppearance.FlatButtons;
                this.ItemSize = new Size(0, 1);
                this.SizeMode = TabSizeMode.Fixed;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x1328)
                {
                    return;
                }
                base.WndProc(ref m);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            LoadProducts("sport", flowLayoutPanel2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            LoadProducts("grunge", flowLayoutPanel3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            LoadProducts("y2k", flowLayoutPanel4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
            LoadProducts("casual", flowLayoutPanel5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
            LoadProducts("business", flowLayoutPanel6);
        }

        private void cosDeCumparaturiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage8;
        }

        private void datePersonaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage9;
        }

        private void detaliiComandaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage10;
        }

        private void flowLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void acasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void businessToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
            LoadProducts("business", flowLayoutPanel6);
        }

        private void sportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            LoadProducts("sport", flowLayoutPanel2);
        }

        private void grungeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            LoadProducts("grunge", flowLayoutPanel3);
        }

        private void y2KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            LoadProducts("y2k", flowLayoutPanel4);
        }

        private void casualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
            LoadProducts("casual", flowLayoutPanel5);
        }

        private void toateProduseleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            LoadProducts("", flowLayoutPanel1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsSignUpComplete())
            {
                MessageBox.Show(this, "Cont creat cu succes! Vă puteți intoarce la coș pentru a plasa comanda.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tabControl1.SelectedTab = tabPage8; 
            }
            else
            {
                MessageBox.Show(this, "Vă rugăm să completați toate câmpurile pentru a crea un cont.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage11;
        }

        private bool IsAdminLoggedIn = false;

        private void iNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(IsAdminLoggedIn)
            {
                tabControl1.SelectedTab = tabPage12;
            }
            else
            {
                MessageBox.Show(this, "Acces restricționat. Vă rugăm să vă autentificați.");

                tabControl1.SelectedTab = tabPage11;
            }
        }

        private void button8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage13;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage12;
        }

        private void button10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnVerifyPassword_Click(sender, e);
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.clientiTableAdapter.FillBy(this.bazadedate_magazinDataSet.Clienti);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.produse1TableAdapter();

            var dataTable = new bazadedate_magazinDataSet.produse1DataTable();

            adapter.TopProduseQuery(dataTable);

            produse1DataGridView.DataSource = dataTable;
            produse1DataGridView.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.produse2TableAdapter();

            var dataTable = new bazadedate_magazinDataSet.produse2DataTable();

            adapter.TotalVenitStil(dataTable);

            produse2DataGridView.DataSource = dataTable;
            produse2DataGridView.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.prodvarstaTableAdapter();

            var dataTable = new bazadedate_magazinDataSet.prodvarstaDataTable();

            adapter.TotalProdVarsta(dataTable);

            prodvarstaDataGridView.DataSource = dataTable;
            prodvarstaDataGridView.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage13;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.prodsexfmTableAdapter();

            var dataTable = new bazadedate_magazinDataSet.prodsexfmDataTable(); 

            adapter.ProdDupaSexFM(dataTable);

            prodsexfmDataGridView.DataSource = dataTable;
            prodsexfmDataGridView.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.ProdRecenteTableAdapter();

            var dataTable = new bazadedate_magazinDataSet.ProdRecenteDataTable();

            adapter.AchizitiiRecente(dataTable);

            prodRecenteDataGridView.DataSource = dataTable;
            prodRecenteDataGridView.Visible = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.dateTopTableAdapter();

            var dataTable = new bazadedate_magazinDataSet.dateTopDataTable();

            adapter.DateTopAchizitii(dataTable);

            dateTopDataGridView.DataSource = dataTable;
            dateTopDataGridView.Visible = true;
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            var adapter = new bazadedate_magazinDataSetTableAdapters.detaliiFullTableAdapter();

            var dataTable = new bazadedate_magazinDataSet.detaliiFullDataTable(); 

            adapter.DateClienti(dataTable);

            detaliiFullDataGridView.DataSource = dataTable;
            detaliiFullDataGridView.Visible = true;
        }
    }
}