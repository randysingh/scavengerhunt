using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface ISettingsService
    {
        event EventHandler SettingsChanged;

        T GetValueOrDefault<T>(string key, T defaultValue);

        T GetValueOrDefault<T>(string key);

        bool SetValue<T>(string key, T value);

        bool SetSerializedValue<T>(string key, T value);

        T GetDeserializedValueOrDefault<T>(string key);

        T GetDeserializedValueOrDefault<T>(string key, T value);

        void ClearValue(string key);

        bool HaveValue(string key);

        void NotifySettingsChanged();
    }
}
