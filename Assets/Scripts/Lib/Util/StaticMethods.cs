using UnityEngine;

namespace Lib.Util {
    public static class StaticMethods {
        public static void logWithMethodName (string msg = "", [System.Runtime.CompilerServices.CallerMemberName] string memberName = "") {
            Debug.Log (Time.time + "\n(" + memberName + ")" + msg);
        }
    }
}
