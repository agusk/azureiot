using System;
using System.Threading;

using Firmata.NET;
namespace FirmataDotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            int led1 = 12;
            int led2 = 11;
            int led3 = 10;
            Arduino arduino = new Arduino("COM6");

            // initialisasi
            arduino.pinMode(led1, Arduino.OUTPUT);
            arduino.pinMode(led2, Arduino.OUTPUT);
            arduino.pinMode(led3, Arduino.OUTPUT);

            while(true)
            {
                // LED 1
                arduino.digitalWrite(led1, Arduino.HIGH);
                arduino.digitalWrite(led2, Arduino.LOW);
                arduino.digitalWrite(led3, Arduino.LOW);

                Thread.Sleep(2000); // sleep 2 detik

                // LED 2
                arduino.digitalWrite(led1, Arduino.LOW);
                arduino.digitalWrite(led2, Arduino.HIGH);
                arduino.digitalWrite(led3, Arduino.LOW);

                Thread.Sleep(2000); // sleep 2 detik


                // LED 3
                arduino.digitalWrite(led1, Arduino.LOW);
                arduino.digitalWrite(led2, Arduino.LOW);
                arduino.digitalWrite(led3, Arduino.HIGH);

                Thread.Sleep(2000); // sleep 2 detik
            }

        }
    }
}
