using BoldorMihai.Models;
using BoldorMihai.Data;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BoldorMihai;

public partial class Comenzi : ContentPage
{
	public Comenzi()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopListsAsync();
    }
    async void OnShopListAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoComanda
        {
            BindingContext = new ShopList()
        });
    }
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new InfoComanda
            {
                BindingContext = e.SelectedItem as ShopList
            });
        }
    }
}