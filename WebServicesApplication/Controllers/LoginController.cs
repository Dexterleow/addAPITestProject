using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebServicesApplication.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebServicesApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
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

                    HttpResponseMessage Res = await client.GetAsync("rest/Users/GetUser?name=" + userRegistration.name);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var UserDetails = Res.Content.ReadAsStringAsync().Result;
                        var myUserDetails = JsonSerializer.Deserialize<UserRegistration>(UserDetails);


                        if (userRegistration.password == myUserDetails.password)
                        {
                            return RedirectToAction("Index", "User");
                        }

                    }
                }

                return View();

            }

            return View();
        }
    }
}