using System.Collections.Generic;
using System.Linq;

namespace vr.mock.api.Services
{
    /// <summary>
    /// Thread Safe Local Cache
    /// </summary>
    public class ThreadSafeLocalCache : ILocalCache
    {
        public T Get<T>(string key)
        {
            return (T) ThreadSafeStore.Instance.Get(key);
        }

        public void Put<T>(string key, T value)
        {
            ThreadSafeStore.Instance.Put(key, value);
        }

        public bool Remove(string key)
        {
            return ThreadSafeStore.Instance.Remove(key);
        }

        public List<T> GetAll<T>()
        {
            return ThreadSafeStore.Instance.GetAll()?.Cast<T>().ToList() ?? new List<T>();
        }
    }

    /// <summary>
    /// Thread Safe Store (singleton)
    /// </summary>
    internal class ThreadSafeStore
    {
        #region Singleton

        /// <summary>
        /// Locking object for thread safety
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Instance of the <see cref="ThreadSafeStore"/>
        /// </summary>
        private static volatile ThreadSafeStore instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ThreadSafeStore"/> class from being created.
        /// </summary>
        private ThreadSafeStore()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ThreadSafeStore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ThreadSafeStore();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        private readonly object storeLocker = new object();

        private readonly Dictionary<string, object> store = new Dictionary<string, object>();

        public object Get(string key)
        {
            lock (this.storeLocker)
            {
                return this.store.ContainsKey(key) ? this.store[key] : null;
            }
        }

        public void Put(string key, object value)
        {
            lock (this.storeLocker)
            {
                if (this.store.ContainsKey(key))
                {
                    this.store[key] = value;
                }
                else
                {
                    this.store.Add(key, value);
                }
            }
        }

        public List<object> GetAll()
        {
            lock (this.storeLocker)
            {
                return this.store.Count > 0 ? this.store.Values.ToList() : null;
            }
        }

        public bool Remove(string key)
        {
            lock (this.storeLocker)
            {
                return this.store.ContainsKey(key) && this.store.Remove(key);
            }
        }
    }
}