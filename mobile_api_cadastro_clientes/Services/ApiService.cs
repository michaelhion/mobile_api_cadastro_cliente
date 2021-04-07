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
        List<ClienteModel> clientes;
        private HttpClient client;     

        public ApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClienteModel>> GetClienteAsync()
        {
            Uri uri = new Uri(string.Format(Constants.ClienteUrl, string.Empty));

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<ClienteModel> posts = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
                clientes = new List<ClienteModel>(posts);
                return clientes;
            }
            else
            {
                clientes = null;
                return clientes;
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
            
            string uri = Constants.ClienteUrl + "/" + cliente.Id;
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
            HttpResponseMessage resp = await client.DeleteAsync(uri);
            if (!resp.IsSuccessStatusCode)
            {
                resp.Content.ReadAsStringAsync();
            }
        }
    }
}