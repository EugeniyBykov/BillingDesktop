using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }  

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        [Required]
        [MaxLength(30)]
        public string SendAddress { get; set; }

        public DateTime? NextPay { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        public virtual List<Client> Clients { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}
