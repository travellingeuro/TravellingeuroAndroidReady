﻿using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace travellingeuro.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresentationPage : ContentPage
    {
        public PresentationPage()
        {
            InitializeComponent();
        }
    }
}
