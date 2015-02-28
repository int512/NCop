﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by NCop Copyright © 2015
//    Changes to this file may cause incorrect behavior and will be lost if
//    the code is regenerated.
// </auto-generated
// ------------------------------------------------------------------------------

namespace NCop.Core.Exceptions
{
	using System;
	using NCop.Core.Extensions;
	using System.Runtime.Serialization;
	
	[Serializable]
	public class AttributeNotFoundException : SystemException, ISerializable
	{
        private readonly string message = string.Empty;
		private readonly bool messageInitialized = false;
		
        public AttributeNotFoundException(string message) 
		    : base(message) {
            messageInitialized = true;
        }

        public AttributeNotFoundException(string message, Exception innerException) 
		    : base(message, innerException) {
            messageInitialized = true;
        }
		
		
		public override string Message {
            get {
                if (messageInitialized) {
                    return base.Message;
                }

                return message;
            }
        }
		
		protected AttributeNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) {

            if (info == null) {
                throw new ArgumentNullException("info");
            }

            message = info.GetString("AttributeMessage");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            info.AddValue("AttributeMessage", Message);
        }
	}	
}