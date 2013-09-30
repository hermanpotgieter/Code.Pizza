using System;

namespace Code.Pizza.Common.Utilities
{
    public class Enforce
    {
        public static void Implements<TInterface>(object instance, string message)
        {
            Implements<TInterface>(instance.GetType(), message);
        }

        public static void Implements<TInterface>(Type type, string message)
        {
            if(!typeof(TInterface).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void InheritsFrom<TBase>(object instance, string message)
        {
            InheritsFrom<TBase>(instance.GetType(), message);
        }

        public static void InheritsFrom<TBase>(Type type, string message)
        {
            if(type.BaseType != typeof(TBase))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void IsEqual<TException>(object thisObject, object otherObject, string message) where TException : Exception
        {
            if(thisObject != otherObject)
            {
                throw (TException) Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static void TypeOf<TType>(object instance, string message)
        {
            if(!(instance is TType))
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}