using System;
using Xamarin.Forms;
namespace Wash2
{
    public class OnBoardingTemplateView : StackLayout
    {
        public BoxView BackGroundBoxView;

        public OnBoardingTemplateView()
        {
            BackGroundBoxView = new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Children.Add(BackGroundBoxView);
        }
    }
}
