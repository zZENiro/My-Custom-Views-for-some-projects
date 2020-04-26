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
    public partial class GradientFrame : Frame
    {
        protected readonly BindableProperty FrameThicknessProperty =
            BindableProperty.Create(
                "FrameThickness",
                typeof(float),
                typeof(GradientFrame),
                0f);

        protected readonly BindableProperty RadiusProperty =
            BindableProperty.Create(
                "Radius",
                typeof(float),
                typeof(GradientFrame),
                0f);

        protected readonly BindableProperty EndColorProperty =
            BindableProperty.Create(
                "EndColor",
                typeof(Color),
                typeof(GradientFrame),
                Color.Transparent);

        protected readonly BindableProperty StartColorProperty =
            BindableProperty.Create(
                "StartColor",
                typeof(Color),
                typeof(GradientFrame),
                Color.Transparent);

        protected readonly BindableProperty AlphaProperty =
            BindableProperty.Create(
                "Alpha",
                typeof(int),
                typeof(GradientFrame),
                255);

        public float FrameThickness
        {
            get => (float)GetValue(FrameThicknessProperty);
            set => SetValue(FrameThicknessProperty, value);
        }

        public float Radius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public int Alpha
        {
            get => (int)GetValue(AlphaProperty);
            set => SetValue(AlphaProperty, value);
        }

        public GradientFrame()
        {
            InitializeComponent();
        }
    }
}