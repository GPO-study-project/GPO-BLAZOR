using DBAccess.DBAgents.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBAgent.Models;

/// <summary>
/// Договор
/// </summary>
[Table("Договор")]
public partial class Contract
{
    public int Id { get; set; }

    /// <summary>
    /// Номер договора о практике
    /// </summary>
    [Column("НомерДоговораОПрактике")]
    public string Number { get; set; } = null!;

    /// <summary>
    /// Дата практики
    /// </summary>
    [Column("Дата практики")]
    public int DataPractic { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    [Column("Организация")]
    public int? Organisation { get; set; }

    /// <summary>
    /// Помещение
    /// </summary>
    [Column("Помещение")]
    public string? Room { get; set; }


    /// <summary>
    /// Статус
    /// </summary>
    [Column("Статус")]
    public int Status { get; set; }

    /// <summary>
    /// Материально техническое обеспече
    /// </summary>
    [Column("МатериальноТехническоеОбеспече")]
    public string[]? Equipment { get; set; }


    public virtual ICollection<AskForm> AskForms { get; set; } = new List<AskForm>();

    public virtual Organization OrganizationNavigation { get; set; } = null!;
    public virtual PracticTime PracticTimenNavigation { get; set; } = null!;
    public virtual Status StatusNavigation { get; set; } = null!;

}
