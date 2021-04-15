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
    public partial class clients : Form
    {
        public clients()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void fillgrid()
        {
            var Allitems = (from d in db.clients
                            select new { d.clientID,d.clientName, d.clientPhone, d.clientFax, d.clientMail, d.clientSite }).ToList();
            dataGridView1.DataSource = Allitems;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client cl = new client();
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                int id;
                int c = db.clients.Select(x => x.clientID).Count();
                if (c!=0)
                {
                    id = db.clients.Select(x => x.clientID).Max();
                    
                }
                else
                {
                    id = 0;
                }
                
               cl.clientName = textBox1.Text;
               cl.clientPhone = textBox2.Text;
               cl.clientFax = textBox3.Text;
               cl.clientMail = textBox4.Text;
               cl.clientSite = textBox5.Text;
               cl.clientID = ++id;
                db.clients.Add(cl);
                try
                {
                    db.SaveChanges();
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
                    fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("خطأ في ادخال البيانات");
                }
            }
            else
            {
                MessageBox.Show("تأكد من ادخال البيانات");
            }
        }

        private void clients_Load(object sender, EventArgs e)
        {
            fillgrid();
            textBox7.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != ""||textBox7.Text!="")
            {
                client cl = db.clients.Find(int.Parse(textBox7.Text));
                cl.clientName = textBox1.Text;
                cl.clientPhone = textBox2.Text;
                cl.clientFax = textBox3.Text;
                cl.clientMail = textBox4.Text;
                cl.clientSite = textBox5.Text;
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
                client cl = db.clients.Find(int.Parse(textBox7.Text));
                db.clients.Remove(cl);

                if (MessageBox.Show(" هل تريد مسح العميل", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.SaveChanges();
                    MessageBox.Show("تم المسح", "Delete");
                    fillgrid();
                }
            }
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
                textBox7.Text= row.Cells[0].Value.ToString();

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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            var cls = db.clients.Where(x => x.clientName.Contains(textBox6.Text))
                     .Select(x => new { x.clientID,x.clientName, x.clientPhone, x.clientFax, x.clientMail, x.clientSite }).ToList();
            dataGridView1.DataSource = cls;
        }

       

        private void clientOrderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            c_orders cl = new c_orders();
            cl.Show();
            this.Hide();
        }

        private void manageSupplierToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            suppliers sup = new suppliers();
            sup.Show();
            this.Hide();
        }

        private void supplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_Orders c_ord= new s_Orders();
            c_ord.Show();
            this.Hide();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items itm = new Items();
            itm.Show();
            this.Hide();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stocks st=new stocks();
            st.Show();
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
