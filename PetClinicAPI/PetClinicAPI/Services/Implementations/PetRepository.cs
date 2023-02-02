using MySql.Data.MySqlClient;
using PetClinicAPI.DBUtils;
using PetClinicAPI.Models;

namespace PetClinicAPI.Services.Implementations
{
    public class PetRepository : IPetRepository
    {
        public int Create(Pet item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO pets(ClientId, Name, Birthday) 
                                        VALUES(@ClientId, @Name, @Birthday)";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = item.ClientId;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = item.Name;
            command.Parameters.Add("@Birthday", MySqlDbType.DateTime).Value = item.Birthday;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Update(Pet item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE pets SET 
                                    ClientId = @ClientId, Name = @Name, Birthday = @Birthday 
                                    WHERE PetId=@PetId";
            command.Parameters.Add("@PetId", MySqlDbType.Int32).Value = item.PetId;
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = item.ClientId;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = item.Name;
            command.Parameters.Add("@Birthday", MySqlDbType.DateTime).Value = item.Birthday;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM pets WHERE PetId=@PetId";
            command.Parameters.Add("@PetId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public IList<Pet> GetAll(int clientId)
        {
            List<Pet> list = new();
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM pets WHERE ClientId=@ClientId";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = clientId;
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pet pet = new()
                {
                    PetId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Birthday = (DateTime)reader.GetMySqlDateTime(3)
                };
                list.Add(pet);
            }
            return list;
        }

        public Pet GetById(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM pets WHERE PetId=@PetId";
            command.Parameters.Add("@PetId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Pet pet = new()
                {
                    PetId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Birthday = (DateTime)reader.GetMySqlDateTime(3)
                };
                return pet;
            }
            else
            {
                return null;
            }
        }

    }
}
