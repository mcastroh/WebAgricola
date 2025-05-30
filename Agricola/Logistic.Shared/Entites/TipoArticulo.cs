﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Logistic.Shared.Entites;

public partial class TipoArticulo
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string Codigo { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool EsActivo { get; set; }

    /// <summary>
    /// Aud.Fch.Insert
    /// </summary>
    public DateTime AuditInsertFecha { get; set; }

    /// <summary>
    /// Aud.Fch.InsertUser
    /// </summary>
    public string AuditInsertUsuario { get; set; }

    /// <summary>
    /// Auditoría Update Fecha
    /// </summary>
    public DateTime? AuditUpdateFecha { get; set; }

    /// <summary>
    /// Auditoría Update Fecha
    /// </summary>
    public string AuditUpdateUsuario { get; set; }
}