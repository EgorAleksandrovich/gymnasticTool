using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace First_appl_MVVM.Data
{
    public class Repository
    {
        private string _connectionString = "Server=.\\SQLEXPRESS;Database=Mydatabase;Integrated security=true";

        public ObservableCollection<Competition> GetCompetitions()
        {
            ObservableCollection<Competition> _competitions = new ObservableCollection<Competition>();
            SqlConnection myConection = new SqlConnection(_connectionString);
            myConection.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id, CompetitionName, DateCompetition, Country FROM [dbo].[Competitions]", myConection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Competition competition = new Competition()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CompetitionName = reader["CompetitionName"].ToString(),
                    Country = reader["Country"].ToString(),
                    DateCompetition = (DateTime)reader["DateCompetition"]
                };
                _competitions.Add(competition);
            }
            reader.Close();
            myConection.Close();
            return _competitions;
        }

        public List<Competitor> GetCompetitors(int idCompetition)
        {
            List<Competitor> _сompetitors = new List<Competitor>();
            SqlConnection myConection = new SqlConnection(_connectionString);
            myConection.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id, IdGymnast FROM [dbo].[Competitors] WHERE idCompetition = @idCompetition", myConection);
            cmd.Parameters.AddWithValue("@idCompetition", idCompetition);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Competitor сompetitors = new Competitor()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IdGymnast = Convert.ToInt32(reader["IdGymnast"]),
                    IdCompetition = idCompetition
                };
                _сompetitors.Add(сompetitors);
            }
            reader.Close();
            myConection.Close();
            return _сompetitors;
        }

        public List<Gymnast> GetGymnasts(List<Competitor> competitors)
        {
            List<Gymnast> _gymnasts = new List<Gymnast>();
            if (competitors == null)
            {
                MessageBox.Show("You did not create new competitions or did not choose from existing ones. Please choose or create new competitions!");
                return _gymnasts;
            }
            else
            {
                int count = 1;
                string query = "SELECT Id, FirstName, LastName, Country FROM [dbo].[Gymnasts] WHERE Id IN(";
                foreach (Competitor competitor in competitors)
                {
                    if (competitors.Count != count)
                    {
                        query = query + "'" + competitor.IdGymnast.ToString() + "'" + ",";
                    }
                    else
                    {
                        query = query + "'" + competitor.IdGymnast.ToString() + "'" + ")";
                    }
                    count++;
                }
                SqlConnection myConection = new SqlConnection(_connectionString);
                myConection.Open();
                SqlCommand cmd = new SqlCommand(query, myConection);
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
                    _gymnasts.Add(gymnast);
                }
                reader.Close();
                myConection.Close();
                return _gymnasts;
            }
        }

        public List<Rating> GetDisciplineRatings()
        {
            List<Rating> _disciplineRatings = new List<Rating>();
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
                _disciplineRatings.Add(rating);
            }
            reader.Close();
            myConection.Close();
            return _disciplineRatings;
        }

        public int AddGymnast(Gymnast gymnastInfo)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Gymnasts] (LastName, FirstName, Country) Output Inserted.Id VALUES (@LastName, @FirstName, @Country)", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@LastName", gymnastInfo.LastName);
                cmd.Parameters.AddWithValue("@FirstName", gymnastInfo.FirstName);
                cmd.Parameters.AddWithValue("@Country", gymnastInfo.Country);
                myConection.Open();
                int idGymnast = (int)cmd.ExecuteScalar();
                return idGymnast;
            }
        }

        public void AddCompetitor(int idCompetition, int idGymnast)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Competitors] (IdGymnast, IdCompetition) VALUES (@idGymnast, @idCompetition)", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@idGymnast", idGymnast);
                cmd.Parameters.AddWithValue("@idCompetition", idCompetition);
                myConection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveRatings(PersonalRatingsDiscpline personalRatingsDiscpline)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("MERGE [dbo].[Ratings] WITH (SERIALIZABLE) AS R USING (VALUES (@Id, @rating)) AS U (Id, Rating) ON U.Id = R.Id WHEN MATCHED THEN UPDATE SET R.Rating = U.Rating WHEN NOT MATCHED THEN INSERT (GymnastId, Rating, Discipline) VALUES (@gymnastId, @rating, @discipline);", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@discipline", personalRatingsDiscpline.Discipline);
                cmd.Parameters.AddWithValue("@gymnastId", personalRatingsDiscpline.Id);
                cmd.Parameters.AddWithValue("@rating", personalRatingsDiscpline.Rating);
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
