using ExamenJanvier2023.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using WpfApplication1.ViewModels;

namespace ExamenJanvier2023.ViewModels 
{
    class ProductVM : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> _listProducts;
        private ObservableCollection<CountryModel> _listCountries;
        private NorthwindContext context = new NorthwindContext();
        private ProductModel? _selectedProduct;
        private DelegateCommand _markAsAbbandoned;
        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }


        public ObservableCollection<ProductModel> ListProducts { get { return _listProducts = _listProducts?? LoadProduct(); }
        }

        private ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> products = new ObservableCollection<ProductModel>();
            List<Product> listProductGlobal = (from p in context.Products where p.Discontinued == false select p).ToList();


            foreach (var product in listProductGlobal)
            {
                ProductModel newProductModel = new ProductModel(product);

                newProductModel.Category = (from category in context.Categories where category.CategoryId == product.CategoryId select category.CategoryName).First();

                newProductModel.Fournisseur = (from sup in context.Suppliers where sup.SupplierId == product.SupplierId select sup.ContactName).First();

                products.Add(newProductModel);
            }

            return products;


        }

       public ObservableCollection<CountryModel> ListCountries {  get { return _listCountries = _listCountries ?? LoadCountries(); } }

        public void markAbandonned()
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("Aucun produit select");
            }
            else
            {

                Product productToUpdate = context.Products.Where(e => e.ProductId == _selectedProduct.ProductId).First();
                if (productToUpdate != null)
                {
                    productToUpdate.Discontinued = true;
                    context.Products.Update(productToUpdate);
                    context.SaveChanges();
                    ListProducts.Remove(SelectedProduct);
                    SelectedProduct = null;
                   
                    OnPropertyChanged("SelectedProduct");
                    MessageBox.Show("Supprimer de la db");
                   

                }

            }
        }

        public DelegateCommand MarkProductAsAbandonedCommand { 
            get { return _markAsAbbandoned = _markAsAbbandoned ??  new DelegateCommand(markAbandonned); }
        }
        private ObservableCollection<CountryModel> LoadCountries()
        {

           


           var queryResult = (from order in context.Orders
                                       join customer in context.Customers on order.CustomerId equals customer.CustomerId
                                       join orderDetail in context.OrderDetails on order.OrderId equals orderDetail.OrderId
                                       group orderDetail.ProductId by customer.Country into groupedProducts
                                       where groupedProducts.Any()
                                       orderby groupedProducts.Count() descending
                                       select new CountryModel(groupedProducts.Key, groupedProducts.Count())
                                        ).ToList();

            ObservableCollection<CountryModel> allCountryWithTotalOrder = new ObservableCollection<CountryModel>(queryResult);

            return allCountryWithTotalOrder;
        }

        public ProductModel SelectedProduct 
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            } // a mettre avant laccolde a gauche si on a besoin 

        }



    }

  
}
