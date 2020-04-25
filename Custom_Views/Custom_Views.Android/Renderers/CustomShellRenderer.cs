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
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Custom_Views.CustomShells;

[assembly: ExportRenderer(typeof(CustomShell), typeof(CustomShellRenderer))]
namespace Custom_Views.Droid.Renderers
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context) { }

        protected override void OnElementSet(Shell element) => base.OnElementSet(element);

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem) => base.CreateShellItemRenderer(shellItem);

        protected override IShellFlyoutRenderer CreateShellFlyoutRenderer() => base.CreateShellFlyoutRenderer();

        // FIXME: Нужно использовать внешние свойства класса 
        // CustomShell
        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {
            var flyout = base.CreateShellFlyoutContentRenderer();

            GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.BottomTop,
                new Int32[] {
                    App.LookupColor(Color.Blue).ToAndroid(),
                    App.LookupColor(Color.DeepSkyBlue).ToAndroid()
                }
            );

            var cl = ((CoordinatorLayout)flyout.AndroidView);
            cl.SetBackground(gradient);

            var g = (AppBarLayout)cl.GetChildAt(0);
            g.SetBackgroundColor(Color.Transparent.ToAndroid());
            g.OutlineProvider = null;

            var header = g.GetChildAt(0);
            header.SetBackgroundColor(Color.Transparent.ToAndroid());

            return flyout;
        }
    }
}