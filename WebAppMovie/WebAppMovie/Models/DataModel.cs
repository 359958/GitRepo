using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppMovie.Models
{
    [Serializable]
    public class UserData
    {
        public int CID { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name ="Name")]
        public string CName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Phone Number")]
        public string CPhone { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Emailid")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Cemail { get; set; }
        [Required]
        [StringLength(15,MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UsersLogin
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    [Serializable]
    public class BookingID
    {
        public int BookId { get; set; }
    }

    public class MovieDetails
    {
        public string MovieName { get; set; }

        public string MovieID { get; set; }
    }

    public class MovieDetailsdelete
    {
        public string MovieName { get; set; }
        public string MovieId { get; set; }
        public string ImagePath { get; set; }
        public string FC { get; set; }
        public string SC { get; set; }
        public string TC { get; set; }
        public string Upto { get; set; }
    }

    public class Showdates
    {
        public DateTime AllDays { get; set; }
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
        public string BookingDate { get; set; }
        public int NoTickets { get; set; }
        public int CUSTOMERID { get; set; }
       
    }

    public class BookMovieTicket
    {

        public int BookinID { get; set; }
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
    public class UserDetails
    {
        public int CID { get; set; }
        public string CName { get; set; }

        public string CPhone { get; set; }

        public string Cemail { get; set; }

        public string Password { get; set; }
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

  

         public class ComplientList
        {
            public string cusName { get; set; }
            public string Issue { get; set; }
            public string descriptiontext { get; set; }
            public string compstatus { get; set; }
            public string posteddate { get; set; }

        }

    public class TicketAvaliblity
    {
        public string Screen { get; set; }
        public string ClassType { get; set; }
        public int s1 { get; set; }
        public int s2 { get; set; }
        public int s3 { get; set; }
        public int s4 { get; set; }

    }

    

}