using System;
using System.Runtime.Serialization;

namespace PolpgUI.Exceptions
{
    [Serializable]
    public class TemplateNotFoundException : SystemException
    {
        public TemplateNotFoundException()
        {

        }

        public TemplateNotFoundException(string message) : base(message)
        {
        }

        public TemplateNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }
        
        protected TemplateNotFoundException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}