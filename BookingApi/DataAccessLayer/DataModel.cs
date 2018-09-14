using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDetails
    {
        public int CID { get; set; }
        public string CName { get; set; }

        public string CPhone { get; set; }

        public string Cemail { get; set; }

        public string Password { get; set; }
    }

    public class LoginDetails
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class movieDetails
    {
        public string MovieID { get; set; }

        public string MovieName { get; set; }
    }

    public class MovieDetailsdelete
    {
        public string MovieName { get; set; }
        public string MovieId { get; set; }
        public string ImagePath { get; set; }
        public int FC { get; set; }
        public int SC { get; set; }
        public int TC { get; set; }
        public DateTime Upto { get; set; }
    }

    public class movieDetailsList
    {
        public string Screen { get; set; }
        public string Movie { get; set; }
        public DateTime RunningUpto { get; set; }
        public DateTime From { get; set; }

        public string path { get; set; }
    }

    public class BookMovie
    {
        public string Movieid { get; set; }
        public string Classid { get; set; }
        public string Showid { get; set; }
        public DateTime BookingDate { get; set; }
        public int NoTickets { get; set; }
        public int CUSTOMERID { get; set; }

    }

    public class BookMovieTicket
    {
        public int Movieid { get; set; }
        public string MovieName { get; set; }
        public string ShowDate { get; set; }
        public string ScreenName { get; set; }
        public string SeatDetails { get; set; }
        public int Tickets { get; set; }
        public int Total { get; set; }

    }

    public class ComplientStatus
    {
        public int cusid { get; set; }
        public string Issue { get; set; }
        public string descriptiontext { get; set; }

        public string compstatus { get; set; }

        public string posteddate { get; set; }
    }

    public class AddSeats
    {
        public string ScreenId { get; set; }
        public string Classid { get; set; }
        public int NoofTickets { get; set; }

    }

    public class ChangePrice
    {
        public string Classid { get; set; }
        public int Amount { get; set; }

    }


}
