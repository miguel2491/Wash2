using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Splash;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Wash2
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
