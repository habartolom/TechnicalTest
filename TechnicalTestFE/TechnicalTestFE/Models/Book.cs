using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestFE.Models
{
    public class Book
    {
        [Key]
        [JsonProperty("id")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int BookId { get; set; }

        [JsonProperty("titulo")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Title { get; set; }

        [JsonProperty("anio")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int Year { get; set; }

        [JsonProperty("genero")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Genre { get; set; }

        [JsonProperty("noPaginas")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int Pages { get; set; }

        [JsonProperty("autor")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Author { get; set; }
    }
}
