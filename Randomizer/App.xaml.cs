﻿using System.Windows;
using Randomizer.Managers;

namespace Randomizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StationManager.Initialize();
        }
    }
}
