using System.Collections.Generic;
using System.Text;

namespace ToKO.Generators
{
    public static class KOModelJavascriptObjectExtensions
    {
        public static string ToJavascriptObject(this KOModel model, string name)
        {
            var sb = new StringBuilder();
            sb.Append(name + " = (function() {");
            sb.AppendLine("\tfunction " + name + "() {");
            AddAttibutes(sb, model);
            AddObjects(sb, model);
            AddArrays(sb, model);
            sb.AppendLine("}");
            sb.AppendLine("return " + name + ";");
            sb.AppendLine("})();");
            return sb.ToString();
        }

        private static void AddAttibutes(StringBuilder sb, KOModel model)
        {
            foreach (var attribute in model.Attributes)
            {
                var line = "\tthis." + attribute.Key + ": " + KOUtils.WrapObservable(KOUtils.ToJavascriptValue(attribute.Value.Value), attribute.Value.Observable);
                line += ";";
                sb.AppendLine(line);
            }
        }

        private static void AddObjects(StringBuilder sb, KOModel model)
        {
            foreach (var observable in model.Objects)
            {
                var line = "\tthis." + observable.Key + ": " +
                           KOUtils.WrapObservable(observable.Value.Value.ToJavascript(), observable.Value.Observable);
                line += ";";
                sb.AppendLine(line);
            }
        }


        private static void AddArrays(StringBuilder sb, KOModel model)
        {
            foreach (var observableArray in model.Arrays)
            {
                var line = new StringBuilder();
                line.Append("\tthis.");
                line.Append(observableArray.Key + ": " + KOUtils.WrapObservableArray(BuildArray(observableArray), observableArray.Value.Observable));
                line.Append(";");
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
