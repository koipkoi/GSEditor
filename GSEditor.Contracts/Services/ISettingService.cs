using GSEditor.Models.Settings;

namespace GSEditor.Contracts.Services;

public interface ISettingService
{
  public AppSettings AppSettings { get; set; }

  public void Apply();
}
