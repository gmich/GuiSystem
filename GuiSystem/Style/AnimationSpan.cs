using System;

namespace GuiSystem.Style
{
    public class AnimationSpan
    {
        private readonly TimeSpan timeSpan;
        private double timePassed;
        public event EventHandler OnAnimationEnd;

        public AnimationSpan(TimeSpan timeSpan)
        {
            this.timeSpan = timeSpan;
        }

        public void Update(double timeDelta)
        {
            timePassed += timeDelta;

            if (timePassed >= timeSpan.TotalSeconds)
            {
                OnAnimationEnd?.Invoke(this, EventArgs.Empty);
            }
        }

        public static AnimationSpan Forever => new AnimationSpan(TimeSpan.MaxValue);

        public static AnimationSpan Seconds(double seconds) => new AnimationSpan(TimeSpan.FromSeconds(seconds));

        public static AnimationSpan Zero => new AnimationSpan(TimeSpan.Zero);

    }
}
