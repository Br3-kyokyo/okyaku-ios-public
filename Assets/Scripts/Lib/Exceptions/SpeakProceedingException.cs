using System;
using System.Runtime.Serialization;

class SpeakProceedingException : BaseOkyakuException {
    public SpeakProceedingException () { }

    public SpeakProceedingException (string message) : base (message) { }

    public SpeakProceedingException (string message, Exception inner) : base (message, inner) { }

    protected SpeakProceedingException (SerializationInfo info, StreamingContext context) : base (info, context) { }
}
