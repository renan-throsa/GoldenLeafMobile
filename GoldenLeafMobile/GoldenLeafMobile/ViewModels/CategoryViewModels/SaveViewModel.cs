using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class SaveViewModel : BaseEntryPage
    {

        public SaveViewModel(Clerk clerk)
        {
            Clerk = clerk;
            Category = new Category();
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
