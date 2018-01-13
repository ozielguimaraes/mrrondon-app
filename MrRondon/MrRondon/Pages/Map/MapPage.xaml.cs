﻿using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MrRondon.Pages.Map
{
    public partial class MapPage : ContentPage
    {
        private readonly MapaPageModel _pageModel;

        public MapPage()
        {
            InitializeComponent();
            if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new MapaPageModel();
            else _pageModel = (MapaPageModel)BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_pageModel.Pins.Count == 0) _pageModel.LoadPinsCommand.Execute(null);

            var portoVelhoPosition = new Position(-8.756544, -63.8899265);
            Companies.MoveToRegion(MapSpan.FromCenterAndRadius(portoVelhoPosition, Distance.FromMiles(3)));

            foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);
        }
    }
}