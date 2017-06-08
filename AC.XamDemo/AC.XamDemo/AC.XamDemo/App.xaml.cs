using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Autofac;

namespace AC.XamDemo
{
	public partial class App : Application
	{
        public IContainer Container;

        public App ()
		{
			InitializeComponent();

			//MainPage = new AC.XamDemo.MainPage();
		}

		protected override void OnStart ()
		{
            CreateContainer();
            //AC.XamDemo.MainPage window = Container.Resolve<AC.XamDemo.MainPage>();
            this.MainPage = Container.Resolve<AC.XamDemo.MainPage>();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<AC.XamDemo.MainPage>();
            builder.RegisterType<MainPageViewModel>();
            Container = builder.Build();
        }
    }
}
