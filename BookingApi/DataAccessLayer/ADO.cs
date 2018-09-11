using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessLayer
{
    public class MovieCustReg
    {
        private SqlConnection conn = null;
        private SqlDataAdapter Adapter = null;
        private string query = string.Empty;
        public SqlConnection Conn { get => conn; set => conn = value; }
        bool IsInsert = false;
        public MovieCustReg()
        {
            string connectionString = ConfigurationManager.AppSettings["MovieDBConnect"];
            Conn = new SqlConnection(@"Server = tcp:301chennai.database.windows.net, 1433; Initial Catalog = Movie; Persist Security Info = False; User ID = chennai; Password =Mindtree@123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }
        public SqlConnection DBConnection()
        {

            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            return Conn;
        }

        public bool createUser(UserDetails obj)
        {

            try
            {
                query = "INSERT INTO dbo.Customer_details (CName, CPhone, Cemail,Password) " +
                        "VALUES (@CName, @CPhone, @Cemail,@Password)";
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    objCmd.Parameters.Add("@CName", SqlDbType.VarChar, 50).Value = obj.CName;
                    objCmd.Parameters.Add("@CPhone", SqlDbType.VarChar, 50).Value = obj.CPhone;
                    objCmd.Parameters.Add("@Cemail", SqlDbType.VarChar, 50).Value = obj.Cemail;
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = obj.Password;
                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool updateUser(UserDetails obj)
        {

            try
            {
                query = "update Customer_details set CName=@CName,CPhone=@CPhone,Password=@Password where cid=" + obj.CID;
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    objCmd.Parameters.Add("@CName", SqlDbType.VarChar, 50).Value = obj.CName;
                    objCmd.Parameters.Add("@CPhone", SqlDbType.VarChar, 50).Value = obj.CPhone;
                  
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = obj.Password;

                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true:false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateSeat(AddSeats obj)
        {

            try
            {
                query = "update Tickets set Nooftickets=@Nooftickets where ScreenId=@ScreenId and Classid=@Classid";
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    objCmd.Parameters.Add("@Nooftickets", SqlDbType.VarChar, 50).Value = obj.NoofTickets;
                    objCmd.Parameters.Add("@ScreenId", SqlDbType.VarChar, 50).Value = obj.ScreenId;

                    objCmd.Parameters.Add("@Classid", SqlDbType.VarChar, 50).Value = obj.Classid;

                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateAmount(ChangePrice obj)
        {

            try
            {
                query = "update Movie_Class set Amount=@Amount where Classid=@Classid";
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    
                    objCmd.Parameters.Add("@Amount", SqlDbType.VarChar, 50).Value = obj.Amount;

                    objCmd.Parameters.Add("@Classid", SqlDbType.VarChar, 50).Value = obj.Classid;

                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SQLCommand(string query)
        {
            try
            {
                Adapter = new SqlDataAdapter();

                using (SqlCommand myCommand = new SqlCommand())
                {
                    DataTable dataTable = new DataTable();
                    dataTable = null;
                    DataSet ds = new DataSet();
                    myCommand.Connection = DBConnection();
                    myCommand.CommandText = query;
                    Adapter.SelectCommand = myCommand;
                    Adapter.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserDetails> Login(LoginDetails obj)
        {
            List<UserDetails> lst = new List<UserDetails>();
            
            DataSet ds = new DataSet();
            query = "select Cid, CName,CPhone,Cemail from dbo.Customer_details where Cemail = " + "'" + obj.UserName + "' and Password=" + "'" + obj.Password + "'";
            ds = SQLCommand(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lst = ds.Tables[0].AsEnumerable()
              .Select(row => new UserDetails
              {
                  CID = row.Field<int>(0),
                  CName = row.Field<string>(1),
                  CPhone = row.Field<string>(2),
                  Cemail= row.Field<string>(3)
              }).ToList();
            }
            return lst;


        }


        public List<movieDetails> MovieRunning()
        {
            List<movieDetails> lst = new List<movieDetails>();
            DataSet ds = new DataSet();
            query = " Select distinct MS.MovieId,MS.MovieName from Movie_Showing MS where MS.Runningdate >= GETDATE()";
            ds = SQLCommand(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lst = ds.Tables[0].AsEnumerable()
              .Select(row => new movieDetails
              {
                  MovieID = row.Field<string>(0),
                  MovieName = row.Field<string>(1)
              }).ToList();
            }
            return lst;
        }

        public DataTable AdminMovieRunning()
        {
            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spMovieRunning";
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];
            }

            }

        public bool AdminAddMovie(movieDetailsList obj)
        {
            try
            {
                //  query = "INSERT INTO dbo.Movie_Showing (From,Runningdate, ScreenName, MovieName) " +
                // "VALUES (@From,@Runningdate, @ScreenName, @MovieName)";
                Adapter = new SqlDataAdapter();
                using (SqlCommand objCmd = new SqlCommand("SpInsertMovie", DBConnection()))
                {
                    DataTable dataTable = new DataTable();
                    dataTable = null;
                    DataSet ds = new DataSet();

                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = obj.From;
                    objCmd.Parameters.Add("@Runningdate", SqlDbType.VarChar, 50).Value = obj.RunningUpto;
                    objCmd.Parameters.Add("@ScreenName", SqlDbType.VarChar, 50).Value = obj.Screen;
                    objCmd.Parameters.Add("@MovieName", SqlDbType.VarChar, 50).Value = obj.Movie;
                    
                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool AdminUpdateMovie(movieDetailsList obj)
        {
            try
            {
                query = "update Movie_Showing set Runningdate = " + "'" + obj.RunningUpto + "' where ScreenName='"+obj.Screen+"' and MovieName='"+obj.Movie+"'";
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    //objCmd.Parameters.Add("@Runningdate", SqlDbType.VarChar, 50).Value = obj.RunningUpto;
                    //objCmd.Parameters.Add("@ScreenName", SqlDbType.VarChar, 50).Value = obj.Screen;
                    //objCmd.Parameters.Add("@MovieName", SqlDbType.VarChar, 50).Value = obj.Movie;

                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable AdminComplients()
        {
            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Complientlist";
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];
            }

        }

        public DataTable BookMovie(BookMovie obj)
        {
            DataTable dataTable = new DataTable();
            SqlCommand cmdd = new SqlCommand("SpBookTickets", DBConnection());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.Add("@Movieid", SqlDbType.NVarChar, 50).Value = obj.Movieid;
            cmdd.Parameters.Add("@Classid", SqlDbType.NVarChar, 50).Value = obj.Classid;
            cmdd.Parameters.Add("@Showid", SqlDbType.NVarChar, 50).Value = obj.Showid;
            cmdd.Parameters.Add("@BookingDate", SqlDbType.DateTime).Value = obj.BookingDate;
            cmdd.Parameters.Add("@NoTickets", SqlDbType.Int, 50).Value = obj.NoTickets;
            cmdd.Parameters.Add("@CUSTOMERID", SqlDbType.Int, 50).Value = obj.CUSTOMERID;
           
                cmdd.Connection = conn;
               

                adp.SelectCommand = cmdd;
                adp.Fill(dataTable);
                return dataTable;

           
        }

        public DataTable PrintTicket(string Bookid)
        {
            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spGenerateTicket";
                myCommand.Parameters.Add("@BookingId", SqlDbType.VarChar).Value = Bookid;
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];


            }
        }

        public DataTable MyPrintTicket(string Cusid)
        {
            int c = Convert.ToInt32(Cusid);

            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "mybookings";
                myCommand.Parameters.Add("@Customerid", SqlDbType.Int).Value = c;
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];


            }
        }

        public DataTable Loaddate(string MovieId)
        {
            

            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "getdates";
                myCommand.Parameters.Add("@movieid", SqlDbType.VarChar).Value = MovieId;
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];


            }
        }

        public DataTable TicketsAvliblity(string showdate)
        {
            
            Adapter = new SqlDataAdapter();
            using (SqlCommand myCommand = new SqlCommand())
            {
                DataTable dataTable = new DataTable();
                dataTable = null;
                DataSet ds = new DataSet();
                myCommand.Connection = DBConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SpCheckTicketsAval";
                myCommand.Parameters.Add("@Showdate", SqlDbType.NVarChar).Value = showdate;
                Adapter.SelectCommand = myCommand;
                Adapter.Fill(ds);
                return ds.Tables[0];


            }
        }

        public bool postcomplient(ComplientStatus obj)
        {
            try
            {
                query = "Insert into complient(cusid , Issue , descriptiontext , compstatus, posteddate) values(@cusid , @Issue , @descriptiontext , @compstatus, @posteddate)";
                //"VALUES (@CName, @CPhone, @Cemail,@Password)";
                obj.posteddate = DateTime.Now.ToString();
                obj.compstatus = "Posted";
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = DBConnection();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = query;

                    objCmd.Parameters.Add("@cusid", SqlDbType.VarChar, 50).Value = obj.cusid;
                    objCmd.Parameters.Add("@Issue", SqlDbType.VarChar, 50).Value = obj.Issue;
                    objCmd.Parameters.Add("@descriptiontext", SqlDbType.VarChar, 50).Value = obj.descriptiontext;
                    objCmd.Parameters.Add("@compstatus", SqlDbType.VarChar, 50).Value = obj.compstatus;
                    objCmd.Parameters.Add("@posteddate", SqlDbType.DateTime, 50).Value = obj.posteddate;
                    IsInsert = objCmd.ExecuteNonQuery() > 0 ? true : false;
                    Conn.Close();
                    return IsInsert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDetails> UpdateProfile(string cusid)
        {
            List<UserDetails> lst = new List<UserDetails>();
            DataSet ds = new DataSet();
            query = "Select CName ,	CPhone,	Cemail,	Password from Customer_details where CId="+cusid;
            ds = SQLCommand(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lst = ds.Tables[0].AsEnumerable()
              .Select(row => new UserDetails
              {
                  CName = row.Field<string>(0),
                  CPhone  = row.Field<string>(1),
                  Cemail = row.Field<string>(2)
              }).ToList();
            }
            return lst;
        }
    }
}
