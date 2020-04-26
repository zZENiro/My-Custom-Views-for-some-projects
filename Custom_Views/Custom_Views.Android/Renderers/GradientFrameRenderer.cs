using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Custom_Views.CustomTemplates;
using Custom_Views.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientFrame), typeof(GradientFrameRenderer))]
namespace Custom_Views.Droid.Renderers
{
    [Obsolete]
    public class GradientFrameRenderer : FrameRenderer
    {
        GradientFrame _targetElem;
        GradientFrameDrawable _drawableElem;

        public GradientFrameRenderer(Context context) : base(context) =>
            _targetElem = Element as GradientFrame;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName || e.PropertyName == Frame.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && e.OldElement == null)
            {
                _targetElem = e.NewElement as GradientFrame;

                UpdateBackground();
            }
        }

        private void UpdateBackground()
        {
            _drawableElem?.Dispose();
            this.SetBackground(_drawableElem = new GradientFrameDrawable(this.Element as GradientFrame, Context.ToPixels));
        }

        class GradientFrameDrawable : Drawable
        {
            readonly Func<double, float> _convertToPixels;
         
            GradientFrame _frame;
            Android.Graphics.Color _startColor;
            Android.Graphics.Color _endColor;
            int _alpha;
            float _radius;
            float _frameThickness;
            Bitmap _normalBitmap;

            public override bool IsStateful => false;

            public override int Opacity => 0;

            public GradientFrameDrawable(GradientFrame frame, Func<double, float> convertToPixels)
            {
                _frame = frame;
                _radius = _frame.Radius;
                _frameThickness = _frame.FrameThickness;
                _convertToPixels = convertToPixels;
                _alpha = _frame.Alpha;
                _startColor = _frame.StartColor.ToAndroid();
                _endColor = _frame.EndColor.ToAndroid();

                _frame.PropertyChanged += FrameOnPropChanged;
            }

            private void FrameOnPropChanged(object sender, PropertyChangedEventArgs e)
            {
                
            }

            public override void Draw(Canvas canvas)
            {

            }

            public override void SetAlpha(int alpha)
            {

            }

            public override void SetColorFilter(ColorFilter colorFilter)
            {

            }
        }
    }
}