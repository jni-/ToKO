using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ToKO.Generators;
using ToKO.TestUtils;
using ToKO.TestUtils.SampleObjects;

namespace ToKO.Tests.Generators
{
    [TestClass]
    class KOModelJavascriptObjectExtensionsTest
    {
        const string ObjectName = "Name";

        [TestMethod]
        public void SerialiseSimpleObjectList()
        {
            var simpleObject = new SimpleObject {Integer = 3, SomeString = "test"};

            var model = new List<SimpleObject> {simpleObject}.ToKO("list");

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName, "this.list: ko.observableArray([{integer: ko.observable(3), someString: ko.observable('test')}]);");
        }

        [TestMethod]
        public void SerialiseSimpleObject()
        {
            var simpleObject = new SimpleObject {Integer = 3, SomeString = "test"};

            var model = simpleObject.ToKO();

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName, "this.integer: ko.observable(3);this.someString: ko.observable('test');");
        }

        [TestMethod]
        public void SerializeComplexObject()
        {
            var simpleObject = new SimpleObject { Integer = 3, SomeString = "test" };
            var complexObject = new ComplexObject {Number = 4, SimpleObject = simpleObject};

            var model = complexObject.ToKO();

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName, 
                                                        "this.number: ko.observable(4);" +
                                                        "this.simpleObject: ko.observable({" +
                                                            "integer: ko.observable(3), " +
                                                            "someString: ko.observable('test')" +
                                                        "});".Simplify());
        }
    
        [TestMethod]
        public void SerializeComplexObjectWithList()
        {
            var simpleObject = new SimpleObject { Integer = 3, SomeString = "test" };
            var complexObjectWithList = new ComplexObjectWithList {Number = 4, SimpleObjects = new List<SimpleObject> {simpleObject}, Numbers = new List<int> {1, 2, 3}};

            var model = complexObjectWithList.ToKO();

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName,
                                                        "this.number: ko.observable(4);" +
                                                        "this.simpleObjects: ko.observableArray([{" +
                                                            "integer: ko.observable(3), " +
                                                            "someString: ko.observable('test')" +
                                                        "}]);" +
                                                        "this.numbers: ko.observableArray([1, 2, 3]);".Simplify());
        }    

        [TestMethod]
        public void SerializeComplexObjectWithEmptyList()
        {
            var complexObjectWithList = new ComplexObjectWithList {Number = 4, SimpleObjects = new List<SimpleObject>() };

            var model = complexObjectWithList.ToKO();

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName, 
                                                        "this.number: ko.observable(4);" +
                                                        "this.simpleObjects: ko.observableArray([]);" +
                                                        "this.numbers: ko.observableArray([]);".Simplify());
        }

        [TestMethod]
        public void SerializeSimpleObjectWithKOAttributes()
        {
            var simpleObject = new SimpleObjectWithAttributes {SomeString = "test", Date = new DateTime(2013, 10, 30, 12, 15, 16), SimpleObject = new SimpleObject {Integer = 1, SomeString = "test2"}};

            var model = simpleObject.ToKO();

            model.ToJavascriptObject(ObjectName).Simplify().Should().BeInObject(ObjectName, 
                                                        "this.surname: ko.observable('test');" +
                                                        "this.date: new Date(2013, 10, 30, 12, 15, 16, 0);" +
                                                        "this.nested: {" +
                                                            "integer: ko.observable(1), " +
                                                            "someString: ko.observable('test2')" +
                                                        "};".Simplify());
        }

       
    }
}
