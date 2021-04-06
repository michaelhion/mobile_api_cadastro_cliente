using DocumentFormat.OpenXml.Spreadsheet;
using mobile_api_cadastro_clientes.Model;
using MsgPack.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace mobile_api_cadastro_clientes.Services
{
    class ApiService
    {
        //List<ClienteModel> clientes;
        private HttpClient client;
        private JsonSerializer _serializer = new JsonSerializer();
        

        public ApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClienteModel>> GetProdutosAsync()
        {
            try
            {
                string url = Constants.ClienteUrl;
                var response = await client.GetStringAsync(url);
                var clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(response);
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddClienteAsync(ClienteModel cliente, bool isNewItem = true)
        {
            
            Uri uri = new Uri(string.Format(Constants.ClienteUrl, string.Empty));


            string json = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
            
        }
        
        public async Task UpdateClienteAsync(ClienteModel cliente)
        {
            string id = cliente.Id;
            string end = "/" + id;
            string uri = Constants.ClienteUrl + end;
            //Uri uri = new Uri(string.Format(Constants.ClienteUrl, cliente.Id));


            string json = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);
        }
        public async Task DeleteClienteAsync(ClienteModel cliente)
        {
            string id = cliente.Id;
            string end = "/" + id;
            string uri = Constants.ClienteUrl + end;
            //string finalUri = "/" + cliente.Id;
            //var uri = new Uri(string.Format(Constants.ClienteUrl, finalUri));
            HttpResponseMessage resp = await client.DeleteAsync(uri);
            if (!resp.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao deletar clientes");

            }
        }
    }
}