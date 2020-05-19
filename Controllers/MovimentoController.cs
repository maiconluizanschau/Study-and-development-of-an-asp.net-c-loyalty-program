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
    // [Authorize]
    public class MovimentoController : Controller
    {
        Helper helper = new Helper();
        public ActionResult Index()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                if (ModelState.IsValid)
                {

                    string token = Session["EstaLogado"].ToString();
                    // IRestResponse response = helper.RequisicaoRest(Movimento, token);
                    IRestResponse response = helper.RequisicaoRest("http://localhost:51747/movimento");
                    if (response.StatusCode.ToString() == "OK")
                    {
                        //ViewBag.movimentos = response.Content;
                        ViewBag.movimentos = JsonConvert.DeserializeObject<List<MovimentoUsuario>>(response.Content);
                        return View();
                    }
                }
                return RedirectToAction("Index", "Login");

            }
            return RedirectToAction("Index", "Login");

        }

        //  [Authorize]
        public ActionResult BuscaMovimento()

        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                if (ModelState.IsValid)
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
            return View("Index");


        }


        //buscar movimentos detalhados por empresa e usuario
        public ActionResult BuscaEmpresaUsuario(int IdEmpresa)
        {


            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                if (ModelState.IsValid)
                {
                    // var movimento = "IdMovimento";
                    //var Idempresa = "";

                    string token = Session["TokenUsuario"].ToString();
                    string IdUsuario = Session["IdUsuario"].ToString();

                    var dados = "";
                    //dados += "&Telefone=" + edicaousuarios["Telefone"].ToString();

                    dados += IdUsuario + "/";
                    dados += IdEmpresa;
                    var dado = "";
                    //dados += IdUsuario;

                    //string IdEmpresa = Session["IdEmpresa"].ToString();
                    //string IdEmpresa = Session["IdEmpresa"].ToString();
                    //string Cpf = Session["Cpf"].ToString();
                    //200.149:98
                    //  IRestResponse response = helper.RequisicaoRest("http://localhost:51747/empresa", empresa+"/Cnpj", token, "POST")
                    IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Movimento/Detalhes/" + "/" + dados, dado, token, "GET");

                    if (response.StatusCode.ToString() == "OK")
                    {

                        ViewBag.IdUsuarioIdEmpresa = JsonConvert.DeserializeObject<List<Movimento>>(response.Content);
                        return View();
                    }
                }
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");




        }


    }



}