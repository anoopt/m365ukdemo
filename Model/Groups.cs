using System.Collections.Generic;

namespace M365UK.Functions.Model
{
    public class Value
    {
        public string displayName { get; set; }
    }

    public class Groups
    {
        public List<Value> value { get; set; }
    }
}
