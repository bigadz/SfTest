using System;
using System.Threading.Tasks;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;

using Syncfusion.ListView.XForms;
using Syncfusion.DataSource;

namespace AjentiExplorer.Views
{
	public class SearchListPage : BaseContentPage
	{
		private SearchListViewModel viewModel;

		public SearchListPage(SearchListViewModel viewModel)
		{
			BindingContext = this.viewModel = viewModel;
			this.viewModel.Navigation = this.Navigation;

			SetBinding(TitleProperty, new Binding("Title"));

			NavigationPage.SetHasNavigationBar(this, false);

			// Listen for messages from the modelview(s)
			MessagingCenter.Subscribe<InRangeViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);
			MessagingCenter.Subscribe<BaseSearchViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);
			MessagingCenter.Subscribe<LocationViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);

			var navigationDrawer = LayoutFactories.NavigationDrawer.Create(viewModel);
			Content = navigationDrawer;


            var listViewGridLayout = new GridLayout
			{
				SpanCount = Device.Idiom == TargetIdiom.Phone ? 1 : 2,
			};
			
			var listView = new SfListView
			{
				LayoutManager = listViewGridLayout,
                ItemsSource = this.viewModel.LocationViewModels,
                ItemTemplate = new DataTemplate(typeof(Cells.LocationCell)),
                ItemSpacing = new Thickness(2),
                ItemSize = 204,
                RowSpacing = 4,
                ColumnSpacing = 4,
                SelectionMode = SelectionMode.None,
			};
			//listView.DataSource.SortDescriptors.Add(new SortDescriptor("Title")); -- Crashes on device

			listView.ItemTapped += SfListView_ItemTapped;


			//var listView = new ListView
			//{
   //             ItemsSource = this.viewModel.LocationViewModels,
			//	ItemTemplate = new DataTemplate(typeof(Cells.LocationCell)),
   //             RowHeight = 210,
			//};
            //listView.ItemTapped += ListView_ItemTapped;

			var layoutGrid = new Grid
			{
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				}
			};
			layoutGrid.Children.Add(LayoutFactories.NavigationDrawer.NavigationBar, 0, 1, 0, 1);
            layoutGrid.Children.Add(listView, 0, 1, 1, 2);
			layoutGrid.Children.Add(LayoutFactories.BusyIndicator.Create(viewModel), 0, 1, 0, 2);
			navigationDrawer.ContentView = layoutGrid;

			this.Appearing += async (sender, e) =>
			{
				NavigationPage.SetHasNavigationBar(this, false);

                if (this.viewModel.LocationViewModels.Count > 0)
                    return;

				// If Searching....
				this.viewModel.SearchString = "FISH";

				await this.viewModel.SearchAsync();
			};

            this.Disappearing += (sender, e) =>
            {
                if (App.SwitchingTopLevelPages)
                {
                    var navDrawer = this.Content as Syncfusion.SfNavigationDrawer.XForms.SfNavigationDrawer;
                    LayoutFactories.NavigationDrawer.ReleaseNavigationDrawer(navDrawer);
                    this.Content = null;
                }
                else
                {
                    NavigationPage.SetHasNavigationBar(this, true);
                }
            };
		}

        async void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new LocationPage(e.ItemData as LocationViewModel));
        }

        async void ListView_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new LocationPage(e.Item as LocationViewModel));
        }
    }
}

