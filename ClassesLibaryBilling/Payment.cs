using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibaryBilling
{
    [Serializable]
    public class Payment
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime PayDate { get; set; }

        public int PayPeriod { get; set; }

        public Decimal Sum { get; set; }
    }
}
