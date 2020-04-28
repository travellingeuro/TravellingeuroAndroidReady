using Syncfusion.SfMaps.XForms;
using System;
using Xamarin.Forms;

namespace travellingeuro.Controls
{
    public class CustomMarker : MapMarker
    {
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public ImageSource Image { get; set; }
    }

}

