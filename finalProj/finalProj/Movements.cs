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
    public partial class Movements : Form
    {
        public Movements()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void Movements_Load(object sender, EventArgs e)
        {
            var Items = (from itm in db.items
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox1.DisplayMember = "itemName";
            comboBox1.ValueMember = "itemCode";
            comboBox1.DataSource = Items;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var movitem= (from itm in db.items
                                 join mov in db.movements
                                 on itm.itemCode equals mov.itemCode
                                 join st in db.stocks
                                 on mov.fromStock equals st.stockID
                                join st1 in db.stocks
                                on mov.toStock equals st1.stockID
                                join sup in db.suppliers
                                on mov.SupplierID equals sup.supplierID
                                where itm.itemCode==(int)comboBox1.SelectedValue
                          select new {  itm.itemName,fromStock=st.stockName,toStock=st1.stockName,sup.supplierName,mov.Quantity,mov.moveDate}).ToList();
            dataGridView1.DataSource = movitem;
        }

        private void clientOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();

        }

        private void manageOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
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

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items itm = new Items();
            itm.Show();
            this.Hide();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stocks st = new stocks();
            st.Show();
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
