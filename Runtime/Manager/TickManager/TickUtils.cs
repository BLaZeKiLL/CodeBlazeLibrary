namespace CodeBlaze.GameFramework.Manager.TickManager {

    public static class TickUtils {

        public static int SecToTicks(int sec) => (int) (sec * (1f / TickManager.TICK_TIMER));

        public static int TicksToSec(int ticks) => (int) (ticks / (1f / TickManager.TICK_TIMER));

    }

}