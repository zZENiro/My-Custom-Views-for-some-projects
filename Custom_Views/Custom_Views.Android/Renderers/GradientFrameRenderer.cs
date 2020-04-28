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
            this.SetBackground(_drawableElem = new GradientFrameDrawable(_targetElem, Context.ToPixels));
        }

        class GradientFrameDrawable : Drawable
        {
            #region

            readonly Func<double, float> _convertToPixels;

            GradientFrame _frame;
            Bitmap _normalBitmap;
            bool _isDisposed;

            public override bool IsStateful => false;

            public override int Opacity => 0;

            #endregion

            public GradientFrameDrawable(GradientFrame frame, Func<double, float> convertToPixels)
            {
                _frame = frame;
                _convertToPixels = convertToPixels;

                _frame.PropertyChanged += FrameOnPropChanged;
            }


            private void DrawOutline(Canvas canvas, int width, int height, float radius)
            {
                using (var painter = new Paint() { AntiAlias = true })
                using (var path = new Path())
                using (var direction = Path.Direction.Cw)
                using (var style = Paint.Style.Stroke)
                using (var outlineRect = new RectF(0, 0, width, height))
                {
                    float rx = _convertToPixels(radius);
                    float ry = _convertToPixels(radius);

                    path.AddRoundRect(outlineRect, rx, ry, direction);
                    painter.SetStyle(style);
                    painter.StrokeWidth = _frame.FrameThickness;
                    painter.Color = _frame.BorderColor.ToAndroid();
                    painter.Alpha = _frame.BorderAlpha;

                    canvas.DrawPath(path, painter);
                }
            }

            private void DrawBackground(Canvas canvas, int width, int height, float radius, bool pressed)
            {
                using (var painter = new Paint() { AntiAlias = true })
                using (var path = new Path())
                using (var direction = Path.Direction.Cw)
                using (var style = Paint.Style.Fill)
                using (var rect = new RectF(0, 0, width, height))
                {
                    var gradient = new LinearGradient(0, 0, 0, (float)height * 2, 
                        color0: _frame.StartColor.ToAndroid(),                              
                        color1: _frame.EndColor.ToAndroid(),                           
                        tile:   Shader.TileMode.Mirror);

                    float rx = _convertToPixels(radius);
                    float ry = _convertToPixels(radius);
                    path.AddRoundRect(rect, rx, rx, direction);

                    painter.Alpha = _frame.Alpha;
                    painter.SetShader(gradient);
                    painter.SetStyle(style);

                    // FIXME: Тени рисуется и видно её резкие грани, где она заканчивает рисоваться  
                    painter.SetShadowLayer(              
                        radius: _frame.ShadowRadius,
                        dx: _frame.ShadowDx,
                        dy: _frame.ShadowDy,
                        shadowColor: Android.Graphics.Color.Black);
                
                    canvas.DrawPath(path, painter);
                }
            }

            #region 

            Bitmap CreateBitmap(bool pressed, int width, int height)
            {
                Bitmap bitmap;
                using (Bitmap.Config config = Bitmap.Config.Argb8888)
                    bitmap = Bitmap.CreateBitmap(width, height, config);

                using (var canvas = new Android.Graphics.Canvas(bitmap))
                {
                    DrawCanvas(canvas, width, height, pressed);
                }

                return bitmap;
            }

            void DrawCanvas(Canvas canvas, int width, int height, bool pressed)
            {
                if (_frame.Radius == -1f)
                    _frame.Radius = 0f; 

                DrawBackground(canvas, width, height, _frame.Radius, pressed);
                DrawOutline(canvas, width, height, _frame.Radius);
            }

            public override void Draw(Canvas canvas)
            {
                int width = Bounds.Width();
                int height = Bounds.Height();

                if (width <= 0 || height <= 0)
                {
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }
                    return;
                }

                if (_normalBitmap == null || _normalBitmap.Height != height || _normalBitmap.Width != width)
                {
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }

                    _normalBitmap = CreateBitmap(false, width, height);
                }
                Bitmap bitmap = _normalBitmap;
                using (var paint = new Paint())
                    canvas.DrawBitmap(bitmap, 0, 0, paint);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && !_isDisposed)
                {
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }

                    _isDisposed = true;
                }

                base.Dispose(disposing);
            }

            private void FrameOnPropChanged(object sender, PropertyChangedEventArgs e)
            {

            }

            public override void SetAlpha(int alpha)
            {

            }

            public override void SetColorFilter(ColorFilter colorFilter)
            {

            }
            
            #endregion
        }
    }
}