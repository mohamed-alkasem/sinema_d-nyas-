using Newtonsoft.Json;

namespace Sinema_Dunyasi.Helpers
{
    public static class SessionExtensions
    {
        // Session'a bir nesne kaydetmek için
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
