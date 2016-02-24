using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Windows.Devices.Gpio;
namespace LedBlinking
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // variables
        private int state = 1;
        private const int LED1_PIN = 13;
        private const int LED2_PIN = 6;
        private const int LED3_PIN = 5;        
        private GpioPin gpio13;
        private GpioPin gpio6;
        private GpioPin gpio5;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();

            Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Cleanup
            gpio13.Dispose();
            gpio6.Dispose();
            gpio5.Dispose();
        }

        private void Timer_Tick(object sender, object e)
        {
            TurnOffAll();
            TurnOn(state);
            state++;
            if (state > 3)
                state = 1;
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                return;
            }

            // init gpio
            gpio13 = gpio.OpenPin(LED1_PIN);
            gpio13.SetDriveMode(GpioPinDriveMode.Output);

            gpio6 = gpio.OpenPin(LED2_PIN);
            gpio6.SetDriveMode(GpioPinDriveMode.Output);

            gpio5 = gpio.OpenPin(LED3_PIN);
            gpio5.SetDriveMode(GpioPinDriveMode.Output);

        }
        private void TurnOffAll()
        {
            gpio13.Write(GpioPinValue.Low);
            gpio6.Write(GpioPinValue.Low);
            gpio5.Write(GpioPinValue.Low);
        }
        private void TurnOn(int led)
        {
            switch (led)
            {
                case 1:
                    gpio13.Write(GpioPinValue.High);
                    break;
                case 2:
                    gpio6.Write(GpioPinValue.High);
                    break;
                case 3:
                    gpio5.Write(GpioPinValue.High);
                    break;

            }
        }
    }
}
