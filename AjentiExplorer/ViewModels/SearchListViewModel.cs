using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class SearchListViewModel: BaseSearchViewModel
    {
        public SearchListViewModel()
        {
			this.Title = "Search";

            this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Search);

		}
    }
}
