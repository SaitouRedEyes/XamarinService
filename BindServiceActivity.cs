using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinService
{
    [Activity(Label = "BindServiceActivity")]
    public class BindServiceActivity : Activity, IServiceConnection
    {
        private Count counter;
        private CountBinder binder;
        private bool isConnected;
        private TextView textViewShowNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.bind_service);

            isConnected = false;
            binder = null;

            textViewShowNumber = (TextView)FindViewById(Resource.Id.tvShowNumber);

            Button buttonStartBind = (Button)FindViewById(Resource.Id.btnStartBind);
            Button buttonStopBind = (Button)FindViewById(Resource.Id.btnStopBind);
            Button buttonGetCounter = (Button)FindViewById(Resource.Id.btnGetCount);            

            buttonStartBind.Click += OnButtonStartBindClicked;
            buttonStopBind.Click += OnButtonStopBindClicked;
            buttonGetCounter.Click += OnButtonGetCounterClicked;
        }

        protected override void OnStop()
        {
            base.OnStop();
            UnbindConnection();
        }

        private void UnbindConnection()
        {
            if (binder != null)
            {
                binder = null;
                counter = null;
                UnbindService(this);
                Toast.MakeText(this, "Unbind Service", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Service is already unbind", ToastLength.Short).Show();
            }
        }

        private void OnButtonGetCounterClicked(object sender, EventArgs e)
        {
            if (counter != null)
            {
                Toast.MakeText(this, "Count: " + counter.GetCount(), ToastLength.Short).Show();
                textViewShowNumber.Text = counter.GetCount().ToString();
            }
            else
            {
                Toast.MakeText(this, "Problems to access the service", ToastLength.Short).Show();
            }
        }

        private void OnButtonStopBindClicked(object sender, EventArgs e)
        {
            UnbindConnection();
        }

        private void OnButtonStartBindClicked(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(CountService));

            BindService(i, this, Bind.AutoCreate);
            Toast.MakeText(this, "Bind Service", ToastLength.Short).Show();
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            binder = (CountBinder)service;

            isConnected = binder != null;

            if (isConnected)
            {
                counter = binder.Service;
                Toast.MakeText(this, "Service Connected", ToastLength.Short).Show();
            }
            else
            {
                counter = null;
                Toast.MakeText(this, "Service Not Connected", ToastLength.Short).Show();
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            counter = null;
            binder = null;
            isConnected = false;

            Toast.MakeText(this, "Service Disconnected", ToastLength.Short).Show();
        }        
    }
}