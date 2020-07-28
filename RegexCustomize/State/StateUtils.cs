using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    public static class StateUtils
    {
        public static T FromJson<T>(this string json)
        {
            T deserializedObj = JsonConvert.DeserializeObject<T>(json);
            return deserializedObj;
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
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
