namespace Logistic.BackEnd.API.Models;

public class TablaGlobalSelectDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool EsActivo { get; set; }
    public DateTime AuditInsertFecha { get; set; }
    public string AuditInsertUsuario { get; set; } = string.Empty;
    public DateTime? AuditUpdateFecha { get; set; }
    public string AuditUpdateUsuario { get; set; } = string.Empty;
}