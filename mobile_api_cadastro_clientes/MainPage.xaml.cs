using mobile_api_cadastro_clientes.Model;
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
        private ObservableCollection<ClienteModel> _post;
        public MainPage()
        {
            InitializeComponent();
        }
        async void Clicado(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Detalhes());
        }

        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            List<ClienteModel> posts = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
            _post = new ObservableCollection<ClienteModel>(posts);
            MyListView.ItemsSource = _post;
            base.OnAppearing();
        }
    }
}
