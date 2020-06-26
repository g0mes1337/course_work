using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course_work
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex ==
                dataGridView1.Columns["DeleteBtn"].Index)
            {

                Connect DB = new Connect();

                DB.openConnection();
                DataGridViewCellCollection cells = dataGridView1.Rows[e.RowIndex].Cells;


                MySqlCommand command = new MySqlCommand("DELETE FROM `sale` WHERE id_sale=id_sale LIMIT 1", DB.getConnection());
                command.Prepare();
                command.ExecuteNonQuery();


                DB.closeConnection();
            
                this.Hide();
                Form4 main = new Form4();
                main.Show();


            }
            else { return; }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Connect DB = new Connect();


            MySqlCommand command = new MySqlCommand("SELECT product.name,sale.Cost,sale.executor,sale.customer FROM `sale` INNER JOIN product WHERE product.id_product = sale.id_goods", DB.getConnection());

            DB.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[4]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();


            }
            reader.Close();

            DB.closeConnection();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaleForm sale = new SaleForm();
            sale.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
