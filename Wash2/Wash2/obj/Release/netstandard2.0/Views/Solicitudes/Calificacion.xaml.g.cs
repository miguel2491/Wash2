//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Wash2.Views.Solicitudes.Calificacion.xaml", "Views/Solicitudes/Calificacion.xaml", typeof(global::Wash2.Views.Solicitudes.Calificacion))]

namespace Wash2.Views.Solicitudes {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\Solicitudes\\Calificacion.xaml")]
    public partial class Calificacion : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::LaavorRatingConception.RatingConception ratingStar;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry txt_comentario;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button Btn_EnviarCal;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(Calificacion));
            ratingStar = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::LaavorRatingConception.RatingConception>(this, "ratingStar");
            txt_comentario = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "txt_comentario");
            Btn_EnviarCal = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "Btn_EnviarCal");
        }
    }
}
