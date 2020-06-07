using Microsoft.AspNetCore.Http;

namespace DatingApp.API.helpers
{
   public static class Exceptions
   {
      public static void AddAppErr(this HttpResponse res, string message)
      {
         res.Headers.Add("Application-Error", message);
         res.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
         res.Headers.Add("Access-Control-Allow-Origin", "*");
      }
   }
}