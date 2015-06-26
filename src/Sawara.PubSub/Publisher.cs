namespace Sawara.PubSub
{
    using log4net;
    using System;
    using System.Collections.Generic;

    public sealed class Publisher<T>
    {
        public Publisher()
        {
        }

        public Publisher<T> Subscribe(Subscriber<T> sub)
        {
            this.subscribers.Add(sub);
            return this;
        }

        public Publisher<T> Unsubscribe(Subscriber<T> sub)
        {
            this.subscribers.Remove(sub);
            return this;
        }

        public Publisher<T> Publish(T data)
        {
            foreach (var subscriber in this.subscribers)
            {
                try
                {
                    subscriber(data);
                }
                catch (Exception e)
                {
                    logger.Error("Got an exception from subscriber.", e);
                }
            }
            return this;
        }

        private static ILog logger = LogManager.GetLogger("Sawara.Publisher");

        private IList<Subscriber<T>> subscribers = new List<Subscriber<T>>();
    }
}
