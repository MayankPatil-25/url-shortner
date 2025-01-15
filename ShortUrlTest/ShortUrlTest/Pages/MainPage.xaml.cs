using ShortUrlTest.Models;
using ShortUrlTest.Services.LocalStorage;
using ShortUrlTest.ViewModels;

namespace ShortUrlTest;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		
		BindingContext = new MainViewModel();
	}
	
	private async void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedItem = (sender as CollectionView)?.SelectedItem;
		if (selectedItem != null && selectedItem is UrlItem item)
		{
			await DisplayAlert("Full Original Url!", item.OriginalUrl, "Ok");
			(sender as CollectionView).SelectedItem = null;
		}
	}
}