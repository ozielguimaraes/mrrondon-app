﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Extensions;
using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class ConfigurationPageModel : BasePageModel
    {
        public ConfigurationPageModel()
        {
            Title = "Configurações";

            var until = ApplicationManager<object>.Find("PlaceUntil");

            var defaultValue = EnumExtensions.GetEnumAttribute(AccountManager.DefaultSetting.PlaceUntilOption);
            var value = until == null ? double.Parse(defaultValue.KeyValue) : double.Parse(until.ToString());
            SetValue(value);
            ItemSelectedCommand = new Command<DistanceOptions>(ExecuteItemSelected);
        }

        private List<DistanceOptions> _items;
        public List<DistanceOptions> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand ItemSelectedCommand { get; set; }

        public void SetValue(double until)
        {
            var values = EnumExtensions.ConvertEnumToList<PlaceUntilOption>().ToList();
            Items = new List<DistanceOptions>();
            foreach (var item in values)
            {
                var distance = double.Parse(item.KeyValue);
                var isTheSame = distance.Equals(until);
                var distanceOption = new DistanceOptions(distance, item.Description, isTheSame);
                Items.Add(distanceOption);
            }
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", until);
        }

        private void ExecuteItemSelected(DistanceOptions item)
        {
            SetValue(item.Distance);
        }
    }

    public class DistanceOptions
    {
        public DistanceOptions(double distance, string description, bool isChecked)
        {
            Distance = distance;
            Description = description;
            Icon = isChecked ? "check" : string.Empty;
        }

        public double Distance { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
    }
}