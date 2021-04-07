using mobile_api_cadastro_clientes.Model;
using mobile_api_cadastro_clientes.Services;
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
    public partial class Detalhes : ContentPage
    {
        private readonly HttpClient client = new HttpClient();
        private List<ClienteModel> clientes;
        ApiService service;

        public Detalhes(ClienteModel clienteModel)
        {

            InitializeComponent();
            service = new ApiService();

            txtId.Text = clienteModel.Id;
            txtNome.Text = clienteModel.FirstName;
            txtSobrenome.Text = clienteModel.Surname;
            txtIdade.Text = clienteModel.Age.ToString();
        }

        private void LimpaCliente()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtIdade.Text = "";
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
                ClienteModel novoCliente = new ClienteModel();
                novoCliente.Id = txtId.Text;
                novoCliente.FirstName = txtNome.Text;
                novoCliente.Surname = txtSobrenome.Text;
                novoCliente.Age = Convert.ToInt32(txtIdade.Text);
                await service.DeleteClienteAsync(novoCliente);
                LimpaCliente();
                await service.GetClienteAsync();
                AtualizaDados();
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        async void AtualizaDados()
        {
            clientes = await service.GetClienteAsync();

            //ListView.ItemsSource = clientes;
        }

        private async void OnAtualizar(object sender, EventArgs e)
        {
            if (Valida())
            {
                ClienteModel novoCliente = new ClienteModel();
                novoCliente.Id = txtId.Text;
                novoCliente.FirstName = txtNome.Text;
                novoCliente.Surname = txtSobrenome.Text;
                novoCliente.Age = Convert.ToInt32(txtIdade.Text);
                novoCliente.CreationDate = DateTime.Now;
                try
                {
                    await service.UpdateClienteAsync(novoCliente);
                    LimpaCliente();
                    await Navigation.PushAsync(new MainPage());
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
        
    }
}