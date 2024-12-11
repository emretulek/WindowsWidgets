using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace Widgets
{
    internal class JsonFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ConfigJsonStruct? Read(string filePath)
        {
            var widgetConfig = new ConfigJsonStruct();
            try
            {
                string jsonString = File.ReadAllText(filePath);
                widgetConfig = JsonConvert.DeserializeObject<ConfigJsonStruct>(jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JsonFile 24: " + ex);
            }

            return widgetConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="widgetConfig"></param>
        public static void Write(string filePath, ConfigJsonStruct widgetConfig)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(widgetConfig, Formatting.Indented);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JsonFile 39: " + ex);
            }
        }
    }
}
