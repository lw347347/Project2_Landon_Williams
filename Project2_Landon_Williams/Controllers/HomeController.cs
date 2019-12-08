using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2_Landon_Williams.Controllers
{
    public class HomeController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Login
        [HttpPost]
        public ActionResult Index(User user)
        {
            // Check if the user exists
            List<User> users = db.Users.ToList<User>();
            users = db.Users.Where(x => x.UserEmail == user.UserEmail).ToList<User>();

            if (users.Count() < 1)
            {
                // The user doesn't exist
                return View(user);
            } else
            {
                // The user DOES exist
                // Check the password now
                if (users[0].Password == user.Password)
                {
                    // The password is correct so send them to the right view
                    FormsAuthentication.SetAuthCookie(user.UserEmail, true);

                    return RedirectToAction("Index", "MissionQuestions", null);

                } else
                {
                    // The password is incorrect
                    return View(user);
                }
            }
        }
    }
}