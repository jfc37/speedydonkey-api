using System;
using Common;

namespace Models
{
    public class ReferenceData : DatabaseEntity
    {
        public virtual string Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
    }
}
