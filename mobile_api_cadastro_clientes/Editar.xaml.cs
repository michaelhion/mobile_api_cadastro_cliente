using mobile_api_cadastro_clientes.Model;
using mobile_api_cadastro_clientes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_api_cadastro_clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editar : ContentPage
    {
        ApiService service;
        List<ClienteModel> clientes;
        public Editar()
        {
            InitializeComponent();
            service = new ApiService();
            //Adicionar();
        }

        private async void Adicionar(object sender, EventArgs e)
        {
            if (Valida())
            {
                ClienteModel novoCliente = new ClienteModel
                {
                    FirstName = txtNome.Text,
                    Surname = txtSobrenome.Text,
                    Age = Convert.ToInt32(txtIdade.Text)
                };
                try
                {
                    await service.AddClienteAsync(novoCliente);
                    LimpaProduto();
                    //AtualizaDados();
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

        private void LimpaProduto()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtIdade.Text = "";
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtIdade.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
    }
}