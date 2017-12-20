using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using First_appl_MVVM.Data;

namespace First_appl_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      new WindowsHelper();
    }
  }
}
