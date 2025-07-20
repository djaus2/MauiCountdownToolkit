using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCountdownToolkit
{
    public static class CountdownIcons
    {
        public static ImageSource? GetEmbeddedIcon(string name)
        {
            var assembly = typeof(CountdownIcons).Assembly;
            var resourceName = $"MauiCountdownToolkit.Resources.Images.{name}.svg";

            var stream = assembly.GetManifestResourceStream(resourceName);
            return stream != null
                ? ImageSource.FromStream(() => stream)
                : null;
        }
    }
}

