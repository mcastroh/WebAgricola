let tablaData;

$(document).ready(function () {

    console

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Ventas/ListaPeriodoActual',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idOrdenCompra", "visible": false, "searchable": false },
            { "data": "fechaDocumento" },
            { "data": "documentoIdentidad"},
            { "data": "razonSocial"},
            { "data": "serieDocumento"},
            { "data": "numeroDocumento"},           
            { "data": "totalCompra"},
            { "data": "monedaSimbolo"},
            {
                "data": "estadoDocumento", render: function (data) {
                    if (data == "A")
                        return `<span class="badge badge-info">Abierto</span>`
                    else
                        return `<span class="badge badge-success">Cerrado</span>`
                }
            },             
            {
                "data": "estadoPago", render: function (data) {
                    if (data == "C")
                        return `<span class="badge badge-success">Cancelado</span>`
                    else
                        return `<span class="badge badge-warning">Pendiente</span>`
                }
            },  
            {
                "defaultContent":
                    '<button class="btn btn-primary btn-editar  btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm mr-2"><i class="fas fa-trash-alt"></i></button>' +
                    '<button class="btn btn-success btn-listar  btn-sm mr-2"><i class="fas fa-file-pdf"></i></button>', 
                "orderable": false,
                "searchable": false,
                "width": "150px"
            }
              
        ],
        order: [[1, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte de Compras',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});  