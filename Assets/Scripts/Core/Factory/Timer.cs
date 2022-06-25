using UnityEngine;

namespace Core.Factory
{
    public class Timer : ITimer
    {
        public float Delay { get; private set; } = 0.0f;
        public void Tick()
        {
            Delay += Time.deltaTime;
        }

        public void ResetDelay()
        {
            Delay = 0.0f;
        }
    }
}