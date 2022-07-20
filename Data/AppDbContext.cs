using MySql.Data.MySqlClient;
using mcc_web_client.Models;

namespace mcc_web_client.Data
{
    public class AppDbContext
    {
        public string ConnectionString { get; set; }

        public AppDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<MCCD> GetAllData()
        {
            List<MCCD> list = new List<MCCD>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                // Get last added 50 rows, sort by asc (1, 2, ..., 50)
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " +
                    "(SELECT * FROM mydb.mymcc ORDER BY id DESC LIMIT 50)Var1 " +
                    "ORDER BY id ASC;", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MCCD()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            tmp = Convert.ToSingle(reader["temperature"]),
                            vbr = Convert.ToSingle(reader["vibrations"]),
                            rpm = Convert.ToSingle(reader["rpm"]),
                            recordingTime = Convert.ToDateTime(reader["recording_time"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
