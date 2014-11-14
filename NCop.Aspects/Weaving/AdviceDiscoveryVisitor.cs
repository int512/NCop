﻿using NCop.Aspects.Advices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCop.Aspects.Weaving
{
    public class AdviceDiscoveryVisitor
    {
        internal bool HasFinallyAdvice { get; private set; }

        internal bool HasOnMethodEntryAdvice { get; private set; }

        internal bool HasOnMethodInvokeAdvice { get; private set; }

        internal bool HasOnMethodSuccessAdvice { get; private set; }

        internal bool HasOnMethodExceptionAdvice { get; private set; }

        internal bool HasOnSetPropertyInvokeAdvice { get; private set; }

        internal bool HasOnGetPropertyInvokeAdvice { get; private set; }

        internal FinallyAdviceAttribute FinallyAdvice { get; private set; }

        internal OnMethodEntryAdviceAttribute OnMethodEntryAdvice { get; private set; }

        internal OnMethodInvokeAdviceAttribute OnMethodInvokeAdvice { get; private set; }

        internal OnMethodSuccessAdviceAttribute OnMethodSuccessAdvice { get; private set; }

        internal OnMethodExceptionAdviceAttribute OnMethodExceptionAdvice { get; private set; }
        
        internal OnGetPropertyInvokeAdviceAttribute OnGetPropertyInvokeAdvice { get; private set; }

        internal OnSetPropertyInvokeAdviceAttribute OnSetPropertyInvokeAdvice { get; private set; }

        internal void Visit(FinallyAdviceAttribute advice) {
            FinallyAdvice = advice;
            HasFinallyAdvice = true;
        }

        internal void Visit(OnMethodEntryAdviceAttribute advice) {
            OnMethodEntryAdvice = advice;
            HasOnMethodEntryAdvice = true;
        }

        internal void Visit(OnMethodInvokeAdviceAttribute advice) {
            OnMethodInvokeAdvice = advice;
            HasOnMethodInvokeAdvice = true;
        }

        internal void Visit(OnMethodSuccessAdviceAttribute advice) {
            OnMethodSuccessAdvice = advice;
            HasOnMethodSuccessAdvice = true;
        }

        internal void Visit(OnMethodExceptionAdviceAttribute advice) {
            OnMethodExceptionAdvice = advice;
            HasOnMethodExceptionAdvice = true;
        }

		internal void Visit(OnGetPropertyInvokeAdviceAttribute advice) {
			OnGetPropertyInvokeAdvice = advice;
            HasOnGetPropertyInvokeAdvice = true;
        }

        internal void Visit(OnSetPropertyInvokeAdviceAttribute advice) {
            OnSetPropertyInvokeAdvice = advice;
            HasOnSetPropertyInvokeAdvice = true;
        }
    }
}
