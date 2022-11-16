using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using taskodc.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace taskodc.Controllers
{
    public class DefaultController : Controller
    {
        //https://localhost:44301/api/Users/
        // GET: Default

        [HttpPost]
        public ActionResult Register(User newuser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/api/Users/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("student", newuser);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(newuser);
        }

        [HttpGet]
        public ActionResult Index()
        {
            {
                IEnumerable<User> members = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44301/api/Users/");

                    var responseTask = client.GetAsync("member");
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<User>>();
                        readTask.Wait();

                        members = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        members = Enumerable.Empty<User>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return View(members);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateProfile(User student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new ODCEntities1())
            {
                var existingStudent = ctx.Users.Where(s => s.Id == student.Id).FirstOrDefault<User>();

                if (existingStudent != null)
                {
                    existingStudent.firstName = student.firstName;
                    existingStudent.lastName = student.lastName;

                    ctx.SaveChanges();
                }
                else
                {
                    return (IHttpActionResult)HttpNotFound();
                }
            }
            return Ok();
        }

        private IHttpActionResult Ok()
        {
            throw new NotImplementedException();
        }

        private IHttpActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}