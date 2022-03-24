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
    public partial class SKStorage : Form
    {
        public SKStorage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SKStorage_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "souvenirsDataSet.storage". При необходимости она может быть перемещена или удалена.
            this.storageTableAdapter.Fill(this.souvenirsDataSet.storage);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.storageTableAdapter.Update(this.souvenirsDataSet);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Suppliers a = new Suppliers();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Auth a = new Auth();
            a.Show();
        }

        private void SKStorage_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Auth a = new Auth();
            a.Show();
        }
    }
}
