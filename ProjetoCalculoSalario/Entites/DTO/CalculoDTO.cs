using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCalculoSalario.Entites.DTO
{
    class CalculoDTO
    {
        private string hora;
        private string salario;

        public string Hora { get => hora; set => hora = value; }
        public string Salario { get => salario; set => salario = value; }
    }
}
