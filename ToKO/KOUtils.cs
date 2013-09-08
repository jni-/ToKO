using System;
using ToKO.Generators;

namespace ToKO
{
    public class KOUtils
    {
        public static string WrapObservableArray(string arrayValue, bool observable)
        {
            if (observable)
            {
                return "ko.observableArray([" + arrayValue + "])";
            }
            return arrayValue;
        }

        public static string WrapObservable(string value, bool observable)
        {
            if (observable)
            {
                return "ko.observable(" + value + ")";
            }
            return value;
        }

        public static string ToJavascriptValue(dynamic value)
        {
            int dummyInt;
            double dummyDouble;
            if (value == null)
            {
                return "undefined";
            }

            if (value is KOModel)
            {
                return (value as KOModel).ToJavascript();
            }

            if (value is DateTime)
            {
                return String.Format("new Date({0}, {1}, {2}, {3}, {4}, {5}, {6})", value.Year, value.Month,
                    value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
            }

            if (int.TryParse(value.ToString(), out dummyInt) || double.TryParse(value.ToString(), out dummyDouble))
            {
                return value.ToString();
            }

            return "'" + value.ToString() + "'";
        }
    }
}