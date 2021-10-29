using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public virtual ICollection <Book> books { get; set; }
    }
}
