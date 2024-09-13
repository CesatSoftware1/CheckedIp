using System;
using System.Diagnostics;
using System.IO;

internal class Program
{

    static void Main(string[] args)
    {
        string filePath = @"C:\Users\Antonio Romero\Documents\GitHub\BeldenCableInspection\bin\Debug\config.txt";
        string[] lines = File.ReadAllLines(filePath);

        string IpAddress = "";
        string Subnet = "";
        string DefaultGetway = "";

        foreach (string line in lines)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                switch (parts[0].Trim())
                {
                    case "IpAddress":
                        IpAddress = parts[1].Trim();
                        break;
                    case "Subnet":
                        Subnet = parts[1].Trim();
                        break;
                    case "DefaultGetway":
                        DefaultGetway = parts[1].Trim();
                        break;
                }
            }
        }
        SetIP("/c netsh interface ip set address Ethernet static " + IpAddress + " " + Subnet + " " + DefaultGetway);
    }

    private static void SetIP(string arg)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            psi.Arguments = arg;
            Process.Start(psi);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
}
