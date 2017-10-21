using System;
namespace AjentiExplorer.Models
{
    public class DrawerMenuItem: ObservableObject
    {
		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

        bool current = false;
		public bool Current
		{
			get { return current; }
			set { SetProperty(ref current, value); }
		}

		//public DrawerMenuItem(string title)
		//{
		//    this.Title = title;
		//}
		public DrawerMenuItem()
		{
		}



        public override string ToString()
        {
            return this.Title;
        }

        public enum MenuItems
        {
            Collections,
            Favourites,
            Map,
            Recent,
            Search,
            Logout
        };

		public static ObservableRangeCollection<DrawerMenuItem> CollectionFor(MenuItems currentItem)
		{
			var collection = new ObservableRangeCollection<DrawerMenuItem>
			{
				//new DrawerMenuItem { Title = "Collections", Current = currentItem.Equals(MenuItems.Collections) },
				new DrawerMenuItem { Title = "Favourites", Current = currentItem.Equals(MenuItems.Favourites) },
				new DrawerMenuItem { Title = "Map", Current = currentItem.Equals(MenuItems.Map) },
				new DrawerMenuItem { Title = "Recent", Current = currentItem.Equals(MenuItems.Recent) },
				new DrawerMenuItem { Title = "Search", Current = currentItem.Equals(MenuItems.Search) },
				new DrawerMenuItem { Title = "Logout" },
			};
			return collection;
		}

    }
}
