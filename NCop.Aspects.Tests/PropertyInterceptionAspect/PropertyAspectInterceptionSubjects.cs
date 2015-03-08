﻿using NCop.Aspects.Framework;
using NCop.Composite.Framework;
using NCop.Mixins.Framework;
using System.Collections.Generic;

namespace NCop.Aspects.Tests.PropertyAspect.Subjects
{
    public interface IPropertyAspectSubjects
    {
        List<AspectJoinPoints> PropertyInterceptionAspectOnFullProperty { get; set; }
        //List<AspectJoinPoints> PropertyInterceptionAspectOnPartialGetProperty { get; }
        //List<AspectJoinPoints> PropertyInterceptionAspectOnPartialSetProperty { set; }
        //List<AspectJoinPoints> SetPropertyInterceptionAspectOnFullProperty { get; set; }
        //List<AspectJoinPoints> GetPropertyInterceptionAspectOnFullProperty { get; set; }
        //List<AspectJoinPoints> SetPropertyInterceptionAspectOnPartialSetProperty { set; }
        //List<AspectJoinPoints> GetPropertyInterceptionAspectOnPartialGetProperty { get; }
        //List<AspectJoinPoints> MultipleSetPropertiesInterceptionAspectsOnSetProperty { set; }
        //List<AspectJoinPoints> MultipleGetPropertiesInterceptionAspectsOnGetProperty { get; }
        //List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialGetProperty { get; }
        //List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialSetProperty { set; }
        //List<AspectJoinPoints> MultiplePropertyInterceptionAspectsOnFullProperty { get; set; }
        //List<AspectJoinPoints> GetAndSetPropertyInterceptionAspectOnFullProperty { get; set; }
        //List<AspectJoinPoints> GetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { get; }
        //List<AspectJoinPoints> SetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { set; }
    }

    public class Mixin : IPropertyAspectSubjects
    {
        public List<AspectJoinPoints> PropertyInterceptionAspectOnFullProperty { get; set; }

        public List<AspectJoinPoints> PropertyInterceptionAspectOnPartialGetProperty { get; set; }

        public List<AspectJoinPoints> PropertyInterceptionAspectOnPartialSetProperty { get; set; }

        public List<AspectJoinPoints> SetPropertyInterceptionAspectOnFullProperty { get; set; }

        public List<AspectJoinPoints> GetPropertyInterceptionAspectOnFullProperty { get; set; }

        public List<AspectJoinPoints> SetPropertyInterceptionAspectOnPartialSetProperty { get; set; }
        
        public List<AspectJoinPoints> GetPropertyInterceptionAspectOnPartialGetProperty { get; set; }

        public List<AspectJoinPoints> MultipleSetPropertiesInterceptionAspectsOnSetProperty { get; set; }

        public List<AspectJoinPoints> MultipleGetPropertiesInterceptionAspectsOnGetProperty { get; set; }

        public List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialGetProperty { get; set; }

        public List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialSetProperty { get; set; }

        public List<AspectJoinPoints> MultiplePropertyInterceptionAspectsOnFullProperty { get; set; }

        public List<AspectJoinPoints> GetAndSetPropertyInterceptionAspectOnFullProperty { get; set; }

        public List<AspectJoinPoints> GetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { set; get; }

        public List<AspectJoinPoints> SetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { set; get; }
    }

    [TransientComposite]
    [Mixins(typeof(Mixin))]
    public interface IPropertyAspectSubjectsComposite : IPropertyAspectSubjects
    {
        [PropertyInterceptionAspect(typeof(FullPropertyInterceptionAspect))]
        new List<AspectJoinPoints> PropertyInterceptionAspectOnFullProperty { get; set; }
        //new List<AspectJoinPoints> PropertyInterceptionAspectOnPartialGetProperty { get; }
        //new List<AspectJoinPoints> PropertyInterceptionAspectOnPartialSetProperty { set; }
        //new List<AspectJoinPoints> SetPropertyInterceptionAspectOnFullProperty { get; set; }
        //new List<AspectJoinPoints> GetPropertyInterceptionAspectOnFullProperty { get; set; }
        //new List<AspectJoinPoints> SetPropertyInterceptionAspectOnPartialSetProperty { set; }
        //new List<AspectJoinPoints> GetPropertyInterceptionAspectOnPartialGetProperty { get; }
        //new List<AspectJoinPoints> MultipleSetPropertiesInterceptionAspectsOnSetProperty { set; }
        //new List<AspectJoinPoints> MultipleGetPropertiesInterceptionAspectsOnGetProperty { get; }
        //new List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialGetProperty { get; }
        //new List<AspectJoinPoints> MultiplePropertyInterceptionAspectOnPartialSetProperty { set; }
        //new List<AspectJoinPoints> MultiplePropertyInterceptionAspectsOnFullProperty { get; set; }
        //new List<AspectJoinPoints> GetAndSetPropertyInterceptionAspectOnFullProperty { get; set; }
        //new List<AspectJoinPoints> GetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { get; }
        //new List<AspectJoinPoints> SetPropertyInterceptionAspectWrappedWithPropertyInterceptionAspect { set; }
    }

    public class FullPropertyInterceptionAspect : PropertyInterceptionAspect<List<AspectJoinPoints>>
    {
        public override void OnGetValue(PropertyInterceptionArgs<List<AspectJoinPoints>> args) {
            var value = args.GetCurrentValue();

            value.Add(AspectJoinPoints.GetPropertyInterception);
            args.ProceedGetValue();
        }

        public override void OnSetValue(PropertyInterceptionArgs<List<AspectJoinPoints>> args) {
            args.Value.Add(AspectJoinPoints.SetPropertyInterception);
            base.OnSetValue(args);
        }
    }
}