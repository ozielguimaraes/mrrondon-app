﻿using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class BasePageModel : ObservableObject
    {
        protected IMessageService MessageService;
        protected INavigationService NavigationService;

        private string _title = Constants.AppName;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        private City City => Account.Current.City;

        protected BasePageModel()
        {
            IsPresented = false;
            IsLoading = false;
            Title = Constants.AppName;
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }
    }
}