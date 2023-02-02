using MySql.Data.MySqlClient;
using PetClinicAPI.DBUtils;
using PetClinicAPI.Models;

namespace PetClinicAPI.Services.Implementations
{
    public class ConsultationRepository : IConsultationRepository
    {
        public int Create(Consultation item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO consultations(ClientId, PetId, ConsultationDate, Description) 
                                        VALUES(@ClientId, @PetId, @ConsultationDate, @Description)";
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = item.ClientId;
            command.Parameters.Add("@PetId", MySqlDbType.Int32).Value = item.PetId;
            command.Parameters.Add("@ConsultationDate", MySqlDbType.DateTime).Value = item.ConsultationDate;
            command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = item.Description;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Update(Consultation item)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE consultations SET 
                                    ClientId = @ClientId, PetId = @PetId, 
                                    ConsultationDate = @ConsultationDate, Description = @Description
                                    WHERE ConsultationId=@ConsultationId";
            command.Parameters.Add("@ConsultationId", MySqlDbType.Int32).Value = item.ConsultationId;
            command.Parameters.Add("@ClientId", MySqlDbType.Int32).Value = item.ClientId;
            command.Parameters.Add("@PetId", MySqlDbType.Int32).Value = item.PetId;
            command.Parameters.Add("@ConsultationDate", MySqlDbType.DateTime).Value = item.ConsultationDate;
            command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = item.Description;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM consultations WHERE ConsultationId=@ConsultationId";
            command.Parameters.Add("@ConsultationId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public IList<Consultation> GetAll(int id)
        {
            List<Consultation> list = new();
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM consultations";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Consultation consultation = new()
                {
                    ConsultationId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    PetId = reader.GetInt32(2),
                    ConsultationDate = (DateTime)reader.GetMySqlDateTime(3),
                    Description = reader.GetString(4),
                };
                list.Add(consultation);
            }
            return list;
        }

        public Consultation GetById(int id)
        {
            using MySqlConnection connection = new DBMySQLUtils().GetMySQLConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM consultations WHERE ConsultationId=@ConsultationId";
            command.Parameters.Add("@ConsultationId", MySqlDbType.Int32).Value = id;
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Consultation consultation = new()
                {
                    ConsultationId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    PetId = reader.GetInt32(2),
                    ConsultationDate = (DateTime)reader.GetMySqlDateTime(3),
                    Description = reader.GetString(4),
                };
                return consultation;
            }
            else
            {
                return null;
            }
        }

    }
}
