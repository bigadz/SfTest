using System;
using System.Collections.Generic;
using AjentiExplorer.ViewModels;

using Xamarin.Forms;

namespace AjentiExplorer
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
