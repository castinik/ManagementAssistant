using FileManager;
using ManagementAssistantCharger.API;
using ManagementAssistantCharger.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistantCharger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String _filePath = String.Empty;
            String _workerPath = String.Empty;
            String type = String.Empty;
            Console.WriteLine(" * * * Management Assistant Charger is ready * * * ");
            Console.WriteLine("\n\n # Press any key to continue. . . .");
            Console.ReadKey();

            //Choose between file or db
            Int32 newChoose = 0;
            while (true)
            {
                Console.WriteLine(" # Upload from DB or upload/download from file?\n 0 -> From DB\n 1 -> From FILE");
                try
                {
                    newChoose = Convert.ToInt32(Console.ReadLine());
                    if (newChoose == 0 || newChoose == 1)
                    {
                        if(newChoose == 0)
                        {
                            Int32 dailyChoose = 0;
                            while (true)
                            {
                                Console.WriteLine(" # Choose an action: \n 0 -> Daily\n 1 -> Intraday");
                                try
                                {
                                    dailyChoose = Convert.ToInt32(Console.ReadLine());
                                    if (dailyChoose == 0 || dailyChoose == 1)
                                        break;
                                }
                                catch
                                {
                                    Console.Beep();
                                    Console.WriteLine(" - Invalid choose");
                                    Console.ReadKey();
                                    continue;
                                }
                            }
                            while (true)
                            {
                                Console.WriteLine(" # Choose a market: (stock, crypto, forex)");
                                type = Console.ReadLine();
                                if (type == "stock" || type == "crypto" || type == "forex")
                                    break;
                            }
                            if (dailyChoose == 0)
                            {
                                Download download = new Download(_filePath, type, true);
                            }
                            else
                            {
                                Int32 interval = 0;
                                while (true)
                                {
                                    Console.WriteLine(" # Choose the interval: \n" +
                                        " 0 -> 1 minute\n" +
                                        " 1 -> 5 minute\n" +
                                        " 2 -> 15 minute\n" +
                                        " 3 -> 30 minute\n" +
                                        " 4 -> 60 minute");
                                    try
                                    {
                                        interval = Convert.ToInt32(Console.ReadLine());
                                        if (interval >= 0 && interval <= 4)
                                            break;
                                    }
                                    catch
                                    {
                                        Console.Beep();
                                        Console.WriteLine(" - Invalid choose");
                                        Console.ReadKey();
                                        continue;
                                    }
                                }
                                Download download = new Download(_filePath, type, interval, true);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }   
                }
                catch
                {
                    Console.Beep();
                    Console.WriteLine(" - Invalid choose");
                    Console.ReadKey();
                    continue;
                }
            }

            //Worker path selection
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" # Insert worker path:");
                _workerPath = Console.ReadLine();
                if (!String.IsNullOrEmpty(_workerPath))
                {
                    if (Directory.Exists(_workerPath))
                        break;
                    else
                        Console.Beep();
                }
                else
                    Console.Beep();
            }
            //File surgent selection
            while (true)
            {
                List<String> files = Directory.GetFiles(_workerPath).ToList();
                Console.Write(" # Insert the file surgent ( ");
                foreach (String file in files)
                {
                    Console.Write(file.Split('\\').Last() + " ");
                }
                Console.WriteLine(")");
                String output = Console.ReadLine();
                if (String.IsNullOrEmpty(output))
                {
                    Console.Beep();
                    continue;
                }
                try
                {
                    if (files.Contains(files.Where(x => x.Split('\\').Last() == output).First()))
                    {
                        _filePath = files.Where(x => x.Split('\\').Last() == output).First();
                        type = _filePath.Split('.')[_filePath.Split('.').Length - 2].Split('\\').Last();
                        break;
                    }
                    else
                        Console.Beep();
                }
                catch
                {
                    Console.Beep();
                }
            }
            //Donwload or Upload
            Int32 choose = 0;
            while (true)
            {
                Console.WriteLine(" # Choose an action: \n 0 -> Daily\n 1 -> Intraday");
                try
                {
                    choose = Convert.ToInt32(Console.ReadLine());
                    if (choose == 0 || choose == 1)
                        break;
                }
                catch
                {
                    Console.Beep();
                    Console.WriteLine(" - Invalid choose");
                    Console.ReadKey();
                    continue;
                }
            }
            if(choose == 0)
            {
                Download download = new Download(_filePath, type);
            }
            else
            {
                Int32 interval = 0;
                while (true)
                {
                    Console.WriteLine(" # Choose the interval: \n" +
                        " 0 -> 1 minute\n" +
                        " 1 -> 5 minute\n" +
                        " 2 -> 15 minute\n" +
                        " 3 -> 30 minute\n" +
                        " 4 -> 60 minute");
                    try
                    {
                        interval = Convert.ToInt32(Console.ReadLine());
                        if (interval >= 0 && interval <= 4)
                            break;
                    }
                    catch
                    {
                        Console.Beep();
                        Console.WriteLine(" - Invalid choose");
                        Console.ReadKey();
                        continue;
                    }
                }
                Download download = new Download(_filePath, type, interval);
            }
            Console.ReadKey();
        }
    }
}
