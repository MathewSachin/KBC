using System;
using Android.OS;

namespace KBC
{
    class CountDown : CountDownTimer
    {
        public event Action Tick;
        public event Action Finish;

        public CountDown(long totaltime, long interval)
            : base(totaltime, interval) { }

        public override void OnTick(long millisUntilFinished) => Tick?.Invoke();

        public override void OnFinish() => Finish?.Invoke();
    }
}