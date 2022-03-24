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
    public partial class Suppliers : Form
    {
        public Suppliers()
        {
            InitializeComponent();
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "souvenirsDataSet.supplier". При необходимости она может быть перемещена или удалена.
            this.supplierTableAdapter.Fill(this.souvenirsDataSet.supplier);
            int role = Convert.ToInt32(Auth.role);
            if (role == 1)
            {
                button2.Enabled = true;
            }
            else if (role == 2)
            {
                button2.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.supplierTableAdapter.Update(this.souvenirsDataSet);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
