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
    public partial class Auth : Form
    {
        DB db = new DB();

        public static string role;
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string logBox = textBox1.Text;
            string passBox = textBox2.Text;

            int a;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT idrole FROM worker WHERE login = @Ul AND password = @Up", db.getConnection());

            command.Parameters.Add("@Ul", MySqlDbType.VarChar).Value = logBox;
            command.Parameters.Add("@Up", MySqlDbType.VarChar).Value = passBox;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            a = table.Rows[0].Field<int>("idrole");

            role = a.ToString();

            if (table.Rows.Count > 0)
            {
                if (table.Rows[0].Field<int>("idrole") == 1)
                {
                    this.Hide();
                    MOrders bwa = new MOrders();
                    bwa.Show();
                }

                if (table.Rows[0].Field<int>("idrole") == 2)
                {
                    this.Hide();
                    SKStorage sk = new SKStorage();
                    sk.Show();
                }
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Логин";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Пароль";
            textBox2.ForeColor = Color.Gray;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
            textBox2.ForeColor = Color.Black;
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
