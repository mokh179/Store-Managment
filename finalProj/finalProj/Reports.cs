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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        private void Reports_Load(object sender, EventArgs e)
        {
            var stocks1 = (from st in db.stocks
                           select new { st.stockID, st.stockName }).ToList();
            comboBox1.DisplayMember = "stockName";
            comboBox1.ValueMember = "stockID";
            comboBox1.DataSource = stocks1;
            this.groupBox1.Visible = false;

            var Items = (from itm in db.items
                         select new { itm.itemCode, itm.itemName }).ToList();
            comboBox2.DisplayMember = "itemName";
            comboBox2.ValueMember = "itemCode";
            comboBox2.DataSource = Items;

            //////////////////////////////
            

            comboBox5.DisplayMember = "itemName";
            comboBox5.ValueMember = "itemCode";
            comboBox5.DataSource = Items;
            ////////////////////////////////////////////////////

            groupBox1.Visible=false;
            groupBox2.Visible=false;
            groupBox3.Visible = false;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date==DateTime.Now.Date)
            {
                var Allitems = (from d in db.stocks
                                join mng in db.managers
                                on d.manegerID equals mng.mangerID
                                join ord in db.supplymentOrders
                                on d.stockID equals ord.stockID
                                join ord_det in db.supplyment_orderDetails
                                on ord.orderID equals ord_det.orderID
                                join itm in db.items
                                on ord_det.itemCode equals itm.itemCode
                                join sup in db.suppliers
                                on ord.supplierID equals sup.supplierID
                                where d.stockID == (int)comboBox1.SelectedValue
                                select new { d.stockAddress, sup.supplierName, itm.itemName, ord_det.quantity, ord.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }
            else
            {
                var Allitems = (from d in db.stocks
                                join mng in db.managers
                                on d.manegerID equals mng.mangerID
                                join ord in db.supplymentOrders
                                on d.stockID equals ord.stockID
                                join ord_det in db.supplyment_orderDetails
                                on ord.orderID equals ord_det.orderID
                                join itm in db.items
                                on ord_det.itemCode equals itm.itemCode
                                join sup in db.suppliers
                                on ord.supplierID equals sup.supplierID
                                where d.stockID == (int)comboBox1.SelectedValue&&ord.orderDate>= dateTimePicker1.Value.Date
                                select new {  d.stockAddress,sup.supplierName, itm.itemName, ord_det.quantity, ord.orderDate }).ToList();
                dataGridView1.DataSource = Allitems;
            }
            
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = true;
            this.groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stocks = (from st in db.stocks
                          join st_c in db.stock_item
                          on st.stockID equals st_c.stockID
                          join i in db.items
                          on st_c.itemCode equals i.itemCode
                          where st_c.itemCode == (int)comboBox2.SelectedValue
                          select new { st.stockID, st.stockName }).ToList();

            comboBox3.DisplayMember = "stockName";
            comboBox3.ValueMember = "stockID";
            comboBox3.DataSource = stocks;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dateTimePicker2.Value.Date == DateTime.Now.Date)
            {
                var AllItems = (from itm in db.items
                                join st_itm in db.stock_item
                                on itm.itemCode equals st_itm.itemCode
                                join st in db.stocks
                                on st_itm.stockID equals st.stockID
                                join sup_ord in db.supplymentOrders
                                on st.stockID equals sup_ord.stockID
                                join sup in db.suppliers
                                on sup_ord.supplierID equals sup.supplierID
                                join ord_det in db.supplyment_orderDetails
                                on sup_ord.orderID equals ord_det.orderID
                                where itm.itemCode == (int)comboBox2.SelectedValue && st.stockID == (int)comboBox3.SelectedValue
                                select new { sup_ord.orderID, sup.supplierName, ord_det.quantity, sup_ord.orderDate }).ToList();
                dataGridView1.DataSource = AllItems;
            }
            else
            {
                var AllItems = (from itm in db.items
                                join st_itm in db.stock_item
                                on itm.itemCode equals st_itm.itemCode
                                join st in db.stocks
                                on st_itm.stockID equals st.stockID
                                join sup_ord in db.supplymentOrders
                                on st.stockID equals sup_ord.stockID
                                join sup in db.suppliers
                                on sup_ord.supplierID equals sup.supplierID
                                join ord_det in db.supplyment_orderDetails
                                on sup_ord.orderID equals ord_det.orderID
                                where itm.itemCode == (int)comboBox2.SelectedValue && st.stockID == (int)comboBox3.SelectedValue&&sup_ord.orderDate==dateTimePicker2.Value
                                select new { sup_ord.orderID, sup.supplierName, ord_det.quantity, sup_ord.orderDate }).ToList();
                dataGridView1.DataSource = AllItems;
            }
                
           
                    
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var AllItems = (from itm in db.items
                            join st_itm in db.stock_item
                            on itm.itemCode equals st_itm.itemCode
                            join st in db.stocks
                            on st_itm.stockID equals st.stockID
                            join sup_ord in db.supplymentOrders
                            on st.stockID equals sup_ord.stockID
                            join sup in db.suppliers
                            on sup_ord.supplierID equals sup.supplierID
                            join ord_det in db.supplyment_orderDetails
                            on sup_ord.orderID equals ord_det.orderID
                            where itm.itemCode == (int)comboBox2.SelectedValue
                            orderby sup_ord.orderDate ascending
                            select new { st.stockName, sup.supplierName, ord_det.quantity, sup_ord.orderDate, periodInStock=(sup_ord.orderDate.Year-DateTime.Now.Year)+" Years " +"/"+(sup_ord.orderDate.Day - DateTime.Now.Day)+" Days" }).ToList();
            dataGridView1.DataSource = AllItems;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            var items = db.items.Where(x => x.expiredDate.Year - x.ProductionDate.Value.Year <= 1 && x.expiredDate.Day - x.ProductionDate.Value.Day < 365).Select(x => new { x.itemName, periodToExpired = (x.expiredDate.Year - x.ProductionDate.Value.Year + " Years") + (Math.Abs(x.expiredDate.Day - x.ProductionDate.Value.Day) + " Days") }).ToList();
            dataGridView1.DataSource = items;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clientsOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_orders c_ord = new c_orders();
            c_ord.Show();
            this.Hide();
        }

        private void manageClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients cl = new clients();
            cl.Show();
            this.Hide();
        }

        private void supplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel5_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            Movements mov = new Movements();
            mov.Show();
            this.Hide();
        }
    }
}
