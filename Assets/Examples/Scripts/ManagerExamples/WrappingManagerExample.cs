using CodeBlaze.GameFramework.Manager;

using UnityEngine;

namespace CodeBlaze.Examples.Manager {

    public class WrappingManagerExample : WrappingManager<WrappingManagerExample, NameProvider> {

        public string GetDemoName() => Behaviour.GetDemoName();

    }

}