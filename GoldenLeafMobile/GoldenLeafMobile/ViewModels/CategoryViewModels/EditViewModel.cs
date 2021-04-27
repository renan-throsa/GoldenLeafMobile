using GoldenLeafMobile.Models.CategoryModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class EditViewModel : BaseEntryPage
    {

        public EditViewModel(Category _category)
        {
            Category = _category;
            SaveCategoryCommand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Category>(Category, ASK);
                    },
                      () =>
                      {
                          return !string.IsNullOrEmpty(Category.Title);
                      }

                );
        }


    }
}
