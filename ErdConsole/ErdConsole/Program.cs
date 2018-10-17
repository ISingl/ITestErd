using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErdDevice;

namespace ErdConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        void Run()
        {
            Erd erd = new Erd("192.168.1.1", "community");
            Console.WriteLine(erd.GetMonitorVoltageSignal());
            Console.WriteLine(erd.GetVoltageSensorContact10());
            Console.WriteLine(erd.GetTestFlag());
            Console.ReadKey();
        }
    }
}
