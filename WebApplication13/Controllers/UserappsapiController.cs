using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class UserappsapiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Userappsapi
        public ListUSer Getuserapp()
        {
            if(SessionModel.sessionRole == "admin")
            {
                ListUSer uSer = new ListUSer
                {
                    Status=1,
                    Message="all user are availble for you as an admin",
                   userapps= db.userapp.ToList()

            };
                return uSer;
                 
            }
            List<Userapp> list = new List<Userapp>();
            ListUSer uSer1 = new ListUSer
            {
                Status = 0,
                Message = "users availa ble only for admins you cants see them",
                userapps = list

            };
            return uSer1 ;
        }

        // GET: api/Userappsapi/5
        [ResponseType(typeof(Userapp))]
        public IHttpActionResult GetUserapp(int id)
        {
            Userapp userapp = new Userapp();

            userapp.userId = id;
            
            Error MyUSerModel=new Error
            {
                Status=0,
                Message = "wrong id or you not an admin",
                userapps=userapp
            };
           userapp = db.userapp.Find(id);
            if (userapp == null|| SessionModel.sessionRole!="admin")
            {
                return Ok(MyUSerModel);
            }
           
            MyUSerModel.Status = 1;
            MyUSerModel.Message = "user got";
            return Ok(userapp);
        }

        // PUT: api/Userappsapi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserapp(int id, Userapp userapp)
        {
         
            Error edituser = new Error
            {
                Status = 0,
                Message ="type valid model",
                userapps = userapp
            };
            if (!ModelState.IsValid)
            {
               
                return Ok(edituser);
            }

            if (id != userapp.userId)
            {
                edituser.Message = "wrong id";
                return Ok(edituser);
            }

            db.Entry(userapp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserappExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            edituser.Status = 1;
            edituser.Message = "put success";
            return Ok(edituser);
        }

        // POST: api/Userappsapi
        [ResponseType(typeof(Userapp))]
        public IHttpActionResult PostUserapp(Userapp userapp)
        {
            
            Error post = new Error
            {
                Status = 0,
                Message ="model not vaild",
                userapps = userapp
            };

            if (!ModelState.IsValid)
            {
                return Ok(post);
            }

            db.userapp.Add(userapp);
            db.SaveChanges();
            post.Status = 1;
            post.Message = "post success";
            return Ok(post);
        }

        // DELETE: api/Userappsapi/5
        [ResponseType(typeof(Userapp))]
        public IHttpActionResult DeleteUserapp(int id)
        {
            Userapp userapp = db.userapp.Find(id);
            
            Error delet = new Error
            {
                Status = 0,
                Message = "id is wrong",
                userapps = userapp
            };
           
            if (userapp == null)
            {
               
                return Ok(delet);
            }

            db.userapp.Remove(userapp);
            db.SaveChanges();
            delet.Status = 1;
            delet.Message = "delet success";
            return Ok(delet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserappExists(int id)
        {
            return db.userapp.Count(e => e.userId == id) > 0;
        }

        [ResponseType(typeof(Userapp))]
        [ActionName("login")]
        public IHttpActionResult Postlogin(string Password ,string Email)
        {

            Userapp userError = new Userapp();
            userError.Password = Password;
            userError.Email = Email;
            if (!ModelState.IsValid)
            {
                Error myError = new Error
                {
                    Status = 0,
                    Message = "model not valid",
                    userapps = userError
            };
                return  Ok(myError);
            }


      Userapp user= db.userapp.FirstOrDefault(i=>i.Email== Email && i.Password==Password );
            if (user!=null) {
                if (user.uR == "admin")
                {
                 
                    Error myError2= new Error
                    {
                        Status = 1,
                        Message = "login succesefull as an admin",
                        userapps = user
        
                    };
                    SessionModel.sessionRole = "admin";
                    return Ok(myError2);
                }
                else if(user.uR == "user")
                {
                   
                    SessionModel.sessionRole = "user";
                    Error myError3= new Error
                    {
                        Status = 2,
                        Message = "regester succesefull as user",
                       
                        userapps = user
        
                    };
                    return Ok(myError3);
                }
            }
            
            Error myError4 = new Error
            {
                Status = 0,
                Message = "please use right user name and password",
                userapps = userError
            };
            return Ok(myError4);
        }


        [ResponseType(typeof(Userapp))]
        [ActionName("rejester")]
        public IHttpActionResult PostRejester(Userapp userapp)
        {

            Error error5 = new Error
            {
                Status = 0,
                Message = "model  not valid",
                userapps = userapp
            };
            if (ModelState.IsValid)
            {
                Userapp user = new Userapp();
                user= db.userapp.FirstOrDefault(i => i.Email == userapp.Email|| i.userId==userapp.userId);
               
                if (user == null)
                {
                   
                    if (userapp.uR=="user "&&userapp.image.Contains("jpeg")|| userapp.image.Contains("jpg")|| userapp.image.Contains("png")|| userapp.image.Contains("gif")&&userapp.name.Length>3)
                    {
                         userapp.uR = "user";
                        db.userapp.Add(userapp);
                        db.SaveChanges();
                       

                        Error myresjest = new Error
                        {
                            Status = 2,
                            Message = "regester succese",
                            userapps = userapp
                        };
                        return Ok(myresjest);
                    }
                    else
                    {
                       
                        error5.Message = "image extention not vaild or user name >3 ";
                       
                        return Ok(error5);
                    }
                }
                else {

                    
                    error5.Message = "Email already exist";
                    return Ok(error5); }
                
                
            }

          
            return Ok(error5);
        }

    }
}