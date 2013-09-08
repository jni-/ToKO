using System;
using ToKO.Attributes;

namespace ToKO.TestUtils.SampleObjects
{
    public class SimpleObjectWithAttributes
    {
        [KOName("surname")]
        public string SomeString { get; set; }

        [KOIgnore]
        public int Number { get; set; }

        [KONotObservable]
        public DateTime Date { get; set; }

        [KONotObservable, KOName("nested")]
        public SimpleObject SimpleObject { get; set; }
    }
}