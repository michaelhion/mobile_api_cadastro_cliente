//using mobile_api_cadastro_clientes.Model;
//using mobile_api_cadastro_clientes.Services;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace mobile_api_cadastro_clientes
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class Editar : ContentPage
//    {
//        private HttpClient client;
//        private JsonSerializer _serializer = new JsonSerializer();

//        ApiService service;

//        public Editar(): this(new ClienteModel())
//        {
            
//        }
//        public Editar(ClienteModel cliente)
//        {
//            InitializeComponent();
//            service = new ApiService();
            
            
//        }
        
        


//        private async void Atualizar(object sender, EventArgs e)
//        {
//            if (Valida())
//            {
//                try
//                {
//                    var cliente = ((ListView)sender);
//                    ClienteModel clienteAtualizar = (ClienteModel)cliente.BindingContext;
//                    txtId.Text = clienteAtualizar.Id;
//                    clienteAtualizar.Id = txtId.Text;
//                    //txtNome.Text = clienteAtualizar.FirstName;

//                    clienteAtualizar.FirstName = txtNome.Text;
//                    //txtSobrenome.Text = clienteAtualizar.Surname;
//                    clienteAtualizar.Surname = txtSobrenome.Text;
//                    //txtIdade.Text = clienteAtualizar.Age.ToString();
//                    //clienteAtualizar.Age = Convert.ToInt32(txtIdade.Text);
//                    await service.UpdateClienteAsync(clienteAtualizar);
//                    LimpaProduto();
                    
//                }
//                catch (Exception ex)
//                {
//                    await DisplayAlert("Erro", ex.Message, "OK");
//                }
//            }
//            else
//            {
//                await DisplayAlert("Erro", "Dados inválidos...", "OK");
//            }
//        }
//        private void LimpaProduto()
//        {
//            txtNome.Text = "";
//            txtSobrenome.Text = "";
//            txtIdade.Text = "";
//        }

//        private bool Valida()
//        {
//            if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtIdade.Text))
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

        
//    }
//}