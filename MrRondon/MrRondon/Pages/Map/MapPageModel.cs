﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MrRondon.Pages.Map
{
    public class MapPageModel : BasePageModel
    {
        public MapPageModel()
        {
            Title = Constants.AppName;
            LoadPinsCommand = new Command(async () => await ExecuteLoadPins());
            SetActualCityCommand = new Command<string>(async (address) => await SetActualCity(address));
        }

        public ICommand LoadPinsCommand { get; set; }
        public ICommand SetActualCityCommand { get; set; }

        private List<Pin> _pins = new List<Pin>();
        public List<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private async Task ExecuteLoadPins()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                var service = new EventService();
                var events = await service.GetNearbyAsync();

                foreach (var item in events)
                {
                    var pin = new Pin
                    {
                        Id = item.AddressId,
                        Label = item.Name,
                        Address = item.Address.FullAddress,
                        Type = PinType.Place,
                        Position = new Position(item.Address.Latitude, item.Address.Longitude)
                    };

                    Pins.Add(pin);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await NavigationService.PushAsync(new ErrorPage(new ErrorPageModel(ex.Message, Title) { IsLoading = false }));
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        public async Task SetActualCity(string cityName)
        {
            cityName = string.IsNullOrWhiteSpace(cityName) ? Constants.DefaultSetting.City.Name : cityName;

            var cityService = new CityService();
            var cities = await cityService.GetAsync(cityName.Trim());
            var city = cities.FirstOrDefault() ?? Constants.DefaultSetting.City;
            CurrentCity = city;
            ApplicationManager<City>.AddOrUpdate("city", city);
        }
    }
}