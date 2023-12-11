

//Eventos 
$(document).ready(function () {
    $("#btnRegistrar").click(function () {

        $("#title").text("Registrar Raza");

        var razaFormData = $("#razaForm").serialize();

        $.ajax({
            type: "POST",
            url: "/Raza/SaveRaza",
            data: razaFormData,
            success: function (response) {
                console.log(response);
                window.location.href = "/Raza/Index";
            }
        });
    });

    $("#btnModificar").click(function () {
        var razaFormData = $("#razaForm").serialize();

        console.log("Si entra a la funcion");

        $.ajax({
            type: "POST",
            url: "/Raza/EditRaza",
            data: razaFormData,
            success: function () {
                window.location.href = "/Raza/Index";
            }
        });
    });

    $("#btnEliminar").click(function () {
        var id = $("#Id").val();
        $.ajax({
            type: "POST",
            url: "/Raza/EliminarRaza/" + id,
            success: function () {
                window.location.href = "/Raza/Index";
            }
        });
    });
});

//Funciones
function Edit(id) {
    ActivarCampos();
    $.ajax({
        type: "GET",
        url: "/Raza/ObtenerRaza/" + id,
        contentType: "application/json;charset=utf8",
        datatype: "json",
        success: function (response) {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                $("#razaModal").modal("show");
                $("#title").text("Actualizar Raza");
                $("#btnRegistrar").css("display", "none");
                $("#btnEliminar").css("display", "none");
                $("#btnModificar").css("display", "block");
                $("#Id").val(response.id);
                $("#Nombre").val(response.nombre);
                $("#Descripcion").val(response.descripcion);
                $("#OrigenGeografico").val(response.origenGeografico);
                $("#ObservacionEstado").val(response.observacionEstado);
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
        url: "/Raza/ObtenerRaza/" + id,
        contentType: "application/json;charset=utf8",
        datatype: "json",
        success: function (response) {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                $("#razaModal").modal("show");
                $("#title").text("Eliminar Raza");
                $("#btnRegistrar").css("display", "none");
                $("#btnEliminar").css("display", "block");
                $("#btnModificar").css("display", "none");
                $("#Id").val(response.id).prop('readonly', true);
                $("#Nombre").val(response.nombre).prop('readonly', true);
                $("#Descripcion").val(response.descripcion).prop('readonly', true);
                $("#OrigenGeografico").val(response.origenGeografico).prop('readonly', true);
                $("#ObservacionEstado").val(response.observacionEstado).prop('readonly', true);
            }
        },
        error: function () {
            alert("No se logró leer la data.");
        }
    });
}

function LimpiarCampos() {
    console.log("Limpiando campos");
    $("#title").text("Registrar Raza");
    $("#btnRegistrar").css("display", "block");
    $("#btnEliminar").css("display", "none");
    $("#btnModificar").css("display", "none");
    ActivarCampos();
    $("#Id").val('');
    $("#Nombre").val('');
    $("#Descripcion").val('');
    $("#OrigenGeografico").val('');
    $("#ObservacionEstado").val('');
}

function ActivarCampos() {
    $("#Id").prop('readonly', false);
    $("#Nombre").prop('readonly', false);
    $("#Descripcion").prop('readonly', false);
    $("#OrigenGeografico").prop('readonly', false);
    $("#ObservacionEstado").prop('readonly', false);
}

