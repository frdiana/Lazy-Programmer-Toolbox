using LazyProgrammerToolbox.Helpers;
using LazyProgrammerToolbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LazyProgrammerToolbox.Converters
{
    /// <summary>
    /// Converts different json format to others
    /// </summary>
    public static class AppSettingsConverter
    {
        private static List<AzureSettingsJsonFormatItem> lst = new List<AzureSettingsJsonFormatItem>();

        /// <summary>
        /// Converts an appsettings.json file to azure app service format
        /// </summary>
        /// <param name="input">Full path to json file</param>
        /// <param name="output"></param>
        public static void AppSettingsToAzSettings(string input,string output)
        {
            if (!File.Exists(input) || !JsonHelper.IsValidJson(File.ReadAllText(input)))
            {
                ConsoleHelper.WriteError("Invalid file");
                return;
            }
            
            using (JsonDocument document = JsonDocument.Parse(File.ReadAllText(input), JsonHelper.GetDefaultJsonDocumentOptions()))
            {
                ConvertJsonObject(document.RootElement, parent:null,arrayIndex:null);
                
                string outputFileSerialized = JsonSerializer.Serialize(lst.OrderBy(x => x.Name), JsonHelper.GetDefaultJsonSerializerOptions());
                File.WriteAllText(output, outputFileSerialized);
            }
         }


        /// <summary>
        /// Recursively scan all json nodes and add elements to list
        /// </summary>
        /// <param name="rootElement">Reference to root jsonElement</param>
        /// <param name="parent">Json's parent property name</param>
        /// <param name="arrayIndex">Null if scanning json object, with a value if scanning json array</param>
        private static void ConvertJsonObject(JsonElement rootElement, string parent, int? arrayIndex)
        {
            foreach (var jElement in rootElement.EnumerateObject())
            {
                string name = GetJsonNodeName(jElement, parent, arrayIndex);

                if (jElement.Value.ValueKind == JsonValueKind.Object)
                {
                    ConvertJsonObject(jElement.Value, GetJsonNodeName(jElement, parent, arrayIndex), arrayIndex);
                }
                else if (jElement.Value.ValueKind == JsonValueKind.Array)
                {
                    ConvertJsonArray(jElement.Value, GetJsonNodeName(jElement, parent, arrayIndex));
                }
                else if (jElement.Value.ValueKind == JsonValueKind.String)
                {
                    string value = jElement.Value.GetString();
                    AddToList(name, value);
                }

                else if (jElement.Value.ValueKind == JsonValueKind.Number)
                {
                    string value = jElement.Value.GetInt32().ToString();
                    AddToList(name, value);
                }

                else if (jElement.Value.ValueKind == JsonValueKind.True ||
                         jElement.Value.ValueKind == JsonValueKind.False)
                {
                    string value = jElement.Value.GetBoolean().ToString();
                    AddToList(name, value);
                }
            }
        }

        internal static void AppSettingsToAzKeyVault(string arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recursively scan json array elements and add to the list
        /// </summary>
        /// <param name="jsonArray">Reference to json array</param>
        /// <param name="parent">Json's parent property name</param>
        private static void ConvertJsonArray(JsonElement jsonArray, string parent)
        {
            int index = 0;
            foreach (var jElement in jsonArray.EnumerateArray())
            {
                if (jElement.ValueKind == JsonValueKind.Object)
                {
                    ConvertJsonObject(jElement, parent, index);
                }
                else if (jElement.ValueKind == JsonValueKind.Array)
                {
                    ConvertJsonArray(jsonArray, null);
                }
                else if (jElement.ValueKind == JsonValueKind.String)
                {
                    lst.Add(new AzureSettingsJsonFormatItem()
                    {
                        Name = $"{ parent }:{index}",
                        SlotSetting = false,
                        Value = jElement.GetString()
                    });
                }

                else if (jElement.ValueKind == JsonValueKind.Number)
                {
                    lst.Add(new AzureSettingsJsonFormatItem()
                    {
                        Name = $"{ parent }:{index}",
                        SlotSetting = false,
                        Value = jElement.GetInt32().ToString()
                    });
                }

                else if (jElement.ValueKind == JsonValueKind.True || jElement.ValueKind == JsonValueKind.False)
                {
                    lst.Add(new AzureSettingsJsonFormatItem()
                    {
                        Name = $"{ parent }:{index}",
                        SlotSetting = false,
                        Value = jElement.GetBoolean().ToString()
                    });
                }
                index++;

            }
        }

        /// <summary>
        /// Add elements to the list
        /// </summary>
        /// <param name="name">Property name in az settings format</param>
        /// <param name="value"><Property value/param>
        private static void AddToList(string name,string value)
        {
            lst.Add(new AzureSettingsJsonFormatItem()
            {
                Name = name,
                SlotSetting = false,
                Value = value
            });
        }

        /// <summary>
        /// Helper method for build property name, based on parent or array element
        /// </summary>
        /// <param name="jsonProperty">Leaf json node</param>
        /// <param name="parent">Json's parent property name</param>
        /// <param name="arrayIndex">Null if scanning json object, with a value if scanning json array</param>
        /// <returns>Return json node name</returns>
        private static string GetJsonNodeName(JsonProperty jsonProperty, string parent,int? arrayIndex)
        {
            if (string.IsNullOrEmpty(parent))
            {
                return jsonProperty.Name;
            }
            else
            {
                string jsonNodeName = string.Empty;

                jsonNodeName = arrayIndex == null ? 
                    $"{ parent }:{ jsonProperty.Name}" : 
                    $"{parent}:{arrayIndex}:{jsonProperty.Name}";

                return jsonNodeName;
            }
        }
    }
}
