﻿using Syncfusion.SfMaps.XForms;
using System.Collections.Generic;
using System.Linq;
using travellingeuro.Helper;
using Xamarin.Forms;

namespace travellingeuro.Views
{
    public partial class Stats : ContentPage
    {
        public Stats()
        {
            InitializeComponent();
        }
        private void ValuePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

            var latitudes = new List<double>();
            var longitudes = new List<double>();


            if (map.Markers != null)
            {
                foreach (MapMarker marker in map.Markers)
                {
                    latitudes.Add(double.Parse(marker.Latitude, System.Globalization.CultureInfo.InvariantCulture));
                    longitudes.Add(double.Parse(marker.Longitude, System.Globalization.CultureInfo.InvariantCulture));
                }
            }

            double lowestLat = latitudes.Min();
            double highestLat = latitudes.Max();
            double lowestLong = longitudes.Min();
            double highestLong = longitudes.Max();
            double finalLat = (lowestLat + highestLat) / 2;
            double finalLong = (lowestLong + highestLong) / 2;
            double distance = DistanceCalculator.GeoCodeCalc.CalcDistance(lowestLat, lowestLong, highestLat, highestLong, DistanceCalculator.GeoCodeCalcMeasurement.Kilometers);

            map.GeoCoordinates = new Point(finalLat, finalLong);
            map.Radius = distance;
            map.DistanceType = DistanceType.KiloMeter;

        }
    }
}
