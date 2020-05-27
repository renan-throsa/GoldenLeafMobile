﻿using GoldenLeafMobile.Models.ProductModels;
using System.Linq;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ProductViewModel
{
    public class EditViewModel : BaseProductViewModel
    {

        private int _indexValue;

        public int IndexValue
        {
            get { return _indexValue; }
            set { _indexValue = value; OnPropertyChanged(); }
        }


        public EditViewModel(Product product) : base(product)
        {
            Product = product;
            SaveProductComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Product>(Product, ASK);

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Product.Description)
                        && UnitCost > 0
                        && UnitCost < 100
                        && SelectedCategory != null;
                    }
                );
        }

        //Must be called only after GetCategories()!
        public void SetCategoryIndex()
        {
            var cat = Categories.First(c => c.Id == Product.Id);
            IndexValue = Categories.IndexOf(cat);

        }
    }
}
