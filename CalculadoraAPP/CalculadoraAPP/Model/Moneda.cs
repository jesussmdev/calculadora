using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAPP.Model
{
    public class Moneda
    {

        public Moneda()
        {

        }


            public Moneda(string code, string name, string rate, string date, string inverseRate)
        {
            this.code = code;
            this.name = name;
            this.rate = rate;
            this.date = date;
            this.inverseRate = inverseRate;
        }

        

        public String code { get; set; }
        public String alphaCode { get; set; }
        public String numericCode { get; set; }
        public String name { get; set; }
        public String rate { get; set; }
        public String date { get; set; }
        public String inverseRate { get; set; }

    }
}
