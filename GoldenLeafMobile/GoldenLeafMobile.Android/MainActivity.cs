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
using Android.Support.V4.Content;

[assembly: Dependency(typeof(MainActivity))]
namespace GoldenLeafMobile.Droid
{
    [Activity(Label = "Palma de Ouro", Icon = "@drawable/appIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        static Java.IO.File FileImagePath;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
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

                using (var stream = new Java.IO.FileInputStream(FileImagePath))
                {
                    bytes = new byte[FileImagePath.Length()];
                    stream.Read(bytes);
                }
                MessagingCenter.Send<byte[]>(bytes, "ProfilePicture");
            }

        }

        public void TakePicture()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            FileImagePath = CreateFileImage();
            Android.Net.Uri photoUri = FileProvider.GetUriForFile(
                this,
                "com.mithril.goldenleafmobile",
                FileImagePath);

            intent.PutExtra(MediaStore.ExtraOutput, photoUri);           
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivityForResult(intent, 0);
        }

        private Java.IO.File CreateFileImage()
        {
            Java.IO.File imageFile;            
            Java.IO.File directory = new Java.IO.File(Environment
                .GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "pictures");

            if (!directory.Exists())
            {
                directory.Mkdirs();
            }

            imageFile = new Java.IO.File(directory, "ProfileImage.jpg");
            return imageFile;
        }


    }
}