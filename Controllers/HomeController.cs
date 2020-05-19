using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Controllers;
namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null && Session["Status"].ToString() != "1")
            {
                
                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();

                return View();
            }
            return RedirectToAction("Index", "Login");
        }
            public ActionResult Login()
        {
            return View();

        }


        //lista de usuarios cadastrados na empresa 



    }
}