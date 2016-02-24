using System;
using System.Linq;

using System.IO.Ports;
namespace DotnetLed
{
    class Program
    {
        static bool running = true;
        static void Main(string[] args)
        {
            try
            {
                SerialPort serialPort = new SerialPort("COM8");
                serialPort.BaudRate = 9600;

                Console.CancelKeyPress += delegate (object sender,
                    ConsoleCancelEventArgs e)
                {
                    e.Cancel = true;
                    running = false;
                };

                serialPort.Open();
                int pin = 1;
                while (running)
                {
                    Console.WriteLine("Send command: {0}", pin);
                    serialPort.Write(pin.ToString());
                    pin++;
                    if (pin > 3)
                        pin = 1;
                    System.Threading.Thread.Sleep(2000);
                }
                serialPort.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
    }
}
