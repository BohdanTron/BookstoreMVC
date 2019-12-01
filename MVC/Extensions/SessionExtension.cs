using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVC.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None, 
                new JsonSerializerSettings 
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            ));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
