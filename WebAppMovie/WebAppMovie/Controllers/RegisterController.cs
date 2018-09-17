using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers
{
    public class RegisterController : Controller
    {

        string webapiurl = System.Configuration.ConfigurationManager.AppSettings["WebapiUrl"].ToString();
        // GET: Register
        string JsonData = string.Empty;
        public ActionResult Home()
        {
            return View();
        }
    
      

        public ActionResult CreateUser2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser2(UserData obj)
        {
            var sus = AddUserDetails(obj);
            if (sus.Contains("Successfully"))
            {
                ViewBag.Message = "Successfully";
            }
            else if (sus.Contains("Exist"))
            {
                ViewBag.Message = "Email Id Exist already";
            }
            return View();
       
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserData obj)
        {
           var sus = AddUserDetails(obj);
            if (sus.Contains("Successfully"))
            {
                ViewBag.Message = "Successfully";
            }
            return View();
        }
        public string AddUserDetails(UserData obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Register/createUser", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
                
            }
        }

        // GET: Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(UsersLogin obj)
        {
            string cook = string.Empty;
            JsonData=CheckDetails(obj);
            var UserData = JsonConvert.DeserializeObject<IEnumerable<UserData>>(JsonData);
            foreach (var item in UserData)
            {
                Session["UserID"] = item.CName.ToString();
                Session["CID"] = item.CID.ToString();
                cook = item.Cemail.ToString();

            }

            if (JsonData.Length > 0)
            {

                FormsAuthentication.SetAuthCookie(cook, false);
                return RedirectToAction("view1", "BookMovie");
            }
            else
                return View();
            return View();
        }
        // GET: Register/Create
        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(UsersLogin obj)
        {
            string cook = string.Empty;
            string s = CheckDetails(obj);
              var cc = JsonConvert.DeserializeObject<IEnumerable<UserData>>(s);

            foreach (var item in cc)
            {
                Session["UserID"] = item.CName.ToString();
                Session["CID"] = item.CID.ToString();
                cook = item.Cemail.ToString();

            }
          
            if (s.Length > 0)
            {
                
                FormsAuthentication.SetAuthCookie(cook, false);
                return RedirectToAction("BookMovie", "BookMovie");
            }
            else
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = "";
            Session["CID"] = "";
            return RedirectToAction("LoginPage", "Register");
        }

        public string Httpclientcall(object obj,string view)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Register/"+view, contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;
            }
        }
        public string CheckDetails(UsersLogin obj)
        {

           return Httpclientcall(obj, "loginUser");
            //using (HttpClient client = new HttpClient())
            //{
            //    string stringData = JsonConvert.SerializeObject(obj);
            //    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            //    HttpResponseMessage response = client.PostAsync(webapiurl + "/Register/loginUser", contentData).Result;
            // var list  = response.Content.ReadAsStringAsync().Result;
            //    return list;
            //}

            
        }

        [HttpPost]
        public ActionResult Signup()
        {
            return RedirectToAction("CreateUser", "Register");
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult summa(FormCollection collection)
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

        // GET: Register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Register/Edit/5
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

        // GET: Register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Register/Delete/5
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


       

        public string RaiseComplient(ComplientStatus obj)
        {
            return Httpclientcall(obj, "PostComplient");
        }

        public ActionResult EditUserProfile()
        {
            List<UserDetails> obj = updateuserProfile();
            UserData edit = new UserData();

            edit.CName = obj[0].CName;
            edit.CPhone = obj[0].CPhone;
               
            return View(edit);
        }

        [HttpPost]
        public ActionResult EditUserProfile(UserData obj)
        {
           var sus =  UpdateUserDetails(obj);
            if (sus.Contains("Successfully"))
            {
                ViewBag.Message = "Profile Created";
            }
            else if(sus.Contains("Exist"))
            {
                ViewBag.Message = "Email Id Exist already";
            }
            return View();
        }

        public string UpdateUserDetails(UserData obj)
        {
            obj.CID = int.Parse(Session["CID"].ToString());
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(webapiurl + "/Register/UpdateUser", contentData).Result;
                var list = response.Content.ReadAsStringAsync().Result;
                return list;

            }
        }
        public List<UserDetails> updateuserProfile()
        {
            string userid = Session["CID"].ToString();
           

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync(webapiurl + "/Register/GetUserProfile?cusid=" + userid).Result;
            response.EnsureSuccessStatusCode();
            List<UserDetails> stateList = response.Content.ReadAsAsync<List<UserDetails>>().Result;

            if (stateList!=null)
            {
                return stateList;
            }
            else
            {
                return null;
            }
        }
    }
}
