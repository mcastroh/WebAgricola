
$(document).ready(function () {

    $.getJSON("getUbigeoDepartamento", {}, function (dataDepartamentos) {
        $("#cboprovincia").empty();
        $("#cbodistrito").empty();
        $("#cbodepartamento").append($("<option>").attr({ "value": "" }).text("Seleccionar"));
        $.each(dataDepartamentos, function (i, obj) {
            $("#cbodepartamento").append("<option value='" + obj.idDepartamento + "'>" + obj.departamentoName + "</option>");
        });
    });

    $("#cbodepartamento").change(function () {
        $("#cboprovincia").empty();
        $("#cbodistrito").empty();
        let keyDepartamento = $("#cbodepartamento").val();
        $.getJSON("getUbigeoProvinciasByDepartamento", { "b_departamento": keyDepartamento }, function (data) {
            $("#cboprovincia").append($("<option>").attr({ "value": "" }).text("Seleccionar"));
            $.each(data, function (i, obj) {
                $("#cboprovincia").append("<option value='" + obj.idProvincia + "'>" + obj.provinciaName + "</option>");
            });
        });
    });

    $("#cboprovincia").change(function () {
        $("#cbodistrito").empty();
        let keyProvincia = $("#cboprovincia").val();
        $.getJSON("getUbigeoDistritosByDepartamentoAndProvincia", { "b_provincia": keyProvincia }, function (data) {
            $("#cbodistrito").append($("<option>").attr({ "value": "" }).text("Seleccionar"));
            $.each(data, function (i, obj) {
                $("#cbodistrito").append("<option value='" + obj.idDistrito + "'>" + obj.distritoName + "</option>");
            });
        });
    });

}); 