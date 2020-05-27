﻿using GoldenLeafMobile.Models.ProductModels;
using GoldenLeafMobile.ViewModels.ProductViewModel;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace GoldenLeafMobile.ViewModels.ProductViewModels
{
    public class ProductEntryViewModel : BaseProductViewModel
    {
        public ICommand ReadBarCodeComand { get; set; }               

        public ProductEntryViewModel(Product product) : base(product)
        {

            SaveProductComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Product>(Product, ASK);

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Product.Description)
                        && !string.IsNullOrEmpty(Product.Code)
                        && UnitCost > 0
                        && UnitCost < 100
                        && SelectedCategory != null;
                    }
                );

            ReadBarCodeComand = new Command(() =>
            {
                var scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Code = result.Text;
                    });
                };

            });
        }

    }
}
