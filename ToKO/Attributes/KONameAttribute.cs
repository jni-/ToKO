using System;

namespace ToKO.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited =  true, AllowMultiple =  false)]
    public class KONameAttribute : Attribute
    {
        public string Name { get; set; }

        public KONameAttribute(string name)
        {
            Name = name;
        }
    }
}