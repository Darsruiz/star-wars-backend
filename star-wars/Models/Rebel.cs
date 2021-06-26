using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.Models
{
    public class Rebel
    {
        [Key]
        public string Name { get; set; }
        public string Planet { get; set; }

        public DateTime Datetime { get; set; }

    }
}
