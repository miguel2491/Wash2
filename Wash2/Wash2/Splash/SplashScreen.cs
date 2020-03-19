using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wash2.Models;
using Wash2.SQLiteDB;
using Wash2.Views.Login;
using Xamarin.Forms;

namespace Wash2.Splash
{
    class SplashScreen : ContentPage
    {
        Image splashImage;

        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "iko.png",
                WidthRequest = 150,
                HeightRequest = 150,
                Opacity = 0
            };

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#225374");
            this.Content = sub;
        }
        public Usuario user;
        public UserDB userDataBase;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.FadeTo(1, 150, null);
            await splashImage.ScaleTo(1, 1000);
            await splashImage.ScaleTo(0.6, 1500, Easing.BounceOut);
            await splashImage.FadeTo(0, 270, null);
            userDataBase = new UserDB();
            var user_exist = userDataBase.GetMembers();
            int RowCount = 0;
            int usercount = user_exist.Count();
            RowCount = Convert.ToInt32(usercount);

            if (RowCount > 0)
            {

                Application.Current.MainPage = new MainPage();
            }
            else {
                Application.Current.MainPage = new NavigationPage(new Banner());
            }

        }
    }
}
