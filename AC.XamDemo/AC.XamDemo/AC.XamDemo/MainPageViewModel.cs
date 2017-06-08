using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Autofac;

namespace AC.XamDemo
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<API_Result> _serviceCallResults;
        public ObservableCollection<API_Result> ServiceCallResults
        {
            get { return _serviceCallResults; }
            set
            {
                if (_serviceCallResults != value)
                {
                    _serviceCallResults = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainPageViewModel()    // In your app,  inject IAdaptiveClient in the constructor of your ViewModel 
        {
            ServiceCallResults = new ObservableCollection<API_Result>();
        }

        public void InitalizeViewModel()
        {
            // In your application you will want to inject IAdaptiveClient into your ViewModels much the same way as is
            // done in the Demo class in this project.
            // Because this is a demo, we want to create conditions where Adaptive client can demonstrate its fall back capabilities.
            // We register the components of this application as well as some mocks to accomplish that goal.

            // First pass - connect to SQL box on LAN:

            using (var scope = new ContainerBuilder().Build().BeginLifetimeScope(builder => AutofacHelper.RegisterComponents(builder)))
            {
                Demo demo = scope.Resolve<Demo>();
                API_Result result = demo.Simulate_API_Calls();
                result.Title = "First pass - connect to SQL box on local area network:";
                ServiceCallResults.Add(result);
            }


            // Second pass - simulate no LAN connectivity, fall back to WebAPI server:

            using (var scope = new ContainerBuilder().Build().BeginLifetimeScope(builder => AutofacHelper.RegisterMocks(builder)))
            {
                Demo demo = scope.Resolve<Demo>();
                API_Result result = demo.Simulate_API_Calls();
                result.Title = "Second pass - simulate no LAN connectivity, fall back to WebAPI server:";
                ServiceCallResults.Add(result);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
