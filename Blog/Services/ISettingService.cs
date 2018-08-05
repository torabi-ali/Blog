using Blog.DomainClass;
using System.Collections.Generic;

namespace Blog.Services
{
    public interface ISettingService
    {
        void SaveSetting<T>(T settings) where T : ISettings, new();
        void CreateSetting(Setting setting);
        void UpdateSetting(Setting setting);
        void DeleteSetting(Setting setting);
        IList<Setting> GetAllSettings();
    }
}
