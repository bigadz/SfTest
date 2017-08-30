using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class FavouriteLocationsViewModel: BaseViewModel
    {
        public FavouriteLocationsViewModel()
        {
			this.Title = "Favourites";

            this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Favourites);


		}
    }
}
