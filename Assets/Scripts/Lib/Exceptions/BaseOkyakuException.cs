[System.Serializable]
public class BaseOkyakuException : System.Exception {
    public BaseOkyakuException () { }
    public BaseOkyakuException (string message) : base (message) { }
    public BaseOkyakuException (string message, System.Exception inner) : base (message, inner) { }
    protected BaseOkyakuException (
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base (info, context) { }
}
