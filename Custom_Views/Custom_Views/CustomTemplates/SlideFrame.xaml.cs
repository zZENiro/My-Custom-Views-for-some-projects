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
    public partial class SlideFrame : Frame
    {
        protected readonly BindableProperty RadiusProperty =
            BindableProperty.Create(
                "CornerRadius",
                typeof(float),
                typeof(SlideFrame),
                -1f
                );

        protected readonly BindableProperty FrameDifferenceProperty =
            BindableProperty.Create(
                "FrameDifference",
                typeof(float),
                typeof(SlideFrame),
                10);

        protected readonly BindableProperty FramesThicknessProperty =
            BindableProperty.Create(
                "FramesThickness",
                typeof(float),
                typeof()
                );

        public new float CornerRadius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public SlideFrame()
        {
            InitializeComponent();
        }
    }
}