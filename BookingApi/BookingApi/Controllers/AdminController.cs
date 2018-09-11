using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;

namespace BookingApi.Controllers
{
    public class AdminController : ApiController
    {
        // GET: api/Admin
        MovieCustReg objReg = new MovieCustReg();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Admin/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admin
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Admin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Admin/5
        public void Delete(int id)
        {
        }

        [HttpGet, ActionName("GetMovieRunning")]
        public HttpResponseMessage GetMovieRunning()
        {
            HttpResponseMessage response;
            try
            {
                var movieResponse = objReg.AdminMovieRunning();
                if (movieResponse != null)
                    response = Request.CreateResponse(HttpStatusCode.OK, movieResponse);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }

        [HttpGet, ActionName("GetComplients")]
        public HttpResponseMessage GetComplients()
        {
            HttpResponseMessage response;
            try
            {
                var complientResponse = objReg.AdminComplients();
                if (complientResponse != null)
                    response = Request.CreateResponse(HttpStatusCode.OK, complientResponse);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }

        [HttpPost, ActionName("AddMovie")]
        public HttpResponseMessage AddMovie(movieDetailsList obj)
        {
            bool status = false;
            HttpResponseMessage response;
            try
            {
                status= objReg.AdminAddMovie(obj);
                if (status == true)
                {
                    var showmessage = "Movie Saved Successfully.";

                    return Request.CreateResponse(HttpStatusCode.OK, showmessage);

                }
                else
                {

                    var showmessage = "Movie Not Saved Please try again.";

                    return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, ActionName("UpdateMovie")]
        public HttpResponseMessage UpdateMovie(movieDetailsList obj)
        {
            bool status = false;
            HttpResponseMessage response;
            try
            {
                status = objReg.AdminUpdateMovie(obj);
                if (status == true)
                {
                    var showmessage = "Movie Updated Successfully.";

                    return Request.CreateResponse(HttpStatusCode.OK, showmessage);

                }
                else
                {

                    var showmessage = "Movie Not updated Please try again.";

                    return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage Updateseat(AddSeats obj)
        {
            bool status = false;
            status = objReg.updateSeat(obj);
            if (status == true)
            {
                var showmessage = "Updated Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, showmessage);

            }
            else
            {
                var showmessage = "Fail Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }


        [HttpPost]
        public HttpResponseMessage UpdatePrice(ChangePrice obj)
        {
            bool status = false;
            status = objReg.updateAmount(obj);
            if (status == true)
            {
                var showmessage = "Updated Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, showmessage);

            }
            else
            {
                var showmessage = "Fail Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }
    }
}
