using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace AutoSoftSite1
{
    internal class TMySqlConnect // Пользовательский класс для подключения к MySQL
    {
        private readonly string connectionString;
        private MySqlConnection conn;

        /// <summary>
        /// Подключение к MySQL по ссылке
        /// </summary>
        public TMySqlConnect()
        {
            string host = "server143.hosting.reg.ru";
            int port = 3306;
            string database = "u2920585_default";
            string username = "u2920585_sofiam";
            string password = "sm1235255123sm";

            connectionString = $"server={host};user={username};database={database};password={password};CharSet=utf8mb4";
            conn = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Получение открытого соединения
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open(); // Открываем соединение, если оно еще не открыто
                }
                return conn;
            }
        }

        /// <summary>
        /// Чтение результата запроса из таблицы
        /// </summary>
        /// <param name="sqlQuery"> Запрос в виде строчки </param>
        /// <returns> MySqlDataReader для чтения данных </returns>
        public MySqlDataReader ExecuteReader(string sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, Connection);
            try
            {
                return command.ExecuteReader(); // исполнение запроса и получение данных из БД
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка выполнения запроса: " + ex.Message);
            }
        }
        public MySqlDataReader ExecuteReader(string sqlQuery, string param, string value)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, Connection);
            try
            {
                command.Parameters.AddWithValue(param, value);
                return command.ExecuteReader(); // исполнение запроса и получение данных из БД
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка выполнения запроса: " + ex.Message);
            }
        }
        public MySqlDataReader ExecuteReader(string sqlQuery, string param, int value)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, Connection);
            try
            {
                command.Parameters.AddWithValue(param, value);
                return command.ExecuteReader(); // исполнение запроса и получение данных из БД
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка выполнения запроса: " + ex.Message);
            }
        }
        /// <summary>
        /// Выполнение запроса, который не возвращает данные (например, INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="sqlQuery"> Запрос в виде строчки </param>
        /// <returns> Количество затронутых строк </returns>
        public int ExecuteNonQuery(string sqlQuery)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, int value)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, int value, string param1, string value1)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    command.Parameters.AddWithValue(param1, value1);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, int value, string param1, string value1, string param2, string value2, string param3, string value3, string param4, string value4, string param5, string value5, string param6, string value6, string param7, int value7)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    command.Parameters.AddWithValue(param1, value1);
                    command.Parameters.AddWithValue(param2, value2);
                    command.Parameters.AddWithValue(param3, value3);
                    command.Parameters.AddWithValue(param4, value4);
                    command.Parameters.AddWithValue(param5, value5);
                    command.Parameters.AddWithValue(param6, value6);
                    command.Parameters.AddWithValue(param7, value7);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, string value, string param1, string value1, string param2, string value2, string param3, int value3)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    command.Parameters.AddWithValue(param1, value1);
                    command.Parameters.AddWithValue(param2, value2);
                    command.Parameters.AddWithValue(param3, value3);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, int value, string param1, int value1, string param2, string value2, string param3, int value3)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    command.Parameters.AddWithValue(param1, value1);
                    command.Parameters.AddWithValue(param2, value2);
                    command.Parameters.AddWithValue(param3, value3);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        public int ExecuteNonQuery(string sqlQuery, string param, string value, string param1, string value1, string param2, int value2)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue(param, value);
                    command.Parameters.AddWithValue(param1, value1);
                    command.Parameters.AddWithValue(param2, value2);
                    return command.ExecuteNonQuery(); // выполнение запроса
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка выполнения запроса: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Закрыть соединение
        /// </summary>
        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public object ExecuteScalar(string sql, params object[] parameters)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        for (int i = 0; i < parameters.Length; i += 2)
                        {
                            command.Parameters.AddWithValue(parameters[i].ToString(), parameters[i + 1]);
                        }
                    }
                    return command.ExecuteScalar();
                }
            }
        }
    }
}
