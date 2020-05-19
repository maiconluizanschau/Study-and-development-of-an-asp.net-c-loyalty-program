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
    public class EmpresaController : Controller
    {
        Helper helper = new Helper();
        // GET: CadastroEmpresa
        public ActionResult Index()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                return View();
            }
            return View("Index");
        }

       public ActionResult BuscaEmpresa()


        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                string token = Session["TokenUsuario"].ToString();
               var empresa = "IdEmpresa";
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/empresa", empresa+"/Cnpj", token,"POST");
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.Empresa = JsonConvert.DeserializeObject<List<EmpresaDTO>>(response.Content);
                    return View();
                }
            }
            return View("Index");
        }


        //cadastrar empresa
        public ActionResult CadastroEmpresa(FormCollection cadastroempresa)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                var empresa = "";

                empresa += "&Nome=" + cadastroempresa["Nome"].ToString();
                empresa += "&Cnpj=" + cadastroempresa["Cnpj"].ToString();
                empresa += "&Cidade=" + cadastroempresa["Cidade"].ToString();
                empresa += "&Telefone=" + cadastroempresa["Telefone"].ToString();
                empresa += "&Email=" + cadastroempresa["Email"].ToString();


                Session["EstaLogado"] = cadastroempresa.ToString();
                string token = Session["TokenUsuario"].ToString();

                // string token = Session["EstaLogado"].ToString();
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/empresa", empresa, token, "POST");
                if (response.StatusCode.ToString() == "OK")
                {
                    TempData["msg"] = "Empresa cadastrado com sucesso";
                    //return RedirectToAction("", "Usuario");
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Ops, algo de errado aconteceu, nao foi possivel  salvar dados da empresa";
            // return RedirectToAction("Index", "Usuario");
            return RedirectToAction("Index", "Empresa");
            // }
            // return View();
        }
    }


    
}