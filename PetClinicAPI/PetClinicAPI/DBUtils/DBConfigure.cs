using MySql.Data.MySqlClient;

namespace PetClinicAPI.DBUtils
{
    public class DBConfigure
    {
        public static void PrepareSchema() 
        {
            using MySqlConnection connection = new DBMySQLUtils().CreateSchemaConnection();
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "CREATE DATABASE IF NOT EXISTS petclinic";
            command.ExecuteNonQuery();

            command.CommandText = "USE petclinic";
            command.ExecuteNonQuery();

            command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS clients(
                    ClientId INTEGER PRIMARY KEY auto_increment,
                    Document VARCHAR(45),
                    SurName VARCHAR(45),
                    FirstName VARCHAR(45),
                    Patronymic VARCHAR(45),
                    Birthday DATETIME)";
            command.ExecuteNonQuery();
            command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS pets(
                    PetId INTEGER PRIMARY KEY auto_increment,
                    ClientId INTEGER,
                    Name VARCHAR(45),
                    Birthday DATETIME,
                    FOREIGN KEY (ClientId) REFERENCES clients (ClientId) ON DELETE CASCADE
                    )";
            command.ExecuteNonQuery();
            command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS consultations(
                    ConsultationId INTEGER PRIMARY KEY auto_increment,
                    ClientId INTEGER,
                    PetId INTEGER,
                    ConsultationDate DATETIME,
                    Description VARCHAR(200),
                    FOREIGN KEY (ClientId) REFERENCES clients (ClientId) ON DELETE CASCADE,
                    FOREIGN KEY (PetId) REFERENCES pets (PetId) ON DELETE CASCADE
                    )";
            command.ExecuteNonQuery();
        }
    }
}
