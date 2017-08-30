using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class LocationCollectionViewModel : BaseViewModel
    {
        public LocationCollectionViewModel()
        {
			this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Collections);

		}
    }
}
