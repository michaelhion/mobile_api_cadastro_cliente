using mobile_api_cadastro_clientes.Model;
using mobile_api_cadastro_clientes.Services;
using MsgPack.Serialization;
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

        async void Editar(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Editar());
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
            //string content = await client.GetStringAsync(Constants.ClienteUrl);
            //List<ClienteModel> posts = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
            //clientes = new List<ClienteModel>(posts);

            //MyListView.ItemsSource = clientes;
            base.OnAppearing();
        }

        async void AtualizaDados()
        {
            clientes = await service.GetClienteAsync();

            MyListView.ItemsSource = clientes;
        }

        private async void OnAtualizar(object sender, EventArgs e)
        {
            if (Valida())
            {
                try
                {
                    var mi = ((MenuItem)sender);
                    ClienteModel clienteAtualizar = (ClienteModel)mi.CommandParameter;
                    clienteAtualizar.Id = txtId.Text;
                    clienteAtualizar.FirstName = txtNome.Text;
                    clienteAtualizar.Surname = txtSobrenome.Text;
                    clienteAtualizar.Age = Convert.ToInt16(txtIdade.Text);
                    clienteAtualizar.CreationDate = DateTime.Now;
                    await service.UpdateClienteAsync(clienteAtualizar);
                    LimpaCliente();
                    AtualizaDados();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Dados inválidos...", "OK");
            }
        }

        private void LimpaCliente()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtIdade.Text = "";
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cliente = e.SelectedItem as ClienteModel;
            txtId.Text = cliente.Id;
            txtNome.Text = cliente.FirstName;
            txtSobrenome.Text = cliente.Surname;
            txtIdade.Text = cliente.Age.ToString();
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text)) //&& string.IsNullOrEmpty(txtIdade.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void OnDeletar(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                ClienteModel clienteDeletar = (ClienteModel)mi.CommandParameter;
                await service.DeleteClienteAsync(clienteDeletar);
                LimpaCliente();
                await service.GetClienteAsync();
                AtualizaDados();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
