using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LazyProgrammerToolbox.Models
{
   
    /// <summary>
    /// Rapresents a single azure config element.
    /// </summary>
    public class AzureSettingsJsonFormatItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("slotSetting")]
        public bool SlotSetting { get; set; }
    }
}
