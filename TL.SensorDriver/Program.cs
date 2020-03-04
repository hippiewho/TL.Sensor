using System;
using System.Device.Gpio;
using System.Threading;

namespace TL.SensorDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            SensorDriver driver = new SensorDriver();
            driver.Run();
        }
    }
}
