using System;
namespace AjentiExplorer.Models
{
    public class Photo: ObservableObject
    {

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string url = string.Empty;
        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }
    }
}
