using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebAppMovie.Models
{
    public class DropDown
    {

        public static IEnumerable<SelectListItem> Classid()
        {
            List<SelectListItem> Classid = new List<SelectListItem>() {
                new SelectListItem(){ Text="First Class",Value="C1"},
                new SelectListItem(){ Text="Second Class",Value="C2"},
                new SelectListItem(){ Text="Third Class",Value="C3"},
            };

            return Classid.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> Showid()
        {
            List<SelectListItem> Showid = new List<SelectListItem>() {
                new SelectListItem(){ Text="11:30",Value="S1"},
                new SelectListItem(){ Text="2:30",Value="S2"},
                new SelectListItem(){ Text="6:30",Value="S3"},
                 new SelectListItem(){ Text="10:30",Value="S4"}
            };

            return Showid.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> NoTickets()
        {
            List<SelectListItem> NoTickets = new List<SelectListItem>() {
                new SelectListItem(){ Text="1",Value="1"},
                new SelectListItem(){ Text="2",Value="2"},
                new SelectListItem(){ Text="3",Value="3"},
                 new SelectListItem(){ Text="4",Value="4"},
                  new SelectListItem(){ Text="6",Value="5"},
                  new SelectListItem(){ Text="7",Value="6"}
            };

            return NoTickets.AsEnumerable();
        }

     

        public static IEnumerable<SelectListItem> ScreenList()
        {
            List<SelectListItem> ScreenList = new List<SelectListItem>() {
                new SelectListItem(){ Text="Screen 1",Value="S1"},
                new SelectListItem(){ Text="Screen 2",Value="S2"},
                new SelectListItem(){ Text="Screen 3",Value="S3"},
                new SelectListItem(){ Text="Screen 4",Value="S4"}

            };

            return ScreenList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> IssueList()
        {
            List<SelectListItem> IssueList = new List<SelectListItem>() {
                new SelectListItem(){ Text="Paymet Related",Value="I1"},
                new SelectListItem(){ Text="Show Related",Value="I2"},
                new SelectListItem(){ Text="Hospitality",Value="I3"},
                 new SelectListItem(){ Text="Other type",Value="I4"}

            };

            return IssueList.AsEnumerable();
        }

        public static CloudStorageAccount GetConnectionString()
        {
            string accountname = ConfigurationManager.AppSettings["accountName"];
            string key = ConfigurationManager.AppSettings["key"];
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", accountname, key);
            return CloudStorageAccount.Parse(connectionString);
        }

    }


}