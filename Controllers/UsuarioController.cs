using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Models;
using Newtonsoft.Json;
using RestSharp;


namespace Site.Controllers
{
    public class UsuarioController : Controller
    {
        Helper helper = new Helper();
        // [Authorize]
        public ActionResult Index()
        {
            if (Session["EstaLogado"] == null && Session["TokenUsuario"] == null)
            {


                return View();

            }
            return RedirectToAction("Index", "Login");
        }
        // [Authorize]
        public ActionResult Editar(int id)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {

                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();

                var dados = "";

                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/" + "/" + IdUsuario, dados, token, "GET");
           
                if (response.StatusCode.ToString() == "OK")
                {

                    ViewBag.usuariosE = JsonConvert.DeserializeObject<List<UsuarioUpdate>>(response.Content);
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }



        //salvar usuarios
        public ActionResult SalvarEdicaoUsuarios(FormCollection edicaousuarios)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();
                var dados = "";
                //  dados += "&IdUsuario=" + edicaousuarios["IdUsuario"].ToString();
                dados += "&Nome=" + edicaousuarios["Nome"].ToString();
                dados += "&Telefone=" + edicaousuarios["Telefone"].ToString();
                dados += "&DataNascimento=" + edicaousuarios["DataNascimento"].ToString();
                dados += "&Email=" + edicaousuarios["Email"].ToString();

                Session["EstaLogado"] = edicaousuarios.ToString();
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/" + IdUsuario, dados, token, "PUT");

                if (response.StatusCode.ToString() == "OK")
                {
                    Session["EstaLogado"] = edicaousuarios.ToString();
                    TempData["msg"] = "Usuario atualizado com sucesso com sucesso";

                    return RedirectToAction("Index", "Perfil");


                }
            }
            TempData["msg"] = "Ops, algo de errado aconteceu, nao foi possivel  atualizar usuario";
          
            return RedirectToAction("Index", "Home");



        }
        // [Authorize]
        public ActionResult Deletar(int id)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {

                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/usuario" + "/" + id, "", "DELETE");
                if (response.StatusCode.ToString() == "OK")
                {


                    return RedirectToAction("BuscaUsuario", "Usuario");
                }
            }
            return View("Index");

        }


        //listar dados pessoais do usuário
        // [Authorize]
        public ActionResult Usuarios()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                var dados = "";
                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();
                string Email = Session["Email"].ToString();
                // dados += IdUsuario + "/";
                //    IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario" + "/" + IdUsuario, token, "GET");
                //  IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/",IdUsuario , token, "GET");
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario" + "/" + IdUsuario, dados, token, "GET");


                if (response.StatusCode.ToString() == "OK")
                {


                    ViewBag.usuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(response.Content);




                    return View();
                }
            }
            return View("Index");


        }


        // [Authorize]
        public ActionResult CadastroUsuario(FormCollection cadastrousuario)
        {
            // Session["render"] = "Usuario";
            if (Session["EstaLogado"] == null && Session["TokenUsuario"] == null)
            {

                var dados = "";
                dados += "&Senha=" + cadastrousuario["Senha"].ToString();
                dados += "&Email=" + cadastrousuario["Email"].ToString();
                dados += "&Cpf=" + cadastrousuario["Cpf"].ToString();
                dados += "&Nome=" + cadastrousuario["Nome"].ToString();
                dados += "&DataNascimento=" + cadastrousuario["DataNascimento"].ToString();
                dados += "&Sexo=" + cadastrousuario["Sexo"].ToString();
                dados += "&Telefone=" + cadastrousuario["Telefone"].ToString();
                dados += "&Sexo=" + cadastrousuario["Sexo"].ToString();


                //Session["EstaLogado"] = cadastrousuario.ToString();
                //string token = Session["TokenUsuario"].ToString();
                var token = "";


                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario", dados, token, "POST");
                //  IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario" + "/" + IdUsuario, dados, token, "GET");
                if (response.StatusCode.ToString() == "OK")
                {
                    Session["EstaLogado"] = cadastrousuario.ToString();
                    TempData["msg"] = "Usuario cadastrado com sucesso";

                    return RedirectToAction("Index", "Perfil");


                }
            }
            TempData["msg"] = "Ops, algo de errado aconteceu, nao foi possivel salvar usuario";
            ViewBag.ViewBagCadastro = "Algo de errado ocorreu, nao foi possivel salvar o usuario! Se o problema persisitir entre em contato conosco através do número:55 - 3028 9900. Obrigado =)";
            return RedirectToAction("Index", "Usuario");



        }




        //buscar usuarios listar
        //listar dados pessoais do usuário
        // [Authorize]
        public ActionResult BuscaUsuario()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {

                string token = Session["TokenUsuario"].ToString();
                string IdUsuario = Session["IdUsuario"].ToString();
                string Email = Session["Email"].ToString();
                var dados = "";

                //    IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario" + "/" + IdUsuario, token, "GET");
                //  IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/",IdUsuario , token, "GET");
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario" + "/" + IdUsuario, dados, token, "GET");


                if (response.StatusCode.ToString() == "OK")
                {

                    ViewBag.usuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(response.Content);

                    return View();
                }
            }
            return RedirectToAction("Index", "Home");

        }
    }
}