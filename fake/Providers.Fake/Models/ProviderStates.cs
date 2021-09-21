using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Providers.Fake.Models
{
    public class ProviderStates
    {
        public string consumer { get; set; }
        public string state { get; set; }
        public List<string> states { get; set; }
    }
}
