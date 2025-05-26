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
                Arguments = $"start {filePath}",
                UseShellExecute = true,
                CreateNoWindow = false,
            }
        };
        process.Start();
        Console.WriteLine($"Proces zapnut");
    }
}