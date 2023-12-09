using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SistemaHotel.Services;

public class UtilityFunctions
{
    public static string NormalizeString(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        // Remove excess white space
        input = input.Trim();
        input = Regex.Replace(input, @"\s+", " ");

        // Convert to title case
        TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
        input = textInfo.ToTitleCase(input.ToLower());

        return input;
    }

    public static string RemoveAllWhiteSpace(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return input.Replace(" ", string.Empty);
    }

    public static void ConsolePrintModelErrors(ModelStateDictionary modelState)
    {
        foreach (var modelStateKey in modelState.Keys)
        {
            var modelStateVal = modelState[modelStateKey];
            foreach (var error in modelStateVal.Errors)
            {
                Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
            }
        }
    }
}