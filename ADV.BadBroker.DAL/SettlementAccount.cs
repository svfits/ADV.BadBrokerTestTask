using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.BadBroker.DAL
{
    public class SettlementAccount
    {
        [Key]
        public Guid Id { get; set; }

        public Сurrency Currency { get; set; }

        public Decimal TotalCurrency { get; set; }

        public DateTime Start { get; set; } = DateTime.Now;

        [Required]
        public User User { get; set; } = null!;
    }
}
