using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RestSharp;
using System.Net;

namespace Site.Controllers
{
    public class Helper
    {
    
        public IRestResponse Login(string url, string dados = "")
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(url);
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            string encodedBody = string.Format("grant_type=password" + dados);
            request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
            request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
            var response = client.Execute(request);
            return response;

        }
        //backup funcionandodo 
        internal IRestResponse RequisicaoRest(string url, string dados = "", string token= "", string metodo = "GET")


        //teste sem token 
        //internal IRestResponse RequisicaoRest(string url,  string dados = "", string metodo = "GET")
        {

            RestClient client = new RestClient();
            client.BaseUrl = new Uri(url);
            RestRequest request = new RestRequest();
            if (metodo == "GET")
            {
                request.Method = Method.GET;
            }
            else if (metodo == "POST")
            {
                request.Method = Method.POST;
            }
            else if (metodo == "PUT")
            {
                request.Method = Method.PUT;
            }
            else if (metodo == "PATCH")
            {
                request.Method = Method.PATCH;
            }
            else if (metodo == "DELETE")
            {
                request.Method = Method.DELETE;
            }

            string encodedBody = string.Format("grant_type=password" + dados);
            request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
            request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
            request.AddHeader("authorization", "bearer " + token);
            var response = client.Execute(request);
            return response;

        }

        }

    //
}