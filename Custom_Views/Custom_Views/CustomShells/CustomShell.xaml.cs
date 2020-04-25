using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Custom_Views.CustomShells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomShell : Shell
    {
        public CustomShell()
        {
            InitializeComponent();
        }
    }
}