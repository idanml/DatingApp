using System;
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

      public static int CalculateAge(this DateTime DateOfBirth)
      {
         var age = DateTime.Today.Year - DateOfBirth.Year;
         if (DateOfBirth.AddYears(age) > DateTime.Today)
            age--;

         return age;
      }
   }
}