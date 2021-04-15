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
    public partial class c_orders : Form
    {
        public c_orders()
        {
            InitializeComponent();
        }

        Model1 db = new Model1();
        private void fillgrid()
        {
            var Allitems = (from d in db.clientOrders
                            join cl in db.clients
                            on d.clientID equals cl.clientID
                            join st in db.stocks
                            on d.stockID equals st.stockID
                            join od in db.client_orderDetails
                            on d.orderID equals od.orderID
                            join it in db.items
                            on od.itemCode equals it.itemCode
                            join sup in db.suppliers
                            on od.supplierID equals sup.supplierID
                            select new { d.orderID, cl.clientName, st.stockName, it.itemName,sup.supplierName ,od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
            dataGridView1.DataSource = Allitems;
        }
        private void c_orders_Load(object sender, EventArgs e)
        {
         
            var clnts = (from cl in db.clients
                          select new { cl.clientID, cl.clientName }).ToList();
            comboBox1.DisplayMember = "clientName";
            comboBox1.ValueMember = "clientID";
            comboBox1.DataSource = clnts;

            var stocks = (from st in db.stocks
                          select new { st.stockID, st.stockName }).ToList();
            comboBox2.DisplayMember = "stockName";
            comboBox2.ValueMember = "stockID";
            comboBox2.DataSource = stocks;
            fillgrid();
            textBox8.Visible = false;
            this.groupBox3.Visible = false;
            button6.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clientOrder cl_ord = new clientOrder();
            int id;
            int c = db.clientOrders.Select(s => s.orderID).Count();
            if (c != 0)
            {
                id = db.clientOrders.Select(s => s.orderID).Max();
            }
            else
            {
                id = 0;
            }
            cl_ord.orderID = ++id;
            cl_ord.clientID = (int)comboBox1.SelectedValue;
            cl_ord.stockID = (int)comboBox2.SelectedValue;
            cl_ord.orderDate = dateTimePicker1.Value;
            db.clientOrders.Add(cl_ord);
            try
            {
                db.SaveChanges();
                var Items = (from itm in db.items
                             join st in db.stock_item
                             on itm.itemCode equals st.itemCode
                             where st.stockID == (int)comboBox2.SelectedValue
                             select new { itm.itemCode, itm.itemName }).ToList();
                comboBox3.DisplayMember = "itemName";
                comboBox3.ValueMember = "itemCode";
                comboBox3.DataSource = Items;
                comboBox1.Text = comboBox2.Text = string.Empty;
                textBox3.Text = cl_ord.orderID.ToString();
                textBox9.Text = (string)comboBox1.Text;
                this.groupBox3.Visible = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            stock_item it;
            int supID = (from itm in db.items
                         join sup_order in db.supplyment_orderDetails
                         on itm.itemCode equals sup_order.itemCode
                         join ord in db.supplymentOrders
                         on sup_order.orderID equals ord.orderID
                         where itm.itemCode==(int)comboBox3.SelectedValue
                         select ord.supplierID).First();
            client_orderDetails cl_ord_det = new client_orderDetails();
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                it = (from d in db.stock_item
                      where d.itemCode == (int)comboBox3.SelectedValue && d.stockID == (int)comboBox2.SelectedValue
                      select d).FirstOrDefault();
                if (it != null)
                {
                    if (it.Quantity<=0&&it.Quantity> int.Parse(textBox1.Text))
                    {
                        MessageBox.Show("لا يوجد كميه كافيه ");
                    }
                    else
                    {
                        it.Quantity -= int.Parse(textBox1.Text);
                    }
                    
                }
                cl_ord_det.orderID = int.Parse(textBox3.Text);
                cl_ord_det.itemCode = (int)comboBox3.SelectedValue;
                cl_ord_det.quantity = int.Parse(textBox1.Text);
                cl_ord_det.itemPrice = int.Parse(textBox2.Text);
                cl_ord_det.supplierID = supID;
                db.client_orderDetails.Add(cl_ord_det);

                try
                {
                    db.SaveChanges();
                    textBox1.Text = textBox2.Text = comboBox1.Text = comboBox2.Text = comboBox3.Text = string.Empty;
                    fillgrid();
                }
                catch (Exception ex)
                {
                   // MessageBox.Show(ex.Message);
                    MessageBox.Show("يوجد خطأ ف ادخال البيانات");
                }

            }
            else
            {
                MessageBox.Show("تأكد من وجود البيانات");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int order = int.Parse(textBox3.Text);
            int code = db.items.Where(x => x.itemName == textBox8.Text).Select(x => x.itemCode).First();
            client_orderDetails od = db.client_orderDetails.Find(int.Parse(textBox3.Text), code);
            clientOrder c_or = db.clientOrders.Find(int.Parse(textBox3.Text));
            od.quantity = int.Parse(textBox1.Text);
            od.itemPrice = int.Parse(textBox2.Text);
            c_or.clientID = (int)comboBox1.SelectedValue;
            c_or.stockID = (int)comboBox2.SelectedValue;
            c_or.orderDate = dateTimePicker1.Value;
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

        private void button3_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0&&groupBox3.Visible!=false)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox3.Text = row.Cells[0].Value.ToString();
               
                    comboBox1.Text = row.Cells[1].Value.ToString();
                    comboBox2.Text = row.Cells[2].Value.ToString();
                    comboBox3.Text = row.Cells[3].Value.ToString();
                    textBox8.Text = row.Cells[3].Value.ToString();
                    textBox1.Text = row.Cells[4].Value.ToString();
                    textBox2.Text = row.Cells[5].Value.ToString();
                    dateTimePicker1.Value = (DateTime)row.Cells[7].Value;
                    // this.groupBox3.Visible = true;
                    textBox9.Text = row.Cells[1].Value.ToString();
                
                
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            var Items = (from itm in db.items
                         where itm.itemName.Contains(textBox6.Text)
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox3.DisplayMember = "itemName";
            comboBox3.ValueMember = "itemCode";
            comboBox3.DataSource = Items;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            var stocks = (from st in db.stocks
                          where st.stockName.Contains(textBox5.Text)
                          select new { st.stockID, st.stockName }).ToList();
            comboBox2.DisplayMember = "stockName";
            comboBox2.ValueMember = "stockID";
            comboBox2.DataSource = stocks;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            var clnts = (from cl in db.clients
                        where cl.clientName.Contains(textBox7.Text)
                        select new { cl.clientID, cl.clientName }).ToList();
            comboBox1.DisplayMember = "clientName";
            comboBox1.ValueMember = "clientID";
            comboBox1.DataSource = clnts;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                var Allitems = (from d in db.clientOrders
                                join cl in db.clients
                                on d.clientID equals cl.clientID
                                join st in db.stocks
                                on d.stockID equals st.stockID
                                join od in db.client_orderDetails
                                on d.orderID equals od.orderID
                                join it in db.items
                                on od.itemCode equals it.itemCode
                                where d.orderDate >= dateTimePicker1.Value
                                select new { d.orderID, cl.clientName, st.stockName, it.itemName, od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }
            else
            {
                var Allitems = (from d in db.clientOrders
                                join cl in db.clients
                                on d.clientID equals cl.clientID
                                join st in db.stocks
                                on d.stockID equals st.stockID
                                join od in db.client_orderDetails
                                on d.orderID equals od.orderID
                                join it in db.items
                                on od.itemCode equals it.itemCode
                                where cl.clientName.Contains(textBox4.Text) && d.orderDate >= dateTimePicker1.Value
                                select new { d.orderID, cl.clientName, st.stockName, it.itemName, od.quantity, perKG = od.itemPrice, d.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items itm = new Items();
            itm.Show();
            this.Hide();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_Orders sor = new s_Orders();
            sor.Show();
            this.Hide();
        }

        private void manageSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            suppliers sup = new suppliers();
            sup.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

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
