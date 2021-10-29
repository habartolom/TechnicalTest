using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestFE.Models
{
    public class Author
    {
        [Key]
        [JsonProperty("id")]
        public int AuthorId { get; set; }
        
        [JsonProperty("nombreCompleto")]
        [Required(ErrorMessage = "Campo obligatorio")]

        public string Name { get; set; }
        
        [JsonProperty("fechaNacimiento")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime BirthDate { get; set; }
        
        [JsonProperty("ciudadProcedencia")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string City { get; set; }
        
        [JsonProperty("correoElectronico")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }
    }
}
