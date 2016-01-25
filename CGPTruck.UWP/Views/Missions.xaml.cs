﻿using CGPTruck.UWP.Core;
using CGPTruck.UWP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace CGPTruck.UWP.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Missions : Page
    {
        Settings s = Settings.getInstance();
        MainPage mainP = null;

        public Missions()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainP = (MainPage)e.Parameter;
            LoadMission();
            //Latitude = 44.78670251489458, Longitude = -0.6313490867614746
        }

        private void LoadMission()
        {
            if (s.actualMission != null)
            {
                Title.Text = s.actualMission.Name;
                textBlock1.Text = s.actualMission.Description;
            }
            else
            {
                Title.Text = "Aucune mission attribuée";
                textBlock1.Text = "Patientez jusqu'a l'attribution d'une mission par l'administrateur. Faite vous un café en attendant :D";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Visibility = Visibility.Collapsed;
            if (s.actualMission.Steps.Count == 0 || s.actualMission.Steps.Last().Step_Type == StepType.PickingUp)
                mainP.setDestination(new BasicGeoposition() { Latitude = s.actualMission.PickupPlace.Position.Latitude, Longitude = s.actualMission.PickupPlace.Position.Longitude });
            else
                mainP.setDestination(new BasicGeoposition() { Latitude = s.actualMission.DeliveryPlace.Position.Latitude, Longitude = s.actualMission.DeliveryPlace.Position.Longitude });
        }

        private async void Change_Status(object sender, RoutedEventArgs e)
        {
            string message = "";

            if (s.actualMission.Steps.Count == 0)
                message = "Vous allez démarrer la mission.\nContinuer ?";
            else if (s.actualMission.Steps.Last().Step_Type == StepType.PickingUp)
                message = "Vous allez signaler que vous recuperez le colis.\nContinuer ?";

            MessageDialog msg = new MessageDialog(message, "Changement de statut");
            msg.Commands.Add(new UICommand("Annuler", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            msg.Commands.Add(new UICommand("Valider", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            await msg.ShowAsync();

        }

        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Valider")
            {
                if (s.actualMission.Steps.Count == 0)
                {
                    Step _step = new Step();
                    _step.Date = DateTime.Now;
                    _step.Step_Type = StepType.PickingUp;
                    _step.StepNumber = 1;
                    _step.Position = new Position();
                    _step.Position.Latitude = mainP.actualPosition.Position.Latitude;
                    _step.Position.Longitude = mainP.actualPosition.Position.Longitude;
                    s.actualMission.Steps.Add(_step);
                }

                else if (true)
                    ;
            }
        }
    }
}
