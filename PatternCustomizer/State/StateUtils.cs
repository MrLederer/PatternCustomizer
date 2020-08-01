using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    public static class StateUtils
    {
        private static JsonSerializerSettings jsonSetting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
        };
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, jsonSetting);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSetting);
        }

        public static string GetDefaultFilePath()
        {
            return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            nameof(PatternCustomizer), "setting.json"
          );
        }
    }
}
