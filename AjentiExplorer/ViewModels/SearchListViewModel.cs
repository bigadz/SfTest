using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class SearchListViewModel: BaseSearchViewModel
    {
        public SearchListViewModel()
        {
			this.Title = "Search";

			this.MenuItems = new ObservableRangeCollection<DrawerMenuItem>
			{
				new DrawerMenuItem { Title = "Map" },
				new DrawerMenuItem { Title = "Search", Current= true  },
				new DrawerMenuItem { Title = "Favourites" },
				new DrawerMenuItem { Title = "Logout" },
			};
        }
    }
}
