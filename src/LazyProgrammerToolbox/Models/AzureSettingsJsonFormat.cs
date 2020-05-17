using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.Models
{
    /// <summary>
    /// The poco class for serialize Azure app service settings
    /// </summary>
    public class AzureSettingsJsonFormat
    {
        public List<AzureSettingsJsonFormatItem> Properties { get; set; } = new List<AzureSettingsJsonFormatItem>();
    }

    /// <summary>
    /// Rapresents a single azure config node.
    /// </summary>
    public class AzureSettingsJsonFormatItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool SlotSetting { get; set; }
    }
}
