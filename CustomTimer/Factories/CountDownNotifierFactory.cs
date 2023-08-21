using System;
using CustomTimer.Implementation;
using CustomTimer.Interfaces;

namespace CustomTimer.Factories
{
    /// <summary>
    /// Implements the factory method pattern <see><cref>https://en.wikipedia.org/wiki/Factory_method_pattern</cref></see>
    /// for creating an object of the class that implements the <see cref="ICountDownNotifier"/> interface.
    /// </summary>
#pragma warning disable CA1052
    public class CountDownNotifierFactory
#pragma warning restore CA1052
    {
        /// <summary>
        /// Create an object of the class that implements the <see cref="ICountDownNotifier"/> interface.
        /// </summary>
        /// <param name="timer">A reference to a class CustomTimer.</param>
        /// <returns>A reference to an object of the class that implements the <see cref="ICountDownNotifier"/> interface.</returns>
        /// <exception cref="ArgumentNullException">When timer is null.</exception>
        public static ICountDownNotifier CreateNotifierForTimer(Timer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            CountDownNotifier notifier = new CountDownNotifier();
            notifier.Init(
                (name, ticks) => Console.WriteLine($"Timer '{name}' started with {ticks} ticks."),
                name => Console.WriteLine($"Timer '{name}' stopped."),
                (name, ticks) => Console.WriteLine($"Timer '{name}' tick. Ticks left: {ticks}"));

            return notifier;
        }
    }
}
