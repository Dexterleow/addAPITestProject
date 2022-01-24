using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebServicesApplication.Models;

namespace WebServicesApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //Hosted web API REST Service base url
        string Baseurl = "https://peter-htet.outsystemscloud.com/ITDInterviews/";
        public async Task<ActionResult> Index()
        {
            List<UserRegistration> EmpInfo = new List<UserRegistration>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("rest/Users/GetAllUsers");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list

                    EmpInfo = JsonConvert.DeserializeObject<List<UserRegistration>>(EmpResponse);
                }
                //returning the employee list to view
                return View(EmpInfo);

            }
        }


        public async Task<ActionResult> Delete(string id)
        {

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("rest/Users/DeleteUser?id=" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var deleteUser = Res.Content.ReadAsStringAsync().Result;

                }
            }

            return RedirectToAction("Index");
        }








    }
}