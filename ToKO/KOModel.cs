using System.Collections.Generic;

namespace ToKO
{
    public class KOModel
    {
        public IDictionary<string, KOValue<dynamic>> Attributes { get; set; }
        public IDictionary<string, KOValue<KOModel>> Objects { get; set; }
        public IDictionary<string, KOValue<dynamic>> Arrays { get; set; }

        public KOModel()
        {
            Attributes = new Dictionary<string, KOValue<dynamic>>();
            Objects = new Dictionary<string, KOValue<KOModel>>();
            Arrays = new Dictionary<string, KOValue<dynamic>>();
        }
        public void AddAttribute(string name, dynamic value, bool observable)
        {
            Attributes.Add(name, new KOValue<dynamic>(value, observable));
        }

        public void AddObject(string name, KOModel model, bool observable)
        {
            Objects.Add(name, new KOValue<KOModel>(model, observable));
        }

        public void AddArray(string name, dynamic value, bool observable)
        {
            Arrays.Add(name, new KOValue<dynamic>(value, observable));
        }
    }
}