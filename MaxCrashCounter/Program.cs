using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaxCrashCounter {
    class Program {
        static void Main(string[] args) {
            int crashes = ReadCrash();
            Console.WriteLine("Number of crashes so far: " + crashes);
            while (true) {
                List<Process> proc = Process.GetProcessesByName("senddmp").ToList();
                if (proc.Count > 0) {
                    crashes++;
                    proc[0].Kill();
                    Console.WriteLine("Yep it crashed. Crash number:" + crashes);
                    string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MaxCrashCount.txt";
                    File.WriteAllText(path, crashes.ToString());
                    
                }
                Thread.Sleep(1000);
            }
        }


        private static int ReadCrash() {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MaxCrashCount.txt";
            if (File.Exists(path)) {
                return Int32.Parse(File.ReadAllText(path));
            }
            else {
                return 0;
            }
        }
    }
}
