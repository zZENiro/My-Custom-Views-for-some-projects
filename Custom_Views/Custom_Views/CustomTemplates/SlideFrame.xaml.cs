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
                "Radius",
                typeof(float),
                typeof(SlideFrame),
                -1f);

        protected readonly BindableProperty FrameDifferenceProperty =
            BindableProperty.Create(
                "FrameDifference",
                typeof(float),
                typeof(SlideFrame),
                10f);

        protected readonly BindableProperty FramesThicknessProperty =
            BindableProperty.Create(
                "FramesThickness",
                typeof(float),
                typeof(SlideFrame),
                2f);

        protected readonly BindableProperty ContentBoundsProperty =
            BindableProperty.Create(
                "ContentBoundsDifference",
                typeof(float),
                typeof(SlideFrame),
                2f);

        public float Radius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public float FrameDifference
        {
            get => (float)GetValue(FrameDifferenceProperty);
            set => SetValue(FrameDifferenceProperty, value);
        }

        public float FrameThickness
        {
            get => (float)GetValue(FramesThicknessProperty);
            set => SetValue(FramesThicknessProperty, value);
        }

        public float ContentBounds
        {
            get => (float)GetValue(ContentBoundsProperty);
            set => SetValue(ContentBoundsProperty, value);
        }

        public SlideFrame()
        {
            InitializeComponent();
        }
    }
}