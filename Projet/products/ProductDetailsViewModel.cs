using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace Projet
{
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private int _quantity;

        public Projet.Product Product { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProductDetailsViewModel(Projet.Product product)
        {
            Product = product;
            Quantity = 1; 
        }
    }
}
