

//Eventos 
$(document).ready(function () {
    $("#btnRegistrar").click(function () {

        $("#title").text("Registrar Animal");

        var animalFormData = $("#animalForm").serialize();

        $.ajax({
            type: "POST",
            url: "/Dashboard/SaveAnimal",
            data: animalFormData,
            success: function (response) {
                console.log(response);
                window.location.href = "/Dashboard/Index";
            }
        });
    });

    $("#btnModificar").click(function () {
        var animalFormData = $("#animalForm").serialize();

        console.log("Si entra a la funcion");

        $.ajax({
            type: "POST",
            url: "/Dashboard/EditarAnimal",
            data: animalFormData,
            success: function () {
                window.location.href = "/Dashboard/Index";
            }
        });
    });

    $("#btnEliminar").click(function () {
        var id = $("#Id").val();
        $.ajax({
            type: "POST",
            url: "/Dashboard/EliminarAnimal/" + id,
            success: function () {
                window.location.href = "/Dashboard/Index";
            }
        });
    });
});

//Funciones
function Edit(id) {
    ActivarCampos();
    $.ajax({
        type: "GET",
        url: "/Dashboard/ObtenerAnimal/" + id,
        contentType: "application/json;charset=utf8",
        datatype: "json",
        success: function (response) {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                $("#animalModal").modal("show");
                $("#title").text("Actualizar Animal");
                $("#btnRegistrar").css("display", "none");
                $("#btnEliminar").css("display", "none");
                $("#btnModificar").css("display", "block");
                $("#Id").val(response.id);
                $("#Nombre").val(response.nombre);
                $("#Especie").val(response.especie);
                $("#Edad").val(response.edad);
                $("#ObservacionEstado").val(response.observacionEstado);
                $("#Id_Raza").val(response.id_Raza);
            }
        },
        error: function () {
            alert("No se logró leer la data.");
        }
    });
}

function Eliminar(id) {
    $.ajax({
        type: "GET",
        url: "/Dashboard/ObtenerAnimal/" + id,
        contentType: "application/json;charset=utf8",
        datatype: "json",
        success: function (response) {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                $("#animalModal").modal("show");
                $("#title").text("Eliminar Animal");
                $("#btnRegistrar").css("display", "none");
                $("#btnEliminar").css("display", "block");
                $("#btnModificar").css("display", "none");
                $("#Id").val(response.id).prop('readonly', true);
                $("#Nombre").val(response.nombre).prop('readonly', true);
                $("#Especie").val(response.especie).prop('readonly', true);
                $("#Edad").val(response.edad).prop('readonly', true);
                $("#ObservacionEstado").val(response.observacionEstado).prop('readonly', true);
                $("#Id_Raza").val(response.id_Raza).prop('disabled', true);
            }
        },
        error: function () {
            alert("No se logró leer la data.");
        }
    });
}

function LimpiarCampos() {
    console.log("Limpiando campos");
    $("#title").text("Registrar Animal");
    $("#btnRegistrar").css("display", "block");
    $("#btnEliminar").css("display", "none");
    $("#btnModificar").css("display", "none");
    ActivarCampos();
    $("#Id").val('');
    $("#Nombre").val('');
    $("#Especie").val('');
    $("#Edad").val('');
    $("#ObservacionEstado").val('');
    $("#Id_Raza").val(1);
}

function ActivarCampos() {
    $("#Id").prop('readonly', false);
    $("#Nombre").prop('readonly', false);
    $("#Especie").prop('readonly', false);
    $("#Edad").prop('readonly', false);
    $("#ObservacionEstado").prop('readonly', false);
    $("#Id_Raza").prop('disabled', false);
}

