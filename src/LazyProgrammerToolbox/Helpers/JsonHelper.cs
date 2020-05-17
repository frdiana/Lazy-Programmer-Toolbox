using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LazyProgrammerToolbox.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Validate a json string
        /// </summary>
        /// <param name="json"></param>
        /// <returns>True if the json in well format, False otherwise</returns>
        public static bool IsValidJson(string json)
        {
            try
            {
                JsonDocument document = JsonDocument.Parse(json, GetDefaultOptions());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns default options for JsonDocumentOptions object
        /// </summary>
        /// <returns></returns>
        public static JsonDocumentOptions GetDefaultOptions()
        {
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            };
            return options;
        }
    }
}
