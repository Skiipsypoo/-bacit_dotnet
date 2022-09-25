using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using MySqlConnector;

namespace bacit_dotnet.MVC.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }

        public IEnumerable<User> GetUsers()
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            var reader = ReadData("Select id, name, email, phone from users;", connection);
            var users = new List<User>();
            while (reader.Read())
            {
                var user = new User();
                user.Id = reader.GetInt32("id");
                user.Name = reader.GetString(1);
                user.Email = reader.GetString(2);
                user.Phone = reader.GetString(3);
                Console.WriteLine(reader.GetString(3));
                users.Add(user);
            }
            connection.Close();
            return users;
        }
        public void SetUsers()
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            var query = "Insert into users(id,name,email,phone) values (2, 'Even','even@lgbtq.com','67864000')";
            WriteData(query, connection);
        }
        public void SetSuggestions(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            var query = "Insert into suggestions(title,name,team,description,TimeStamp) values (@title,@name,@team,@description,@timestamp)";
            WriteSuggestions(query, connection, model);


            /*create table suggestions(title varchar(32),name varchar(32),team int, description varchar(32), TimeStamp varchar(32));
Query OK, 0 rows affected (0.490 sec)
*/
        }
        private MySqlDataReader ReadData(string query, MySqlConnection conn)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader(); ;
        }

        private void WriteData(string query, MySqlConnection conn)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.ExecuteNonQuery(); ;

        }

        private void WriteSuggestions(string query, MySqlConnection conn, SuggestionViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@title", model.Title);
            command.Parameters.AddWithValue("@name", model.Name);
            command.Parameters.AddWithValue("@team", model.Team);
            command.Parameters.AddWithValue("@description", model.Description);
            command.Parameters.AddWithValue("@timestamp", model.TimeStamp);
            command.ExecuteNonQuery(); ;
        }
    }
}
