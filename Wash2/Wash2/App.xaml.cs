using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Splash;
using Wash2.Views.AutoLavados;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Net.WebRequestMethods;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Wash2
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }

        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Device.StartTimer(TimeSpan.FromMinutes(1), () => //Will start after 1 min
            {
                Task.Run(() =>
                {
                    Console.WriteLine("CADA MINUTE");
                // do something with time...
            });

                return false; // To repeat timer,always return true.If you want to stop the timer,return false
            });
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
