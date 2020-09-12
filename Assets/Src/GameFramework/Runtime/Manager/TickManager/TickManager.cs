using System;

using UnityEngine;

namespace CodeBlaze.GameFramework.Manager.TickManager {

    public class TickManager : Manager<TickManager> {

        public class TickEventArgs : EventArgs {

            public int Tick { get; set; }

        }

        public event EventHandler<TickEventArgs> OnTick;

        public event EventHandler<TickEventArgs> OnMicroTick;

        public const float TICK_TIMER = .2f;
        
        public int Tick { get; private set; }

        private float _time;

        protected override void OnUpdate() {
            _time += Time.deltaTime;

            if (!(_time >= TICK_TIMER)) return;

            _time -= TICK_TIMER;
            Tick++;
            
            OnMicroTick?.Invoke(this, new TickEventArgs { Tick = Tick});
            
            if (Tick % 5 == 0) OnTick?.Invoke(this, new TickEventArgs { Tick = Tick});
        }

    }

}