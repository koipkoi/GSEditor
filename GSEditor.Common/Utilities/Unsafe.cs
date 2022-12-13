using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace GSEditor.Common.Utilities;

public sealed class Unsafe
{
#pragma warning disable IDE0051
#pragma warning disable CS0414
  private static readonly BindingFlags _bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
#pragma warning restore CS0414
#pragma warning restore IDE0051
  private static readonly BindingFlags _staticBindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

  private Unsafe() { }

  public static void ClearAllEventHandler(object obj)
  {
    var t = obj.GetType();
    var events = t.GetEvents();
    foreach (var eventInfo in events)
    {
      var field = t.GetField(eventInfo.Name, _staticBindingFlags);
      if (field == null)
        continue;

      if (field.GetValue(obj) is not Delegate eventHandler)
        continue;

      eventInfo.RemoveEventHandler(obj, eventHandler);
    }
  }
}
