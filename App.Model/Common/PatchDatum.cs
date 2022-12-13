using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Common
{
    public class PatchDatum
    {
        public string Path { get; set; } = null!;
        public object? Value { get; set; }
    }
}
