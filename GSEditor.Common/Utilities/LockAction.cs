using System;

namespace GSEditor.Common.Utilities;

public sealed class LockAction
{
  private bool _isLocked = false;
  public bool IsLocked => _isLocked;

  public void Run(Action action)
  {
    if (_isLocked)
      return;

    try
    {
      _isLocked = true;
      action.Invoke();
      _isLocked = false;
    }
    catch (Exception)
    {
      _isLocked = false;
    }
  }
}
