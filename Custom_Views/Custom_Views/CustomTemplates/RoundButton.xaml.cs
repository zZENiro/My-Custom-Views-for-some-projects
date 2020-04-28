using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Custom_Views.CustomTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundButton : ContentPage
    {
        public RoundButton()
        {
            InitializeComponent();
        }
    }
}