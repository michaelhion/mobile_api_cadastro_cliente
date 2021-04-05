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

        public async Task AddClienteAsync(ClienteModel cliente, bool isNewItem = true)
        {
            string url = "http://192.168.100.6:8000/api/Clientes";
            Uri uri = new Uri(string.Format(url, string.Empty));


            //string json = JsonSerializer.Serialize<ClienteModel>(cliente, SerializerOptions);
            string json = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
            
        }
        //public async Task AddClienteAsync(ClienteModel cliente)
        //{
        //    try
        //    {
        //        string url = "http://192.168.100.6:8000/api/Clientes";
        //        var uri = new Uri(string.Format(url, cliente));
        //        var data = JsonConvert.SerializeObject(cliente);
        //        var content = new StringContent(data, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = null;
        //        response = await client.PostAsync(uri, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new Exception("Erro ao incluir produto");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task UpdateClienteAsync(ClienteModel cliente)
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
        public async Task DeleteClienteAsync(ClienteModel cliente)
        {
            string url = "http://192.168.100.6:8000/api/Clientes";
            var uri = new Uri(string.Format(url, cliente.Id));
            await client.DeleteAsync(uri);
        }
    }
}