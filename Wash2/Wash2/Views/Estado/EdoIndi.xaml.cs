using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Estado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EdoIndi : ContentPage
	{
		public EdoIndi (int id_s)
		{
			InitializeComponent ();
            //_ = GetInfoCalificacion(id_s);
        }
	}
}