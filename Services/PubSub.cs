using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codename_boquete.Services
{
    public delegate void PubSubEventHandler<T>(object sender, PubSubEventArgs<T> args);


    // Argumentos
    public class PubSubEventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public PubSubEventArgs(T item)
        {
            Item = item;
        }
    }
    
    // Clase que maneja los evento
    public static class PubSub<T>
    {
        private static Dictionary<string, PubSubEventHandler<T>> events = new Dictionary<string, PubSubEventHandler<T>>();
        // Test
        private static Dictionary<string, PubSubEventArgs<T>> messages = new Dictionary<string, PubSubEventArgs<T>>();
        //

        public static void AddEvent(string name, PubSubEventHandler<T> handler)
        {
            if (!events.ContainsKey(name))
                events.Add(name, handler);
        }
        public static void RaiseEvent(string name, object sender, PubSubEventArgs<T> args)
        {
            if (events.ContainsKey(name) && events[name] != null)
                events[name](sender, args);
        }
        public static void RegisterEvent(string name, PubSubEventHandler<T> handler)
        {
            if (events.ContainsKey(name))
                events[name] += handler;
        }

        public static void AddMessage(string name, PubSubEventArgs<T> data)
        {
            if (!messages.ContainsKey(name))
            {
                messages.Add(name, data);
            }
        }
    }
}
