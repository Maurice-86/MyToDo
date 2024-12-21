using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class MenuBarModel
    {
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Type? MyType { get; set; }
    }
}
