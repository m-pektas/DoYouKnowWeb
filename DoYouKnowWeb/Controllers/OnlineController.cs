using DoYouKnowWeb.Database;
using DoYouKnowWeb.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoYouKnowWeb.Controllers
{
    public class OnlineController : Controller
    {
        // GET: Online
        public ActionResult Index(int usrId)
        {
            OnlineIndexModel oim = new OnlineIndexModel();
            using (DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion())
            {
                MyUser usr = db.MyUser.Where(x => x.Id == usrId).FirstOrDefault();
                if (usr != null)
                {
                    oim.usr = usr;
                    oim.cache = 0;
                    return View(oim);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult SettingModel(string saveProfile, string logOut, HttpPostedFileBase updatePhoto, int Id, string name, string surname, string birthday)
        {
            MyUser usr;
            if (saveProfile != "" && saveProfile != null)
            {

                using (DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion())
                {
                    usr = db.MyUser.Where(x => x.Id == Id).FirstOrDefault();
                    usr.Name = name;
                    usr.Surname = surname;

                    try
                    {
                        usr.Birthday = DateTime.Parse(birthday);
                    }
                    catch (Exception)
                    {
                        usr.Birthday = usr.Birthday;

                    }
                    db.SaveChanges();
                }

                if (updatePhoto != null && updatePhoto.ContentLength>0)
                {
                    ImageConverter imgConvrt = new ImageConverter();
                    byte[] byt = (byte[]) imgConvrt.ConvertTo(updatePhoto, typeof(byte[]));

                    using (DoYouKnowDBLastVersion db = new DoYouKnowDBLastVersion())
                    {
                        usr.Image = byt;
                        db.SaveChanges();    
                    }


                    //secilen profil photo veritabanına yazılacak..
                    return RedirectToAction("Index", "Online", new { userId = Id });//şidilik
                }
                
            }
            else if (logOut != null && logOut != "")
            {
                //logout işlemini gerceklestir..
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home"); // hiçbişey olmazsa login ekranına dön

        }

        public ActionResult Members(string username)
        {
            //ViewBag.Title = "Members";

            //List<User> userList;
            //string[] ns = username.Split();
            //string n = ns[0];
            //if (ns.Length > 1)
            //{
            //    string s = ns[1];
            //    using (DoYouKnowDB db = new DoYouKnowDB())
            //    {
            //        userList = db.Member.Where(x => x.Name == n || x.Surname == s).ToList();
            //    }
            //}
            //else
            //{
            //    using (DoYouKnowDB db = new DoYouKnowDB())
            //    {
            //        userList = db.Member.Where(x => x.Name == n || x.Surname == n).ToList();
            //    }
            //}

            return View(/*userList*/);
        }

        public ActionResult FollowedList()
        {
            //List<User> son = new List<User>();
            //User u = new User();
            //u.Name = "Ufuk";
            //u.Surname ="Anteplioğlu";
            //User u2 = new User();
            //u.Name = "Cevher";
            //u.Surname = "Söylemez";
            //son.Add(u);
            //son.Add(u2);


            //ViewBag.title = "Followed List";
            return View("members");/*, son*/

        }

        public ActionResult GroupCache()
        {
            HttpRuntime.Cache["group"] = 1;
            return RedirectToAction("Index", new { Id = 1 });
        }

        public ActionResult ChatCache()
        {
            HttpRuntime.Cache["group"] = 2;
            return RedirectToAction("Index", new { Id = 1 });
        }

    }
}