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
    public partial class stocks : Form
    {
        public stocks()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void fillgrid()
        {
            var Allitems = (from d in db.stocks
                            join mng in db.managers
                            on d.manegerID equals mng.mangerID
                            select new { d.stockID, d.stockName, d.stockAddress,mng.managerName}).ToList();
            dataGridView1.DataSource = Allitems;
        }
        private void stocks_Load(object sender, EventArgs e)
        {
            var mangers= (from mng in db.managers
                          select new { mng.mangerID, mng.managerName }).ToList();
            comboBox1.DisplayMember = "managerName";
            comboBox1.ValueMember = "mangerID";
            comboBox1.DataSource = mangers;
            fillgrid();
            textBox4.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stock st = new stock();
            if (textBox1.Text != "" || textBox2.Text != "" || comboBox1.Text != "")
            {
                int id;
                int c = db.stocks.Select(s => s.stockID).Count();
                if (c != 0)
                {
                    id = db.items.Select(s => s.itemCode).Max();
                }
                else
                {
                    id = 0;
                }

                st.stockName = textBox1.Text;
                st.stockAddress = textBox2.Text;
                st.manegerID =(int)comboBox1.SelectedValue;
                st.stockID = ++id;
                db.stocks.Add(st);
                try
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox2.Text = comboBox1.Text = string.Empty;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطأ في ادخال البيانات");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || comboBox1.Text != "")
            {
                stock st = db.stocks.Find(int.Parse(textBox4.Text));
                st.stockName = textBox1.Text;
               st.manegerID = (int)comboBox1.SelectedValue;
                st.stockAddress = textBox2.Text;
                try
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox2.Text = comboBox1.Text=string.Empty;

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
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[0].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || comboBox1.Text != "")
            {
                stock st = db.stocks.Find(int.Parse(textBox4.Text));
                db.stocks.Remove(st);
                if (MessageBox.Show("هل تريد مسح ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.SaveChanges();
                    fillgrid();
                    textBox1.Text = textBox2.Text = comboBox1.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("تأكد من وجود بيانات");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var Allitems = (from d in db.stocks
                            join mng in db.managers
                            on d.manegerID equals mng.mangerID
                            where d.stockName.Contains(textBox3.Text)
                            select new { d.stockID, d.stockName, d.stockAddress, mng.managerName }).ToList();
            dataGridView1.DataSource = Allitems;
           
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items it = new Items();
            it.Show();
            this.Hide();
        }

        private void manageClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void clientOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();
        }

        private void manageSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            suppliers sup = new suppliers();
            sup.Show();
            this.Hide();
        }

        private void supplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_Orders s_ord = new s_Orders();
            s_ord.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main mn = new main();
            mn.Show();
            this.Hide();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
            this.Hide();
        }
    }
}
