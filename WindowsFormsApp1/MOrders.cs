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
    public partial class MOrders : Form
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=12345;database=souvenirs");

        public static string BillId;
        public MOrders()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select idbill AS ID, idcustomer as Клиент, Address as Адрес_доставки, Total as Сумма_заказа, Date as Дата_заказа, deliverystate as Состояние_доставки from bill", conn);

            adapter.SelectCommand = command;

            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            conn.Close();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            DateTime date = DateTime.Now;
            string Today = date.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("insert bill (date) value (@d)", conn);
            command.Parameters.Add("@d", MySqlDbType.VarChar).Value = Today;
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);

            string sql = "select idbill from bill where date = @d";
            
            MySqlCommand command1 = new MySqlCommand(sql, conn);
            command1.Parameters.Add("@d", MySqlDbType.VarChar).Value = Today;
            string bwa = command1.ExecuteScalar().ToString();
            BillId = bwa;
            conn.Close();

            this.Hide();
            MMakeOrder a = new MMakeOrder();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Auth a = new Auth();
            a.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Auth a = new Auth();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers a = new Suppliers();
            a.Show();
        }
    }
}
