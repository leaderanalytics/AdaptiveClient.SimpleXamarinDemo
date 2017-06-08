using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace AC.XamDemo
{
	public partial class MainPage : ContentPage
	{
		public MainPage(MainPageViewModel vm)
		{
			InitializeComponent();
            vm.InitalizeViewModel();
            BindingContext = vm;
        }
    }
}
