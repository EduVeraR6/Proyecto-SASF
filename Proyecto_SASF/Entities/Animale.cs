using AGE.Utils;
using Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto_SASF.Entities;

[Table("animales")]
public partial class Animale
{
    [Key]
    [Required(ErrorMessage = "El animal no puede ser nulo.")]
    [NumerosValidation(nameof(Id))]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombres")]
    [Required(ErrorMessage = "Los nombres no pueden ser nulos.")]
    [StringLength(50, ErrorMessage = "Los nombres deben contener máximo 50 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Column("edad")]
    [NumerosValidation(nameof(Edad))]
    public int? Edad { get; set; }

    [Column("especie")]
    [Required(ErrorMessage = "El nombre de la especie no puede ser nulo.")]
    [StringLength(50, ErrorMessage = "La especie debe contener máximo 50 caracteres.")]
    public string Especie { get; set; } = null!;

    [Required(ErrorMessage = "La raza del animal no puede ser nulo.")]
    [NumerosValidation(nameof(RazaId))]
    [Column("raza_id")]
    public int RazaId { get; set; }

    [Required(ErrorMessage = "El estado no puede ser nulo.")]
    [Column("estado")]
    [EstadoValidation]
    public string Estado { get; set; } = null!;

    [Column("fecha_estado", TypeName = "timestamp(0) without time zone")]
    [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
    public DateTime FechaEstado { get; set; }

    [Column("observacion_estado")]
    [StringLength(2000, ErrorMessage = "La observación debe contener máximo 200 caracteres.")]
    public string? ObservacionEstado { get; set; }

    [Column("usuario_ingreso")]
    [Required(ErrorMessage = "El campo usuario ingreso no puede ser nulo.")]
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

    [JsonIgnore]
    public virtual Raza Raza { get; set; } = null!;
}
