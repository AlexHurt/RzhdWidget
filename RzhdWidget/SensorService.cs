using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Support.V4.Content;
using Android.Widget;

namespace RzhdWidget
{
    internal class SensorService : Service, ILocationListener
    {
        private LocationManager _locationManager;
        private string mProvider;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            _locationManager = (LocationManager)GetSystemService(LocationService);
            mProvider = _locationManager.GetBestProvider(new Criteria(), false);
            if (!String.IsNullOrEmpty(mProvider))
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) !=
                    Permission.Granted &&
                    ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) !=
                    Permission.Granted)
                {
                    StopSelf();
                    return;
                }

                var location = _locationManager.GetLastKnownLocation(mProvider);
                _locationManager.RequestLocationUpdates(mProvider, 15000, 1, this);
                if (location != null)
                    OnLocationChanged(location);
                else
                    Toast.MakeText(this, "No Location Provider Found", ToastLength.Short).Show();
            }
        }

        public void OnLocationChanged(Location location)
        {
            
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            throw new System.NotImplementedException();
        }
    }
}