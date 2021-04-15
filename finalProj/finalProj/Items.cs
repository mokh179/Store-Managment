using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProj
{
    public partial class Items : Form
    {
        Model1 db = new Model1();
        string[] modules = { "Kg", "Ltr" };
        private void fillgrid()
        {
            var Allitems = (from d in db.items
                            select new { d.itemCode,d.itemName, d.itemModule1, d.itemModule2, d.ProductionDate,d.expiredDate }).ToList();
            dataGridView1.DataSource = Allitems;
        }
        public Items()
        {
            InitializeComponent();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            var Items = (from itm in db.items
                         //join st in db.stock_item
                         //on itm.itemCode equals st.itemCode
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox3.DisplayMember = "itemName";
            comboBox3.ValueMember = "itemCode";
            comboBox3.DataSource = Items;


            var stocks1 = (from st in db.stocks
                          select new { st.stockID, st.stockName }).ToList();

            
            comboBox5.DisplayMember = "stockName";
            comboBox5.ValueMember = "stockID";
            comboBox5.DataSource = stocks1;

            fillgrid();
            comboBox1.Items.AddRange(modules);
            comboBox2.Items.AddRange(modules);
            this.textBox3.Visible = false;
            this.groupBox1.Visible = false;
            button6.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            item it = new item();
            if (textBox1.Text!=""||comboBox1.Text!="")
            {
                int id;
                int c= db.items.Select(s => s.itemCode).Count();
                if (c!=0)
                {
                    id = db.items.Select(s => s.itemCode).Max();
                }
                else
                {
                    id = 0;
                }
                it.itemName = textBox1.Text;
                it.itemModule1 = comboBox1.Text;
                it.itemModule2 = comboBox2.Text;
                it.expiredDate = dateTimePicker1.Value;
                it.ProductionDate = dateTimePicker2.Value;
                it.itemCode = ++id;
                db.items.Add(it);
                try
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox3.Text = comboBox1.Text = comboBox2.Text = string.Empty;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطأ في ادخال البيانات");
                }
            }
            else
            {
                MessageBox.Show("تأكد من وجود بيانات");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox3.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Value = (DateTime)row.Cells[5].Value;
                dateTimePicker2.Value = (DateTime)row.Cells[4].Value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text != "" || comboBox1.Text != ""|| textBox3.Text!="")
            {
                item it = db.items.Find(int.Parse(textBox3.Text));
                it.itemName = textBox1.Text;
                it.itemModule1 = comboBox1.Text;
                it.itemModule2 = comboBox2.Text;
                it.expiredDate = dateTimePicker1.Value;
                it.ProductionDate = dateTimePicker2.Value;
                try
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox3.Text = comboBox1.Text = comboBox2.Text = string.Empty;

                }
                catch (Exception)
                {
                    MessageBox.Show("خطأ في ادخال البيانات");
                }
            }
            else
            {
                MessageBox.Show("تأكد من وجود بيانات");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                item it = db.items.Find(int.Parse(textBox3.Text));
                db.items.Remove(it);
                if (MessageBox.Show("هل تريد مسح المنتج", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox3.Text = comboBox1.Text = comboBox2.Text = string.Empty;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var Allitems = db.items.Where(x => x.itemName.Contains(textBox2.Text)).Select(x => new {x.itemCode, x.itemName, x.itemModule1, x.itemModule2, x.expiredDate }).ToList();
            dataGridView1.DataSource = Allitems;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            int supID = (from s_ord in db.supplymentOrders
                         join s in db.supplyment_orderDetails
                         on s_ord.orderID equals s.orderID
                         select  s_ord.supplierID).FirstOrDefault();

            item it = (from d in db.items
                      where d.itemCode == (int)comboBox3.SelectedValue
                      select d).First();
                      

            stock_item st_it2;
            stock_item st_it = (from d in db.stock_item
                                where d.itemCode == (int)comboBox3.SelectedValue && d.stockID == (int)comboBox4.SelectedValue
                                select d).FirstOrDefault();
            st_it2 = (from d in db.stock_item
                      where d.itemCode == (int)comboBox3.SelectedValue && d.stockID == (int)comboBox5.SelectedValue
                      select d).FirstOrDefault();

            if (int.Parse(textBox4.Text)!= st_it.Quantity&& int.Parse(textBox4.Text)< st_it.Quantity)
            {
                st_it.Quantity -= int.Parse(textBox4.Text);
                if (st_it2!=null)
                {
                    st_it2.Quantity += int.Parse(textBox4.Text);
                }
                else
                {
                    st_it2 = new stock_item(); 
                    st_it2.stockID = (int)comboBox5.SelectedValue;
                    st_it2.itemCode = (int)comboBox3.SelectedValue;
                    st_it2.Quantity = int.Parse(textBox4.Text);
                    db.stock_item.Add(st_it2);

                }

                movement mov = new movement();
                mov.moveDate = dateTimePicker3.Value;
                mov.itemCode = (int)comboBox3.SelectedValue;
                mov.fromStock = (int)comboBox4.SelectedValue;
                mov.toStock = (int)comboBox5.SelectedValue;
                mov.SupplierID = supID;
                mov.Quantity= int.Parse(textBox4.Text);
                int dat = it.ProductionDate.Value.Year;
                int dat2 = it.expiredDate.Year;
                mov.expirationTime = dat2 - dat;
                db.movements.Add(mov);
                try
                {
                    
                    db.SaveChanges();
                    MessageBox.Show("تم النقل", "Information");
                    textBox4.Text = comboBox3.Text = comboBox4.Text = comboBox5.Text = string.Empty;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("خطأ في ادخال البيانات");
                }


            }

            

        }

        private void supplierOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_Orders s_ord = new s_Orders();
            s_ord.Show();
            this.Hide();
        }

        private void manageSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            suppliers sup = new suppliers();
            sup.Show();
            this.Hide();
        }

        private void clientOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();

        }

        private void manageClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stocks st = new stocks();
            st.Show();
            this.Hide();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

   

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var stocks = (from st in db.stocks
                          join st_c in db.stock_item
                          on st.stockID equals st_c.stockID
                          join i in db.items
                          on st_c.itemCode equals i.itemCode
                          where st_c.itemCode == (int)comboBox3.SelectedValue
                          select new { st.stockID, st.stockName }).ToList();

            comboBox4.DisplayMember = "stockName";
            comboBox4.ValueMember = "stockID";
            comboBox4.DataSource = stocks;
        }

        private void movmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Movements mov = new Movements();
            mov.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.groupBox2.Visible = false;
            this.groupBox1.Visible = true;
            button5.Visible = false;
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = true;
            button6.Visible = false;
            button5.Visible = true;
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
            this.Hide();
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main mn = new main();
            mn.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}