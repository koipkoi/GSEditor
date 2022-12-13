using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace GSEditor.Common.Utilities;

public sealed class Platforms
{
  public static string AppName => Assembly.GetEntryAssembly()!.GetName().Name!;
  public static string AppVersion => Assembly.GetEntryAssembly()!.GetName().Version!.ToString();

  public static string AppDir => AppDomain.CurrentDomain.BaseDirectory;
  public static string AppDataDir
  {
    get
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      return path;
    }
  }

  public static int WindowsBuildVersion
  {
    get
    {
      var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")!;
      var currentBuild = int.Parse((reg.GetValue("CurrentBuild") as string)!);
      return currentBuild;
    }
  }

  public static bool IsDesignerMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
}
