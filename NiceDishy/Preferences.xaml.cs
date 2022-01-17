using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NiceDishy
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : Window
    {
        public Preferences()
        {
            InitializeComponent();

            slFreqSendingData.Value = FreqSendingData;
            slFreqSpeedTests.Value = FreqSpeedTests;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            FreqSendingData = Convert.ToInt32(slFreqSendingData.Value); ;
            FreqSpeedTests = Convert.ToInt32(slFreqSpeedTests.Value); ;

            Close();
        }

        #region Preferences
        // Frequency for sending data in minute
        public static int FreqSendingData
        {
            set
            {
                RegistryKey subKey = Registry.CurrentUser.OpenSubKey("Software", true);
                using (var key = subKey.CreateSubKey("NiceDishy"))
                {
                    key.SetValue("freqSendingData", value);
                }
            }

            get
            {
                RegistryKey subKey = Registry.CurrentUser.OpenSubKey("Software", true);
                using (var key = subKey.CreateSubKey("NiceDishy"))
                {
                    var value = key.GetValue("freqSendingData");
                    if (value == null)
                        return 5;
                    return Convert.ToInt32(value);
                }
            }
        }

        // Frequency for running speed tests in minute
        public static int FreqSpeedTests
        {
            set
            {
                RegistryKey subKey = Registry.CurrentUser.OpenSubKey("Software", true);
                using (var key = subKey.CreateSubKey("NiceDishy"))
                {
                    key.SetValue("freqSpeedTests", value);
                }
            }

            get
            {
                RegistryKey subKey = Registry.CurrentUser.OpenSubKey("Software", true);
                using (var key = subKey.CreateSubKey("NiceDishy"))
                {
                    var value = key.GetValue("freqSpeedTests");
                    if (value == null)
                        return 60;
                    return Convert.ToInt32(value);
                }
            }
        }
        #endregion
    }
}
