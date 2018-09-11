using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApi.Controllers
{
    public class BookMovieController : ApiController
    {
        MovieCustReg objReg = new MovieCustReg();
        [HttpPost,ActionName("BookTicket")]
        public HttpResponseMessage BookTicket(BookMovie uObj)
        {
            int status = 0;
         var dt= objReg.BookMovie(uObj);
            if (dt.Rows.Count >0 )
            {
                var showmessage = "Product Saved Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, dt);

            }
            else
            {
                var showmessage = "Product Not Saved Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }

        [HttpGet]
        public HttpResponseMessage Get(string bookid)
        {
            HttpResponseMessage response;
            try
            {
                var movieResponse = objReg.PrintTicket(bookid);
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

        [HttpGet,ActionName("MyBooking")]
        public HttpResponseMessage GetMyTickets(string cusid)
        {
            HttpResponseMessage response;
            try
            {
                var movieResponse = objReg.MyPrintTicket(cusid);
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

        [HttpGet]
        public HttpResponseMessage GetMovieDate(string MovieId)
        {
            HttpResponseMessage response;
            try
            {
                var movieDate = objReg.Loaddate(MovieId);
                if (movieDate != null)
                    response = Request.CreateResponse(HttpStatusCode.OK, movieDate);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }




        [HttpGet, ActionName("TicketsAval")]
        public HttpResponseMessage CheckAvaliblity(string showdate)
        {
            HttpResponseMessage response;
            try
            {
                var ticketResponse = objReg.TicketsAvliblity(showdate);
                if (ticketResponse != null)
                    response = Request.CreateResponse(HttpStatusCode.OK, ticketResponse);
                else

                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return response;
        }

        [HttpPost, ActionName("Complient")]
        public HttpResponseMessage PostComplient(ComplientStatus uObj)
        {
            bool status = false;
            status = objReg.postcomplient(uObj);
            if (status == true)
            {
                var showmessage = "Complient Saved Successfully.";

                return Request.CreateResponse(HttpStatusCode.OK, showmessage);

            }
            else
            {
                var showmessage = "OOPS!!!..Failed,Please try again.";

                return Request.CreateResponse(HttpStatusCode.BadRequest, showmessage);

            }
        }



    }
}
