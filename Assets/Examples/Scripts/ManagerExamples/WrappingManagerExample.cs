using CodeBlaze.GameFramework.Manager;

namespace CodeBlaze.Examples.Manager {

    public class WrappingManagerExample : WrappingManager<WrappingManagerExample, NameProvider> {

        public string GetDemoName() => Behaviour.GetDemoName();

    }

}