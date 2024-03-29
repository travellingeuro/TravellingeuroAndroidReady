﻿using Syncfusion.SfMaps.XForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace travellingeuro.Controls
{
    [Preserve(AllMembers = true)]
    public class CustomMarker : MapMarker
    {
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public ImageSource Image { get; set; }
    }

}

