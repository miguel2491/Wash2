using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OnBoardingPageXaml : ContentPage
	{
		public OnBoardingPageXaml ()
		{
			InitializeComponent ();
            BindingContext = new OnboardingPageVm();
		}
	}
}