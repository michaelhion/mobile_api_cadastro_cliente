using mobile_api_cadastro_clientes.Model;
using mobile_api_cadastro_clientes.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace mobile_api_cadastro_clientes
{
    public partial class MainPage : ContentPage
    {
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
            await Navigation.PushAsync(new Criar());
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(string.Format(Constants.ClienteUrl, string.Empty));
            
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<ClienteModel> posts = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
                clientes = new List<ClienteModel>(posts);
                MyListView.ItemsSource = clientes;
            }
            
            base.OnAppearing();
        }

        async void AtualizaDados()
        {
            clientes = await service.GetClienteAsync();

            MyListView.ItemsSource = clientes;
        }

        

        

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as ClienteModel;
                await Navigation.PushAsync(new Detalhes(selection));
                #region DisabledSelectionHighlighting
                ((ListView)sender).SelectedItem = null;
                #endregion
            }
        }
    }
}
