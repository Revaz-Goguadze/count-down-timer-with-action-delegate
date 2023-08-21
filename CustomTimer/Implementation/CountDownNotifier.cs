using System;
using CustomTimer.Factories;
using CustomTimer.Interfaces;

namespace CustomTimer.Implementation
{
    /// <inheritdoc/>
    public class CountDownNotifier : ICountDownNotifier
    {
        /// <inheritdoc cref="CustomTimer.Interfaces.ICountDownNotifier" />
        private Action<string, int> startHandler;
        private Action<string> stopHandler;
        private Action<string, int> tickHandler;

        // ReSharper disable once ParameterHidesMember
        public void Init(Action<string, int> startHandler, Action<string> stopHandler, Action<string, int> tickHandler)
        {
            this.startHandler = startHandler;
            this.stopHandler = stopHandler;
            this.tickHandler = tickHandler;
        }

        public void Run()
        {
            // Initialize a TimerFactory instance
            TimerFactory timerFactory = new TimerFactory();

            // Create a timer using the TimerFactory instance
            Timer timer = timerFactory.CreateTimer("MyTimer", 5);

            // Subscribe to Timer events
            timer.Started += (name, ticks) => this.startHandler?.Invoke(name, ticks);
            timer.Tick += (name, ticks) => this.tickHandler?.Invoke(name, ticks);
            timer.Stopped += name => this.stopHandler?.Invoke(name);

            // Run the Timer
            timer.Run();
        }
    }
}
