const MODELO_BASE = {
    id: 0,
    codigo: "",
    name: "",
    esActivo: 1
};

let tituloEntity = 'Tipo de Artículo';
let tablaData;
let filaSeleccionada;

$(cargaInicial);

$("#btn-nuevo").on("click", function () {
    LimpiarControles();
    mostarModal();
});

$('#tbdata tbody').on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }
    let data = tablaData.row(filaSeleccionada).data();
    LimpiarControles();
    mostarModal(data);
});

$('#tbdata tbody').on("click", ".btn-eliminar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }
    let data = tablaData.row(filaSeleccionada).data();
    swal(
        {
            title: "¿Está seguro?",
            text: `Eliminar ${tituloEntity} ${data.name}`,
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
                fetch(`/TipoVia/Eliminar?id=${data.id}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(filaSeleccionada).remove().draw();
                            swal("Listo", `${tituloEntity} fue eliminado`, "success");
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error");
                        }
                    })
            }
        }
    );
});

$("#btn-guardar").on("click", function () {
    let flgValidar = ValidarEntidad();
    if (flgValidar == "ERROR") { return; }

    let modelo = structuredClone(MODELO_BASE);

    modelo["id"] = parseInt($("#txtPrimaryKey").val());
    modelo["codigo"] = $("#txtCodigo").val();
    modelo["name"] = $("#txtName").val();
    modelo["esActivo"] = $("#cboEstado").val() == 1 ? true : false;

    let formData = new FormData();
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.id == 0) {
        fetch("/TipoVia/Crear", {
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
                    swal("Listo", `${tituloEntity} fue creado`, "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
    else {
        fetch("/TipoVia/Editar", {
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
                    swal("Listo", `${tituloEntity} fue modificado`, "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
});

function ValidarEntidad() {
    LimpiarControles();
    var _name = document.getElementById('txtName').value;
    var _codigo = document.getElementById('txtCodigo').value;
    let flgError = "OK";
    if (_name == "") {
        document.getElementById("NameMensaje").innerHTML = 'Debe ingresar Descripción';
        $(`input[id="txtName"]`).focus()
        flgError = "ERROR";
    }
    if (_codigo == "") {
        document.getElementById("codigoMensaje").innerHTML = 'Debe ingresar Código';
        $(`input[id="txtCodigo"]`).focus()
        flgError = "ERROR";
    }
    return flgError;
}

function LimpiarControles() {
    document.getElementById("codigoMensaje").innerHTML = '';
    document.getElementById("NameMensaje").innerHTML = '';
}

function mostarModal(modelo = MODELO_BASE) {
    $("#txtPrimaryKey").val(modelo.id);
    $("#txtCodigo").val(modelo.codigo);
    $("#txtName").val(modelo.name);
    $("#cboEstado").val(modelo.esActivo == false ? 0 : 1);
    $("#modalData").modal("show");
}

function cargaInicial() {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/TipoArticulo/ListaAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "visible": false, "searchable": false },
            { "data": "codigo" },
            { "data": "name" },
            {
                "data": "esActivo", render: function (data) {
                    if (data == true)
                        return `<span class="badge badge-info">Activo</span>`
                    else
                        return `<span class="badge badge-danger">No Activo</span>`
                }
            },
            {
                "defaultContent":
                    '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"> Editar </i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm mr-2"><i class="fas fa-trash-alt"></i> Eliminar </button>',
                "orderable": false,
                "searchable": false,
                "width": "300px"
            }
        ],
        order: [[1, "asc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: `${tituloEntity}`,
                exportOptions: {
                    columns: [1, 2, 3]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
} 