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
    public partial class s_Orders : Form
    {
        public s_Orders()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void fillgrid()
        {
            var Allitems = (from d in db.supplymentOrders
                            join sup in db.suppliers
                            on d.supplierID equals sup.supplierID
                            join st in db.stocks
                            on d.stockID equals st.stockID
                            join od in db.supplyment_orderDetails 
                            on d.orderID equals od.orderID
                            join it in db.items
                            on od.itemCode equals it.itemCode
                            select new { d.orderID, sup.supplierName,st.stockName,it.itemName,od.quantity, perKG=od.itemPrice  ,d.orderDate}).ToList();
            dataGridView1.DataSource = Allitems;
        }

        private void s_Orders_Load(object sender, EventArgs e)
        {
            var Items = (from itm in db.items
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox3.DisplayMember = "itemName";
            comboBox3.ValueMember = "itemCode";
            comboBox3.DataSource = Items;
            var stocks = (from st in db.stocks
                          select new { st.stockID, st.stockName }).ToList();
            comboBox2.DisplayMember = "stockName";
            comboBox2.ValueMember = "stockID";
            comboBox2.DataSource = stocks;

            var sups = (from sup in db.suppliers
                        select new { sup.supplierID, sup.supplierName }).ToList();
            comboBox1.DisplayMember = "supplierName";
            comboBox1.ValueMember = "supplierID";
            comboBox1.DataSource = sups;
            fillgrid();
            textBox8.Visible = false;
            this.groupBox3.Visible = false;
            button6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && groupBox3.Visible != false)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox3.Text = row.Cells[0].Value.ToString();
              
                    comboBox1.Text = row.Cells[1].Value.ToString();
                    comboBox2.Text = row.Cells[2].Value.ToString();
                    comboBox3.Text = row.Cells[3].Value.ToString();
                    textBox8.Text = row.Cells[3].Value.ToString();
                    textBox1.Text = row.Cells[4].Value.ToString();
                    textBox2.Text = row.Cells[5].Value.ToString();
                    dateTimePicker1.Value = (DateTime)row.Cells[6].Value;
                   // this.groupBox3.Visible = true;
                    textBox9.Text = row.Cells[1].Value.ToString();
                
               
            }
        }

        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var Allitems = (from d in db.supplymentOrders
                            join sup in db.suppliers
                            on d.supplierID equals sup.supplierID
                            join st in db.stocks
                            on d.stockID equals st.stockID
                            join od in db.supplyment_orderDetails
                            on d.orderID equals od.orderID
                            join it in db.items
                            on od.itemCode equals it.itemCode
                            where sup.supplierName.Contains(textBox4.Text)
                            select new { d.orderID, sup.supplierName, st.stockName, it.itemName, od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
            dataGridView1.DataSource = Allitems;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            var sups = (from sup in db.suppliers
                        where sup.supplierName.Contains(textBox7.Text)
                        select new { sup.supplierID, sup.supplierName }).ToList();
            comboBox1.DisplayMember = "supplierName";
            comboBox1.ValueMember = "supplierID";
            comboBox1.DataSource = sups;
        }

        

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items it = new Items();
            it.Show();
            this.Hide();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stocks st = new stocks();
            st.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                var Allitems = (from d in db.supplymentOrders
                                join sup in db.suppliers
                                on d.supplierID equals sup.supplierID
                                join st in db.stocks
                                on d.stockID equals st.stockID
                                join od in db.supplyment_orderDetails
                                on d.orderID equals od.orderID
                                join it in db.items
                                on od.itemCode equals it.itemCode
                                where d.orderDate >= dateTimePicker1.Value
                                select new { d.orderID, sup.supplierName, st.stockName, it.itemName, od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }
            else
            {
                var Allitems = (from d in db.supplymentOrders
                                join sup in db.suppliers
                                on d.supplierID equals sup.supplierID
                                join st in db.stocks
                                on d.stockID equals st.stockID
                                join od in db.supplyment_orderDetails
                                on d.orderID equals od.orderID
                                join it in db.items
                                on od.itemCode equals it.itemCode
                                where sup.supplierName.Contains(textBox4.Text) && d.orderDate >= dateTimePicker1.Value
                                select new { d.orderID, sup.supplierName, st.stockName, it.itemName, od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }


        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            supplymentOrder sup_ord = new supplymentOrder();
            int id;
            int c = db.supplymentOrders.Select(s => s.orderID).Count();
            if (c != 0)
            {
                id = db.supplymentOrders.Select(s => s.orderID).Max();
            }
            else
            {
                id = 0;
            }
            sup_ord.orderID = ++id;
            sup_ord.supplierID = (int)comboBox1.SelectedValue;
            sup_ord.stockID = (int)comboBox2.SelectedValue;
            sup_ord.orderDate = dateTimePicker1.Value;
            db.supplymentOrders.Add(sup_ord);
            try
            {
                db.SaveChanges();
                comboBox1.Text = comboBox2.Text = string.Empty;
                textBox3.Text = sup_ord.orderID.ToString();
                textBox9.Text = (string)comboBox1.Text;
                this.groupBox3.Visible = true;
                // this.groupBox2.Enabled = false;
                #region group
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox7.Enabled = false;
                textBox5.Enabled = false;
                button5.Enabled = false; 
                #endregion
                button6.Visible = true;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show("يوجد خطأ ف ادخال البيانات");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            stock_item it;
            supplyment_orderDetails sup_ord_det = new supplyment_orderDetails();
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                it = (from d in db.stock_item
                      where d.itemCode == (int)comboBox3.SelectedValue && d.stockID == (int)comboBox2.SelectedValue
                      select d).FirstOrDefault();
                if (it!=null)
                {
                    it.Quantity += int.Parse(textBox1.Text);
                }
                else
                {
                    it = new stock_item();
                    int st_id= (int)comboBox2.SelectedValue;
                    int itm_id= (int)comboBox3.SelectedValue;
                    it.stockID = st_id;
                    it.itemCode = itm_id;
                    it.Quantity += int.Parse(textBox1.Text);
                    db.stock_item.Add(it);
                }
               
              
                sup_ord_det.orderID = int.Parse(textBox3.Text);
                sup_ord_det.itemCode = (int)comboBox3.SelectedValue;
                sup_ord_det.quantity = int.Parse(textBox1.Text);
                sup_ord_det.itemPrice = int.Parse(textBox2.Text);
                db.supplyment_orderDetails.Add(sup_ord_det);
      
                try
                {
                    db.SaveChanges();
                    

                    textBox1.Text = textBox2.Text = comboBox1.Text = comboBox2.Text = comboBox3.Text = string.Empty;
                    fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("يوجد خطأ ف ادخال البيانات");
                }

            }
            else
            {
                MessageBox.Show("تأمد من وجود البيانات");
            }
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            var sups = (from sup in db.suppliers
                        where sup.supplierName.Contains(textBox7.Text)
                        select new { sup.supplierID, sup.supplierName }).ToList();
            comboBox1.DisplayMember = "supplierName";
            comboBox1.ValueMember = "supplierID";
            comboBox1.DataSource = sups;
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            var stocks = (from st in db.stocks
                          where st.stockName.Contains(textBox5.Text)
                          select new { st.stockID, st.stockName }).ToList();
            comboBox2.DisplayMember = "stockName";
            comboBox2.ValueMember = "stockID";
            comboBox2.DataSource = stocks;
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            var Items = (from itm in db.items
                         where itm.itemName.Contains(textBox6.Text)
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox3.DisplayMember = "itemName";
            comboBox3.ValueMember = "itemCode";
            comboBox3.DataSource = Items;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int order = int.Parse(textBox3.Text);
            int code = db.items.Where(x => x.itemName == textBox8.Text).Select(x => x.itemCode).First();
            supplyment_orderDetails od = db.supplyment_orderDetails.Find(int.Parse(textBox3.Text), code);
            supplymentOrder s_or = db.supplymentOrders.Find(int.Parse(textBox3.Text));
            od.quantity = int.Parse(textBox1.Text);
            od.itemPrice = int.Parse(textBox2.Text);
            s_or.supplierID = (int)comboBox1.SelectedValue;
            s_or.stockID = (int)comboBox2.SelectedValue;
            s_or.orderDate = dateTimePicker1.Value;
            try
            {
                db.SaveChanges();
                fillgrid();
                textBox1.Text = textBox2.Text = textBox3.Text = comboBox1.Text = comboBox2.Text = comboBox3.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("يوجد خطأ ف ادخال البيانات");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int code = db.items.Where(x => x.itemName == textBox8.Text).Select(x => x.itemCode).First();
            supplyment_orderDetails s_or = db.supplyment_orderDetails.Find(int.Parse(textBox3.Text), code);
            db.supplyment_orderDetails.Remove(s_or);
            if (MessageBox.Show("هل تريد مسح ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.SaveChanges();
                MessageBox.Show("تم المسح");
                fillgrid();
                textBox1.Text = textBox3.Text = comboBox1.Text = comboBox2.Text = comboBox3.Text = string.Empty;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region group
            comboBox1.Enabled =true;
            comboBox2.Enabled =true;
            textBox7.Enabled = true;
            textBox5.Enabled = true;
            button5.Enabled = true;
            #endregion
            groupBox3.Visible = false;
        }

        private void clientOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void manageClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void clientsOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders cl = new c_orders();
            cl.Show();
            this.Hide();
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
