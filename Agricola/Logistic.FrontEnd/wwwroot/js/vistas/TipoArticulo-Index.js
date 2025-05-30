const MODELO_BASE = {
    idTipoArticulo: 0,
    descripcion: "",
    esActivo: 1
};
 
let tablaData;

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/TipoArticulo/ListaAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idTipoArticulo", "visible": false, "searchable": false },
            { "data": "descripcion" }, 
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
                filename: 'Reporte Tipo Documento Identidad',
                exportOptions: {
                    columns: [1, 2]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});

function mostarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idTipoArticulo)
    $("#txtDescripcion").val(modelo.descripcion) 
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
    modelo["idTipoArticulo"] = parseInt($("#txtId").val());
    modelo["descripcion"] = $("#txtDescripcion").val();   
    modelo["esActivo"] = $("#cboEstado").val();

    let formData = new FormData();
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idTipoArticulo == 0) {
        fetch("/TipoArticulo/Crear", {
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
                    swal("Listo", "Tipo de Artículo fue creado", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
    else {
        fetch("/TipoArticulo/Editar", {
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
                    swal("Listo", "Tipo de Artículo fue modificado", "success");
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
            text: `Eliminar Tipo de Artículo "${data.descripcion}"`,
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

                fetch(`/TipoArticulo/Eliminar?idTipoArticulo=${data.idTipoArticulo}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo", "Tipo Documento de Identidad fue eliminado", "success");
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error");
                        }
                    })
            }
        }
    );

});
