using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateAmLib
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconURL { get; set; }

        public List<Rate> Rates { get; set; } = new List<Rate>();
        
    }
}
