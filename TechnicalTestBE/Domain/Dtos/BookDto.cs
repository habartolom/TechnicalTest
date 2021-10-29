using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public string Genero { get; set; }
        public int NoPaginas { get; set; }
        public string Autor { get; set; }
    }
}
