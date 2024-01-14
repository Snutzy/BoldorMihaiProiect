using SQLite;
using SQLiteNetExtensions.Attributes;
using BoldorMihai.Models;
using BoldorMihai.Data;

namespace BoldorMihai;
public partial class InfoComanda : ContentPage
{
    public InfoComanda()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnAddPButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPrepComanda((ShopList)
        this.BindingContext)
        {
            BindingContext = new Product()
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
    async void OnDelPButtonClicked(object sender, EventArgs e)
    {
        var selectedProduct = listView.SelectedItem as Product;
        if (selectedProduct != null)
        {
            await App.Database.DeleteProductAsync(selectedProduct);
            await ReloadListView();
        }
    }
    async Task ReloadListView()
    {
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}