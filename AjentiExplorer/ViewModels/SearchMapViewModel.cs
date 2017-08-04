using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class SearchMapViewModel: BaseSearchViewModel
    {
        public SearchMapViewModel()
        {
			this.Title = "Map";

			this.MenuItems = new ObservableRangeCollection<DrawerMenuItem>
			{
				new DrawerMenuItem { Title = "Map", Current= true },
				new DrawerMenuItem { Title = "Search" },
				new DrawerMenuItem { Title = "Favourites" },
				new DrawerMenuItem { Title = "Logout" },
			};
        }
    }
}
