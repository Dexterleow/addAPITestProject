using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebServicesApplication.Models;

namespace WebServicesApplication.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration

        string Baseurl = "https://peter-htet.outsystemscloud.com/ITDInterviews/";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                    //var myUserRegistration = JsonConvert.SerializeObject(userRegistration);
                    userRegistration.Id = 0;
                    JsonContent content = JsonContent.Create(userRegistration);
                    HttpResponseMessage Res = await client.PostAsync("rest/Users/AddUpdateUser", content);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var newUser = Res.Content.ReadAsStringAsync().Result;
                    }
                }

                return RedirectToAction("Index", "User");
            }

            return View();
        }



    }
}