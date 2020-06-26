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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((inputLogin.Text == "") )
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if ((InputPassword.Text == ""))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (UserCheck() == true)
            {
                return;
            }

            Connect DB = new Connect();



            MySqlCommand command = new MySqlCommand("INSERT INTO `user`(`login`, `password`) VALUES (@login,@password)", DB.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = inputLogin.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = InputPassword.Text;


            DB.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан");
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");

            }

            DB.closeConnection();
        }
        public Boolean UserCheck()
        {
            Connect DB = new Connect();
            
            DB.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `login` FROM `user` WHERE `login`=@login", DB.getConnection());
            command.Parameters.Add("@login", MySqlDbType.String).Value = inputLogin.Text;

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();
            
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь существует");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
