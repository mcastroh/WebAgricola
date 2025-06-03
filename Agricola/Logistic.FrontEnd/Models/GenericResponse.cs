namespace Logistic.FrontEnd.Models;

public class GenericResponse<T> where T : class
{
    public bool Estado { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public T? Objeto { get; set; }
    public List<T>? Objetos { get; set; }
}