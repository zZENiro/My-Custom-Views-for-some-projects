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
    public partial class RoundButton : Button
    {
        protected readonly BindableProperty MainColorProperty =
            BindableProperty.Create(
                "MainColor",
                typeof(Color),
                typeof(RoundButton),
                Color.White);

        protected readonly BindableProperty BottomColorProperty =
            BindableProperty.Create(
                "BottomColor",
                typeof(Color),
                typeof(RoundButton),
                Color.White);

        public Color MainColor
        {
            get => (Color)GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }

        public Color BottomColor
        {
            get => (Color)GetValue(BottomColorProperty);
            set => SetValue(BottomColorProperty, value);
        }

        public RoundButton()
        {
            InitializeComponent();
        }
    }
}