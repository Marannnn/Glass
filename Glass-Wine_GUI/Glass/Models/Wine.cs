using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Glass.Models;

public class Wine
{
    public void StartFile(string filePath, string filePrefix)
    {
        //TODO: start with prefixes
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo(filePath)
            {
                FileName = "wine",
                Arguments = filePath,
                UseShellExecute = true,
                CreateNoWindow = false,
            }
        };
        process.Start();
        Console.WriteLine($"Zapnul jsem proces: {process.StartInfo.FileName} {process.StartInfo.Arguments}");
    }
}