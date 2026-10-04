namespace magazin_vestimentar_adela
{
    partial class cart
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
            this.numeProdus = new System.Windows.Forms.Label();
            this.pret = new System.Windows.Forms.Label();
            this.total = new System.Windows.Forms.Label();
            this.cantitate = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cantitateLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cantitate)).BeginInit();
            this.SuspendLayout();
            // 
            // numeProdus
            // 
            this.numeProdus.AutoSize = true;
            this.numeProdus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeProdus.Location = new System.Drawing.Point(116, 71);
            this.numeProdus.Name = "numeProdus";
            this.numeProdus.Size = new System.Drawing.Size(38, 18);
            this.numeProdus.TabIndex = 0;
            this.numeProdus.Text = "label";
            // 
            // pret
            // 
            this.pret.AutoSize = true;
            this.pret.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pret.Location = new System.Drawing.Point(46, 106);
            this.pret.Name = "pret";
            this.pret.Size = new System.Drawing.Size(41, 18);
            this.pret.TabIndex = 1;
            this.pret.Text = "pret :";
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.Location = new System.Drawing.Point(46, 178);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(73, 18);
            this.total.TabIndex = 3;
            this.total.Text = "Pret final :";
            // 
            // cantitate
            // 
            this.cantitate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantitate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cantitate.Location = new System.Drawing.Point(186, 136);
            this.cantitate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantitate.Name = "cantitate";
            this.cantitate.Size = new System.Drawing.Size(55, 24);
            this.cantitate.TabIndex = 4;
            this.cantitate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(49, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Confirma";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(186, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 35);
            this.button2.TabIndex = 6;
            this.button2.Text = "Inapoi";
            this.button2.UseMnemonic = false;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // cantitateLabel
            // 
            this.cantitateLabel.AutoSize = true;
            this.cantitateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantitateLabel.Location = new System.Drawing.Point(46, 142);
            this.cantitateLabel.Name = "cantitateLabel";
            this.cantitateLabel.Size = new System.Drawing.Size(74, 18);
            this.cantitateLabel.TabIndex = 7;
            this.cantitateLabel.Text = "Cantitate :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Produs :";
            // 
            // cart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cantitateLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cantitate);
            this.Controls.Add(this.total);
            this.Controls.Add(this.pret);
            this.Controls.Add(this.numeProdus);
            this.Name = "cart";
            this.Text = "cart";
            this.Load += new System.EventHandler(this.cart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cantitate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numeProdus;
        private System.Windows.Forms.Label pret;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.NumericUpDown cantitate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label cantitateLabel;
        private System.Windows.Forms.Label label1;
    }
}