using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibaryBilling
{
    [Serializable]
    public class Client
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }
    }
}
