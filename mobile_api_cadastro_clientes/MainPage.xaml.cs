using mobile_api_cadastro_clientes.Model;
using mobile_api_cadastro_clientes.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile_api_cadastro_clientes
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.100.6:8000/api/Clientes";
        private readonly HttpClient client = new HttpClient();
        private List<ClienteModel> clientes;
        ApiService service;
        public MainPage()
        {
            InitializeComponent();
            service = new ApiService();
        }
        async void Criar(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Editar());
        }

        async void Editar(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Editar());
        }

        async void Deletar(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Editar());
        }

        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            List<ClienteModel> posts = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
            clientes = new List<ClienteModel>(posts);
            MyListView.ItemsSource = clientes;
            base.OnAppearing();
        }

        async void AtualizaDados()
        {
            clientes = await service.GetProdutosAsync();
            MyListView.ItemsSource = clientes.OrderBy(item => item.FirstName).ToList();
        }
    }
}
