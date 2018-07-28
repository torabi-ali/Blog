﻿using Blog.Data;
using Blog.DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public virtual async Task SaveSettingAsync<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var value = Convert.ToString(prop.GetValue(settings, null));
                var setting = new Setting(prop.Name, typeof(T).Name, value);
                await CreateSettingAsync(setting);
            }
        }

        public void CreateSetting(Setting setting)
        {
            _context.Add(setting);
            _context.SaveChanges();
        }

        public virtual async Task CreateSettingAsync(Setting setting)
        {
            await _context.AddAsync(setting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettingAsync(Setting setting)
        {
            var settings = await GetAllSettingsAsync();
            var item = settings.FirstOrDefault(p => p.Id == setting.Id);

            item.Name = setting.Name;

            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSettingAsync(Setting setting)
        {
            var settings = await GetAllSettingsAsync();
            var item = settings.FirstOrDefault(p => p.Id == setting.Id);

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Setting>> GetAllSettingsAsync()
        {
            return await _context.Settings.OrderBy(p => p.Name).ToListAsync();
        }

        public virtual async Task<T> LoadSettingAsync<T>() where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var name = typeof(T).Name + "." + prop.Name;
                //load by store
                var setting = await GetSettingByKeyAsync<string>(name);
                if (setting == null)
                    continue;

                object value = setting.ConvertTo(prop.PropertyType);
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public virtual async Task<T> GetSettingByKeyAsync<T>(string name, T defaultValue = default(T))
        {
            var setting = await GetSettingAsync(name);
            if (setting != null)
                return setting.Value.ConvertTo<T>();
            return defaultValue;
        }

        public virtual async Task<Setting> GetSettingAsync(string name)
        {
            var settings = await GetAllSettingsAsync();
            var settingsByName = settings.SingleOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return settingsByName;
        }
    }
}