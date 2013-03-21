using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MemoryLeakSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("____________________________________________________________");

                Console.WriteLine();
                Console.WriteLine();

                WriteLineColoured(ConsoleColor.Yellow, "Choose a sample to run, enter c to exit:");
                Console.WriteLine();
                WriteLineColoured(ConsoleColor.DarkGreen, "Samples:");
                WriteLineColoured(ConsoleColor.DarkGreen, "1) A simple bulk managed leak");
                WriteLineColoured(ConsoleColor.DarkGreen, "2) A gradual managed leak");
                WriteLineColoured(ConsoleColor.DarkGreen, "3) A gradual unmanaged leak");
                Console.WriteLine();

                var key = Console.ReadKey();
                if(key.KeyChar == 'c')
                    break;

                if (Char.IsNumber(key.KeyChar))
                {
                    var option = Convert.ToInt32(key.KeyChar.ToString());
                    switch (option)
                    {
                        case 1:
                            SimpleBulkMemoryLeak();

                            break;
                        case 2:
                            SimpleGradualMemoryLeak();
                            break;     
                        case 3:
                            SimpleUnmanagedMemoryLeak();
                            break;

                        default:
                            break;
                    }
                }
            }
           

        }

        private static void SimpleBulkMemoryLeak()
        {
            var bytes = new byte[50*1024*1024];
            WriteLineColoured(ConsoleColor.Green, "Done! presse enter to release memory.");
            Console.ReadLine();

        }

        private static void SimpleGradualMemoryLeak()
        {
            var list = new List<byte[]>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new byte[1 * 1000 * 1000]);
                Thread.Sleep(200);
            }
            
            WriteLineColoured(ConsoleColor.Green, "Done! presse enter to release memory.");
            Console.ReadLine();

        }

        private static void SimpleUnmanagedMemoryLeak()
        {
            var list = new List<IntPtr>();
            for (int i = 0; i < 400; i++)
            {
                list.Add(Marshal.AllocHGlobal(1*1000*1000));
                Thread.Sleep(200);
            }

            WriteLineColoured(ConsoleColor.Green, "Done! presse enter to release memory.");
            Console.ReadLine();

            list.ForEach(Marshal.FreeHGlobal);

        }

        private static void WriteLineColoured(ConsoleColor color, string text, params object[] args)
        {
            var currentColour = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (text.IndexOf("{") >= 0)
            {
                Console.WriteLine(text, args);
            }
            else
            {
                Console.WriteLine(text);                
            }

            Console.ForegroundColor = currentColour;
        }
    }
}
