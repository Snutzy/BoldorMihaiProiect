using SQLite;
using SQLiteNetExtensions.Attributes;
using BoldorMihai.Models;
using BoldorMihai.Data;

namespace BoldorMihai;

public partial class Preparate : ContentPage
{
    ShopList sl;
    public Preparate(ShopList slist)
    {
        InitializeComponent();
        sl = slist;
        BindingContext = new Product();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = new Product { Description = (BindingContext as Product)?.Description };

        if (!string.IsNullOrEmpty(product.Description))
        {
            await App.Database.SaveProductAsync(product);

            var updatedProducts = await App.Database.GetProductsAsync();
            listView.ItemsSource = null;
            listView.ItemsSource = updatedProducts;

            (BindingContext as Product).Description = string.Empty;
        }
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var selectedProduct = listView.SelectedItem as Product;
        if (selectedProduct != null)
        {
            System.Diagnostics.Debug.WriteLine("Deleting product: " + selectedProduct.Description);
            await App.Database.DeleteProductAsync(selectedProduct);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            var lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };
            await App.Database.SaveListProductAsync(lp);
            p.ListProducts = new List<ListProduct> { lp };
            await Navigation.PopAsync();
        }
    }
}