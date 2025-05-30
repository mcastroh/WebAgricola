const MODELO_BASE = {
    idArticulo: 0,
    codigoArticulo: "",
    descripcionArticulo: "",
    numeroParteFabricante: "",
    observaciones: "",
    idTipoArticulo: 0,
    idGrupoArticulo: 0,
    idSubGrupoArticulo: 0,
    idTipoExistencia: 0,
    idTipoAlmacen: 0,       
    idUnidadCompra: 0,
    idUnidadVenta: 0,
    idUnidadInventario: 0,
    idTipoDetraccion: 0,
    idTipoValorizacion: 0,
    idTipoCuentaMayor: 0,   
    stockMinimo: 0,
    stockMaximo: 0,
    stockSeguridad: 0,       
    esActivo: 1     
};
  
let tablaData;

$(document).ready(function () {

    fetch("/Articulo/ListaTipoExistenciaSunat")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoExistenciaSunat").append(
                        $("<option>").val(item.idTipoExistencia).text(item.descripcion)
                    )
                })
            }
        });

    fetch("/Articulo/ListaAlmacen")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboAlmacenDefault").append(
                        $("<option>").val(item.IdAlmacen).text(item.descripcion)
                    )
                })
            }
        });

    fetch("/Articulo/ListaTiposDeArticulos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => { 
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoArticulo").append(
                        $("<option>").val(item.idTipoArticulo).text(item.descripcion)
                    )
                })
            }
        });
         
    fetch("/Articulo/ListaGruposDeArticulos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboGrupoArticulo").append(
                        $("<option>").val(item.idGrupoArticulo).text(item.descripcion)
                    )
                })
            }
        });         

    fetch("/Articulo/ListaUnidadesDeMedida")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboUnidadCompra").append(
                        $("<option>").val(item.idUnidad).text(item.descripcion)
                    )

                    $("#cboUnidadVenta").append(
                        $("<option>").val(item.idUnidad).text(item.descripcion)
                    )

                    $("#cboUnidadInventario").append(
                        $("<option>").val(item.idUnidad).text(item.descripcion)
                    )

                })
            }
        });   

    fetch("/Articulo/ListaTipoDetraccionSunat")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoDetraccion").append(
                        $("<option>").val(item.idTipoDetraccion).text(item.descripcion)
                    )
                })
            }
        });         

    fetch("/Articulo/ListaTipoValorizacion")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoValorizacion").append(
                        $("<option>").val(item.idTipoValorizacion).text(item.descripcion)
                    )
                })
            }
        });         

    fetch("/Articulo/ListaTipoCuentaMayor")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoCuentaMayor").append(
                        $("<option>").val(item.idTipoCuentaMayor).text(item.name)
                    )
                })
            }
        });      


    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Articulo/ListaAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idArticulo", "visible": false, "searchable": false },
            {
                "data": "urlImagen", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "codigoArticulo" },        
            { "data": "descripcion" },        
            { "data": "nameUnidadInventario" },
            { "data": "nameGrupo" },
            { "data": "nameSubGrupo" },   
            
            { "data": "stockActual" },   
            { "data": "precioUnitarioSoles" },    

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
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Artículos',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6, 7, 8, 9]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    }); 

});

function mostarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idArticulo)
    $("#cboTipoArticulo").val(modelo.idTipoArticulo == 0 ? $("#cboTipoArticulo option:first").val() : modelo.idTipoArticulo)
    $("#cboGrupoArticulo").val(modelo.idGrupoArticulo == 0 ? $("#cboGrupoArticulo option:first").val() : modelo.idGrupoArticulo)
     
    $("#txtDescripcionArticulo").val(modelo.descripcionArticulo)
    $("#cboTipoExistenciaSunat").val(modelo.idTipoExistencia == 0 ? $("#cboTipoExistenciaSunat option:first").val() : modelo.idTipoExistencia)
    $("#cboAlmacenDefault").val(modelo.idAlmacen == 0 ? $("#cboAlmacenDefault option:first").val() : modelo.idAlmacen)



    $("#modalData").modal("show");
}

$("#btnNuevo").click(function () {  
    mostarModal();
});


function grupoSeleccionado() {
    $('#CodigoArticulo').val("");
    let _grupos = document.getElementById('cboGrupoArticulo');
    let _grupoValor = _grupos.options[_grupos.selectedIndex].value;

    $.ajax({
        url: '/Articulo/GetSubGruposByGrupoId',
        type: "GET",
        dataType: "json",
        data: {
            grupoId: _grupoValor
        },
        success: function (response) {
            $("#cboSubGrupoArticulo").empty();

            $("#cboSubGrupoArticulo").append(
                $("<option>").attr({ "value": "" }).text("Seleccionar")
            )

            $.each(response, function (index, elemento) {
                $("#cboSubGrupoArticulo").append(
                    $("<option>").attr({ "value": elemento.idSubGrupoArticulo }).text(elemento.descripcion)
                )
            })
        }
    })
}
 

function obtenerCodigoArticulos() {
    var _subgrupoValor = $("#cboSubGrupoArticulo option:selected").val()

    if (_subgrupoValor == "") {
        $('#CodigoArticulo').val("");
        // document.getElementById("cboSubGrupoArticulo").innerHTML = 'Debe seleccionar sub grupo de artículo';
        // document.getElementById("cboSubGrupoArticulo").style.color = 'red';
        // document.getElementById('cboSubGrupoArticulo').style.fontSize = 12 + "px";
    }
    else {
        $.ajax({
            url: '/Articulo/GetSiguienteCodigoArticulo',
            type: "GET",
            dataType: "json",
            data: {
                subGrupoId: _subgrupoValor
            },
            success: function (data) {
                $('#CodigoArticulo').val(data);
            }
        })
    }
}

function validarFormulario() {

    //
    // Limpia parrafos de mensajes
    document.getElementById("unidadCompraMensaje").innerHTML = '';
    document.getElementById("unidadVentaMensaje").innerHTML = '';
    document.getElementById("unidadInventarioMensaje").innerHTML = '';

    //
    // Tipo de Unidades marcadas
    let _checkUnidadCompra = document.getElementById('CheckUnidadCompra').checked;
    let _checkUnidadVenta = document.getElementById('CheckUnidadVenta').checked;
    let _checkUnidadInventario = document.getElementById('CheckUnidadInventario').checked;

    //
    // Valor de Unidades seleccinadas
    var _unidadCompraValor = $("#cboUnidadCompra option:selected").val()
    var _unidadVentaValor = $("#cboUnidadVenta option:selected").val()
    var _unidadInventarioValor = $("#cboUnidadInventario option:selected").val()

    //
    // Validaciones
    if (_checkUnidadCompra && _unidadCompraValor == "") {
        document.getElementById("unidadCompraMensaje").innerHTML = 'Debe seleccionar unidad de venta';
        document.getElementById("unidadCompraMensaje").style.color = 'red';
        document.getElementById('unidadCompraMensaje').style.fontSize = 12 + "px";
    }

    if (_checkUnidadVenta && _unidadVentaValor == "") {
        document.getElementById("unidadVentaMensaje").innerHTML = 'Debe seleccionar unidad de compra';
        document.getElementById("unidadVentaMensaje").style.color = 'red';
        document.getElementById('unidadVentaMensaje').style.fontSize = 12 + "px";
    }

    if (_checkUnidadInventario && _unidadInventarioValor == "") {
        document.getElementById("unidadInventarioMensaje").innerHTML = 'Debe seleccionar unidad de inventario';
        document.getElementById("unidadInventarioMensaje").style.color = 'red';
        document.getElementById('unidadInventarioMensaje').style.fontSize = 12 + "px";
    }
}


