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
        string webapiurl = System.Configuration.ConfigurationManager.AppSettings["WebapiUrl"].ToString();
        // GET: BookMovie
        string userid = System.Web.HttpContext.Current.Session["CID"].ToString();
        [HttpGet]
        public ActionResult BookMovie2(string Id)

        {
            //ViewBag.Movie = BooKMovieDetails();
            var chk = GetMovieDate(Id);
            ViewBag.Date= chk.Distinct().Select(i => new SelectListItem() { Text = i.AllDays.ToString(), Value = i.AllDays.ToString() }).ToList();
            var outp = BooKMovieDetails();
            var linq = outp.Where(x => x.MovieID == Id).Select(x => new { x.MovieID, x.MovieName }).ToList();
            MovieDetails obj = new MovieDetails();
            foreach (var item in linq)
            {
                obj.MovieID = item.MovieID;
                obj.MovieName = item.MovieName;
            }
            
            
            ViewBag.Classid = DropDown.Classid();
            ViewBag.Showid = DropDown.Showid();
            ViewBag.NoTickets = DropDown.NoTickets();
            return PartialView("DeletePart", obj);
        }

        public ActionResult test()
        {
            return View();
        }

        public List<Showdates> GetMovieDate(string MovieID)
        {
            
            var list = new List<BookMovieTicket>();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync(webapiurl + "/BookMovie/GetMovieDate?MovieId=" + MovieID).Result;
            response.EnsureSuccessStatusCode();
            List<Showdates> stateList = response.Content.ReadAsAsync<List<Showdates>>().Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    using (HttpContent content = response.Content)
            //    {
            //        string jsonS = response.Content.ReadAsStringAsync().Result;
            //        var ds = JsonConvert.DeserializeObject(jsonS);
            //        return ds;
            //    }
            //}

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
            response = httpClient.GetAsync(webapiurl + "/BookMovie/MyBooking?cusid=" + uid.ToString()).Result;
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
                HttpResponseMessage response = client.PostAsync(webapiurl + "/BookMovie/BookTicket", contentData).Result;
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
            response = httpClient.GetAsync(webapiurl + "/BookMovie/Get?bookid=" + bookid.ToString()).Result;
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
            response = httpClient.GetAsync(webapiurl + "/Register/GetAllMovie").Result;
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

        public List<MovieDetailsdelete> BooKMovieDetailsTODelete()
        {
            var list = new List<MovieDetails>();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync(webapiurl + "/Register/GetAllMovie").Result;
            response.EnsureSuccessStatusCode();
            List<MovieDetailsdelete> stateList = response.Content.ReadAsAsync<List<MovieDetailsdelete>>().Result;
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

        public ActionResult View1()
        {
            var obj = BooKMovieDetailsTODelete();
            return View(obj);
        }
        public ActionResult View2()
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
                HttpResponseMessage response = client.PostAsync(webapiurl + "/BookMovie/" + view, contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }
    }
}
