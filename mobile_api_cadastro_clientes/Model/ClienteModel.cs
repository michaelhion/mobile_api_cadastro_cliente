using System;
using System.Collections.Generic;
using System.Text;

namespace mobile_api_cadastro_clientes.Model
{
    public class ClienteModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
