using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Newtonsoft.Json;
using RestSharp;

namespace Site.Controllers
{
    //[Authorize]
    public class ParametroController : Controller
    {
        Helper helper = new Helper();
        public ActionResult Index()
        {
         


            return View();
        }

        [Authorize]
        public ActionResult CadastroParametro(FormCollection cadastroParametro)
        {


            // Session["render"] = "Usuario";
            //if (Session["token"] != null)
            //{
            // string token = Session["token"].ToString();
            var empresa = 1;
            var status = 1;
            var parametro = "";

            parametro += "&Status=" + status.ToString();
            parametro += "&Idempresa=" + empresa.ToString();
            parametro += "&Valor=" + cadastroParametro["Valor"].ToString();
            parametro += "&Pontos=" + cadastroParametro["Pontos"].ToString();





            IRestResponse response = helper.RequisicaoRest("http://localhost:51747/parametro", parametro, "POST");
            if (response.StatusCode.ToString() == "OK")
            {
                TempData["msg"] = "parametro cadastrado com sucesso";
                return RedirectToAction("BuscaParametro", "Parametro");

            }

            TempData["msg"] = "Ops, algo de errado aconteceu, n&atilde;o foi poss&iacute;vel salvar dados da empresa";
            return RedirectToAction("Index", "Usuario");



        }

    }
    

}