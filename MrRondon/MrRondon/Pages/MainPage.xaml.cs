﻿using MrRondon.Pages.Category;
using MrRondon.Pages.Event;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            CurrentPageChanged += (sender, e) =>
            {
                var numPage = Children.IndexOf(CurrentPage);

                switch (numPage)
                {
                    case 0: //EXPLORE 
                        //var categoryPageModel = new ListCategoriesPageModel(); 
                        //categoryPageModel.LoadItemsCommand.Execute(null);
                        //
                        //CurrentPage.BindingContext = categoryPageModel;
                        return;
                    case 1: //EVENTS
                        
                            //var eventPageModel = new ListEventPageModel();
                            //eventPageModel.LoadCitiesCommand.Execute(null);
                            //eventPageModel.LoadItemsCommand.Execute(null);
                            //
                            //CurrentPage.BindingContext = eventPageModel;
                            //return;
                        
                    case 2: //MAP
                    default: return;
                }
            };
        }
    }
}