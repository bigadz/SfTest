using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class FavouriteLocationsViewModel: BaseViewModel
    {
        public FavouriteLocationsViewModel()
        {
			this.Title = "Favourites";

			this.MenuItems = new ObservableRangeCollection<DrawerMenuItem>
			{
				new DrawerMenuItem { Title = "Map" },
				new DrawerMenuItem { Title = "Search" },
				new DrawerMenuItem { Title = "Recent" },
				new DrawerMenuItem { Title = "Favourites", Current = true },
				new DrawerMenuItem { Title = "Logout" },
			};

		}
    }
}
