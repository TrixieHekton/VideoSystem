using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem
{
    public class Customer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Rented { get; set; }
        public int Overdue { get; set; }
        public decimal GrossBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
