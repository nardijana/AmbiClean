using MySql.Data.MySqlClient;
using System;

namespace AmbiClean.Database
{
    public sealed class Database
    {
        private static Database _instance = null;
        private static readonly object _lock = new object();
        private MySqlConnection _connection;

        private readonly string _connectionString =
            "Server=3.89.66.33;Port=3306;Uid=alunos;Pwd=alunos;Database=Ambiclean";

        private Database()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public static Database Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Database();
                    return _instance;
                }
            }
        }

        public MySqlConnection GetConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}

