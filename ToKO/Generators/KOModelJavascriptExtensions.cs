using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToKO.Generators
{
    public static class KOModelJavascriptExtensions
    {
        public static string ToJavascript(this KOModel model)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            AddAttibutes(sb, model);
            AddObjects(sb, model);
            AddArrays(sb, model);
            sb.Append("}");
            return sb.ToString();
        }

        private static void AddAttibutes(StringBuilder sb, KOModel model)
        {
            foreach (var attribute in model.Attributes)
            {
                var line = "\t" + attribute.Key + ": " + KOUtils.WrapObservable(KOUtils.ToJavascriptValue(attribute.Value.Value), attribute.Value.Observable);
                if (attribute.Key != model.Attributes.Last().Key || model.Objects.Any() || model.Arrays.Any())
                {
                    line += ", ";
                }
                sb.AppendLine(line);
            }
        }

        private static void AddObjects(StringBuilder sb, KOModel model)
        {
            foreach (var observable in model.Objects)
            {
                var line = "\t" + observable.Key + ": " +
                           KOUtils.WrapObservable(observable.Value.Value.ToJavascript(), observable.Value.Observable);
                if (observable.Key != model.Objects.Last().Key || model.Arrays.Any())
                {
                    line += ", ";
                }
                sb.AppendLine(line);
            }
        }


        private static void AddArrays(StringBuilder sb, KOModel model)
        {
            foreach (var observableArray in model.Arrays)
            {
                var line = new StringBuilder();
                line.Append("\t");
                line.Append(observableArray.Key + ": " + KOUtils.WrapObservableArray(BuildArray(observableArray), observableArray.Value.Observable));
                if (observableArray.Key != model.Arrays.Last().Key)
                {
                    line.Append(", ");
                }
                sb.AppendLine(line.ToString());
            }
        }

        private static string BuildArray(KeyValuePair<string, KOValue<dynamic>> observableArray)
        {
            var line = new StringBuilder();
            var first = true;
            foreach (var item in observableArray.Value.Value)
            {
                if (!first)
                {
                    line.AppendLine(", ");
                }
                line.Append(KOUtils.ToJavascriptValue(item));
                first = false;
            }
            return line.ToString();
        }
    }
}