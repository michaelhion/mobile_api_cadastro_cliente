using DocumentFormat.OpenXml.Spreadsheet;
using mobile_api_cadastro_clientes.Model;
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
            client.BaseAddress = new Uri("http://192.168.100.6:8000/api/Clientes");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClienteModel>> GetProdutosAsync()
        {
            try
            {
                string url = "http://192.168.100.6:8000/api/Clientes";
                var response = await client.GetStringAsync(url);
                var clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(response);
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddProdutoAsync(ClienteModel cliente)
        {
            try
            {
                string url = "http://192.168.100.6:8000/api/Clientes";
                var uri = new Uri(string.Format(url, cliente.Id));
                var data = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir produto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateProdutoAsync(ClienteModel cliente)
        {
            string url = "http://192.168.100.6:8000/api/Clientes";
            var uri = new Uri(string.Format(url, cliente.Id));
            var data = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar produto");
            }
        }
        public async Task DeletaProdutoAsync(ClienteModel cliente)
        {
            string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
            var uri = new Uri(string.Format(url, cliente.Id));
            await client.DeleteAsync(uri);
        }
    }
}