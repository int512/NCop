﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web;

namespace NCop.IoC.Tests
{
    [TestClass]
    public class NCopContainerTest
    {
        [TestInitialize()]
        public void InitializeAllprivateVariablesForEachTest() {
            HttpContext.Current = null;
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationException))]
        public void Resolve_UsingAutoFactoryOfInterface_ThrowsRegistrationException() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>();
            });

            var instance = container.Resolve<IFoo>();
        }

        [TestMethod]
        public void ResolveOfDependetType_UsingFactoryThatCallsResolveOfDependencyObject_ReturnsTheResolvedInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>();
                registry.Register<IBar>(r => new Bar(r.Resolve<Foo>()));
            });

            Assert.IsNotNull(container.Resolve<IBar>());
        }

        [TestMethod]
        public void Resolve_UsingFactoryWithTypedArgument_ReturnsTheResolvedInstanceThatHoldsTheResultOfTypedArgument() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo, string>((r, name) => new Foo(name));
            });

            var instance = container.Resolve<string, IFoo>("Test");

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Name, "Test");
        }

        [TestMethod]
        public void Resolve_UsingAutoFactoryAndCastingUsingTheAsExpresion_ReturnsTheResolvedInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
            });

            var instance = container.Resolve<IFoo>();

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void Resolve_UsingNamedExpressionOfSpecificTypeAndAutoWithoutNameOfTheSameType_ReturnsDifferentInstancesOfTheSameType() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().Named("NCop");
                registry.Register<Foo>();
            });

            var instance = container.Resolve<Foo>();
            var namedInstance = container.ResolveNamed<Foo>("NCop");

            Assert.AreNotSame(namedInstance, instance);
        }

        [TestMethod]
        public void Resolve_UsingAsSingletonExpressionRegistrationWithAutoFactory_ReturnsTheSameObjectForDifferentResolveCalls() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton();
            });

            var instance = container.Resolve<Foo>();
            var instatance2 = container.Resolve<Foo>();

            Assert.AreSame(instance, instatance2);
        }

        [TestMethod]
        public void Resolve_UsingAsSingletonExpressionRegistrationWithFactory_ReturnsTheSameObjectForDifferentResolveCalls() {
            var container = new NCopContainer(registry => {
                registry.Register(reg => new Foo()).AsSingleton();
            });

            var instance = container.Resolve<Foo>();
            var instatance2 = container.Resolve<Foo>();

            Assert.AreSame(instance, instatance2);
        }

        [TestMethod]
        public void Resolve_UsingAutoFactoryAndCastingWithAsExpressionAndAfterThatUsingTheAsSingletonExpressionRegistration_ReturnsTheSameObjectForDifferentResolveCalls() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>().AsSingleton();
            });

            var instance = container.Resolve<IFoo>();
            var instatance2 = container.Resolve<IFoo>();

            Assert.AreSame(instance, instatance2);
        }

        [TestMethod]
        public void Resolve_UsingAutoFactoryThatBindsToSelf_ReturnsTheResolvedInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().ToSelf();
            });

            var instance = container.Resolve<Foo>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(Foo));
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationException))]
        public void Resolve_UsingAutoFactoryOfInterfaceThatBindsToSelf_ThrowsRegistrationException() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().ToSelf();
            });

            container.Resolve<IFoo>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionException))]
        public void TryResolve_NotRegisteredService_ThrowsRegistrationException() {
            var container = new NCopContainer(registry => { });
            container.Resolve<IFoo>();
        }

        [TestMethod]
        public void TryResolve_NotRegisteredService_DontThrowsRegistrationExceptionAndReturnsNull() {
            var container = new NCopContainer(registry => { });
            var instance = container.TryResolve<IFoo>();

            Assert.IsNull(instance);
        }

        [TestMethod]
        public void TryResolve_NotRegisteredServiceWithMultipuleArguments_DontThrowsRegistrationExceptionAndReturnsNull() {
            var container = new NCopContainer(registry => { });
            var instance = container.TryResolve<string, int, IFoo>("NCop", 9);

            Assert.IsNull(instance);
        }

        [TestMethod]
        public void Resolve_InChildContainerUsesParentContainer_ReusesParentContainerAndReturnsTheReolvedObject() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>();
            });

            var childContainer = container.CreateChildContainer(registry => { });
            var instance = childContainer.Resolve<Foo>();

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void ResolveInChildContainerAndInParentContainer_UsingSingletonRegistrationInParentContainer_ReturnsTheSameInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().WithinHierarchy();
            });

            var childContainer = container.CreateChildContainer(registry => { });
            var instance = container.Resolve<Foo>();
            var instance2 = childContainer.Resolve<Foo>();

            Assert.AreSame(instance, instance2);
        }

        [TestMethod]
        public void ResolveInChildContainerAndInParentContainer_UsingAutoRegistrationInParentContainerAsSingletonReusedWithinContainer_ReturnsTheSameInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().WithinHierarchy();
            });

            var childContainer = container.CreateChildContainer(registry => { });
            var instance = container.Resolve<Foo>();
            var instance2 = childContainer.Resolve<Foo>();

            Assert.AreSame(instance, instance2);
        }

        [TestMethod]
        public void ResolveInChildContainerAndInParentContainer_UsingAutoRegistrationInParentContainerAsSingletoneReusedWithinHierarchy_ReturnsNotTheSameInstance() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().WithinContainer();
            });

            var childContainer = container.CreateChildContainer(registry => { });
            var instance1 = container.Resolve<Foo>();
            var instance2 = container.Resolve<Foo>();
            var instance3 = childContainer.Resolve<Foo>();
            var instance4 = childContainer.Resolve<Foo>();

            Assert.AreSame(instance1, instance2);
            Assert.AreNotSame(instance1, instance3);
            Assert.AreSame(instance3, instance4);
        }

        [TestMethod]
        public void DisposeOfContainer_OfDisposableObjectWhichIsOwnedByContainer_ReturnsDispsoedIndication() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().OwnedByContainer();
            });

            var instance = container.Resolve<Foo>();

            Assert.IsFalse(instance.IsDisposed);
            container.Dispose();
            Assert.IsTrue(instance.IsDisposed);
        }

        [TestMethod]
        public void DisposeOfParentContainer_OfDisposableObjectWhichIsOwnedByChildContainer_ReturnsDispsoedIndication() {
            var parentContainer = new NCopContainer(registry => {
            });

            var childContainer = parentContainer.CreateChildContainer(registry => {
                registry.Register<Foo>();
            });

            var instance = childContainer.Resolve<Foo>();

            Assert.IsFalse(instance.IsDisposed);
            parentContainer.Dispose();
            Assert.IsFalse(instance.IsDisposed);
        }

        [TestMethod]
        public void DisposeOfContainer_OfDisposableObjectWhichIsOwnedExternally_ReturnsNotDispsoedIndication() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().OwnedExternally();
            });

            var instance = container.Resolve<Foo>();

            Assert.IsFalse(instance.IsDisposed);
            container.Dispose();
            Assert.IsFalse(instance.IsDisposed);
        }

        [TestMethod]
        public void DisposeOfContainer_OfDisposableNamedObjectWithAnArgumentWhichIsOwnedExternally_ReturnsTheNamedInstanceWithNotDispsoedIndication() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo, string>((c, name) => new Foo(name))
                        .Named("NCop")
                        .OwnedExternally();
            });

            var instance = container.TryResolve<string, IFoo>("NCop", "NCop") as Foo;

            container.Dispose();

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.Name.Equals("NCop"));
            Assert.IsFalse(instance.IsDisposed);
        }

        [TestMethod]
        public void AutoRegister_WithPropertyDependency_ReturnsTheResolvedInstanceFilledWithTheAutoResolvedProperty() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<PropertyDependency>();
            });

            var instance = container.Resolve<PropertyDependency>();

            Assert.IsNotNull(instance.Foo);
        }

        [TestMethod]
        public void AutoRegister_WithConstructorDependencyThatHasOneDependentArgument_ReturnsTheResolvedInstanceFilledWithTheDependentArgument() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<CtorDependency>();
            });

            var instance = container.Resolve<CtorDependency>();

            Assert.IsNotNull(instance.Foo);
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationException))]
        public void AutoRegister_OfTypeThatHasMoreThanOneConstructorAndWithoutDependencyAttribute_ThrowsRegistrationException() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<AmbiguousConstructor>();
            });

            var instance = container.Resolve<AmbiguousConstructor>();
        }

        [TestMethod]
        public void AutoRegister_OfTypeThatHasMoreThanOneConstructorWithDependencyAttribute_ReturnsResolvedInstanceAndDontThrowsRegistrationException() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<AmbiguousConstructorFixedWithDependencyAttribute>();
            });

            var instance = container.Resolve<AmbiguousConstructorFixedWithDependencyAttribute>();

            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.Foo);
        }

        [TestMethod]
        public void AutoRegister_OfTypeWithDependencyAwareThatHasTwoDependentPropertiesThatOneOfThemIsAnnotatedWithDependencyAttribute_ReturnsResolvedInstanceWithOnlyOneDependentProperty() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<DependencyAwareClass>();
            });

            var instance = container.Resolve<DependencyAwareClass>();

            Assert.IsNotNull(instance.Injected);
            Assert.IsNull(instance.Ignored);
        }

        [TestMethod]
        public void AutoRegister_OfTypeThatHasTwoDependentPropertiesThatOneOfThemIsAnnotatedWithIgnoreDependency_ReturnsResolvedInstanceWithOnlyOneDependentProperty() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>();
                registry.RegisterAuto<IgnoreDependencyClass>();
            });

            var instance = container.Resolve<IgnoreDependencyClass>();

            Assert.IsNotNull(instance.Injected);
            Assert.IsNull(instance.Ignored);
        }

        [TestMethod]
        public void AutoRegister_OfTypeThatHasTwoPropertiesThatOneOfThemIsDependentAndTheOtherFilledByDependentConstructor_ReturnsResolvedInstanceWithBothPropertiesFilled() {
            var container = new NCopContainer(registry => {
                registry.Register<IFoo>().From<Foo>().AsSingleton();
                registry.RegisterAuto<Bar>();
            });

            var instance = container.Resolve<Bar>();

            Assert.IsNotNull(instance.ByCtor);
            Assert.IsNotNull(instance.ByProperty);
            Assert.AreSame(instance.ByCtor, instance.ByProperty);
        }

        [TestMethod]
        public void AutoRegister_OfInterfaceAndCastingWithAsExpressionToConcreteTypeThatHasOneDependentProperty_ReturnsResolvedInstanceWithFilledProperty() {
            var container = new NCopContainer(registry => {
                registry.Register<Baz>().ToSelf();
                registry.RegisterAuto<IFoo>().From<Boo>();
            });

            var instance = container.Resolve<IFoo>() as Boo;

            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.Baz);
        }

        [TestMethod]
        public async Task Resolve_UsingAsPerThreadSingletonExpressionRegistrationWithAutoFactory_ReturnsDiffrentOjectsForDiffrentThreadsAndTheSameObjectForDifferentResolveCallsOnTheSameThread() {
            Foo foo1 = null;
            Foo foo2 = null;

            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().PerThread();
            });

            foo1 = await Task.Factory.StartNew(() => container.Resolve<Foo>());
            foo2 = await Task.Factory.StartNew(() => container.Resolve<Foo>());
            
            Assert.AreNotEqual(foo1, foo2);
            Assert.AreEqual(container.Resolve<Foo>(), container.Resolve<Foo>());
        }

        [TestMethod]
        [ExpectedException(typeof(LifetimeStragtegyException))]
        public void Resolve_UsingAsPerHttpRequestExpressionRegistrationWhenThereIsNoHttpContext_ThrowsLifetimeStragtegyException() {
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().PerHttpRequest();
            });

            container.Resolve<Foo>();
        }

        [TestMethod]
        public void Resolve_UsingAsPerHttpRequestExpressionRegistrationWhenUsingTwoDiffrentContext_ReturnsDiffrentInstancesPerHttpContextInstance() {
            Foo foo = null;
            var container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().PerHttpRequest();
            });

            SetHttpContext();
            foo = container.Resolve<Foo>();
            Assert.AreEqual(foo, container.Resolve<Foo>());
            SetHttpContext();
            Assert.AreNotEqual(foo, container.Resolve<Foo>());
        }

        [TestMethod]
        public void Resolve_UsingAsPerHybridRequestExpressionRegistrationWhenHttpContextIsAvailable_ReturnsDiffrentInstancesPerHttpContextInstance() {
            Foo foo = null;
            NCopContainer container = null;

            SetHttpContext();
            container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().PerHybridRequest();
            });

            foo = container.Resolve<Foo>();
            Assert.AreEqual(foo, container.Resolve<Foo>());
            SetHttpContext();
            Assert.AreNotEqual(foo, container.Resolve<Foo>());
        }

        [TestMethod]
        public async Task Resolve_UsingAsPerHybridRequestExpressionRegistrationWhenHttpContextIsNotAvailable_ReturnsTheSameInstancePerThreadRequest() {
            Foo foo = null;
            Foo foo2 = null;
            NCopContainer container = null;

            container = new NCopContainer(registry => {
                registry.Register<Foo>().AsSingleton().PerHybridRequest();
            });

            foo = container.Resolve<Foo>();
            Assert.AreEqual(foo, container.Resolve<Foo>());
            foo2 = await Task.Factory.StartNew(() => container.Resolve<Foo>());
            Assert.AreEqual(foo, foo2);
        }

        private static void SetHttpContext() {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
        }
    }
}