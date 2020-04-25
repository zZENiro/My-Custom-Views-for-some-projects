using Custom_Views.CustomShells;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Custom_Views
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static Color LookupColor(Color color) => color;
    }
}
