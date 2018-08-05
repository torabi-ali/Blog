using Blog.Data;
using Blog.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Services
{
    public class SettingService : ISettingService
    {
        private readonly ApplicationDbContext _context;

        public SettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var value = Convert.ToString(prop.GetValue(settings, null));
                var setting = new Setting(prop.Name, typeof(T).Name, value);
                CreateSetting(setting);
            }
        }

        public void CreateSetting(Setting setting)
        {
            _context.Add(setting);
            _context.SaveChanges();
        }

        public void UpdateSetting(Setting setting)
        {
            var settings = GetAllSettings();
            var item = settings.FirstOrDefault(p => p.Id == setting.Id);

            item.Name = setting.Name;

            _context.Update(item);
            _context.SaveChanges();
        }

        public void DeleteSetting(Setting setting)
        {
            var settings = GetAllSettings();
            var item = settings.FirstOrDefault(p => p.Id == setting.Id);

            _context.Remove(item);
            _context.SaveChanges();
        }

        public IList<Setting> GetAllSettings()
        {
            return _context.Settings.OrderBy(p => p.Name).ToList();
        }

        public virtual T LoadSetting<T>() where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var name = typeof(T).Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(name);
                if (setting == null)
                    continue;

                object value = setting.ConvertTo(prop.PropertyType);
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public virtual T GetSettingByKey<T>(string name, T defaultValue = default(T))
        {
            var setting = GetSetting(name);
            if (setting != null)
                return setting.Value.ConvertTo<T>();
            return defaultValue;
        }

        public virtual Setting GetSetting(string name)
        {
            var settings = GetAllSettings();
            var settingsByName = settings.SingleOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return settingsByName;
        }
    }
}
