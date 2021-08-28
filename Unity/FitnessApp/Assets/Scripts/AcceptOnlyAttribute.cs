using System;
using UnityEngine;

namespace CustomAttributes
{
    public class AcceptOnlyAttribute : PropertyAttribute
    {
        private Type _typeToAccept;

        public AcceptOnlyAttribute(Type typeToAccept)
        {
            if(!typeToAccept.IsInterface)
                throw new ArgumentException($"The '{nameof(AcceptOnlyAttribute)}' only accepts types which are an interface!", "typeToAccept");
            _typeToAccept = typeToAccept;
        }

        public Type GetAcceptedType() => _typeToAccept;
    }
}