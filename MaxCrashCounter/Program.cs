using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace MaxCrashCounter {
    class Program {
        static void Main(string[] args) {
            int crashes = ReadCrash();
            Console.WriteLine("Number of crashes so far: " + crashes + "\nWaiting for another crash...");
            while (true) {
                List<Process> proc = Process.GetProcessesByName("senddmp").ToList();
                if (proc.Count > 0) {
                    crashes++;
                    foreach (Process pr in proc) {
                        pr.Kill();
                    }
                    Console.WriteLine("Yep it crashed. Crash number:" + crashes);
                    string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MaxCrashCount.Kusak";
                    File.WriteAllText(path, crashes.ToString());
                    Console.WriteLine("Waiting for another crash...");

                }
                Thread.Sleep(1000);
            }
        }


        private static int ReadCrash() {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MaxCrashCount.Kusak";
            if (File.Exists(path)) {
                return Int32.Parse(File.ReadAllText(path));
            }
            else {
                return 0;
            }
        }
    }
}
