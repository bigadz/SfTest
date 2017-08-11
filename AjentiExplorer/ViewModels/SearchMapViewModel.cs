using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class SearchMapViewModel: BaseSearchViewModel
    {
        public SearchMapViewModel()
        {
			this.Title = "Map";

            this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Map);

		}
    }
}
