using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Custom_Views.Droid.Renderers;
using Custom_Views.CustomTemplates;
using Android.Graphics.Drawables;
using Android.Graphics;
using System.ComponentModel;

using AButton = Android.Widget.Button;
using ACanvas = Android.Graphics.Canvas;
using GlobalResource = Android.Resource;

[assembly: ExportRenderer(typeof(SlideFrame), typeof(SlideFrameRenderer))]
namespace Custom_Views.Droid.Renderers
{
    [Obsolete]
    public class SlideFrameRenderer : VisualElementRenderer<Frame>
    {
        float _cornerRadius;
        SlideFrame _targetType;
        CustomFrameDrawable _drawable;

        public SlideFrameRenderer(Context context) : base(context)
        { }

        public override bool OnTouchEvent(MotionEvent e) => (base.OnTouchEvent(e)) ? true : false;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);


            if (e.NewElement != null && e.OldElement == null)
            {
                _targetType = e.NewElement as SlideFrame;
                // обратиться к раудиусу
                // присвоить к _cornerRadius
                // присвоить к Element.CornerRadius = _cornerRadius

                UpdateBackground();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName || e.PropertyName == Frame.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
        }

        void UpdateBackground()
        {
            _drawable?.Dispose();
            this.SetBackground(_drawable = new CustomFrameDrawable(_targetType, Context.ToPixels));
        }

        class CustomFrameDrawable : Drawable
        {
            const int _bounds = 20;

            readonly SlideFrame _frame;
            readonly Func<double, float> _convertToPixels;

            float _radius;
            float _frameThickness;
            float _contentBounds;
            float _boundsDiff;
            bool _isDisposed;
            Bitmap _normalBitmap;

            public CustomFrameDrawable(SlideFrame frame, Func<double, float> convertToPixels)
            {
                // _frame - основной объект, с которым производится логика прорисовки, т.к. он ссылается на свойство Element рендерера
                _frame = frame;

                _radius = _frame.Radius;
                _frameThickness = _frame.FrameThickness;
                _contentBounds = _frame.ContentBounds;
                _boundsDiff = _frame.FrameDifference;

                _convertToPixels = convertToPixels;

                // Когда в рендере срабатывает метод OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) или  OnElementChanged(ElementChangedEventArgs<Frame> e),
                // тогда вызывается event PropertyChanged. 
                // Присвоим для него отрисовку (FrameOnPropertyChanged). 
                frame.PropertyChanged += FrameOnPropertyChanged; 
            }

            public override int Opacity => 0;

            public override bool IsStateful => false;

            public override void Draw(ACanvas canvas)
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

            public override void SetAlpha(int alpha)
            {

            }

            public override void SetColorFilter(ColorFilter colorFilter)
            {

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

            protected override bool OnStateChange(int[] state)
            {
                return false;
            }

            Bitmap CreateBitmap(bool pressed, int width, int height)
            {
                Bitmap bitmap;
                using (Bitmap.Config config = Bitmap.Config.Argb8888)
                    bitmap = Bitmap.CreateBitmap(width, height, config);

                using (var canvas = new ACanvas(bitmap))
                {
                    DrawCanvas(canvas, width, height, pressed);
                }

                return bitmap;
            }

            void DrawBackground(ACanvas canvas, int width, int height, float cornerRadius, bool pressed)
            {
                using (var paint = new Paint { AntiAlias = true })
                using (var path = new Path())
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Fill)
                using (var rect = new RectF(_bounds + _boundsDiff + _contentBounds, _bounds + _boundsDiff + _contentBounds, 
                    width - (_bounds + _boundsDiff + _contentBounds), height - (_bounds + _boundsDiff + _contentBounds)))
                {
                    float rx = _convertToPixels(cornerRadius);
                    float ry = _convertToPixels(cornerRadius);

                    path.AddRoundRect(rect, rx, ry, direction);

                    global::Android.Graphics.Color color = _frame.BackgroundColor.ToAndroid();

                    paint.SetStyle(style);
                    paint.Color = color;

                    canvas.DrawPath(path, paint);
                }
            }

            void DrawOutline(ACanvas canvas, int width, int height, float cornerRadius)
            {
                using (var paint = new Paint { AntiAlias = true })
                using (var path = new Path())
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Stroke)
                using (var rect = new RectF(left: _bounds, top: _bounds, right: width - _bounds, bottom: height - _bounds))
                using (var innerRect = new RectF(
                    left: _bounds + _boundsDiff,
                    top: _bounds + _boundsDiff,
                    right: width - (_bounds + _boundsDiff),
                    bottom: height - (_bounds + _boundsDiff)))
                {
                    float rx = _convertToPixels(cornerRadius);
                    float ry = _convertToPixels(cornerRadius);
                    path.AddRoundRect(rect, rx, ry, direction);
                    path.AddRoundRect(innerRect, rx, ry, direction);

                    paint.StrokeWidth = _frameThickness; 
                    paint.SetStyle(style);
                    paint.Color = _frame.BorderColor.ToAndroid();

                    canvas.DrawPath(path, paint);
                }
            }

            // обновление рисунка
            void FrameOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName
                    || e.PropertyName == Frame.BorderColorProperty.PropertyName
                    || e.PropertyName == Frame.CornerRadiusProperty.PropertyName)
                {
                    if (_normalBitmap == null)
                        return;

                    using (var canvas = new ACanvas(_normalBitmap))
                    {
                        int width = Bounds.Width();
                        int height = Bounds.Height();
                        canvas.DrawColor(global::Android.Graphics.Color.Black, PorterDuff.Mode.Clear);
                        DrawCanvas(canvas, width, height, false);
                    }
                    InvalidateSelf();
                }
            }

            void DrawCanvas(ACanvas canvas, int width, int height, bool pressed)
            {
                if (_radius == -1f)
                    _radius = 0f; // default corner radius

                DrawBackground(canvas, width, height, _radius, pressed);
                DrawOutline(canvas, width, height, _radius);
            }
        }
    }
}