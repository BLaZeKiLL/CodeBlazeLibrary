using System;

using UnityEngine;

namespace CodeBlaze.Library.Android {

    public class ToastManager {
        
        public enum Length {

            SHORT,
            LONG
            
        }

        private static ToastManager instance;

        private AndroidJavaObject activity;
        private AndroidJavaObject context;

        private ToastManager() {
            var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = player.GetStatic<AndroidJavaObject>("currentActivity");
            context = activity.Call<AndroidJavaObject>("getApplicationContext");
        }

        public static void Show(string message, Length length) {
            if (Application.platform != RuntimePlatform.Android) return;
            
            if (instance == null) instance = new ToastManager();
            
            string jlength;
            switch (length) {
                case Length.SHORT:
                    jlength = "LENGTH_SHORT";
                    break;
                case Length.LONG:
                    jlength = "LENGTH_LONG";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
            
            instance.activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                var jmessage = new AndroidJavaObject("java.lang.String", message);
                var jtoast = new AndroidJavaClass("android.widget.Toast");
                var jhandel = jtoast.CallStatic<AndroidJavaObject>("makeText", instance.context, jmessage,
                    jtoast.GetStatic<int>(jlength));
                jhandel.Call("show");
            }));
        }

    }

}