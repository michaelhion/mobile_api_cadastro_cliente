using mobile_api_cadastro_clientes.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_api_cadastro_clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Criar : ContentPage
    {
        private const string Url = "http://192.168.100.6:8000/api/Clientes";
        private readonly HttpClient client = new HttpClient();
        private List<ClienteModel> clientes;
        public Criar()
        {
            InitializeComponent();
        }

        private async Task AddClienteAsync(ClienteModel clientes)
        {
            try
            {
                string url = "http://192.168.100.6:8000/api/Clientes";
                var uri = new Uri(string.Format(url, clientes.Id));
                var data = JsonConvert.SerializeObject(clientes);
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

    }
}