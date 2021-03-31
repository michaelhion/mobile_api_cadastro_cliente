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
            client.BaseAddress = new Uri("http://localhost:8000/api/Clientes");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClienteModel>> ListarClientes()
        {
            try
            {
                var resp = await client.GetAsync(client.BaseAddress);
                var cliente = JsonConvert.DeserializeObject<List<ClienteModel>>(resp.ToString());
                return cliente;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}