using BoldorMihai.Models;

namespace BoldorMihai
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            List<string> pickerItems = new List<string>
            {
                "Comenzi restaurant",
                "Modifica preparatele",
            };

            SchimbaMenu.ItemsSource = pickerItems;
        }

        private void MainMenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = SchimbaMenu.SelectedItem as string;

            switch (selectedItem)
            {
                case "Comenzi restaurant":
                    Navigation.PushAsync(new Comenzi());
                    break;

                case "Modifica preparatele":
                    ShopList shopList = new ShopList();
                    Navigation.PushAsync(new Preparate(shopList));
                    break;


                default:
                    break;
            }
        }
    }
}