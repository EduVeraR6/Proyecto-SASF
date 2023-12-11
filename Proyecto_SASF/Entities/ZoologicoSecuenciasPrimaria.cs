using Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SASF.Entities;

[Table("zoologico_secuencias_primaria")]
public partial class ZoologicoSecuenciasPrimaria
{
    [Key]
    [Column("codigo")]
    [Required(ErrorMessage ="El codigo no puede ser nulo")]
    [NumerosValidation(nameof(Codigo))]
    public int Codigo { get; set; }

    [Column("descripcion")]
    [Required(ErrorMessage = "La descripción no puede ser nula.")]
    [StringLength(200, ErrorMessage = "La descripción debe contener máximo 200 caracteres.")]
    public string Descripcion { get; set; } = null!;

    [Column("valor_inicial")]
    public int ValorInicial { get; set; }

    [Column("incrementa_en")]
    public int IncrementaEn { get; set; }

    [Column("valor_actual")]
    public int ValorActual { get; set; }

    [Required(ErrorMessage = "El estado no puede ser nulo.")]
    [Column("estado")]
    [EstadoValidation]
    public string Estado { get; set; } = null!;

    [Column("fecha_estado", TypeName = "timestamp(0) without time zone")]
    [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
    public DateTime FechaEstado { get; set; }

    [Column("observacion_estado")]
    [StringLength(2000, ErrorMessage = "La observación debe contener máximo 2000 caracteres.")]
    public string? ObservacionEstado { get; set; }

    [Column("usuario_ingreso")]
    public long UsuarioIngreso { get; set; }

    [Column("fecha_ingreso", TypeName = "timestamp(0) without time zone")]
    [Required(ErrorMessage = "La fecha de ingreso no puede ser nula.")]
    public DateTime FechaIngreso { get; set; }

    [Column("ubicacion_ingreso")]
    [Required(ErrorMessage = "La ubicación de ingreso no puede ser nula.")]
    [UbicacionValidation(nameof(UbicacionIngreso))]
    public string UbicacionIngreso { get; set; } = null!;

    [Column("usuario_modificacion")]
    [NumerosValidation(nameof(UsuarioModificacion))]
    public long? UsuarioModificacion { get; set; }

    [Column("fecha_modificacion", TypeName = "timestamp(0) without time zone")]
    public DateTime? FechaModificacion { get; set; }

    [Column("ubicacion_modificacion")]
    [UbicacionValidation(nameof(UbicacionModificacion))]
    public string? UbicacionModificacion { get; set; }
}
