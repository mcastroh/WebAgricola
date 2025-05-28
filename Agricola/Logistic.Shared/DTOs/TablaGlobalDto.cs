namespace Logistic.Shared.DTOs;

public class TablaGlobalDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool EsActivo { get; set; }
    public string AuditUsuario { get; set; } = string.Empty;
}