using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem
{
    public class Video
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int NumIn { get; set; }
        public int NumOut { get; set; }
        public int RentDays { get; set; }
    }
}
