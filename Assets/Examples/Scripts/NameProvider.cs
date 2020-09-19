using System;

using UnityEngine;

namespace CodeBlaze.Examples {

    public class NameProvider : MonoBehaviour {

        private string demoName = "";
        
        private void Start() {
            demoName = "Demo";
        }

        public string GetDemoName() => demoName;

    }

}