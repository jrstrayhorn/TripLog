using System;
using System.Collections.Generic;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class MainPage : ContentPage
    {
        public MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }

        public MainPage()
        {
            BindingContext = new MainViewModel(DependencyService.Get<INavService>());

            var newButton = new ToolbarItem
            {
                Text = "New"
            };

            newButton.SetBinding(ToolbarItem.CommandProperty, "NewCommand");

            ToolbarItems.Add(newButton);

            Title = "TripLog";

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            var entries = new ListView
            {
                ItemTemplate = itemTemplate
            };

            entries.SetBinding(ListView.ItemsSourceProperty, "LogEntries");

            entries.ItemTapped += (sender, e) =>
            {
                var item = (TripLogEntry)e.Item;
                _vm.ViewCommand.Execute(item);
            };

            Content = entries;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Initialize MainViewModel
            if (_vm != null)
                await _vm.Init();
        }
    }
}

