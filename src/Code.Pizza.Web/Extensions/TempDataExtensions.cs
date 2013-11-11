using System.Web.Mvc;

namespace Code.Pizza.Web.Extensions
{
    public static class TempDataExtensions
    {
        private const string sessionExpiredKey = "sessionExpiredKey";

        public static void SessionIsExpiredOrInvalid(this TempDataDictionary tempData, bool expired)
        {
            tempData[sessionExpiredKey] = expired;
        }

        public static bool SessionIsExpiredOrInvalid(this TempDataDictionary tempData)
        {
            bool expired = (bool)(tempData[sessionExpiredKey] ?? false);

            return expired;
        }
    }
}
