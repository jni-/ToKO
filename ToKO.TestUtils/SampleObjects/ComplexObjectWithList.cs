using System.Collections.Generic;

namespace ToKO.TestUtils.SampleObjects
{
    public class ComplexObjectWithList
    {
        public int Number { get; set; }
        public List<SimpleObject> SimpleObjects { get; set; }
        public List<int> Numbers { get; set; }
    }
}