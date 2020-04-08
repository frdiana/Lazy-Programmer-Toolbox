using LazyProgrammerToolbox.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LazyProgrammerToolbox.Converters
{
    public class ToAzSettingsConverter 
    {
        public CommandResult<string> Convert(string fileInAppsettingsFormat)
        {
            try
            {
                var appSettingsJson = JsonConvert.DeserializeObject(fileInAppsettingsFormat) as JObject;
                List<AzureSettingsJsonFormatItem> jsonItems = new List<AzureSettingsJsonFormatItem>();
                var dictResult = new Dictionary<string, string>();
                var toAzConfigResult = ParseJson(appSettingsJson, jsonItems);
                return new CommandResult<string>()
                {
                    Succeded = true,
                    Result = JsonConvert.SerializeObject(jsonItems, Formatting.Indented)
                };
            }
            catch (JsonException ex)
            {
                return new CommandResult<string>()
                {
                    ErrorMessage = "The input file is not in a valid format",
                    Succeded = false
                };
            }
        }


        public static bool ParseJson(JToken token, List<AzureSettingsJsonFormatItem> jsonItems, string parentLocation = "", bool isArray = false)
        {
            int arrayIndex = 0;
            
            if (token.HasValues)
            {
                foreach (JToken child in token.Children())
                {

                    if (token.Type == JTokenType.Property)
                    {
                        if (string.IsNullOrEmpty(parentLocation))
                        {
                            parentLocation += ((JProperty)token).Name;
                        }
                        else
                        {
                            parentLocation += ":" + ((JProperty)token).Name;
                        }

                    }

                    if (token.Type == JTokenType.Array)
                    {
                        ParseJsonArray(child, jsonItems, parentLocation, arrayIndex++);
                    }
                    else
                        ParseJson(child, jsonItems, parentLocation);
                }
                return true;
            }
            else
            {
                AzureSettingsJsonFormatItem item = new AzureSettingsJsonFormatItem()
                {
                    Name = parentLocation,
                    Value = token.ToString()
                };
                jsonItems.Add(item);
                return false;
            }
        }

        private static bool ParseJsonArray(JToken token, List<AzureSettingsJsonFormatItem> jsonItems, string parentLocation, int arrayIndex)
        {
            AzureSettingsJsonFormatItem item = null;
            if (token.HasValues)
            {

                foreach (JToken child in token.Children())
                {
                    if (child.Type == JTokenType.Property)
                    {
                        item = new AzureSettingsJsonFormatItem()
                        {
                            Name = parentLocation + ":" + arrayIndex + ":" + ((JProperty)child).Name,
                            Value = ((JProperty)child).Value.ToString()
                        };
                        jsonItems.Add(item);
                    }
                    else
                    {
                        throw new NotSupportedException("");
                    }
                }
            }
            else
            {

                if (token as JValue != null)
                {
                    item = new AzureSettingsJsonFormatItem()
                    {
                        Name = parentLocation + ":" + arrayIndex,
                        Value = ((JValue)token).Value.ToString()
                    };
                    jsonItems.Add(item);
                }
                else
                {
                    item = new AzureSettingsJsonFormatItem()
                    {
                        Name = parentLocation + ":" + arrayIndex + ":" + ((JProperty)token).Name,
                        Value = ((JProperty)token).Value.ToString()
                    };
                    jsonItems.Add(item);
                }
            }
            return true;
        }
        
    }
}
