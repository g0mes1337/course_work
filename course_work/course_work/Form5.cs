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
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            Connect DB = new Connect();


            MySqlCommand command = new MySqlCommand("SELECT `name`, `price` FROM `product`  ", DB.getConnection());
            
            DB.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[2]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();


            }


            reader.Close();

            DB.closeConnection();
            foreach (string[] s in data)
            {
                comboBox1.Items.Add(s[0]);
            }

            
        }

        private void GetPrice()
        {
            Connect DB = new Connect();


            MySqlCommand command = new MySqlCommand("SELECT  `price` FROM `product` WHERE `name`=@name ", DB.getConnection());
            command.Parameters.Add("@name", MySqlDbType.String).Value = comboBox1.SelectedItem.ToString();

            DB.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[1]);

                data[data.Count - 1][0] = reader[0].ToString();
                
            }
            
            reader.Close();

            DB.closeConnection();
            foreach (string[] s in data)
            {
                User_info.cost = s[0];
            }
            

        }
        private void GetIdGoods()
        {
            Connect DB = new Connect();


            MySqlCommand command = new MySqlCommand("SELECT  `id_product` FROM `product` WHERE `name`=@name ", DB.getConnection());
            command.Parameters.Add("@name", MySqlDbType.String).Value = comboBox1.SelectedItem.ToString();

            DB.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[1]);

                data[data.Count - 1][0] = reader[0].ToString();

            }
            reader.Close();

            DB.closeConnection();
            foreach (string[] s in data)
            {
                User_info.id_goods = s[0];
            }
        }
        //private void getCount()
        //{
        //    Connect DB = new Connect();


        //    MySqlCommand command = new MySqlCommand("SELECT  `count` FROM `product` WHERE `name`=@name ", DB.getConnection());
        //    command.Parameters.Add("@name", MySqlDbType.String).Value = comboBox1.SelectedItem.ToString();

        //    DB.openConnection();

        //    MySqlDataReader reader = command.ExecuteReader();

        //    List<string[]> data = new List<string[]>();

        //    while (reader.Read())
        //    {
        //        data.Add(new string[1]);

        //        data[data.Count - 1][0] = reader[0].ToString();

        //    }
        //    reader.Close();

        //    DB.closeConnection();
        //    foreach (string[] s in data)
        //    {
        //        User_info.count = s[0];
        //    }
        //}
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите получателя");
                return;
            }
            if (label6.Text == "")
            {
                MessageBox.Show("Расчитайте товар");
                return;
            }
            if (comboBox1.SelectedItem== null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Выберите количество");
                return;
            }

            GetIdGoods();
            
            Connect DB = new Connect();

            DB.openConnection();
            
            MySqlCommand command = new MySqlCommand("INSERT INTO `sale`(`id_user`,`id_goods`, `executor`, `customer`, `Cost`) VALUES (@id_user,@id_goods,@executor,@customer,@cost)", DB.getConnection());
            command.Parameters.Add("@executor", MySqlDbType.String).Value = User_info.user_login;
            command.Parameters.Add("@customer", MySqlDbType.String).Value = textBox1.Text;
            command.Parameters.Add("@Cost", MySqlDbType.String).Value = label6.Text;
            command.Parameters.Add("@id_user", MySqlDbType.String).Value = User_info.id_user;
            command.Parameters.Add("@id_goods", MySqlDbType.String).Value = User_info.id_goods;
            
            command.Prepare();
            command.ExecuteNonQuery();
            
            

            DB.closeConnection();
            //delete();
            MessageBox.Show("Товар успешно оформлен");
            
        }

        //private void delete()
        //{
        //    GetIdGoods();

        //    Connect DB = new Connect();
        //    DB.openConnection();

        //    MySqlCommand command = new MySqlCommand("DELETE FROM `sale`  WHERE id_sale=@id_product", DB.getConnection());
        //    command.Parameters.Add("@id_product", MySqlDbType.String).Value = User_info.id_goods;

        //    command.Prepare();
        //    command.ExecuteNonQuery();
        //    DB.closeConnection();

        //    Connect DB1 = new Connect();
        //    DB1.openConnection();

        //    MySqlCommand command1 = new MySqlCommand("DELETE FROM `product`  WHERE id_product=@id_product", DB1.getConnection());
        //    command1.Parameters.Add("@id_product", MySqlDbType.String).Value = User_info.id_goods;
        
        //    command1.Prepare();
        //    command1.ExecuteNonQuery();
        //    DB1.closeConnection();

        //}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetPrice();
            int summ = 0;
            int a = Convert.ToInt32(User_info.cost);
            int count = (int)numericUpDown1.Value;
            
            while (count!=0)
            {
                summ += a;
                count--;
            }
            label6.Text = summ.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form = new Form4();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
