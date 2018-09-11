using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMovie.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAppMovie.Controllers
{
    public class BookMovieController : Controller
    {
        // GET: BookMovie
        string userid = System.Web.HttpContext.Current.Session["CID"].ToString();
        public ActionResult BookMovie2()
        {
            ViewBag.Movie = BooKMovieDetails();
            ViewBag.Classid = DropDown.Classid();
            ViewBag.Showid = DropDown.Showid();
            ViewBag.NoTickets = DropDown.NoTickets();
            return View();
        }

        public ActionResult Mybookings()
        {
            var output = MyTickets(userid);
            return View(output);
        }

        public List<BookMovieTicket> MyTickets(string uid)
        {
            
            var list = new List<BookMovieTicket>();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:60683/api" + "/BookMovie/MyBooking?cusid=" + uid.ToString()).Result;
            response.EnsureSuccessStatusCode();
            List<BookMovieTicket> stateList = response.Content.ReadAsAsync<List<BookMovieTicket>>().Result;
            if (!object.Equals(stateList, null))
            {
                var states = stateList.ToList();
                return states;
            }
            else
            {
                return null;
            }
        }
        public ActionResult BooKMovie()
        {
            ViewBag.Movie = BooKMovieDetails();
            ViewBag.Classid = DropDown.Classid();
            ViewBag.Showid = DropDown.Showid();
            ViewBag.NoTickets = DropDown.NoTickets();
            //BookMovie objcollege = new CollegeDetails();
            //if (Request.HttpMethod != "POST")
            //{
            //    ViewBag.Movie = BooKMovieDetails();
            //}
            return View();
        }

        [HttpPost]
        public ActionResult BooKMovie2(BookMovie obj)
        {
            if (Session["CID"].ToString() is null)
            {
                Session["CID"] = "1015";
                Session["UserID"] = "Guest";
            }
            obj.CUSTOMERID = Convert.ToInt32(Session["CID"].ToString());
            string json = BookMovieTic(obj);

            //var objs = JObject.Parse(json);
            var cc = JsonConvert.DeserializeObject<IEnumerable<BookingID>>(json);

            //TempData["bookid"] = objs["id"];
            foreach (var item in cc)
            {
                int bookid = 1044;//Convert.ToInt32(item.BookId);
                TempData["bookid"] = bookid;
                if (bookid > 0)
                {
                    return RedirectToAction("Mybookings", "BookMovie");
                }
                else
                {
                    ViewBag.Message = "HouseFull";
                    ViewBag.Movie = BooKMovieDetails();
                    ViewBag.Classid = DropDown.Classid();
                    ViewBag.Showid = DropDown.Showid();
                    ViewBag.NoTickets = DropDown.NoTickets();
                }
                
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult BooKMovie(BookMovie obj)
        {
            if (Session["CID"].ToString()=="")
            {
                Session["CID"] = "1015";
                Session["UserID"] = "Guest";
            }
            obj.CUSTOMERID = Convert.ToInt32(Session["CID"].ToString());
           string json= BookMovieTic(obj);
            var cc = JsonConvert.DeserializeObject<IEnumerable<BookingID>>(json);
            foreach (var item in cc)
            {
                int bookid = Convert.ToInt32(item.BookId);
                TempData["bookid"] = bookid;
                return RedirectToAction("Ticket", "BookMovie");
            }
            return View();
        }

        public string BookMovieTic(BookMovie obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:60683/api" + "/BookMovie/BookTicket", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }

        public ActionResult ViewPage()
        {
            return View();
        }

        public ActionResult Ticket()
        {
            var output = TicketDetails("1020");
            return View(output);
        }

        [HttpGet]
        public ActionResult MyTicketDetails(string Id)
        {
            var output = TicketDetails(Id);
            return PartialView("TicketDetails", output);
        }

            public List<BookMovieTicket> TicketDetails(string bookingid)
        {
            int bookid = Convert.ToInt32(bookingid);
            var list = new List<BookMovieTicket>();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:60683/api" + "/BookMovie/Get?bookid=" + bookid.ToString()).Result;
            response.EnsureSuccessStatusCode();
            List<BookMovieTicket> stateList = response.Content.ReadAsAsync<List<BookMovieTicket>>().Result;
            if (!object.Equals(stateList, null))
            {
                var states = stateList.ToList();
                return states;
            }
            else
            {
                return null;
            }
        }
        public List<MovieDetails> BooKMovieDetails()
        {
            var list = new List<MovieDetails>();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:60683/api" + "/Register/GetAllMovie").Result;
            response.EnsureSuccessStatusCode();
            List<MovieDetails> stateList = response.Content.ReadAsAsync<List<MovieDetails>>().Result;
            if (!object.Equals(stateList, null))
            {
                var states = stateList.ToList();
                return states;
            }
            else
            {
                return null;
            }
        }

        // GET: BookMovie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookMovie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookMovie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookMovie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookMovie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookMovie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookMovie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult TicketsAvaliblity()
        {
            return View();
        }

        public ActionResult Complient()
        {
            ViewBag.IssueList = DropDown.IssueList();

            return View();
        }
        [HttpPost]
        public ActionResult Complient(ComplientStatus obj)
        {
            obj.cusid = 26; //Session["UserID"];
            string val = RaiseComplient(obj);

            if (val.Contains("Successfully"))
            {
                ViewBag.Message = "Successfully";
            }
            ViewBag.IssueList = DropDown.IssueList();

            return View();
        }
        public string RaiseComplient(ComplientStatus obj)
        {
            return Httpclientcall(obj, "Complient");
        }

        public string Httpclientcall(object obj, string view)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:60683/api" + "/BookMovie/" + view, contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }
    }
}
