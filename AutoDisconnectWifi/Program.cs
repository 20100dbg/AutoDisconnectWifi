using System;
using System.IO;

namespace AutoDisconnectWifi
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = @"C:\ProgramData\Microsoft\Wlansvc\Profiles\Interfaces\";

            String[] interfaces = Directory.GetDirectories(path);

            for (int i = 0; i < interfaces.Length; i++)
            {
                BrowseInterface(interfaces[i]);
            }

        }

        static void BrowseInterface(String path)
        {
            String[] networks = Directory.GetFiles(path, "*.*");
            
            for (int i = 0; i < networks.Length; i++)
            {
                EditNetwork(networks[i]);
            }
        }

        static void EditNetwork(String path)
        {
            StreamReader sr = new StreamReader(path);
            String str = sr.ReadToEnd();
            
            int idx = str.IndexOf("<connectionMode>") + 16;
            int idx2 = str.IndexOf("</", idx);
            String connMode = str.Substring(idx, idx2 - idx);


            idx = str.IndexOf("<name>") + 6;
            idx2 = str.IndexOf("</", idx);
            String netName = str.Substring(idx, idx2 - idx);

            if (connMode == "auto")
            {
                str = str.Replace("<connectionMode>auto</connectionMode>", "<connectionMode>manual</connectionMode>");
                StreamWriter sw = new StreamWriter(path);
                sw.Write(str);

                Console.WriteLine("Network " + netName + " set to manual.");
            }

            if (connMode == "manual")
            {
                Console.WriteLine("Network " + netName + " is already manual.");
            }

        }
    }
}
