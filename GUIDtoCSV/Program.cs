using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GUIDtoCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Made by Stiig Gade");

            if (File.Exists("UUID.TXT"))
                CreateOutput();

            Console.WriteLine("Done");
        }

        private static void CreateOutput()
        {
            List<PCInfo> pcs = new List<PCInfo>();
            PCInfo tempPC = new PCInfo();

            foreach (string line in File.ReadAllLines("UUID.TXT"))
            {
                if (line.StartsWith("Product Name"))
                    tempPC.ProductName = line.Substring(15);
                else if (line.StartsWith("Serial Number"))
                    tempPC.SerialNumber = line.Substring(16);
                else if (line.StartsWith("Universal Unique ID"))
                    tempPC.UniqueID = line.Substring(24);
                else if (line == "--*--*--*--")
                    pcs.Add(new PCInfo(tempPC));
            }

            CreateCSV(pcs);
        }

        private static void CreateCSV(List<PCInfo> list)
        {
            List<string> lines = new List<string>();
            lines.Add("Serial Number;Unique ID;Product Name");

            foreach (PCInfo pc in list)
            {
                lines.Add(String.Format("{0};{1};{2}", pc.SerialNumber, pc.UniqueID, pc.ProductName));
            }

            StreamWriter file = new StreamWriter("Output.csv");

            foreach (string line in lines)
                file.WriteLine(line);

            file.Close();
        }
    }

    public class PCInfo
    {
        public string ProductName { get; set; }
        public string SerialNumber { get; set; }
        public string UniqueID { get; set; }

        public PCInfo()
        {

        }
        public PCInfo(PCInfo info)
        {
            this.ProductName = info.ProductName;
            this.SerialNumber = info.SerialNumber;
            this.UniqueID = info.UniqueID;
        }
    }
}
