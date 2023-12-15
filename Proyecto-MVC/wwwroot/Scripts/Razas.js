let currentPage = 0;//pagina actual
const pageSize = 10;//total de registros por pagina
let totalElements = 0; //total de registros

//Obteniendo el total de registros
function getTotalElements() {
    return fetch('/Raza/ObtenerTotalRazas', {
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
    return fetch(`/Raza/ObtenerRazas?page=${page}&pageSize=${pageSize}`, {
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


//Busqueda Animal

function fetchDataBusqueda(filtro, page) {
    return fetch(`/Raza/RazaFiltro?filtro=${filtro}&page=${page}&pageSize=${pageSize}`, {
        method: 'GET'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Respuesta no exitosa');
            }
            return response.json();
        })
        .catch(error => {
            console.error('Erro al obtener la lista de busqueda');
            throw error;
        })
}

//Actualizando la tabla Filtro
function updatePageDataUpdate(filtro,page) {
    fetchDataBusqueda(filtro,page)
        .then(data => {
            if (data && Array.isArray(data)) {
                console.log(data);
                const tb = document.querySelector('#tbRaza tbody');
                tb.innerHTML = '';

                for (let i = 0; i < data.length; i++) {
                    const row = document.createElement('tr');

                    row.innerHTML = `
                    <td>${data[i].id}</td>
                    <td>${data[i].nombre}</td>
                    <td>
                        <span class="d-inline-block text-truncate" style="max-width: 150px;">
                          ${data[i].descripcion}
                        </span>
                    </td>
                    <td>${data[i].origenGeografico}</td>
                    <td>${data[i].estado}</td>
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
                        const razaId = btn.getAttribute('data-id');
                        console.log(razaId);
                        Edit(razaId);
                    });
                });

                btnEliminadores.forEach(btn => {
                    btn.addEventListener('click', () => {
                        const razaId = btn.getAttribute('data-id');
                        console.log(razaId);
                        Eliminar(razaId);
                    });
                });

            } else {
                console.error('La respuesta no contiene datos válidos.');
            }
            currentPage = page;

        })
        .catch(error => console.error('Error al cargar la lista de razas:', error));
}
//Actualizando la tabla
function updatePageData(page) {
    fetchData(page)
        .then(data => {
            if (data && Array.isArray(data)) {
                console.log(data);
                const tb = document.querySelector('#tbRaza tbody');
                tb.innerHTML = '';

                for (let i = 0; i < data.length; i++) {
                    const row = document.createElement('tr');

                    row.innerHTML = `
                    <td>${data[i].id}</td>
                    <td>${data[i].nombre}</td>
                    <td>
                        <span class="d-inline-block text-truncate" style="max-width: 150px;">
                          ${data[i].descripcion}
                        </span>
                    </td>
                    <td>${data[i].origenGeografico}</td>
                    <td>${data[i].estado}</td>
                    <td>
                        <button type="button" class="btn btn-primary btnEditar" data-id="${data[i].id}">Editar</button>
                        <button type="button" class="btn btn-danger btnEliminar" data-id="${data[i].id}">Eliminar</button>
                    </td>
                `;
                    tb.appendChild(row);
                }

                const btnNext = document.getElementById('btnNext');
                const btnPrev = document.getElementById('btnPrev');


                btnNext.disabled = true;
                btnPrev.disabled = true;


                // Asignar eventos a los botones después de agregar las filas
                const btnEditores = document.querySelectorAll('.btnEditar');
                const btnEliminadores = document.querySelectorAll('.btnEliminar');

                btnEditores.forEach(btn => {
                    btn.addEventListener('click', () => {
                        const razaId = btn.getAttribute('data-id');
                        console.log(razaId);
                        Edit(razaId);
                    });
                });

                btnEliminadores.forEach(btn => {
                    btn.addEventListener('click', () => {
                        const razaId = btn.getAttribute('data-id');
                        console.log(razaId);
                        Eliminar(razaId);
                    });
                });

            } else {
                console.error('La respuesta no contiene datos válidos.');
            }
            currentPage = page;
            updatePaginationButtons();
        })
        .catch(error => console.error('Error al cargar la lista de razas:', error));
}

//Actualiza los botones de navegacion de la tabla
function updatePaginationButtons() {
    const btnNext = document.getElementById('btnNext');
    const btnPrev = document.getElementById('btnPrev');

    btnNext.disabled = currentPage * pageSize + pageSize >= totalElements;
    btnPrev.disabled = currentPage === 0;
}

//Ayuda a cargar la tabla despues de insertar y actualizar datos
function cargarListaRazas() {
    updatePageData(currentPage);
}


function Edit(id) {
    ActivarCampos();
    fetch("/Raza/ObtenerRaza/" + id, {
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
                var modal = document.getElementById("razaModal");
                modal.classList.add('show');
                modal.style.display = 'block';
                document.body.classList.add('modal-open');
                document.getElementById("title").textContent = "Editar Raza";
                document.getElementById("btnRegistrar").style.display = "none";
                document.getElementById("btnEliminarRegistro").style.display = "none";
                document.getElementById("btnModificar").style.display = "block";
                document.getElementById("Id").value = response.id;
                document.getElementById("Nombre").value = response.nombre;
                document.getElementById("Descripcion").value = response.descripcion;
                document.getElementById("OrigenGeografico").value = response.origenGeografico;
                document.getElementById("ObservacionEstado").value = response.observacionEstado;
            }
        })
        .catch(() => {
            alert("No se logró leer la data.");
        });
}

function Eliminar(id) {
    fetch("/Raza/ObtenerRaza/" + id, {
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
                var modal = document.getElementById("razaModal");
                modal.classList.add('show');
                modal.style.display = 'block';
                document.body.classList.add('modal-open');
                document.getElementById("title").textContent = "Eliminar Animal";
                document.getElementById("btnRegistrar").style.display = "none";
                document.getElementById("btnEliminarRegistro").style.display = "block";
                document.getElementById("btnModificar").style.display = "none";
                document.getElementById("Id").value = response.id;
                document.getElementById("Nombre").value = response.nombre;
                document.getElementById("Descripcion").value = response.descripcion;
                document.getElementById("OrigenGeografico").value = response.origenGeografico;
                document.getElementById("ObservacionEstado").value = response.observacionEstado;

                document.getElementById("Id").readOnly = true;
                document.getElementById("Nombre").readOnly = true;
                document.getElementById("Descripcion").readOnly = true;
                document.getElementById("OrigenGeografico").readOnly = true;
                document.getElementById("ObservacionEstado").readOnly = true;
            }
        })
        .catch(() => {
            alert("No se logró leer la data.");
        });
}

function LimpiarCampos() {

    var modal = document.getElementById("razaModal");
    modal.classList.add('show');
    modal.style.display = 'block';
    document.body.classList.add('modal-open');

    console.log("Limpiando campos");
    document.getElementById("title").textContent = "Registrar Raza";
    document.getElementById("btnRegistrar").style.display = "block";
    document.getElementById("btnEliminarRegistro").style.display = "none";
    document.getElementById("btnModificar").style.display = "none";

    ActivarCampos();


    document.getElementById("Id").value = "";
    document.getElementById("Nombre").value = "";
    document.getElementById("Descripcion").value = "";
    document.getElementById("OrigenGeografico").value = "";
    document.getElementById("ObservacionEstado").value = "";
}

function ActivarCampos() {
    document.getElementById("Id").readOnly = false;
    document.getElementById("Nombre").readOnly = false;
    document.getElementById("Descripcion").readOnly = false;
    document.getElementById("OrigenGeografico").readOnly = false;
    document.getElementById("ObservacionEstado").readOnly = false;
}


//Cerrar Modal
function Cerrar() {

    var modal = document.getElementById("razaModal");
    modal.classList.remove('show');
    modal.style.display = 'none';
    document.body.classList.remove('modal-open');
}

function ValidarCampos() {

    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").values;
    var origen = document.getElementById("OrigenGeografico")

    if (nombre == "" || !isNaN(nombre)) {
        alert("Nombre de la raza Invalida");
        return false;
    }

    if (descripcion == "" || !isNaN(descripcion)) {
        alert("Descripcion de la raza Invalida");
        return false;
    }

    if (origen == "" || !isNaN(origen)) {
        alert(" Origen de la raza Invalida");
        return false;
    }


    return true;
}



document.addEventListener("DOMContentLoaded", function () {

    cargarListaRazas();

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

    document.getElementById("Search").addEventListener("click", () => {


        console.log("Entrando a busqueda");
        var inputBusqueda = document.getElementById("input-Busqueda").value;

        if (!inputBusqueda == "") {
            updatePageDataUpdate(inputBusqueda, currentPage);
        } else {
            alert("Input de busqueda vacio")
            cargarListaRazas();
        }

    })


    //Demas elementos
    document.getElementById("btnRegistrar").addEventListener("click", function () {

        if (ValidarCampos()) {
            document.getElementById("title").textContent = "Registrar Raza";

            var razaForm = document.getElementById("razaForm");
            var razaFormData = new FormData(razaForm);

            fetch("/Raza/SaveRaza", {
                method: "POST",
                body: razaFormData
            })
                .then(response => {
                    if (response.ok) {
                        return response.text();
                    } else {
                        throw new Error("Error en la solicitud");
                    }
                })
                .then(() => {
                    cargarListaRazas();
                    Cerrar();
                })
                .catch(error => console.error("Error:", error));
        }
    });

    document.getElementById("btnModificar").addEventListener("click", function () {


            var razaForm = document.getElementById("razaForm");
            var razaFormData = new FormData(razaForm);

            console.log("Si entra a la funcion");

            fetch("/Raza/EditRaza", {
                method: "POST",
                body: razaFormData
            })
                .then(() => {
                    cargarListaRazas();
                    Cerrar();
                })
                .catch(error => console.error("Error:", error));



    });

    document.getElementById("btnEliminarRegistro").addEventListener("click", function () {
        var id = document.getElementById("Id").value;

        fetch("/Raza/EliminarRaza/" + id, {
            method: "POST"
        })
            .then(() => {
                cargarListaRazas();
                Cerrar();
            })
            .catch(error => console.error("Error:", error));
    });


});








