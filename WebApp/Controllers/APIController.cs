using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DBHelper;
using WebApp.Models.AccountViewModels;

namespace WebApp.Controllers
{
    [RoutePrefix("api")]
    public class ApiController : Controller
    {
        private readonly IDbHelper _db;

        /* HELPER FUNCTIONS */
        // Jsonify takes a string and packages it as a JSON object under the "content" key.
        private JsonResult Jsonify(string content) => Json($"{{ content: '{content}' }}");

        // BadRequest takes a string or JSON object and returns it along with a 500 (BadRequest) status code
        private ActionResult BadRequest(string content) => BadRequest(Jsonify(content));
        private ActionResult BadRequest(JsonResult content) =>
            new HttpStatusCodeResult(HttpStatusCode.BadRequest, content.Data.ToString());

        // Ok takes a string or JSON object and returns it along with a 200 (OK) status code
        private ActionResult Ok(string content) => Ok(Jsonify(content));
        private ActionResult Ok(JsonResult content) => 
            new HttpStatusCodeResult(HttpStatusCode.OK, content.Data.ToString());

        // CTOR receives the DbHelper through Dependency Injection
        public ApiController(IDbHelper db) => _db = db;

        // GET: api/
        // Test function that returns "hello world!" when you navigate to the /api URI
        public string Index()
        {
            return "hello world!";
        }

        // POST: api/user/register
        // Create new user in db >> return a TOKEN
        [HttpPost, Route("user/register")]
        public ActionResult RegisterUser(RegistrationViewModel model)
        {
            var token = "";
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest("Invalid registration model");

            if (_db.CreateNewUser(model.FirstName, model.LastName, model.Phone, model.Email, model.Password)) {
                token = _db.LoginAndGetToken(model.Email, model.Password);
            }
            else {
                return BadRequest("User already exist.");
            }

            return token != null ? Json(new { content = token }) : BadRequest("Registration failed");
        }

        // POST: api/login 
        // Authenticate user >> return a TOKEN
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid login model");

            var token = _db.LoginAndGetToken(model.Email, model.Password);

            return token != null ? Json(new {content=token}) : BadRequest("Invalid login");
        }

        // DELETE: api/user/delete/id 
        // Delete a user from db with ID >> Return OK
        [HttpDelete, Route("user/delete/{id}")]
        public ActionResult DeleteUser(int id)
        {
            var users = _db.GetAllUsers();
            var user = users?.FirstOrDefault(u => u.Id.Equals(id));
            if (user == null) return BadRequest("Could not find user " + id);

            var result = _db.DeleteUser(user.Email);

            return result ? Ok("User deleted") : BadRequest("Error deleting user " + id);
        }

        // GET: api/users/all
        // INSECURE! >> return User list  
        [HttpGet, Route("users/all")]
        public JsonResult GetAllUsers()
        {
            return Json(_db.GetAllUsers(), JsonRequestBehavior.AllowGet);
        }

        // PUT: api/user/update/pwd 
        // User update their password >> Return OK
        [HttpPut, Route("user/update/pwd")]
        public ActionResult UpdateUserPassword(String inToken)
        {
            var users = _db.GetAllUsers();
            var user = users?.FirstOrDefault(u => u.Id.Equals(inToken));

            if (user == null){
                return BadRequest("Update failed");
            }
            else{
                //update password
            }

            return Ok("Completed");
        }

    }
}