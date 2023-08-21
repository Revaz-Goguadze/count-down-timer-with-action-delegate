using System;
using CustomTimer.Factories;
using NUnit.Framework;

namespace CustomTimer.Tests
{
    public class CountDownNotifierTests
    {
        private CountDownNotifierFactory countDownNotifierFactory;
        private TimerFactory timerFactory;

        [SetUp]
        public void Setup()
        {
            this.countDownNotifierFactory = new CountDownNotifierFactory();
            this.timerFactory = new TimerFactory();
        }

        [TestCase("pie", 10)]
        [TestCase("cookies", 5)]
        [TestCase("pizza", 1)]
        public void Run_ValidTimer_AllEventsWorkAsExpected(string name, int totalTicks)
        {
            var timer = this.timerFactory.CreateTimer(name, totalTicks);
            var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

            void TimerStarted(string timerName, int ticks)
            {
                Assert.AreEqual(name, timerName);
                Assert.AreEqual(totalTicks, ticks);
                Console.WriteLine($"Start timer '{timerName}', total {ticks} ticks");
            }

            void TimerStopped(string timerName)
            {
                Assert.AreEqual(name, timerName);
                Console.WriteLine($"Stop timer '{timerName}'");
            }

            var remainsTicks = totalTicks;

            void TimerTick(string timerName, int ticks)
            {
                remainsTicks -= 1;
                Assert.AreEqual(name, timerName);
                Assert.AreEqual(remainsTicks, ticks);
                Console.WriteLine($"Timer '{timerName}', remains {ticks} ticks");
            }

            notifier.Init(TimerStarted, TimerStopped, TimerTick);
            notifier.Run();

            Assert.AreEqual(0, remainsTicks - 1);
        }

        [TestCase("pie", 10)]
        [TestCase("cookies", 5)]
        [TestCase("pizza", 1)]
        public void Run_NullDelegates_TimerIsWorking(string name, int totalTicks)
        {
            var timer = this.timerFactory.CreateTimer(name, totalTicks);
            var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

            Assert.DoesNotThrow(() =>
            {
                notifier.Init(null, null, null);
                notifier.Run();
            });
        }

        [Test]
        public void Ctor_TimerIsNull_ThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => this.countDownNotifierFactory.CreateNotifierForTimer(null));
    }
}
