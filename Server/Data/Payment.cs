using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        
        [Required]
        public DateTime PayDate { get; set; }

        [Required]
        public int PayPeriod { get; set; }

        [Required]
        public Decimal Sum { get; set; }

        public virtual Project Project { get; set; }
    }
}
