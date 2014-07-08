using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResourceGuru;
using ResourceGuru.Models;

namespace TestSpace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ResourceGuruClient client = new ResourceGuruClient("aeedb2885b18f6f44fad13c6174afb1eda2bed0796bd57569ee87cade6dfd744", "7496e3cb663b317b6e2f39568d3ccb38ae8e655782da2ec1396c92a4e1143003");
            //var redirectUri = client.GetAuthorizeUrl("localhost:300");
           // Response.Redirect(redirectUri);

            //client.AuthenticateWithAuthorizationCode("a43163c09bfbefca6169e9757a71d049be99f4ee23e1d978e5a0a718643d8516","https://localhost");
            client.AuthenticateWithPassword("avh@phases.dk", "PhasesRocks123");

            List<Booking> bookings = client.BookingService.GetBookings(subdomain: "phases3");
            var clients = client.ClientService.GetClients("phases3");

            var n = clients.First().Name;
            //ClientDetail clientDetails = client.ClientService.GetClient(subdomain: "my-org", clientId: 123);
            //string clientName = clientDetails.Name;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

          
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}