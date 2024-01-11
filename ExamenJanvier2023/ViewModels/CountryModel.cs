using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanvier2023.ViewModels
{
    class CountryModel
    {
        private string _countryname;

        private int _totalOrder;

        public CountryModel(string countryname, int totalOrder)
        {
            _countryname = countryname;
            _totalOrder = totalOrder;
        }

        public string CountryName { get { return _countryname; } set { _countryname = value; } }

        public int TotalOrder { get { return _totalOrder; } set { _totalOrder = value; } }

    }
}
