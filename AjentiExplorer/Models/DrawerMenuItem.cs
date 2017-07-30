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
