using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class RecentLocationsViewModel: BaseViewModel
    {
        public RecentLocationsViewModel()
        {
			this.Title = "Recent";

            this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Recent);


		}
    }
}
