const MODELO_BASE = {
    id: 0,
    codigo: "",
    name: "",
    esActivo: 1
};

const url = '/api/tipoalmacen';
let tituloEntity = 'Tipo de Almacén';
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
                fetch(`/Generic/Eliminar?url=${url}/${data.id}`, {
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

    //let modelo = structuredClone(MODELO_BASE);

    //modelo["id"] = parseInt($("#txtPrimaryKey").val());
    //modelo["codigo"] = $("#txtCodigo").val();
    //modelo["name"] = $("#txtName").val();
    //modelo["esActivo"] = $("#cboEstado").val() == 1 ? true : false;

    const model = {
        id: parseInt($("#txtPrimaryKey").val()),
        codigo: $("#txtCodigo").val(),
        name: $("#txtName").val(),
        esActivo: $("#cboEstado").val() == 1 ? true : false,
        "auditInsertUsuario": 'cocorocos',
        "auditUpdateUsuario": 'cocorocos',
        "auditInsertFecha": '2025-06-02',
        "auditUpdateFecha": '2025-06-02',
    };

    //if (model.id == 0) {
    //    model["auditInsertUsuario"] = 'cocorocos';
    //    model["auditInsertFecha"] = '2025-06-02';
    //} else {
    //    model["auditUpdateUsuario"] = 'cocorocos';
    //    model["auditUpdateFecha"] = '2025-06-02';
    //}

    let formData = new FormData();
    formData.append("url", JSON.stringify("/api/TipoAlmacen"));
    formData.append("model", JSON.stringify(model));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (model.id == 0) {
        fetch("/Generic/Crear", {
            method: "POST",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado == false) {
                    tablaData.row.add(responseJson.objeto).draw(false);
                    $("#modalData").modal("hide");
                    swal("Listo", `${tituloEntity} fue creado`, "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
    else {
        fetch("/Generic/Editar", {
            method: "PUT",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado == false) {
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
            "url": `/Generic/ListaAll`,
            "type": "GET",
            data: { 'url': url },
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