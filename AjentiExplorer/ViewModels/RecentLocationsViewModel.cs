using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class RecentLocationsViewModel: BaseViewModel
    {
        public RecentLocationsViewModel()
        {
			this.Title = "Recent";

			this.MenuItems = new ObservableRangeCollection<DrawerMenuItem>
			{
				new DrawerMenuItem { Title = "Map" },
				new DrawerMenuItem { Title = "Search" },
				new DrawerMenuItem { Title = "Recent", Current = true },
				new DrawerMenuItem { Title = "Favourites" },
				new DrawerMenuItem { Title = "Logout" },
			};

		}
    }
}
