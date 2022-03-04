using ConsoleConsumoApiNode.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ConsoleConsumoApiNode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CadastrarProduto();

            Listarprodutos();
        }


        public static void CadastrarProduto()
        {
            string uprApi = "http://localhost:4200/Create";
            try
            {
                using (var cliente = new HttpClient())
                {
                    var produto = new Produto();
                    produto.Descricao = "Produto - Teste" + DateTime.Now.ToString();
                    produto.DataCriacao = DateTime.Now;

                    string jsonObjeto = JsonConvert.SerializeObject(produto);
                    var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");

                    var resposta = cliente.PostAsync(uprApi, content);
                    resposta.Wait();
                    if(resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();

                        var produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                  
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static void Listarprodutos()
        {
            string uprApi = "http://localhost:4200/List";
            try
            {
                using (var cliente = new HttpClient())
                {
                    var resposta = cliente.GetStringAsync(uprApi);
                    resposta.Wait();
                    var retorno = JsonConvert.DeserializeObject<Produto[]>(resposta.Result).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }



}
