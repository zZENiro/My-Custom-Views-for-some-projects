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
using Custom_Views.CustomTemplates;
using Xamarin.Forms.Platform.Android;
using Custom_Views.Droid.Renderers;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;

//[assembly: ExportRenderer(typeof(RoundButton), typeof(RoundButtonRenderer))]
namespace Custom_Views.Droid.Renderers
{
    [Obsolete]
    public class RoundButtonRenderer : ButtonRenderer
    {
        //RoundButton _targetView;
        //RoundButtonDrawable _targetDrawable;

        public RoundButtonRenderer()
        { }

        public RoundButtonRenderer(Context context) : base(context)
        { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName || e.PropertyName == Frame.CornerRadiusProperty.PropertyName)
            {
                //UpdateBackground();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && e.OldElement == null)
            {
                //_targetView = e.NewElement as RoundButton;
                //UpdateBackground();
            }

        }

        //void UpdateBackground()
        //{
        //    Control.SetBackgroundDrawable(_targetDrawable = new RoundButtonDrawable(_targetView, Context.ToPixels));
        //    _targetDrawable?.Dispose();
        //}


        //class RoundButtonDrawable : Drawable
        //{
        //    readonly Func<double, float> _convertToPixels;
        //    RoundButton _targetView;

        //    Bitmap _normalBitmap;
        //    bool _isDisposed;

        //    public override int Opacity => 0;

        //    public RoundButtonDrawable(RoundButton targetView, Func<double, float> convertToPixels) =>
        //        (_targetView, _convertToPixels) = (targetView, convertToPixels);

        //    private void DrawBackground(Canvas canvas, int width, int height, object radius, bool pressed)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    private void DrawOutline(Canvas canvas, int width, int height, object radius)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override void Draw(Canvas canvas)
        //    {
        //        int width = Bounds.Width();
        //        int height = Bounds.Height();

        //        if (width <= 0 || height <= 0)
        //        {
        //            if (_normalBitmap != null)
        //            {
        //                _normalBitmap.Dispose();
        //                _normalBitmap = null;
        //            }
        //            return;
        //        }

        //        if (_normalBitmap == null || _normalBitmap.Height != height || _normalBitmap.Width != width)
        //        {
        //            if (_normalBitmap != null)
        //            {
        //                _normalBitmap.Dispose();
        //                _normalBitmap = null;
        //            }

        //            _normalBitmap = CreateBitmap(false, width, height);
        //        }
        //        Bitmap bitmap = _normalBitmap;
        //        using (var paint = new Paint())
        //            canvas.DrawBitmap(bitmap, 0, 0, paint);
        //    }

        //    void DrawCanvas(Canvas canvas, int width, int height, bool pressed)
        //    {
        //        if (_targetView.CornerRadius == -1)
        //            _targetView.CornerRadius = 0;

        //        DrawOutline(canvas, width, height, _targetView.CornerRadius);
        //        DrawBackground(canvas, width, height, _targetView.CornerRadius, pressed);
        //    }

        //    private Bitmap CreateBitmap(bool pressed, int width, int height)
        //    {
        //        Bitmap bitmap;
        //        using (Bitmap.Config config = Bitmap.Config.Argb8888)
        //            bitmap = Bitmap.CreateBitmap(width, height, config);

        //        using (var canvas = new Android.Graphics.Canvas(bitmap))
        //        {
        //            DrawCanvas(canvas, width, height, pressed);
        //        }

        //        return bitmap;
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing && !_isDisposed)
        //        {
        //            if (_normalBitmap != null)
        //            {
        //                _normalBitmap.Dispose();
        //                _normalBitmap = null;
        //            }

        //            _isDisposed = true;
        //        }

        //        base.Dispose(disposing);
        //    }

        //    public override void SetAlpha(int alpha) { }

        //    public override void SetColorFilter(ColorFilter colorFilter) { }
        //}
    }
}