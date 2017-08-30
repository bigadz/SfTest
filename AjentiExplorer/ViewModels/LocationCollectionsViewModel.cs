using System;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
	public class LocationCollectionsViewModel : BaseViewModel
	{
		public LocationCollectionsViewModel()
		{
			this.Title = "Collections";

            this.MenuItems = DrawerMenuItem.CollectionFor(DrawerMenuItem.MenuItems.Collections);

		}

    }
}
