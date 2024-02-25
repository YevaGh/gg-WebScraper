﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            new Currency(){Id = 9, Name = "CNY", IconURL="https://rate.am/images/currency/icon/CNY.gif",Symbol = "¥" },
            new Currency(){Id = 10, Name = "AUD", IconURL="https://rate.am/images/currency/icon/AUD.gif",Symbol = "$" },
            new Currency(){Id = 11, Name = "JPY", IconURL="https://rate.am/images/currency/icon/JPY.gif",Symbol = "¥" },
            new Currency(){Id = 12, Name = "SEK", IconURL="https://rate.am/images/currency/icon/SEK.gif",Symbol = "kr" },

            new Currency(){Id = 13, Name = "HKD", IconURL="https://rate.am/images/currency/icon/HKD.gif",Symbol = "$" },
            new Currency(){Id = 14, Name = "KZT", IconURL="https://rate.am/images/currency/icon/KZT.gif",Symbol = "₸" },
            new Currency(){Id = 15, Name = "XAU", IconURL="https://rate.am/images/currency/icon/XAU.gif",Symbol = "" },
        };
    }
}
