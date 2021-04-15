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
    public partial class suppliers : Form
    {

        public suppliers()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void fillgrid()
        {
            var Allitems = (from d in db.suppliers
                            select new { d.supplierID,d.supplierName, d.supplierPhone, d.supplierFax, d.supplierMail,d.supplierSite }).ToList();
            dataGridView1.DataSource = Allitems;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
     
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void suppliers_Load(object sender, EventArgs e)
        {
            fillgrid();
            textBox7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            supplier sup = new supplier();
            if (textBox1.Text!="" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != ""||textBox7.Text!=null)
            {
                int id;
                int c= db.suppliers.Select(x => x.supplierID).Count();
                if (c!=0)
                {
                   id = db.suppliers.Select(x => x.supplierID).Max();
                }
                else
                {
                    id = 0;
                }
                sup.supplierName = textBox1.Text;
                sup.supplierPhone = textBox2.Text;
                sup.supplierFax = textBox3.Text;
                sup.supplierMail = textBox4.Text;
                sup.supplierSite = textBox5.Text;
                sup.supplierID = ++id;
                db.suppliers.Add(sup);
                try
                {
                    db.SaveChanges();
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
                    fillgrid();
                }
                catch (Exception)
                {

                    MessageBox.Show("خطأ في ادخال البيانات");
                }
            }
            else
            {
                MessageBox.Show("تأكد من ادخال البيانات");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox7.Text != "")
            {
                supplier sup = db.suppliers.Find(int.Parse(textBox7.Text));
                sup.supplierName = textBox1.Text;
                sup.supplierPhone = textBox2.Text;
                sup.supplierFax = textBox3.Text;
                sup.supplierMail = textBox4.Text;
                sup.supplierSite = textBox5.Text;
                try
                {
                    db.SaveChanges();
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
                    fillgrid();
                }
                catch (Exception)
                {

                    MessageBox.Show("خطأ في ادخال البيانات");
                }
            }
            else
            {
                MessageBox.Show("تأكد من ادخال البيانات");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                supplier sup = db.suppliers.Find(int.Parse(textBox7.Text));
                db.suppliers.Remove(sup);

                if (MessageBox.Show(" هل تريد مسح المورد", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.SaveChanges();
                    MessageBox.Show("تم المسح","Delete");
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
                    fillgrid();
                }
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            var sups = db.suppliers.Where(x => x.supplierName.Contains(textBox6.Text)).Select(x => new { x.supplierID,x.supplierName, x.supplierPhone,x.supplierFax, x.supplierMail, x.supplierSite }).ToList();
            dataGridView1.DataSource = sups;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[0].Value.ToString();

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
            char.IsSymbol(e.KeyChar) ||
            char.IsWhiteSpace(e.KeyChar) ||
            char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
           char.IsSymbol(e.KeyChar) ||
           char.IsWhiteSpace(e.KeyChar) ||
           char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void manageClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void clientOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stocks st = new stocks();
            st.Show();
            this.Hide();
        }

        private void supplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_Orders s_ord = new s_Orders();
            s_ord.Show();
            this.Hide();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items it = new Items();
            it.Show();
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
    }
}
