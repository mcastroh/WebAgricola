const MODELO_BASE = {
    idUnidad: 0,
    descripcion: "",
    simbolo: "",
    idSunat: "",
    esActivo: 1
}; 

let tablaData;

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/UnidadMedida/ListaAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idUnidad", "visible": false, "searchable": false },
            { "data": "descripcion" },
            { "data": "simbolo" },
            { "data": "idSunat" },
            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return `<span class="badge badge-info">Activo</span>`
                    else
                        return `<span class="badge badge-danger">No Activo</span>`
                }
            },
            {
                "defaultContent":
                    '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[1, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Unidades de Medida',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});

function mostarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idUnidad)
    $("#txtDescripcion").val(modelo.descripcion)
    $("#txtSimbolo").val(modelo.simbolo)
    $("#txtCodigoSunat").val(modelo.idSunat)
    $("#cboEstado").val(modelo.esActivo)

    $("#modalData").modal("show");
}

$("#btnNuevo").click(function () {
    mostarModal();
});

$("#btnGuardar").click(function () {
    let inputConValor = $("input.input-validar").serializeArray();
    let inputSinValor = inputConValor.filter((item) => item.value.trim() == "");

    if (inputSinValor.length > 0) {
        let mensaje = `Debe ingresar datos: "${inputSinValor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputSinValor[0].name}"]`).focus()
        return;
    }

    let modelo = structuredClone(MODELO_BASE);
    modelo["idUnidad"] = parseInt($("#txtId").val());
    modelo["descripcion"] = $("#txtDescripcion").val();
    modelo["simbolo"] = $("#txtSimbolo").val();
    modelo["idSunat"] = $("#txtCodigoSunat").val();
    modelo["esActivo"] = $("#cboEstado").val();

    let formData = new FormData();
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idUnidad == 0) {
        fetch("/UnidadMedida/Crear", {
            method: "POST",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false);
                    $("#modalData").modal("hide");
                    swal("Listo", "Unidad de Medida fue creado", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
    else {
        fetch("/UnidadMedida/Editar", {
            method: "PUT",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide");
                    swal("Listo", "Unidad de Medida fue modificada", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
});

let filaSeleccionada;

$('#tbdata tbody').on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    let data = tablaData.row(filaSeleccionada).data();
    mostarModal(data);
});

$('#tbdata tbody').on("click", ".btn-eliminar", function () {
    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    let data = tablaData.row(fila).data();

    swal(
        {
            title: "¿Está seguro?",
            text: `Eliminar Unidad de Medida "${data.descripcion}"`,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Si, eliminar",
            cancelButtonText: "No, cancelar",
            closeOnConfirm: false,
            closeOnCancel: true
        },
        function (respuesta) {
            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/UnidadMedida/Eliminar?idUnidad=${data.idUnidad}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo", "Unidad de Medida fue eliminada", "success");
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error");
                        }
                    })
            }
        }
    );

});
