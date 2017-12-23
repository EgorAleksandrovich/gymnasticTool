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
        private string _connectionString = "Server=EGOR-PC;Database=DevelopmentDB;Integrated security=true";
        private string _insertIndex;
        private string _query;
        private List<Gymnast> _gymnasts;
        private List<Rating> _disciplineRatings;
        private ObservableCollection<Competition> _competitions;
        private List<Competitor> _сompetitors;

        public ObservableCollection<Competition> GetCompetitions()
        {
            _competitions = new ObservableCollection<Competition>();
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
            _сompetitors = new List<Competitor>();
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
            _gymnasts = new List<Gymnast>();
            if (competitors.Count == 0)
            {
                return _gymnasts;
            }
            else
            {
                SqlConnection myConection = new SqlConnection(_connectionString);
                myConection.Open();
                SqlCommand cmd = new SqlCommand(GetQuery(competitors), myConection);
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

        public string GetQuery(List<Competitor> competitors)
        {
            _insertIndex = ")";
            _query = String.Format("SELECT Id, FirstName, LastName, Country FROM [dbo].[Gymnasts] WHERE Id IN()");
            string _idCompetitors = null;
            int count = 1;

            foreach (Competitor competitor in competitors)
            {
                if (competitors.Count != count)
                {
                    _idCompetitors = _idCompetitors + competitor.IdGymnast.ToString() + ",";
                }
                else
                {
                    _idCompetitors = _idCompetitors + competitor.IdGymnast.ToString();
                }
                count++;
            }
            _query = _query.Insert(_query.IndexOf(_insertIndex), _idCompetitors);
            return _query;
        }

        public List<Rating> GetDisciplineRatings()
        {
            _disciplineRatings = new List<Rating>();
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
                    Discipline = new Discipline{DisciplineEnum = (DisciplineIs)Enum.Parse(typeof(DisciplineIs), reader["Discipline"].ToString())},
                    Id = Convert.ToInt32(reader["Id"])
                };
                _disciplineRatings.Add(rating);
            }
            reader.Close();
            myConection.Close();
            return _disciplineRatings;
        }

        public List<Rating> GetDisciplineRatings(string discipline, int competitionId)
        {
            _disciplineRatings = new List<Rating>();
            SqlConnection myConection = new SqlConnection(_connectionString);
            myConection.Open();
            SqlCommand cmd = new SqlCommand("SELECT GymnastId, Rating, Discipline, Id FROM [dbo].[Ratings] WHERE Discipline=@Discipline AND CompetitionId=@CompetitionId", myConection);
            cmd.Parameters.AddWithValue("@Discipline", discipline);
            cmd.Parameters.AddWithValue("@CompetitionId", competitionId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Rating rating = new Rating()
                {
                    GymnastId = Convert.ToInt32(reader["GymnastId"]),
                    Value = Convert.ToDouble(reader["Rating"]),
                    Discipline = new Discipline { DisciplineEnum = (DisciplineIs)Enum.Parse(typeof(DisciplineIs), discipline) },
                    Id = Convert.ToInt32(reader["Id"]),
                    IdCompetition = competitionId
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
                SqlCommand cmd = new SqlCommand("MERGE [dbo].[Ratings] WITH (SERIALIZABLE) AS R USING (VALUES (@Id, @rating)) AS U (Id, Rating) ON U.Id = R.Id WHEN MATCHED THEN UPDATE SET R.Rating = U.Rating WHEN NOT MATCHED THEN INSERT (GymnastId, Rating, Discipline, CompetitionId) VALUES (@gymnastId, @rating, @discipline, @competitionId);", myConection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConection;
                cmd.Parameters.AddWithValue("@discipline", personalRatingsDiscpline.Discipline);
                cmd.Parameters.AddWithValue("@gymnastId", personalRatingsDiscpline.Id);
                cmd.Parameters.AddWithValue("@rating", personalRatingsDiscpline.Rating);
                cmd.Parameters.AddWithValue("@Id", personalRatingsDiscpline.IdRating);
                cmd.Parameters.AddWithValue("@competitionId", personalRatingsDiscpline.CompetitionId);
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

        public void RemoveCompetition(int removeId)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                myConection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Competitions] WHERE Id=@id", myConection))
                {
                    command.Parameters.AddWithValue("@id", removeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCompetitor(int idGymnast, int idCompetition)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                myConection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Competitors] WHERE IdGymnast = @idGymnast AND IdCompetition = @idCompetition", myConection))
                {
                    command.Parameters.AddWithValue("@idGymnast", idGymnast);
                    command.Parameters.AddWithValue("@idCompetition", idCompetition);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCompetitor(int idCompetition)
        {
            using (SqlConnection myConection = new SqlConnection(_connectionString))
            {
                myConection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Competitors] WHERE IdCompetition = @idCompetition", myConection))
                {
                    command.Parameters.AddWithValue("@idCompetition", idCompetition);
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

        public Int32 AddCompetition(Competition competition)
        {
            using (SqlConnection myConnection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Competitions] (CompetitionName, DateCompetition, Country) Output Inserted.Id Values (@competitionName, @dateCompetition, @country)", myConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = myConnection;
                cmd.Parameters.AddWithValue("@competitionName", competition.CompetitionName);
                cmd.Parameters.AddWithValue("@dateCompetition", competition.DateCompetition);
                cmd.Parameters.AddWithValue("@country", competition.Country);
                myConnection.Open();
                int idCompetition = (int)cmd.ExecuteScalar();
                return idCompetition;
            }
        }
    }
}
