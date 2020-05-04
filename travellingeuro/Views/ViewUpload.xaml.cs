using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace travellingeuro.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewUpload : ContentPage
    {
        public ViewUpload()
        {
            InitializeComponent();
        }

        private void Listviewnotes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listviewnotes.SelectedItem = null;
        }
    }
}
