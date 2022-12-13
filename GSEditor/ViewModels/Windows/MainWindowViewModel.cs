using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.IO;

namespace GSEditor.ViewModels.Windows;

public sealed class MainWindowViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();
  private readonly ISettingService _settings = App.Services.GetRequiredService<ISettingService>();

  public BindingProperty<bool> RomOpened { get; } = new(false);
  public BindingProperty<bool> RomChanged { get; } = new(false);
  public BindingProperty<string> RomPath { get; } = new("-");

  public MainWindowViewModel()
  {
    _pokegold.RomChanged += OnDataChanged;
    _pokegold.DataChanged += OnDataChanged;
  }

  private void OnDataChanged(object? _, EventArgs __)
  {
    RomOpened.Value = _pokegold.IsOpened;
    RomChanged.Value = _pokegold.IsChanged;
    RomPath.Value = _pokegold.FileName;
  }

  public void QuestionClosing(CancelEventArgs e)
  {
    if (_pokegold.IsChanged)
    {
      switch (_dialogs.ShowYesNoCancel("알림", "변경 사항을 저장하시겠습니까?"))
      {
        case YesNoCancelResult.Yes:
          _pokegold.Write();
          e.Cancel = false;
          break;
        case YesNoCancelResult.No:
          e.Cancel = false;
          break;
        case YesNoCancelResult.Cancel:
          e.Cancel = true;
          break;
      }
    }
  }

  public void Cleanup()
  {
    _pokegold.RomChanged -= OnDataChanged;
    _pokegold.DataChanged -= OnDataChanged;
  }

  public void OpenRom()
  {
    var fileName = _dialogs.ShowOpenFile("열기", "지원하는 파일|*.gb;*.gbc;*.bin|모든 파일|*.*");
    if (fileName != null)
      _pokegold.Open(fileName);
  }

  public void SaveRom()
  {
    if (_pokegold.IsOpened)
      _pokegold.Write();
  }

  public void RunEmulator()
  {
    if (_pokegold.IsOpened)
    {
      var emulatorPath = _settings.AppSettings.EmulatorPath;
      if (emulatorPath == null || !File.Exists(emulatorPath))
      {
        _dialogs.ShowError("알림", "에뮬레이터가 설정되어있지 않아 실패했습니다.");
        return;
      }
      _pokegold.Run(emulatorPath);
    }
  }

  public void OpenEmulatorSetting()
  {
    var fileName = _dialogs.ShowSaveFile("에뮬레이터 설정", "실행 가능한 파일|*.exe|모든 파일|*.*");
    _settings.AppSettings.EmulatorPath = fileName;
    _settings.Apply();
  }

  public void OpenAppInfo()
  {
    _dialogs.ShowAppInfo();
  }
}
