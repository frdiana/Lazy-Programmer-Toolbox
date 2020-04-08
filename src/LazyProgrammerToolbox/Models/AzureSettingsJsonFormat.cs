using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.Models
{
    public class AzureSettingsJsonFormat
    {
        public List<AzureSettingsJsonFormatItem> Properties { get; set; } = new List<AzureSettingsJsonFormatItem>();
    }

    public class AzureSettingsJsonFormatItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool SlotSetting { get; set; }
    }
}
