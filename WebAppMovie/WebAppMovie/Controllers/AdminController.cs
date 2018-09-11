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

namespace WebAppMovie.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var output = AdminMovieDetails();
            return View(output);
        }

        public ActionResult Complient()
        {
            var output = AdminComplientsDetails();
            return View(output);
        }
        public ActionResult AddMovie()
        {
            return View();
        }


        public ActionResult AddMovie2()
        {
            ViewBag.ScreenList = DropDown.ScreenList();
            return View();
        }
        
        [HttpPost]
        public ActionResult AddMovie2(movieDetailsList obj)
        {
            var res = AddMovieindb(obj);
            if (res.Contains("Successfully"))
            {
                ViewBag.Message = res;
            }
            ViewBag.ScreenList = DropDown.ScreenList();
            return View();
        }
        [HttpGet]
        public ActionResult AdminConsole(string type) //ChangePrice
        {
            if (type== "AddTicket")
            {
                ViewBag.Screen = DropDown.ScreenList();
            ViewBag.Class = DropDown.Classid();
                return PartialView("AddSeat");
            }
            else if (type== "ChangePrice")
            {
                ViewBag.Class = DropDown.Classid();
                return PartialView("ChangeAmount");
            }

            return View();
        }


        [HttpPost]
        public ActionResult AddSeats(AddSeats obj)
        {
            var res = AddSeatindb(obj);
            if (res.Contains("Successfully"))
            {
                ViewBag.Message = res;
                ViewBag.Screen = DropDown.ScreenList();
                ViewBag.Class = DropDown.Classid();
            }
            return View("Adminconsole");
        }

        public ActionResult ChangePrice(ChangePrice obj)
        {
            var res = ChangePriceindb(obj);
            if (res.Contains("Successfully"))
            {
                ViewBag.Message = res;
                ViewBag.Screen = DropDown.ScreenList();
                ViewBag.Class = DropDown.Classid();
            }
            return View("Adminconsole");
        }
        public string AddMovieindb(movieDetailsList obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:60683/api" + "/Admin/AddMovie", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }

        public string AddSeatindb(AddSeats obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:60683/api" + "/Admin/UpdatePrice", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }

        public string ChangePriceindb(ChangePrice obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:60683/api" + "/Admin/UpdatePrice", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }

        public List<movieDetailsList> AdminMovieDetails()
        {
            var list = new List<movieDetailsList>();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:60683/api" + "/admin/GetMovieRunning").Result;
            response.EnsureSuccessStatusCode();
            List<movieDetailsList> stateList = response.Content.ReadAsAsync<List<movieDetailsList>>().Result;
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

        public List<ComplientList> AdminComplientsDetails()
        {
            var list = new List<ComplientList>();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:60683/api" + "/admin/GetComplients").Result;
            response.EnsureSuccessStatusCode();
            List<ComplientList> stateList = response.Content.ReadAsAsync<List<ComplientList>>().Result;
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

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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


        public ActionResult EditSeat()
        {
            return View();
        }
    }
}
