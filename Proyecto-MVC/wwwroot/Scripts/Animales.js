let currentPage = 0;
const pageSize = 10;
let totalElements = 0;

//Obteniendo el total de registros
function getTotalElements() {
    return fetch('/Dashboard/ObtenerTotalAnimales', {
        method: 'GET'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Respuesta no exitosa al obtener el total de elementos');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            if (data) {
                totalElements = data;
            } else {
                console.log('La respuesta no contiene datos válidos para el total de elementos');
            }
        })
        .catch(error => console.error('Error al obtner el total de elementos'))
}


//Obteniendo de 10 en 10 los registros
function fetchData(page) {
    return fetch(`/Dashboard/ObtenerAnimales?page=${page}&pageSize=${pageSize}`, {
        method: 'GET'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Respuesta no exitosa');
            }
            return response.json();
        })
        .catch(error => {
            console.error('Error al obtener la lista de animales:', error);
            throw error;
        });
}

//Actualizando la tabla
function updatePageData(page) {
    fetchData(page)
        .then(data => {
            if (data && Array.isArray(data)) {
                console.log(data);
                const tb = document.querySelector('#tbAnimal tbody');
                tb.innerHTML = '';

                for (let i = 0; i < data.length; i++) {
                    const row = document.createElement('tr');

                    row.innerHTML = `
                    <td>${data[i].id}</td>
                    <td>${data[i].nombre}</td>
                    <td>${data[i].edad}</td>
                    <td>${data[i].especie}</td>
                    <td>${data[i].estado}</td>
                    <td>${data[i].raza.nombre}</td>
                    <td>
                        <button type="button" class="btn btn-primary btnEditar" data-id="${data[i].id}">Editar</button>
                        <button type="button" class="btn btn-danger btnEliminar" data-id="${data[i].id}">Eliminar</button>
                    </td>
                `;
                    tb.appendChild(row);
                }

                // Asignar eventos a los botones después de agregar las filas
                const btnEditores = document.querySelectorAll('.btnEditar');
                const btnEliminadores = document.querySelectorAll('.btnEliminar');

                btnEditores.forEach(btn => {
                    btn.addEventListener('click', () => {
                        const animalId = btn.getAttribute('data-id');
                        console.log(animalId);
                        Edit(animalId);
                    });
                });

                btnEliminadores.forEach(btn => {
                    btn.addEventListener('click', () => {
                        const animalId = btn.getAttribute('data-id');
                        console.log(animalId);
                        Eliminar(animalId);
                    });
                });

            } else {
                console.error('La respuesta no contiene datos válidos.');
            }
            currentPage = page;
            updatePaginationButtons();

        })
        .catch(error => console.error('Error al cargar la lista de animales:', error));
}

//Actualiza los botones de navegacion de la tabla
function updatePaginationButtons() {
    const btnNext = document.getElementById('btnNext');
    const btnPrev = document.getElementById('btnPrev');

    btnNext.disabled = currentPage * pageSize + pageSize >= totalElements;
    btnPrev.disabled = currentPage === 0;
}

//Ayuda a cargar la tabla despues de insertar y actualizar datos
function cargarListaAnimales() {
    console.log("Tabla actualizada");
    getTotalElements();
    updatePageData(currentPage);
}


function Edit(id) {
    ActivarCampos();
    fetch("/Dashboard/ObtenerAnimal/" + id, {
        method: "GET",
        headers: {
            "Content-Type": "application/json;charset=utf8"
        }
    })
        .then(response => response.json())
        .then(response => {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                var modal = document.getElementById("animalModal");
                modal.classList.add('show');
                modal.style.display = 'block';
                document.body.classList.add('modal-open');
                document.getElementById("title").textContent = "Editar Animal";
                document.getElementById("btnRegistrar").style.display = "none";
                document.getElementById("btnEliminarRegistro").style.display = "none";
                document.getElementById("btnModificar").style.display = "block";
                document.getElementById("Id").value = response.id;
                document.getElementById("Nombre").value = response.nombre;
                document.getElementById("Especie").value = response.especie;
                document.getElementById("Edad").value = response.edad;
                document.getElementById("ObservacionEstado").value = response.observacionEstado;
                document.getElementById("Id_Raza").value = response.id_Raza;
            }
        })
        .catch(() => {
            alert("No se logró leer la data.");
        });
}

function Eliminar(id) {
    fetch("/Dashboard/ObtenerAnimal/" + id, {
        method: "GET",
        headers: {
            "Content-Type": "application/json;charset=utf8"
        }
    })
        .then(response => response.json())
        .then(response => {
            if (response == null || response == undefined) {
                alert("No se logró leer la data.");
            } else if (response.length == 0) {
                alert("No se encontró el id" + id);
            } else {
                console.log(response);
                var modal = document.getElementById("animalModal");
                modal.classList.add('show');
                modal.style.display = 'block';
                document.body.classList.add('modal-open');
                document.getElementById("title").textContent = "Eliminar Animal";
                document.getElementById("btnRegistrar").style.display = "none";
                document.getElementById("btnEliminarRegistro").style.display = "block";
                document.getElementById("btnModificar").style.display = "none";
                document.getElementById("Id").value = response.id;
                document.getElementById("Nombre").value = response.nombre;
                document.getElementById("Especie").value = response.especie;
                document.getElementById("Edad").value = response.edad;
                document.getElementById("ObservacionEstado").value = response.observacionEstado;
                document.getElementById("Id_Raza").value = response.id_Raza;

                document.getElementById("Id").readOnly = true;
                document.getElementById("Nombre").readOnly = true;
                document.getElementById("Especie").readOnly = true;
                document.getElementById("Edad").readOnly = true;
                document.getElementById("ObservacionEstado").readOnly = true;
                document.getElementById("Id_Raza").disabled = true;
            }
        })
        .catch(() => {
            alert("No se logró leer la data.");
        });
}

function LimpiarCampos() {

    var modal = document.getElementById("animalModal");
    modal.classList.add('show');
    modal.style.display = 'block';
    document.body.classList.add('modal-open');

    console.log("Limpiando campos");
    document.getElementById("title").textContent = "Registrar Animal";
    document.getElementById("btnRegistrar").style.display = "block";
    document.getElementById("btnEliminarRegistro").style.display = "none";
    document.getElementById("btnModificar").style.display = "none";

    ActivarCampos();


    document.getElementById("Id").value = "";
    document.getElementById("Nombre").value = "";
    document.getElementById("Especie").value = "";
    document.getElementById("Edad").value = "";
    document.getElementById("ObservacionEstado").value = "";
    document.getElementById("Id_Raza").value = 1;
}

function ActivarCampos() {
    document.getElementById("Id").readOnly = false;
    document.getElementById("Nombre").readOnly = false;
    document.getElementById("Especie").readOnly = false;
    document.getElementById("Edad").readOnly = false;
    document.getElementById("ObservacionEstado").readOnly = false;
    document.getElementById("Id_Raza").disabled = false;
}


//Cerrar Modal
function Cerrar() {

    var modal = document.getElementById("animalModal");
    modal.classList.remove('show');
    modal.style.display = 'none';
    document.body.classList.remove('modal-open');
}

function validarFormulario() {
    var nombre = document.getElementById("Nombre").value;
    var especie = document.getElementById("Especie").value;
    var edad = document.getElementById("Edad").value;
    var observacionEstado = document.getElementById("ObservacionEstado").value;
    var idRaza = document.getElementById("Id_Raza").value;

    if (nombre.trim() === ""|| !isNaN(nombre) ) {
        alert("Por favor, ingrese un nombre correcto para el animal.");
        return false;
    }

    if (especie.trim() === "" || !isNaN(especie)) {
        alert("Por favor, ingrese la especie correcta del animal.");
        return false;
    }

    if (isNaN(edad) || edad <= 0) {
        alert("Por favor, ingrese una edad válida para el animal.");
        return false;
    }

    if (isNaN(idRaza) || idRaza <= 0) {
        alert("Por favor, ingrese una raza válida para el animal.");
        return false;
    }

    return true;
}



document.addEventListener("DOMContentLoaded", function () {

    cargarListaAnimales();

    getTotalElements().then(() => {
        updatePageData(currentPage);
    })

    // Eventos del anterior y siguiente 
    const btnNext = document.getElementById('btnNext');
    const btnPrev = document.getElementById('btnPrev');

    btnNext.addEventListener('click', () => {
        console.log('Before fetchData, currentPage:', currentPage);
        const siguientePagina = currentPage + 1;
        updatePageData(siguientePagina);
        console.log('After fetchData, currentPage:', currentPage);
    });

    btnPrev.addEventListener('click', () => {
        if (currentPage > 0) {
            const anteriorPagina = currentPage - 1;
            updatePageData(anteriorPagina);
        }
    });

    //Demas elementos


    document.getElementById("btnModificar").addEventListener("click", function () {

            var animalForm = document.getElementById("animalForm");
            var animalFormData = new FormData(animalForm);

            console.log("Si entra a la funcion");

            fetch("/Dashboard/EditarAnimal", {
                method: "POST",
                body: animalFormData
            })
                .then(() => {
                    cargarListaAnimales();
                    Cerrar();
                })
                .catch(error => console.error("Error:", error));
        


    });

    document.getElementById("btnRegistrar").addEventListener("click", function () {


        if (validarFormulario()) {
            var animalForm = document.getElementById("animalForm");
            var animalFormData = new FormData(animalForm);

            console.log("Si entra a la funcion");

            fetch("/Dashboard/SaveAnimal", {
                method: "POST",
                body: animalFormData
            })
                .then(() => {
                    console.log("Registro de animal completado con éxito");
                    cargarListaAnimales();
                    Cerrar();
                })
                .catch(error => console.error("Error:", error));
        }


    });



    document.getElementById("btnEliminarRegistro").addEventListener("click", function () {
        var id = document.getElementById("Id").value;

        fetch("/Dashboard/EliminarAnimal/" + id, {
            method: "POST"
        })
            .then(() => {
                cargarListaAnimales();
                Cerrar();
            })
            .catch(error => console.error("Error:", error));
    });


});








