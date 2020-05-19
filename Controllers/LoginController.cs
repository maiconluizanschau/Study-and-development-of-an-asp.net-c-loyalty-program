using RestSharp;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Mvc;
using System.Data;
using Models;

namespace Site.Controllers
{
    public class LoginController : Controller
    {
        Helper helper = new Helper();
        public ActionResult Index()
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {

                return View();
            }
            return View("Index");

        }

        //para ver se está logado
        public bool estaLogado(Controller a)
        {
            if (a.Session["EstaLogado"] == null)
            {
                if (a.Response.RedirectLocation == null)
                    a.Response.Redirect("~/Login/Index", true);
                return false;
            }
            return true;
        }

        public static bool Logado(HttpSessionStateBase a)
        {
            if (a["EstaLogado"] == null)
            {
                return false;
            }
            return true;
        }

        //sair
        public ActionResult Logout()
        {
            //sessao
            Session["nomeUsuarioLogado"] = null;
            Session["TokenUsuario"] = null;
            Session["EstaLogado"] = null;
            Session["IdUsuario"] = null;
            Session["Email"] = null;
            Session["Nome"] = null;
            // Session["nomeUsuarioLogado"] = null;
            return RedirectToAction("Index");
            //fim  sessao 
        }


        //mandar para página de recuperar senha
        public ActionResult Recuperar()
        {

            return View();

        }


        //envio dos dados quando a pessoa esquece a senha para o email pessoal necessario informar email e cpf
        public ActionResult RecuperaSenha(FormCollection RecuperaSenha)
        {
            var dados = "";
            dados += "/" + RecuperaSenha["Email"].ToString();
            dados += "/" + RecuperaSenha["Cpf"].ToString();

            IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/RecuperaSenha" + dados);

            if (response.StatusCode.ToString() == "OK")
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }


        //Enviando a nova senha do usuario
        public ActionResult NovaSenha(FormCollection Trocasenha)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null && Session["Status"].ToString() != "1")
            {

                var dados = "";
                dados += "&Senha=" + Trocasenha["Senha"].ToString();
                // dados += "/" + RecuperaSenha["Cpf"].ToString();

                string IdUsuario = Session["IdUsuario"].ToString();
                string token = Session["TokenUsuario"].ToString();
                IRestResponse response = helper.RequisicaoRest("http://localhost:51747/Usuario/" + IdUsuario, dados, token, "PATCH");

                if (response.StatusCode.ToString() == "OK")
                {
                    return RedirectToAction("Index", "Perfil");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        // [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LoginForm(FormCollection loginForm)
        {


            var dados = "";

            dados += "&username=" + loginForm["Email"].ToString();
            dados += "&password=" + loginForm["Senha"].ToString();
            dados += "&cnpj=null";

            IRestResponse respondelogin = helper.Login("http://localhost:51747/token", dados);


            if (respondelogin.StatusCode.ToString() == "OK")
            {


                Session["EstaLogado"] = loginForm.ToString();


                //cokkie
                // HttpCookie MyCookie = new HttpCookie();
                //  DateTime now = DateTime.Now;
                // MyCookie.Value = now.ToString();
                //  MyCookie.Expires = now.AddHours(1);
                //Response.Cookies.Add(MyCookie);

                string json = respondelogin.Content.ToString();
                UsuarioLoginToken item = Newtonsoft.Json.JsonConvert.DeserializeObject<UsuarioLoginToken>(json);
                string token = item.access_token;
                string email = item.Email;
                int idusuario = item.IdUsuario;
                string nome = item.Nome;
                string cpf = item.Cpf;
                // byte status = item.Status;
                Byte status = item.Status;
                UsuarioLoginToken Token = new UsuarioLoginToken();

                {
                    Token.access_token = token;
                };

                var usuario = "";

                Session["EstaLogado"] = loginForm.ToString();
                Session.SessionID.ToString();
                Session["TokenUsuario"] = item.access_token.ToString();
                //pegar id banco
                Session["IdUsuario"] = item.IdUsuario.ToString();

                Session["Email"] = item.Email.ToString();
                Session["Nome"] = (item.Nome != null ? item.Nome.ToString() : item.Nome);
                Session["status"] = item.Status.ToString();

                if(Session["Status"].ToString() != "1")
                {
                    return View("NovaSenha");
                }
                /*

                if((Session["Status"] is byte)) 
                    {
                    return View("NovaSenha");
                }
               */
                TempData["msg"] = "=)";
                    return RedirectToAction("Index", "Perfil");
                
               
            }
            
               



            



            ViewBag.Message = "Login ou senha Incorretos! Se o problema persisitir entre em contato conosco 55 - 3028 9900. Obrigado =) ";
            //TempData["msg"] = "Ops =( Sua Empresa ainda nao foi liberada, por favor entre em contato conosco 55 - 3028 9900. Obrigado =)";
            // return RedirectToAction("Index", "Login");
            ViewBag.ViewBagProperty = "Login ou senha Incorretos! Se o problema persisitir entre em contato conosco 55 - 3028 9900. Obrigado =) ";
            //return RedirectToAction("Index", "Login");
            // return View("Index","Login");
            //return RedirectToAction("Index", "Login");
            return View("Index");

        }






    }


}