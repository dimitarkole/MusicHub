using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Infrastucture.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState) =>
            modelState
                .Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage).ToList();
    }
}
