using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace XamarinService
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Intent i;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            i = new Intent(this, typeof(CountService));

            Button btnStartService = (Button)FindViewById(Resource.Id.btnStartService);
            Button btnStopService = (Button)FindViewById(Resource.Id.btnStopService);
            Button btnBindService = (Button)FindViewById(Resource.Id.btnBindService);

            btnStartService.Click += OnButtonStartServiceClicked;
            btnStopService.Click += OnButtonStopServiceClicked;
            btnBindService.Click += OnButtonBindServiceClicked;
        }               

        private void OnButtonStartServiceClicked(object sender, EventArgs e)
        {
            if (!CountService.active)
            {
                StartService(i);
                Toast.MakeText(this, "Start Service", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Service is already running", ToastLength.Short).Show();
            }
        }
        private void OnButtonStopServiceClicked(object sender, EventArgs e)
        {
            if (CountService.active)
            {
                StopService(i);
                Toast.MakeText(this, "Stop Service", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Service isn't started", ToastLength.Short).Show();
            }
        }
        private void OnButtonBindServiceClicked(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(BindServiceActivity)));
        }
        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}