using System.Text.Json;  

namespace ISTOREAPP.Helpers  
{
    // Statik bir sınıf tanımlanıyor. Bu sınıf, yardımcı metotlar için kullanılır.
    public static class SessionExtensions
    {
        // Bu metot, bir nesneyi JSON formatında serileştirip, session (oturum) üzerinde saklar.
        public static void SetJson(this ISession session, string key, object value)
        {
            // 'value' parametresini JSON string formatına dönüştürüp, 'key' ile session'da saklar.
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Bu metot, session'dan JSON verisini alır ve belirtilen tipe deserialize eder.
        // 'T' generik türü, hangi tipte veri döndürüleceğini belirtir.
        public static T? GetJson<T>(this ISession session, string key)
        {
            // 'key' ile session'dan veri alınır. Eğer veri bulunmazsa null döner.
            var sessionData = session.GetString(key);

            // Eğer veri null değilse, JSON verisini istenen tipe dönüştürüp döndürür.
            return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
            // Eğer veri yoksa, 'default(T)' değeri döner (örneğin, referans tipi için null).
        }
    }
}
