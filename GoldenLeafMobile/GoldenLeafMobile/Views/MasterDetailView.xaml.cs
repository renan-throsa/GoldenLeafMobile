using GoldenLeafMobile.Models.ClerkModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
       
        public MasterDetailView(Clerk clerk)
        {
            InitializeComponent();
            this.Master = new MasterView(clerk);
        }
                
        protected async override void OnAppearing()
        {
            base.OnAppearing();
          
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                    await DisplayAlert("Acesso à Câmera", "É preciso dar permissão para acessar a câmera", "OK","Cancelar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                status = results[Permission.Camera];
            }
                        
            else if (status != PermissionStatus.Denied || status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Acesso à Câmera Negado", "Não é possível continuar", "OK");
                IsPresented = false;
            }

        }

    }
}