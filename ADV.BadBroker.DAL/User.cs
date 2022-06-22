using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.BadBroker.DAL
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; } = null!;
    }
}
