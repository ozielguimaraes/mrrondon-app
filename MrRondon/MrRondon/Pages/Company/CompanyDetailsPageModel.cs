﻿using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace MrRondon.Pages.Company
{
    public class CompanyDetailsPageModel : BasePageModel
    {
        public CompanyDetailsPageModel(Entities.Company model)
        {
            Title = model.Name;
            Company = model;
            OpenMapCommand = new Command(OpenMap);
            MarkAsFavoriteCommand = new Command(async () => await MarkAsFavorite());
            ShareCommand = new Command(async () => await Share());
        }

        public ICommand OpenMapCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand MarkAsFavoriteCommand { get; set; }

        private Entities.Company _company;
        public Entities.Company Company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        private void OpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(Company.Name, Company.Address.Latitude, Company.Address.Longitude, NavigationType.Driving);
        }

        private async Task MarkAsFavorite()
        {
            await MessageService.ToastAsync($"Ainda não implementado \n{Company.CompanyId}");
        }

        private async Task Share()
        {
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nEmpresa: {Company.Name}\nLocal: {Company.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)\n",
                Url = "https://play.google.com/store/apps/details?id=br.gov.ro.setur.mrrondon"
            };
            await CrossShare.Current.Share(message);
        }
    }
}