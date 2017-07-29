using AjentiExplorer.Services;
using Xamarin.Forms;

namespace AjentiExplorer.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        protected IDataView dataViewApi = new DataViewApi();

        public INavigation Navigation { get; set;  }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }


		string busyMessage = string.Empty;
		public string BusyMessage
		{
			get { return busyMessage; }
            set { SetProperty(ref busyMessage, value); }
		}

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private ObservableRangeCollection<string> menuItems = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> MenuItems
        {
            get { return menuItems; }
            set { SetProperty(ref menuItems, value); }
        }
    }
}
