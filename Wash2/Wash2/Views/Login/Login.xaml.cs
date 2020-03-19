﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Menu;
using Wash2.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Wash2.SQLiteDB;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        public Usuario user;
        private UserDB userdb;

        public Login ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}

        private async void Iniciar_Sesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var usuario = Email_login.Text;
                var pass = Pass.Text;
                
                var httpClient = new HttpClient();
                var url = "http://www.washdryapp.com/app/public/washer/login";

                var value_check = new Dictionary<string, string>
                         {
                            {"usuario", usuario},
                            {"pass" , pass }
                         };

                var content = new FormUrlEncodedContent(value_check);
                var responseMsg = await httpClient.PostAsync(url, content);
                //var responseMsg = await httpClient.GetAsync(url);
                switch (responseMsg.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("ErroR", "error status 500 InternalServerError", "ok");
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        await DisplayAlert("error", "error status 400 Unauthorized", "ok");
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        await DisplayAlert("error", "error status 403  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        await DisplayAlert("error", "error status 404  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        try
                        {
                            HttpContent resp_content = responseMsg.Content;

                            var json = await resp_content.ReadAsStringAsync();
                            var userResult = JsonConvert.DeserializeObject<List<Usuario>>(json);
                            if (userResult[0].nombre== "fail")
                            {
                                Email_login.Focus();
                                Pass.Focus();
                                Errormsn.IsVisible = true;
                                Errormsn.Text = "Usuario o contraseña invalidos";
                            }
                            else
                            {
                                var userW = new Usuario();
                                userdb = new UserDB();
                                userW.google_id = userResult[0].google_id;
                                userW.id = userResult[0].id;
                                userW.name = userResult[0].name;
                                userW.nombre = userResult[0].nombre;
                                userW.password = userResult[0].password;
                                userW.email = userResult[0].email;
                                userdb.AddMember(userW);
                                Application.Current.MainPage = new MainPage();
                            }
                        }
                        catch (Exception ex){
                            await DisplayAlert("", "" + ex.ToString(), "ok");
                        }
                        
                        break;
                    case System.Net.HttpStatusCode.RequestEntityTooLarge:
                        await DisplayAlert("error", "error status 413  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        await DisplayAlert("error", "error status 408  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                        break;
                }
            }
            catch (Exception ex){
                await DisplayAlert("Error EX", "Error : " + ex.ToString(), "OK");
            }
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new Registro());
        }
        private async void RecuperarPass(object sender, EventArgs args)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new RecuperarPass());
        }
    }
}