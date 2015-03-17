﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCop.Aspects.Tests.PropertyAspect.Subjects;
using System.Collections.Generic;

namespace NCop.Aspects.Tests
{
    [TestClass]
    public class PropertyAspectInterceptionTest : AbstractAspectTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 

        [TestInitialize]
        public void ResetPropertyInterceptionAspectsList() {
            PropertyInterceptionAspects.PropertyInterceptionAspectsList = new List<AspectJoinPoints>();
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Property_AnnotatedWithPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.PropertyInterceptionAspectOnFullProperty = list;
            list = instance.PropertyInterceptionAspectOnFullProperty;

            CollectionAssert.AreEqual(list, new PropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void GetProperty_AnnotatedWithPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = instance.PropertyInterceptionAspectOnPartialGetProperty;

            CollectionAssert.AreEqual(list, new GetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void SetProperty_AnnotatedWithPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.PropertyInterceptionAspectOnPartialSetProperty = list;

            CollectionAssert.AreEqual(list, new SetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void Property_AnnotatedWithSetPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.SetPropertyInterceptionAspectOnFullProperty = list;

            CollectionAssert.AreEqual(list, new SetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void Property_AnnotatedWithGetPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = instance.GetPropertyInterceptionAspectOnFullProperty;

            CollectionAssert.AreEqual(list, new GetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void SetProperty_AnnotatedWithSetPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.SetPropertyInterceptionAspectOnPartialSetProperty = list;

            CollectionAssert.AreEqual(list, new SetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void GetProperty_AnnotatedWithGetPropertyInterceptionAspect_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = instance.GetPropertyInterceptionAspectOnPartialGetProperty;

            CollectionAssert.AreEqual(list, new GetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void GetProperty_AnnotatedWithMultipleGetPropertyInterceptionAspects_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = instance.MultipleGetPropertiesInterceptionAspectsOnGetProperty;

            CollectionAssert.AreEqual(list, new MultipleGetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void SetProperty_AnnotatedWithMultipleSetPropertyInterceptionAspects_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.MultipleSetPropertiesInterceptionAspectsOnSetProperty = list;

            CollectionAssert.AreEqual(list, new MultipleSetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void GetProperty_AnnotatedWithMultiplePropertyInterceptionAspects_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = instance.MultiplePropertyInterceptionAspectOnPartialGetProperty;

            CollectionAssert.AreEqual(list, new MultipleGetPropertyInterceptionAspectOrderedJoinPoints());
        }

        [TestMethod]
        public void SetProperty_AnnotatedWithMultiplePropertyInterceptionAspects_ReturnsTheCorrectSequenceOfAdvices() {
            var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
            var list = new List<AspectJoinPoints>();

            instance.MultiplePropertyInterceptionAspectOnPartialSetProperty = list;

            CollectionAssert.AreEqual(list, new MultipleSetPropertyInterceptionAspectOrderedJoinPoints());
        }

        //[TestMethod]
        //public void SetAndGetProperty_AnnotatedWithMultiplePropertyInterceptionAspects_ReturnsTheCorrectSequenceOfAdvices() {
        //    var instance = container.Resolve<IPropertyAspectSubjectsComposite>();
        //    var list = new List<AspectJoinPoints>();

        //    instance.GetAndSetPropertyInterceptionAspectOnFullProperty = list;
        //    list = instance.GetAndSetPropertyInterceptionAspectOnFullProperty;

        //    CollectionAssert.AreEqual(list, new MultiplePropertyInterceptionAspectOrderedJoinPoints());
        //}
    }
}