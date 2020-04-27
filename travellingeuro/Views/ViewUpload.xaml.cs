using Xamarin.Forms;

namespace travellingeuro.Views
{
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
