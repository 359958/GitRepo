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
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace WebAppMovie.Controllers
{
    public class AdminController : Controller
    {
        string webapiurl = System.Configuration.ConfigurationManager.AppSettings["WebapiUrl"].ToString();
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
        public ActionResult AddMovie2(FormCollection form)
        {
            string Screen = form["Screen"];
            string Movie = form["Movie"];
            string From = form["From"];
            string RunningUpto = form["RunningUpto"];
            string filePath = string.Empty;
            string url = string.Empty;
            if (Request.Files.Count > 0)
            {
                int i = 0;
                CloudStorageAccount cloudStorageAccount = DropDown.GetConnectionString();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(ConfigurationManager.AppSettings["ContainerName"]);
                cloudBlobContainer.CreateIfNotExists();
                foreach (string files in Request.Files)
                {
                    string fileName = Path.GetFileName(Request.Files[i].FileName);
                    CloudBlockBlob azureBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    azureBlockBlob.UploadFromStream(Request.Files[i].InputStream);

                    //var postedFile = Request.Files[file];
                    //filePath = Server.MapPath("~/Content/images/" + postedFile.FileName);
                    //postedFile.SaveAs(filePath);
                    i++;
                    url = azureBlockBlob.Uri.ToString();

                }
            }
            //int str= filePath.IndexOf("Content");
            //int str1 = filePath.Length;
            string s = @"\Content\images\EN.jpg";//filePath.Substring(str, str1-str);
            movieDetailsList obj = new movieDetailsList();
            obj.Screen = Screen;
            obj.Movie = Movie;
            obj.From = Convert.ToDateTime(From);
            obj.RunningUpto = Convert.ToDateTime(RunningUpto);
            obj.path = url;
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
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Admin/AddMovie", contentData).Result;
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
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Admin/UpdatePrice", contentData).Result;
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
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Admin/UpdatePrice", contentData).Result;
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
            response = httpClient.GetAsync(webapiurl + "/admin/GetMovieRunning").Result;
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
            response = httpClient.GetAsync(webapiurl + "/admin/GetComplients").Result;
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
