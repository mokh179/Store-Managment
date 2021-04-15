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
    public partial class main : Form
    {
        
        public main()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void main_Load(object sender, EventArgs e)
        {
            label5.Text = db.items.Count().ToString();
            label6.Text = db.stocks.Count().ToString();
            label7.Text = db.suppliers.Count().ToString();
            label8.Text = db.clients.Count().ToString();
            var Allitems = (from d in db.items
                        join si in db.stock_item
                        on d.itemCode equals si.itemCode
                        join st in db.stocks
                        on si.stockID equals st.stockID
                        select new { d.itemName,d.itemModule1,st.stockName, si.Quantity}).ToList();
            dataGridView1.DataSource = Allitems;
            var stocks = db.stocks.Select(x => x.stockName).ToList();
            foreach (var item in stocks)
            {
                listBox1.Items.Add(item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stockname = (string)listBox1.SelectedItem;
            var Allitems = (from d in db.items
                            join si in db.stock_item
                            on d.itemCode equals si.itemCode
                            join st in db.stocks
                            on si.stockID equals st.stockID
                            where st.stockName== stockname
                            select new { d.itemName, d.itemModule1,si.Quantity }).ToList();
            dataGridView1.DataSource = Allitems;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Items it = new Items();
            it.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            suppliers sup = new suppliers();
            sup.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stocks st = new stocks();
            st.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s_Orders or = new s_Orders();
            or.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();
        }
    }
}
