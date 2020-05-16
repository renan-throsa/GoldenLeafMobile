using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using GoldenLeafMobile.Media;
using GoldenLeafMobile.Droid;
using Android.Content;
using Android.Provider;
using Xamarin.Forms;
using Plugin.Permissions;

[assembly: Dependency(typeof(MainActivity))]
namespace GoldenLeafMobile.Droid
{
    [Activity(Label = "Palma de Ouro", Icon = "@drawable/appIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        static Java.IO.File ImageFile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                byte[] bytes;
                
                using (var stram = new Java.IO.FileInputStream(ImageFile))
                {
                    bytes = new byte[ImageFile.Length()];
                    stram.Read(bytes);
                }
                MessagingCenter.Send<byte[]>(bytes, "ProfilePicture");
            }

        }
        public void TakePicture()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            var file = CreateFileImage();
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivityForResult(intent, 0);
        }

        private Java.IO.File CreateFileImage()
        {
            Java.IO.File imageFile;
            Java.IO.File directory = new Java.IO.File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "Pictures");

            if (!directory.Exists())
            {
                directory.Mkdirs();
            }

            imageFile = new Java.IO.File(directory, "ProfileImage.jpg");
            return imageFile;
        }


    }
}