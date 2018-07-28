using Blog.DomainClass;
using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface ISettingService
    {
        Task CreateSettingAsync(Setting setting);
        Task UpdateSettingAsync(Setting setting);
        Task DeleteSettingAsync(Setting setting);
        Task<IList<Setting>> GetAllSettingsAsync();
        Task<T> LoadSettingAsync<T>() where T : ISettings, new();
        Task<T> GetSettingByKeyAsync<T>(string name, T defaultValue = default(T));
        Task<Setting> GetSettingAsync(string name);
    }
}
