using GoldenLeafMobile.Models.ClerkModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using GoldenLeafMobile.Views.ClientViews;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        MasterView masterPage;

        public MasterDetailView(Clerk clerk)
        {
            InitializeComponent();            
            masterPage = new MasterView(clerk);
            this.Master = masterPage;
            this.Detail = new NavigationPage(new ClientsPage());
            masterPage.listView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
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