using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client.Utilities
{
    public static class SettingsManager
    {
        public static string GetString(string key)
        {
            string result = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }

            return result;
        }

        public static bool GetBool(string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);

            bool result;
            bool isParsed = bool.TryParse(value, out result);

            if (!isParsed) throw new Exception("Не удалось получить логическое значение из конфига");

            return result;
        }
    }
}
