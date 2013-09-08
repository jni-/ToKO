using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ToKO.Attributes;

namespace ToKO
{
    public static class ToKOExtensions
    {
        public static KOModel ToKO(this IEnumerable<object> list, string name)
        {
            var model = new KOModel();
            model.AddArray(name, list.Select(item => ToKO((object) item)).ToList(), true);
            return model;
        }

        public static KOModel ToKO(this object item)
        {
            var model = new KOModel();
            var propertyInfos = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertyInfos)
            {
                if (!property.GetCustomAttributes(typeof (KOIgnoreAttribute)).Any())
                {
                    var observable = !property.GetCustomAttributes(typeof (KONotObservableAttribute)).Any();
                    AddToKOModel(item, property, model, observable);
                }
            }
            return model;
        }

        private static void AddToKOModel(object item, PropertyInfo property, KOModel model, bool observable)
        {
            if (IsSimpleType(property.PropertyType))
            {
                AddAttribute(item, property, model, observable);
            }
            else if (IsList(property.PropertyType))
            {
                AddArray(item, property, model, observable);
            }
            else
            {
                AddObject(item, property, model, observable);
            }
        }

        private static void AddAttribute(object item, PropertyInfo property, KOModel model, bool observable)
        {
            model.AddAttribute(ToJavascriptName(property), property.GetValue(item), observable);
        }

        private static void AddArray(object item, PropertyInfo property, KOModel model, bool observable)
        {
            var listType = GetListType(property.PropertyType);
            if (IsSimpleType(listType))
            {
                dynamic list = property.GetValue(item) ?? new List<object>();
                model.AddArray(ToJavascriptName(property), list, observable);
            }
            else
            {
                var list = property.GetValue(item) as IEnumerable<object> ?? new List<object>();
                model.AddArray(ToJavascriptName(property), list.Select(x => x.ToKO()), observable);
            }
        }

        private static void AddObject(object item, PropertyInfo property, KOModel model, bool observable)
        {
            model.AddObject(ToJavascriptName(property), property.GetValue(item).ToKO(), observable);
        }

        private static bool IsList(Type type)
        {
            return GetListType(type) != null;
        }

        private static Type GetListType(Type type)
        {
            return (from face in type.GetInterfaces()
                where face.IsGenericType && face.GetGenericTypeDefinition() == typeof (IEnumerable<>)
                select face.GetGenericArguments()[0]).FirstOrDefault();
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsValueType || type.IsPrimitive || type.IsAssignableFrom(typeof(string));
        }

        private static string ToJavascriptName(PropertyInfo property)
        {
            var nameAttribute = property.GetCustomAttributes(typeof (KONameAttribute)).FirstOrDefault() as KONameAttribute;
            if (nameAttribute != null)
            {
                return nameAttribute.Name;
            }
            var name = property.Name;
            return char.ToLower(name[0]) + name.Substring(1);
        }
    }
}