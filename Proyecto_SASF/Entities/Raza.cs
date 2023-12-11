using AGE.Utils;
using Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SASF.Entities;
[Table("raza")]
public partial class Raza
{
    public Raza(){
        Animales = new HashSet<Animale>();
    }


    [Key]
    [Required(ErrorMessage = "La raza no puede ser nulo.")]
    [NumerosValidation(nameof(Id))]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombres")]
    [Required(ErrorMessage = "Los nombres no pueden ser nulos.")]
    [StringLength(50, ErrorMessage = "Los nombres deben contener máximo 50 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [Required(ErrorMessage = "La descripcion no puede ser nulo.")]
    [StringLength(200, ErrorMessage = "La Descripcion debe contener máximo 200 caracteres.")]
    public string Descripcion { get; set; } = null!;

    [Column("origen_geografico")]
    [StringLength(50, ErrorMessage = "El origen Geografico debe de contener máximo 50 caracteres.")]
    public string? OrigenGeografico { get; set; }

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

    public virtual ICollection<Animale> Animales { get; set; }
}
