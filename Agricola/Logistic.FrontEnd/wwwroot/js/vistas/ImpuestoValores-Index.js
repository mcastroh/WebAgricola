

let tablaData;

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/ImpuestoValores/ListaAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idImpuesto", "visible": false, "searchable": false },
            { "data": "codigo" },
            { "data": "name" },
            { "data": "porcentaje" },
            { "data": "valor" },  
            { "data": "topeDesde", "visible": false, "searchable": false },
            { "data": "topeHasta", "visible": false, "searchable": false }, 
            { "data": "fechaVigenciaDesde" },
            { "data": "fechaVigenciaHasta" }, 
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
                filename: 'Reporte de Impuestos',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});

function mostarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idImpuesto)
    $("#txtCodigo").val(modelo.codigo)
    $("#txtDescripcion").val(modelo.name)
    $("#txtPorcentaje").val(modelo.porcentaje)
    $("#txtValor").val(modelo.valor)
    $("#txtTopeDesde").val(modelo.topeDesde)
    $("#txtTopeHasta").val(modelo.topeHasta)
    $("#txtFechaVigenciaDesde").val(modelo.fechaVigenciaDesde)
    $("#txtFechaVigenciaHasta").val(modelo.fechaVigenciaHasta) 
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
        let mensaje = inputSinValor[0].name == "Name" ? `Debe ingresar datos: Descripción"` : `Debe ingresar datos: "${inputSinValor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputSinValor[0].name}"]`).focus()
        return;
    }

    let modelo = structuredClone(MODELO_BASE);
    modelo["idImpuesto"] = parseInt($("#txtPrimaryKey").val());
    modelo["codigo"] = $("#txtCodigo").val();
    modelo["name"] = $("#txtName").val();
    modelo["porcentaje"] = $("#txtPorcentaje").val();
    modelo["valor"] = $("#txtValor").val();
    modelo["topeDesde"] = $("#txtTopeDesde").val();
    modelo["topeHasta"] = $("#txtTopeHasta").val();
    modelo["fechaVigenciaDesde"] = $("#txtFechaVigenciaDesde").val();
    modelo["fechaVigenciaHasta"] = $("#txtFechaVigenciaHasta").val(); 
    modelo["esActivo"] = $("#cboEstado").val();
     
    let formData = new FormData();
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idImpuesto == 0) {
        fetch("/ImpuestoValores/Crear", {
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
                    swal("Listo", "Tipo de Impuesto fue creado", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
    }
    else {
        fetch("/ImpuestoValores/Editar", {
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
                    swal("Listo", "Tipo de Impuesto fue modificado", "success");
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


    console.log(data)

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
            text: `Eliminar Tipo de Impuesto "${data.name}"`,
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

                fetch(`/ImpuestoValores/Eliminar?idImpuesto=${data.idImpuesto}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo", "Tipo de Impuesto fue eliminado", "success");
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error");
                        }
                    })
            }
        }
    );

});
