using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.Models
{
    public class CommandResult<T>
    {
        public bool Succeded { get; set; }

        public string ErrorMessage { get; set; }

        public T Result { get; set; }
    }
}
