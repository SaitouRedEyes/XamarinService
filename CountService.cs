using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinService
{
    [Service(IsolatedProcess = false, Name = "com.XamarinService.CountService")]
    class CountService : Service, IRunnable, Count
    {
        protected int count;
        public static bool active;
        private Handler h;

        public override void OnCreate()
        {
            base.OnCreate();
            
            active = true;
            h = new Handler(MainLooper);
            h.Post(this);

            Log.Debug("Counter Service", "OnCreate");
        }
      
        public override void OnDestroy()
        {
            Log.Debug("Counter Service", "OnDestroy");
            Binder = null;
            active = false;

            base.OnDestroy();
        }
        
        public IBinder Binder { get; private set; }

        public int GetCount()
        {
            return count;
        }

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug("Counter Service", "OnBind");

            this.Binder = new CountBinder(this);
            return this.Binder;            
        }

        public override bool OnUnbind(Intent intent)
        {
            Log.Debug("Counter Service", "OnUnbind");

            return base.OnUnbind(intent);
        }

        public void Run()
        {
            if (active)
            {
                Log.Debug("Counter Service", "Count: " + count);
                count++;

                h.PostDelayed(this, 1000);
            }
            else
            {
                count = 0;
                Log.Debug("Counter Service", "Service End!!");

                StopSelf();
            }
        }
        
    }
}