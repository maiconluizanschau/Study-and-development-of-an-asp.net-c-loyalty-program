using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Newtonsoft.Json;
using RestSharp;

namespace Site.Controllers
{
    public class PerfilController : Controller
    {
        Helper helper = new Helper();
        // GET: Perfil
        public ActionResult Index()
        {

            if (Session["EstaLogado"] != null && Session["TokenUsuario"] !=null)
            {
                // var movimento = "IdMovimento";
                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();
                var dados = "";
                //string Cpf = Session["Cpf"].ToString();
                //200.149:98
                //  IRestResponse response = helper.RequisicaoRest("http://localhost:51747/empresa", empresa+"/Cnpj", token, "POST");
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Movimento/Usuario/" + "/" + IdUsuario, dados, token, "GET");

                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.movimentos = JsonConvert.DeserializeObject<List<MovimentoUsuario>>(response.Content);

                    return View();
                }
              
            }

            return View("Index");

        }


       

    }
}