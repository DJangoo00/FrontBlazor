/* servicio de validacion de datos en los formularios */
using System.Text.RegularExpressions;

namespace FrontBlazor.Validations;

public class FormValidator
{
    // Verifica si la cadena solo contiene texto (sin números)
    public bool ContainsOnlyText(string input)
    {
        // Utiliza una expresión regular para permitir solo letras y espacios
        // Puedes ajustar esto según tus requisitos específicos
        var regex = new Regex("^[a-zA-Z ]+$");
        return regex.IsMatch(input);
    }

    // Verifica si la cadena contiene solo texto y números
    public bool ContainsTextAndNumbers(string input)
    {
        // Utiliza una expresión regular para permitir letras, números y espacios
        // Puedes ajustar esto según tus requisitos específicos
        var regex = new Regex("^[a-zA-Z0-9 ]+$");
        return regex.IsMatch(input);
    }
}