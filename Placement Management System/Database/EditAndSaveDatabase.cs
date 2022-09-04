using System;
using MySql.Data.MySqlClient;

namespace Placement_Management_System.Database
{
    internal class EditAndSaveDatabase
    {
        public static MySqlDataReader ReadAndUpdateDatabase(string sqlCommand)
        {
            MySqlConnection conn = MakeConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static MySqlConnection MakeConnection()
        {
            string server = "localhost";
            string database = "project2";
            string username = "root";
            string password = "1234";
            string constring = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            return conn;
        }
    }
}
