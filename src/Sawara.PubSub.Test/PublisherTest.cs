namespace Sawara.PubSub.Test
{
    using Sawara.PubSub;
    using Xunit;

    public class PublisherTest
    {
        [Fact]
        public void Publisher_publishes_to_any_subscribers()
        {
            var publisher = new Publisher<string>();

            publisher.Publish("test");

            int ncalls = 0;
            Subscriber<string> subscriber = (string val) =>
            {
                Assert.Equal("publish", val);
                ++ncalls;
            };
            publisher.Subscribe(subscriber);

            Assert.Equal(0, ncalls);
            publisher.Publish("publish");
            Assert.Equal(1, ncalls);

            publisher.Unsubscribe(subscriber);

            Assert.Equal(1, ncalls);
            publisher.Publish("no one subscribes");
            Assert.Equal(1, ncalls);
        }
    }
}
