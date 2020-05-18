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
          
            var CameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (CameraStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await DisplayAlert("Acesso à Câmera", "É preciso dar permissão acessar à Câmera", "OK", "Cancelar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                CameraStatus = results[Permission.Camera];
            }
                        
            else if (CameraStatus == PermissionStatus.Denied || CameraStatus == PermissionStatus.Unknown)
            {
                await DisplayAlert("Acesso à Câmera Negado", "Não é possível continuar", "OK");
                IsPresented = false;
            }

            var StorageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (StorageStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await DisplayAlert("Acesso ao Armazenamento Externo", "É preciso dar permissão para Armazenamento Externo", "OK", "Cancelar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                StorageStatus = results[Permission.Storage];
            }

            if (StorageStatus == PermissionStatus.Unknown || StorageStatus == PermissionStatus.Denied)
            {
                await DisplayAlert("Armazenamento Externo Negado", "Não é possível continuar", "OK");
                await Navigation.PopToRootAsync();
            }
        }

    }
}