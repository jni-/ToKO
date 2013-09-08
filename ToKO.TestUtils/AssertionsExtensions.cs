using FluentAssertions;
using FluentAssertions.Primitives;

namespace ToKO.TestUtils
{
    public static class AssertionsExtensions
    {
        public static AndConstraint<StringAssertions> BeInObject(this StringAssertions assert, string objectName, string ctorBody)
        {
            return assert.Be(InObject(objectName, ctorBody));
        }

        private static string InObject(string objectName, string ctorBody)
        {
            return objectName + " = (function() {function " + objectName + "() {" + ctorBody + "}return " + objectName +
                   ";})();";
        }
    }
}