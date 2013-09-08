using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToKO.Generators;
using ToKO.TestUtils;
using ToKO.TestUtils.SampleObjects;

namespace ToKO.Tests.Generators
{
    [TestClass]
    public class KOModelJavascriptExtensionsTest
    {
        [TestMethod]
        public void SerialiseSimpleObjectList()
        {
            var simpleObject = new SimpleObject {Integer = 3, SomeString = "test"};

            var model = new List<SimpleObject> {simpleObject}.ToKO("list");

            model.ToJavascript().Simplify().Should().Be("{list: ko.observableArray([{integer: ko.observable(3), someString: ko.observable('test')}])}");
        }

        [TestMethod]
        public void SerialiseSimpleObject()
        {
            var simpleObject = new SimpleObject {Integer = 3, SomeString = "test"};

            var model = simpleObject.ToKO();

            model.ToJavascript().Simplify().Should().Be("{integer: ko.observable(3), someString: ko.observable('test')}");
        }

        [TestMethod]
        public void SerializeComplexObject()
        {
            var simpleObject = new SimpleObject { Integer = 3, SomeString = "test" };
            var complexObject = new ComplexObject {Number = 4, SimpleObject = simpleObject};

            var model = complexObject.ToKO();

            model.ToJavascript().Simplify().Should().Be("{" +
                                                        "number: ko.observable(4), " +
                                                        "simpleObject: ko.observable({" +
                                                            "integer: ko.observable(3), " +
                                                            "someString: ko.observable('test')" +
                                                        "})" +
                                                        "}".Simplify());
        }
    
        [TestMethod]
        public void SerializeComplexObjectWithList()
        {
            var simpleObject = new SimpleObject { Integer = 3, SomeString = "test" };
            var complexObjectWithList = new ComplexObjectWithList {Number = 4, SimpleObjects = new List<SimpleObject> {simpleObject}, Numbers = new List<int> {1, 2, 3}};

            var model = complexObjectWithList.ToKO();

            model.ToJavascript().Simplify().Should().Be("{" +
                                                        "number: ko.observable(4), " +
                                                        "simpleObjects: ko.observableArray([{" +
                                                            "integer: ko.observable(3), " +
                                                            "someString: ko.observable('test')" +
                                                        "}]), " +
                                                        "numbers: ko.observableArray([1, 2, 3])" +
                                                        "}".Simplify());
        }    

        [TestMethod]
        public void SerializeComplexObjectWithEmptyList()
        {
            var complexObjectWithList = new ComplexObjectWithList {Number = 4, SimpleObjects = new List<SimpleObject>() };

            var model = complexObjectWithList.ToKO();

            model.ToJavascript().Simplify().Should().Be("{" +
                                                        "number: ko.observable(4), " +
                                                        "simpleObjects: ko.observableArray([]), " +
                                                        "numbers: ko.observableArray([])" +
                                                        "}".Simplify());
        }

        [TestMethod]
        public void SerializeSimpleObjectWithKOAttributes()
        {
            var simpleObject = new SimpleObjectWithAttributes {SomeString = "test", Date = new DateTime(2013, 10, 30, 12, 15, 16), SimpleObject = new SimpleObject {Integer = 1, SomeString = "test2"}};

            var model = simpleObject.ToKO();

            model.ToJavascript().Simplify().Should().Be("{" +
                                                        "surname: ko.observable('test'), " +
                                                        "date: new Date(2013, 10, 30, 12, 15, 16, 0), " +
                                                        "nested: {" +
                                                            "integer: ko.observable(1), " +
                                                            "someString: ko.observable('test2')" +
                                                        "}" +
                                                        "}");
        }
    }
}
