using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RegexCustomize.State
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

        public string GetDefaultFilePath()
        {
            return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            nameof(PatternCustomizer), FILE_NAME
          );
        }
    }
}
