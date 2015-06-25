using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class SettingsService : ISettingsService
    {
        // Our isolated storage settings 
        private ApplicationDataContainer dataContainer;

        public event EventHandler SettingsChanged;

        public SettingsService()
        {
            try
            {
                dataContainer = ApplicationData.Current.LocalSettings;
            }
            catch
            {
            }
        }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value. 
            if (dataContainer.Values.ContainsKey(key))
            {
                value = (T)dataContainer.Values[key];
            }
            // Otherwise, use the default value. 
            else
            {
                value = defaultValue;
            }

            return value;
        }

        public T GetValueOrDefault<T>(string key)
        {
            return GetValueOrDefault(key, default(T));
        }

        public bool SetValue<T>(string key, T value)
        {
            bool valueChanged = false;

            // If the key exists 
            if (dataContainer.Values.ContainsKey(key))
            {
                // Store the new value 
                dataContainer.Values[key] = value;
                valueChanged = true;
            }
            // Otherwise create the key. 
            else
            {
                dataContainer.Values.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        public bool SetSerializedValue<T>(string key, T value)
        {
            var str = JsonConvert.SerializeObject(value);
            return SetValue<string>(key, str);
        }

        public T GetDeserializedValueOrDefault<T>(string key)
        {
            return GetDeserializedValueOrDefault<T>(key, default(T));
        }

        public T GetDeserializedValueOrDefault<T>(string key, T value)
        {
            var retValue = GetValueOrDefault(key, string.Empty);
            if (string.IsNullOrEmpty(retValue))
                return value;

            return JsonConvert.DeserializeObject<T>(retValue);
        }

        public void ClearValue(string key)
        {
            if (dataContainer.Values.ContainsKey(key))
            {
                dataContainer.Values.Remove(key);
            }
        }

        public bool HaveValue(string key)
        {
            return dataContainer.Values.ContainsKey(key);
        }

        public void NotifySettingsChanged()
        {
            var handler = SettingsChanged;
            if (handler != null)
            {
                SettingsChanged(this, EventArgs.Empty);
            }
        }
    }
}
