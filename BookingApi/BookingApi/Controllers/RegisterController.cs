using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccessLayer;

namespace BookingApi.Controllers
{
    public class RegisterController : ApiController
    {
        MovieCustReg objReg = new MovieCustReg();

        // GET: api/Register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Register
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        public HttpResponseMessage loginUser(LoginDetails lObj)
        {
            bool status = true;

            var status1 = objReg.Login(lObj);
            if (status == true)
            {
                var showmessage = "Product Saved Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, status1);

            }
            else
            {
                var showmessage = "Product Not Saved Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }
        [HttpPost]
        public HttpResponseMessage createUser(UserDetails uObj)
        {
            bool status = false;
            status= objReg.createUser(uObj);
            if (status == true)
            {
                var showmessage = "Product Saved Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, showmessage);

            }
            else
            {
                var showmessage = "Product Not Saved Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateUser(UserDetails uObj)
        {
            bool status = false;
            status = objReg.updateUser(uObj);
            if (status == true)
            {
                var showmessage = "Update Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, showmessage);

            }
            else
            {
                var showmessage = "Update fail Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }



        [HttpGet, ActionName("GetAllMovie")]
        public HttpResponseMessage GetAllMovie()
        {
          
            HttpResponseMessage response;
            try
            {
                var movieResponse = objReg.MovieRunning();
                if (movieResponse != null)
                    response = Request.CreateResponse<List<movieDetails>>(HttpStatusCode.OK, movieResponse);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }

        


        // DELETE: api/Register/5
        public void Delete(int id)
        {
        }

        [HttpGet, ActionName("GetUserProfile")]
        public HttpResponseMessage GetUserProfile(string cusid)
        {

            HttpResponseMessage response;
            try
            {
                var movieResponse = objReg.UpdateProfile(cusid);
                if (movieResponse != null)
                    response = Request.CreateResponse<List<UserDetails>>(HttpStatusCode.OK, movieResponse);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }
    }
}
