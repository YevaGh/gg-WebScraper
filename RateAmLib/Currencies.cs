using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateAmLib
{
    static class Currencies
    {
        public static Currency[] CurrenciesSource =
        {
            new Currency(){Id = 1, Name = "USD", IconURL="https://rate.am/images/currency/icon/USD.gif",Symbol = "$" },
            new Currency(){Id = 2, Name = "EUR", IconURL="https://rate.am/images/currency/icon/EUR.gif",Symbol = "€" },
            new Currency(){Id = 3, Name = "RUR", IconURL="https://rate.am/images/currency/icon/RUR.gif",Symbol = "₽" },
            new Currency(){Id = 4, Name = "GBP", IconURL="https://rate.am/images/currency/icon/GBP.gif",Symbol = "£" },
           
            new Currency(){Id = 5, Name = "GEL", IconURL="https://rate.am/images/currency/icon/GEL.gif",Symbol = "₾" },
            new Currency(){Id = 6, Name = "CHF", IconURL="https://rate.am/images/currency/icon/CHF.gif",Symbol = "₣" },
            new Currency(){Id = 7, Name = "CAD", IconURL="https://rate.am/images/currency/icon/CAD.gif",Symbol = "$" },
            new Currency(){Id = 8, Name = "AED", IconURL="https://rate.am/images/currency/icon/AED.gif",Symbol = "د.إ" },
        };
    }
}
