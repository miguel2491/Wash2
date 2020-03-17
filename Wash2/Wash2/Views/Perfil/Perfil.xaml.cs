using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Perfil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Perfil : ContentPage
	{
		public Perfil ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, true);

            idx = "1";
        }

        private MediaFile _image;
        private string idx;

        private async void BtnIne_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera soportada.", "OK");
                return;
            }
            _image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "auto_" + idx,
                Name = "auto.jpg"
            });
            if (_image == null)
                return;
            // await DisplayAlert("File Location Error", "Error parece que hubo un problema con la camara, confirme espacio en memoria o notifique a sistemas", "OK");
            var xlocal = _image.Path;
            imgx.Source = ImageSource.FromStream(() => {

                return _image.GetStream();


            });
        }
    }
}