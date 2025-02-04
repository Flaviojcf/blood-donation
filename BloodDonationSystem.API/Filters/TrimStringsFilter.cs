﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonationSystem.API.Filters
{
    public class TrimStringsActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg != null)
                {
                    TrimAllStrings(arg);
                }
            }
        }

        private static void TrimAllStrings(object obj)
        {
            var properties = obj.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var prop in properties)
            {
                var value = (string)prop.GetValue(obj);
                if (!string.IsNullOrEmpty(value))
                {
                    prop.SetValue(obj, value.Trim());
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
