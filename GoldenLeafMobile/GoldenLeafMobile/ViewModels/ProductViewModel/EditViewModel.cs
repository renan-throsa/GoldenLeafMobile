using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ProductModels;
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

        public EditViewModel(Clerk clerk, Product product) : base(clerk, product)
        {
            Product = product;
            SaveProductCommand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Product>(Product, ASK);

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Product.Description)
                        && SalePrice > 0
                        && SalePrice < 100
                        && SelectedCategory != null;
                    }
                );
        }

        //Must be called only after GetCategories()!
        public void SetCategoryIndex()
        {
            var cat = Categories.First(c => c.Id == Product.CategoryId);
            IndexValue = Categories.IndexOf(cat);
        }
    }
}
