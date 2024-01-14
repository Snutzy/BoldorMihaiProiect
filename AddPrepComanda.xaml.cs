using SQLite;
using SQLiteNetExtensions.Attributes;
using BoldorMihai.Models;
using BoldorMihai.Data;

namespace BoldorMihai;

public partial class AddPrepComanda : ContentPage
{
    ShopList sl;
    public AddPrepComanda(ShopList slist)
    {
        InitializeComponent();
        sl = slist;
        LoadProducts();
    }
    async void LoadProducts()
    {
        List<Product> products = await App.Database.GetProductsAsync();
        listView.ItemsSource = products;
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product selectedProduct = listView.SelectedItem as Product;

        if (selectedProduct != null)
        {
            ListProduct lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = selectedProduct.ID
            };

            await App.Database.SaveListProductAsync(lp);
            selectedProduct.ListProducts = new List<ListProduct> { lp };

            await Navigation.PopAsync();
        }
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        Product selectedProduct = listView.SelectedItem as Product;

        if (selectedProduct != null)
        {
            ListProduct lp = selectedProduct.ListProducts.FirstOrDefault(p => p.ShopListID == sl.ID);
            if (lp != null)
            {
                await App.Database.DeleteListProductAsync(lp);
                selectedProduct.ListProducts.Remove(lp);
            }
        }
    }
}