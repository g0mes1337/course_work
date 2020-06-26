using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course_work
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {




            Connect DB = new Connect();


            MySqlCommand command = new MySqlCommand("SELECT `name`, SUM(`count`) `count`, `type`, `price`, `width`, `length` FROM `product` GROUP BY `name`, `type`, `price`, `width`, `length` ", DB.getConnection());

            DB.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                

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


            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex ==
            dataGridView1.Columns["UpdateBtn"].Index)
            {
                Connect DB = new Connect();

                DB.openConnection();

                DataGridViewCellCollection cells = dataGridView1.Rows[e.RowIndex].Cells;

                if (cells[0].Value == null || cells[1].Value == null || cells[2].Value == null || cells[3].Value == null || cells[4].Value == null || cells[5].Value == null)
                {
                    MessageBox.Show("Значения не введены");
                    return;
                }

                MySqlCommand command = new MySqlCommand("UPDATE `product` SET `name`=@name,`type`=@type,`price`=@price,`width`=@width,`length`=@lenght WHERE `name`=@name ", DB.getConnection());
                command.Parameters.Add("@name", MySqlDbType.String).Value = cells[0].Value.ToString();
                command.Parameters.Add("@type", MySqlDbType.String).Value = cells[2].Value.ToString();
                command.Parameters.Add("@price", MySqlDbType.String).Value = cells[3].Value.ToString();
                command.Parameters.Add("@width", MySqlDbType.String).Value = cells[4].Value.ToString();
                command.Parameters.Add("@lenght", MySqlDbType.String).Value = cells[5].Value.ToString();


                command.Prepare();
                command.ExecuteNonQuery();


                DB.closeConnection();
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else if (e.RowIndex < 0 || e.ColumnIndex ==
            dataGridView1.Columns["AddBtn"].Index)
                 {

                Connect DB = new Connect();

                DB.openConnection();

                DataGridViewCellCollection cells = dataGridView1.Rows[e.RowIndex].Cells;


                if (cells[0].Value == null || cells[1].Value == null || cells[2].Value == null || cells[3].Value == null || cells[4].Value == null || cells[5].Value == null)
                {
                    MessageBox.Show("Значения не введены");
                    return;
                }
                MySqlCommand command = new MySqlCommand("INSERT INTO `product`(`name`, `type`, `price`, `width`, `length`, `count`) VALUES (@name,@type,@price,@width,@lenght,@count)", DB.getConnection());
                command.Parameters.Add("@name", MySqlDbType.String).Value = cells[0].Value.ToString();
                command.Parameters.Add("@type", MySqlDbType.String).Value = cells[2].Value.ToString();
                command.Parameters.Add("@price", MySqlDbType.String).Value = cells[3].Value.ToString();
                command.Parameters.Add("@width", MySqlDbType.String).Value = cells[4].Value.ToString();
                command.Parameters.Add("@lenght", MySqlDbType.String).Value = cells[5].Value.ToString();
                command.Parameters.Add("@count", MySqlDbType.String).Value = cells[1].Value.ToString();


                command.Prepare();
                command.ExecuteNonQuery();


                DB.closeConnection();
                this.Hide();
                Main main = new Main();
                main.Show();
            }
                 else if(e.RowIndex < 0 || e.ColumnIndex ==
                dataGridView1.Columns["DeleteBtn"].Index)
               {

                Connect DB = new Connect();

                DB.openConnection();
                DataGridViewCellCollection cells = dataGridView1.Rows[e.RowIndex].Cells;


                MySqlCommand command = new MySqlCommand("DELETE FROM `product` WHERE `name`=@name LIMIT 1" , DB.getConnection());
                command.Parameters.Add("@name", MySqlDbType.String).Value = cells[0].Value.ToString();
                command.Prepare();
                command.ExecuteNonQuery();


                DB.closeConnection();

                this.Hide();
                Main main = new Main();
                main.Show();
                

                     }
            else {return; }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form4 sale = new Form4();
            sale.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 sale = new Form4();
            sale.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
