using MySql.Data.MySqlClient;
using PetClinicAPI.DBUtils;
using PetClinicAPI.Models;

namespace PetClinicAPI.Services.Implementations
{
    public class ClientRepository : IClientRepository
    {
        public int Create(Client item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO clients(Document, SurName, FirstName, Patronymic, Birthday) 
                                        VALUES(@Document, @SurName, @FirstName, @Patronymic, @Birthday)";
            command.Parameters.Add("@Document", MySqlDbType.VarChar).Value = item.Document;
            command.Parameters.Add("@SurName", MySqlDbType.VarChar).Value = item.SurName;
            command.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = item.FirstName;
            command.Parameters.Add("@Patronymic", MySqlDbType.VarChar).Value = item.Patronymic;
            command.Parameters.Add("@Birthday", MySqlDbType.DateTime).Value = item.Birthday;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Update(Client item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE clients SET 
                                    Document = @Document, FirstName = @FirstName, 
                                    SurName = @SurName, Patronymic = @Patronymic, Birthday = @Birthday 
                                    WHERE ClientId=@ClientId";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = item.ClientId;
            command.Parameters.Add("@Document", MySqlDbType.VarChar).Value = item.Document;
            command.Parameters.Add("@SurName", MySqlDbType.VarChar).Value = item.SurName;
            command.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = item.FirstName;
            command.Parameters.Add("@Patronymic", MySqlDbType.VarChar).Value = item.Patronymic;
            command.Parameters.Add("@Birthday", MySqlDbType.DateTime).Value = item.Birthday;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM clients WHERE ClientId=@ClientId";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public IList<Client> GetAll(int id)
        {
            List<Client> list = new();
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM clients";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Client client = new()
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.GetString(1),
                    SurName = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Birthday = (DateTime)reader.GetMySqlDateTime(5)
                };
                list.Add(client);
            }
            return list;
        }

        public Client GetById(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM clients WHERE ClientId=@ClientId";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Client client = new()
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.GetString(1),
                    SurName = reader.GetString(2),
                    FirstName = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Birthday = (DateTime)reader.GetMySqlDateTime(5)
                };
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
