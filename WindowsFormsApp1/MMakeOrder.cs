using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MMakeOrder : Form
    {
        DB db = new DB();
        public MMakeOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("delete from bill where idbill = @id", db.getConnection());
            string bwa = MOrders.BillId;
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = bwa;

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            this.Hide();
            MOrders a = new MOrders();
            a.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adapterCl = new MySqlDataAdapter();
            MySqlCommand commandCl = new MySqlCommand("Select customerName FROM customer", db.getConnection());
            MySqlCommand command = new MySqlCommand("Select storage.idproduct, supplier.Supplier, Storage.Name, Storage.price, storage.quantity, storage.Available from storage, supplier where storage.idsupplier = supplier.idsupplier", db.getConnection());
            adapter.SelectCommand = command;
            adapterCl.SelectCommand = commandCl;
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            adapter.Fill(table);
            adapterCl.Fill(dataSet, "customer");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            for (int i = 0; i < dataSet.Tables["customer"].Rows.Count; i++)
            {
                comboBox1.Items.Add(dataSet.Tables["customer"].Rows[i]["customerName"]);
            }
            button2.Enabled = false;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cust = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            string adr = textBox1.Text;
            string item = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adaptera = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Update bill set idcustomer = @id, address = @ad  where idbill = @b", db.getConnection());
            MySqlCommand commanda = new MySqlCommand("Select idcustomer from customer where customerName = @cn", db.getConnection());

            commanda.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cust;

            adaptera.SelectCommand = commanda;

            DataTable tableC = new DataTable();
            adaptera.Fill(tableC);
            int cus = tableC.Rows[0].Field<int>("idcustomer");

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = cus;
            command.Parameters.Add("@ad", MySqlDbType.VarChar).Value = adr;
            command.Parameters.Add("@b", MySqlDbType.VarChar).Value = MOrders.BillId;
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=12345;database=souvenirs");
            conn.Open();
            string sql = "Select sum(storage.price) as summ from bill, storage, basket where bill.idbill = @id and basket.idproduct=storage.idproduct";
            MySqlCommand commandsum = new MySqlCommand(sql, conn);
            commandsum.Parameters.Add("@id", MySqlDbType.VarChar).Value = MOrders.BillId;
            string sum = commandsum.ExecuteScalar().ToString();

            MySqlDataAdapter adapterUp = new MySqlDataAdapter();
            MySqlCommand commandUp = new MySqlCommand("Update bill set total = @t where idbill = @id", db.getConnection());

            commandUp.Parameters.Add("@t", MySqlDbType.VarChar).Value = sum;
            commandUp.Parameters.Add("@id", MySqlDbType.VarChar).Value = MOrders.BillId;

            DataTable table1 = new DataTable();

            adapterUp.SelectCommand = commandUp;
            adapterUp.Fill(table1);

            conn.Close();

            MessageBox.Show("Заказ был оформлен!");

            this.Hide();
            MOrders a = new MOrders();
            a.Show();

            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string item = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Insert basket (idproduct, idbill) values (@p, @b)", db.getConnection());
            command.Parameters.Add("@p", MySqlDbType.VarChar).Value = item;
            command.Parameters.Add("@b", MySqlDbType.VarChar).Value = MOrders.BillId;
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);
            MessageBox.Show("Товар добавлен в корзину!");
            button2.Enabled = true;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("delete from bill where idbill = @id", db.getConnection());
            string bwa = MOrders.BillId;
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = bwa;

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            this.Hide();
            MOrders a = new MOrders();
            a.Show();
        }
    }
}
