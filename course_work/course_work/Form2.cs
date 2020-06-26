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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connect DB = new Connect();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DB.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `id_user`,`login` FROM `user` WHERE `login`=@login AND `password`=@password ", DB.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = inputLogin.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = InputPassword.Text;


            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User_info.id_user = String.Format("{0}", reader[0]);
                User_info.user_login = String.Format("{0}", reader[1]);

            }

            DB.closeConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                Form4 form = new Form4();
                form.Show();
            }
            else
            {
                MessageBox.Show("Неверные данные");

            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
