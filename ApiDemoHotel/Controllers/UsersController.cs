using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiDemoHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly User15Context db;
        public UsersController(User15Context db)
        {
            this.db = db;
        }
        //get users
        //get user by login password
        //add user 
        //edit user (for users - only password; for admin all info + block or not)
        [HttpPost("GetUsers")]
        public async Task<List<User>> GetUsers()
        {
            await Task.Delay(100);
            foreach (var user in db.Users)
            {
                if (user.LastLogInDate != null 
                    &&(DateTime.Now-user.LastLogInDate.Value).TotalDays >= 30
                    ||(DateTime.Now-user.RegistrationDate).TotalDays>=30&&user.LastLogInDate==null)
                {
                    user.IsBlocked = true;
                    await UpdateUser((UserModel)user);
                }
            }
            return db.Users.Include(s => s.IdRoleNavigation).Include(s => s.IdRoomNavigation).ToList();
            }

        [HttpPost("Auth")]
        public async Task<User> AuthUser(SearchUserByLoginPassword searchData)
        {
            await Task.Delay (100);
            var user = db.Users.FirstOrDefault(s => s.Login == searchData.Login && s.Password == searchData.Password);
            return user;
        }

        [HttpPost("SearchLogin")]
        public async Task<bool> SearchLogin(string login)
        {
            await Task.Delay(10);
            if (db.Users.FirstOrDefault(s => s.Login == login) !=null)
                return true;
            return false;
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser(UserModel user)
        {
            var u = (User)user;
            await db.Users.AddAsync(u);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("EditUser")]
        public async Task<ActionResult> UpdateUser(UserModel user)
        {//сделать тип в него передавать данные. надо без навигации..?
            var u = (User)user;
            
            db.Users.Update(u);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
