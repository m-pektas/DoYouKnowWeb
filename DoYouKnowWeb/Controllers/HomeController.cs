using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoYouKnowWeb.Database;
using System.Drawing;
using System.IO;
using System.Data.Entity;

namespace DoYouKnowWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn(string username, string password)
        {
            //userlogin id,username ve password bulunduruyor
            //user ise standart bir kulanıcı bilgilerini

            if (username != null && password != null)
            {
                using (DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion())
                {
                    UserLogin usrlgn = db.UserLogin.Where(x => x.userName == username && x.password == password).FirstOrDefault(); //Veritabanında ilgili bir user login bulundu ise alındı yok ise null 

                    if (usrlgn != null) // yukarıdaki sartları sağlayan userlogin kayıtı var ise
                    {
                        MyUser USR = db.MyUser.Where(x => x.UserLoginId == usrlgn.Id).FirstOrDefault();//kayıtla eşlesen kulanıcı bulunup online/Index e gönderildi
                        return RedirectToAction("Index", "Online", new { usrId = USR.Id });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home"); //yok ise sayfa tekrar yüklendi
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");//yok ise sayfa tekrar yüklendi
            }

        }

        public void Db_Olustur()
        {
            DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion();

            UserLogin usrlgn = new UserLogin();
            Group grp = new Group();
            Event evt = new Event();
            Location lcn = new Location();

            //kayıt olurken buradaki zorunlu bilgiler alınacak username-password te aınacak kayıt ol diyince sıraylaoluşturulacak.

            MyUser myusr = new MyUser();
            myusr.Id = 1;
            myusr.Name = "Muhammed";
            myusr.Surname = "Pektaş";
            myusr.Birthday = DateTime.Now;
            myusr.Email = "mhmdpkts@gmail.com";
            myusr.FollowedList = null;
            myusr.FollowerList = null;
            myusr.Groups = null;
            myusr.Image = null;
            myusr.Status = "Member";
            myusr.TimeToCome = DateTime.Now;
            myusr.Title = "Student";
            myusr.UserLoginId = 1;
            db.MyUser.Add(myusr);
            db.SaveChanges();

            usrlgn.userName = "username";
            usrlgn.password = "password";
            usrlgn.UserId = 1;
            db.UserLogin.Add(usrlgn);
            db.SaveChanges();

            grp.Id = 1;
            grp.Image = null;
            grp.MyEventList = null;
            grp.Name = "Deneme";
            List<int> list = new List<int>();
            list.Add(1);
            grp.UserList = list;
            db.Group.Add(grp);
            db.SaveChanges();


            lcn.Id = 1;
            lcn.MyGroupId = 1;
            lcn.Name = "Deneme Festival Alanı";
            lcn.Adress = "Deneme mah. deneme2 sokak";
            lcn.MyEventId = 1;
            db.Location.Add(lcn);
            db.SaveChanges();


            evt.Id = 1;
            evt.MainLocationId = 1;
            evt.MyGroupId = 1;
            evt.OtherLocationList = null;
            evt.Subject = "Deneme Müzik Festivali";
            db.Event.Add(evt);
            db.SaveChanges();


        }
        
        public ActionResult SignUp(string name,string surname, string Email,string username,string password)
        {
            

            using(DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion())
            {
                UserLogin usrlgn = db.UserLogin.Where(x => x.userName == username && x.password == password).FirstOrDefault();

                if (usrlgn == null)//öyle bir kullanıcı yok ise kayıt edebiliriz
                {

                    MyUser usr = new MyUser();//user login ıd olmadan user olusturuldu
                    usr.Name = name;
                    usr.Surname = surname;
                    usr.Email = Email;
                    usr.Birthday = DateTime.Parse("01/01/1900");
                    usr.TimeToCome = DateTime.Parse("01/01/1900");
                    db.MyUser.Add(usr);
                    db.SaveChanges();

                    MyUser signUpUser = db.MyUser.Where(x => x.Name == name && x.Surname == surname && x.Email == Email).FirstOrDefault();
                    //olurulan usr ıd si ile 

                    UserLogin usrlgnSignUp = new UserLogin();
                    usrlgnSignUp.userName = username;
                    usrlgnSignUp.password = password;
                    usrlgnSignUp.UserId = signUpUser.Id;
                    db.UserLogin.Add(usrlgnSignUp);
                    db.SaveChanges();

                    UserLogin signUpUsrlgn = db.UserLogin.Where(x => x.UserId == signUpUser.Id).FirstOrDefault();
                    signUpUser.UserLoginId = signUpUsrlgn.Id;
                    db.SaveChanges();
                    
                }
                else//böyle bir kullanıcı var tekrar kayıt edilemez
                {
                    return RedirectToAction("Index", "Home");
                }
            }
                return RedirectToAction("Index");//şimdilik böyle
        }


    }
}
