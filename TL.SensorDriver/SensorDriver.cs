using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;
using System.Threading;

namespace TL.SensorDriver
{
    public class SensorDriver
    {
        private int inputPin = 17;
        private int outputPin = 27;
        private int ledPin = 22;
        private int wait = 100;

        public void Run()
        {
            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(inputPin, PinMode.InputPullUp);
                controller.OpenPin(outputPin, PinMode.Output);
                controller.OpenPin(ledPin, PinMode.Output);

                Console.WriteLine($"GPIO pin {inputPin}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.ClosePin(inputPin);
                    controller.ClosePin(outputPin);
                    controller.ClosePin(ledPin);
                    controller.Dispose();
                };


                PinValue oldValue = controller.Read(inputPin);
                while (true)
                {
                    PinValue value = controller.Read(inputPin);
                    if (oldValue != value)
                    {
                        if (value == PinValue.High)
                        {
                            controller.Write(ledPin, PinValue.Low);
                        }
                        else if (value == PinValue.Low)
                        {
                            controller.Write(ledPin, PinValue.High);
                        }
                        oldValue = value;
                        Console.WriteLine($"Value: {value}");
                    }
                    Thread.Sleep(wait);
                }
            }
        }
    }
}
