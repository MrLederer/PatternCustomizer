using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;

namespace PatternCustomizer.State
{
    public static class StateUtils
    {
        public static readonly string DefaultFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof(PatternCustomizer), "settings.json");

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

        /// <summary>
        /// Converts to rgb.
        /// </summary>
        /// <remakrs>For more information visit <see href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.interop.__vscolortype?view=visualstudiosdk-2019"></remakrs>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static int ToRGB(this Color color)
        {
            return color.R | (color.G << 8) | (color.B << 16);
        }

        public static IDictionary<TKey, TValue> ToDictionaryWithKeyOverwritting<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var item in source)
            {
                result[keySelector(item)] = valueSelector(item);
            }
            return result;
        }
    }
}
