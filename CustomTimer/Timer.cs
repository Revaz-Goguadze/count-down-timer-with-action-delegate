namespace CustomTimer
{
    /// <summary>
    /// A custom class for simulating a countdown clock, which implements the ability to send a messages and additional
    /// information about the Started, Tick and Stopped events to any types that are subscribing the specified events.
    ///
    /// - When creating an object of Timer class, it must be assigned:
    ///     - name (not null or empty string, otherwise ArgumentException will be thrown);
    ///     - the number of ticks (the number must be greater than 0, otherwise an exception will throw an ArgumentException).
    ///
    /// - After the timer has been created, it can fire the Started event, the event should contain information about
    /// the name of the timer and the number of ticks to start.
    ///
    /// - After starting the timer, it fires Tick events, which contain information about the name of the timer and
    /// the number of ticks left for triggering, there should be delays between Tick events, delays are modeled by Thread.Sleep.
    ///
    /// - After all Tick events are triggered, the timer should start the Stopped event, the event should contain information about
    /// the name of the timer.
    /// </summary>
    public class Timer
    {
        private readonly string name;
        private readonly int ticks;

#pragma warning disable CA1003
        public event Action<string, int> Started;
#pragma warning restore CA1003

#pragma warning disable CA1003
        public event Action<string, int> Tick;
#pragma warning restore CA1003

#pragma warning disable CA1003
        public event Action<string> Stopped;
#pragma warning restore CA1003

#pragma warning disable SA1201
        public Timer(string name, int ticks)
#pragma warning restore SA1201
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            if (ticks <= 0)
            {
                throw new ArgumentException("Ticks must be greater than 0.", nameof(ticks));
            }

            this.name = name;
            this.ticks = ticks;
        }

        public static Timer CreateInstance(string name, int ticks)
        {
            return new Timer(name, ticks);
        }

        public void Run()
        {
            this.Started?.Invoke(this.name, this.ticks);

            for (int i = this.ticks; i > 0; i--)
            {
                this.Tick?.Invoke(this.name, i);
                Thread.Sleep(1000); // Simulating a delay of 1 second between ticks
            }

            this.Stopped?.Invoke(this.name);
        }
    }
}
