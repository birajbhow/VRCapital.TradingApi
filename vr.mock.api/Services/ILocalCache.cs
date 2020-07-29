
using System.Collections.Generic;

namespace vr.mock.api.Services
{
    /// <summary>
    /// Local cache interface
    /// </summary>
    public interface ILocalCache
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The object</returns>
        T Get<T>(string key);

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Put<T>(string key, T value);

        /// <summary>
        /// Removes the specified key
        /// </summary>
        /// <param name="key"></param>
        bool Remove(string key);

        /// <summary>
        /// Get all values from store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAll<T>();
    }
}
