using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace First_appl_MVVM.Data
{
    public class Repository
    {
        List<Gymnast> gymnasts = new List<Gymnast>();
        List<Rating> disciplineRatings = new List<Rating>();
        string _connectionString = "Server=.\\SQLEXPRESS;Database=Mydatabase;Integrated security=true";

        public List<Gymnast> GetGymnasts()
        {
            SqlConnection myConection = new SqlConnection(_connectionString);
            myConection.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id, FirstName, LastName, Country FROM [dbo].[Gymnasts]", myConection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Gymnast gymnast = new Gymnast()
                {
                    ID = Convert.ToInt32(reader["Id"]),
                    LastName = reader["LastName"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    Country = reader["Country"].ToString()
                };
                gymnasts.Add(gymnast);
            }
            reader.Close();
            myConection.Close();
            return gymnasts;
        }

        public List<Rating> GetDisciplineRatings()
        {
            SqlConnection myConection = new SqlConnection(_connectionString);
            myConection.Open();
            SqlCommand cmd = new SqlCommand("SELECT GymnastId, Rating, Discipline, Id FROM [dbo].[Ratings]", myConection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Rating rating = new Rating()
                {
                    GymnastId = Convert.ToInt32(reader["GymnastId"]),
                    Value = Convert.ToDouble(reader["Rating"]),
                    Discipline = (DisciplineIs)Enum.Parse(typeof(DisciplineIs), reader["Discipline"].ToString()),
                    Id = Convert.ToInt32(reader["Id"])
                };
                disciplineRatings.Add(rating);
            }
            reader.Close();
            myConection.Close();
            return disciplineRatings;
        }

        public int AddGymnast(Gymnast gymnastInfo)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Gymnasts] (LastName, FirstName, Country) output INSERTED.Id VALUES (@LastName, @FirstName, @Country)", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@LastName", gymnastInfo.LastName);
                cmd.Parameters.AddWithValue("@FirstName", gymnastInfo.FirstName);
                cmd.Parameters.AddWithValue("@Country", gymnastInfo.Country);
                myConection.Open();
                int id = (int)cmd.ExecuteScalar();
                return id;
            }
        }

        public void SaveRatings(PersonalRatingsDiscpline personalRatingsDiscpline)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Ratings] ( GymnastId, Rating, Discipline, Id) VALUES (@gymnastId, @rating, @discipline, @Id) ON DUPLICATE KEY UPDATE Rating = @Rating", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@discipline", personalRatingsDiscpline.Discipline);
                cmd.Parameters.AddWithValue("@gymnastId", personalRatingsDiscpline.Id);
                cmd.Parameters.AddWithValue("@Rating", personalRatingsDiscpline.Rating);
                cmd.Parameters.AddWithValue("@Id", personalRatingsDiscpline.IdRating);
                myConection.Open();
                cmd.ExecuteNonQuery();
            }
            personalRatingsDiscpline.IsUpdated = false;
        }

        public void RemoveGymnast(int removeId)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                myConection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Gymnasts] WHERE Id=@id", myConection))
                {
                    command.Parameters.AddWithValue("@id", removeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveDisciplineRatings(int removeId)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                myConection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Ratings] WHERE GymnastId=@removeId", myConection))
                {
                    command.Parameters.AddWithValue("@removeId", removeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
