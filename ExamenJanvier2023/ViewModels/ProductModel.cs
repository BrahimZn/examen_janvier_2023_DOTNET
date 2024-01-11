using ExamenJanvier2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanvier2023.ViewModels
{
    class ProductModel
    {
        private readonly Product product;


        private string _category;
        private string _fournisseur;
        public ProductModel(Product product)
        {
            this.product = product;
        }
        public string ProductName { get { return  product.ProductName ;} }

        public int ProductId { get { return product.ProductId;  } }

        public string Fournisseur {  get { return _fournisseur; } set { _fournisseur = value; } }

        public string Category { get { return _category; } set { _category= value; } }
    }
}
