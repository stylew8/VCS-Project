using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class Setups
    {
        public List<ProductModel>? Bag { get; set; } = new List<ProductModel>();
        public int BagCounter { get; set; } = 0;
        public string Logging { get; set; } = "unUser";
    }
}
