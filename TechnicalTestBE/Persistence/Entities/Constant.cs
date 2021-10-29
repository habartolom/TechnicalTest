using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class Constant
    {
        [Key]
        public string Key { get; set; }
        public int Value { get; set; }
    }
}
