
using System;

namespace ToKO.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited =  true, AllowMultiple =  false)]
    public class KOIgnoreAttribute : Attribute
    {
    }
}