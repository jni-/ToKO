using System;

namespace ToKO.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited =  true, AllowMultiple =  false)]
    public class KONotObservableAttribute : Attribute
    {
    }
}