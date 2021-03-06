﻿using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Transformations;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MrRondon.Helpers;
using Plugin.Permissions;
using Plugin.Toasts;
using Xamarin;
using Xamarin.Forms;

namespace MrRondon.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            base.OnCreate(bundle);

            Forms.SetFlags("FastRenderers_Experimental");
            Forms.Init(this, bundle);

            //Map
            FormsMaps.Init(this, bundle);

            //FFImageLoading initialization
            CachedImageRenderer.Init(true);
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            CachedImageRenderer.Init(true);
            var ignore1 = typeof(CircleTransformation);

            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this, new PlatformOptions { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });
            AppCenter.Start(Constants.AppCenter.Android, typeof(Analytics), typeof(Crashes));

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }
    }
}