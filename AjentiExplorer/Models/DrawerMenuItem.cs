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
    }
}
