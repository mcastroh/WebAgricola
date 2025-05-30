function PruebasMach01() {

    console.log("ingresa function => PruebasMach01");

    let rut = $("#rut").val();
    let nombres = $("#nombres").val();
    let apellidos = $("#apellidos").val();
    let clave = $("#clave").val();

    $.ajax({
        "url": '/PruebasMach01/Crear',
        data: {
            rut: rut,
            nombres: nombres,
            apellidos: apellidos,
            clave: clave
        },
        type: "post",
        success: function (data) {
            if (data.substring(0, 10) === "registrado") {
                alert("registrado correctamente");
                location.href = "/PruebasMach01/Registro";
            }
            else
                alert("Error en el registro");
        },
        error: function (data) {
            console.log("Error: " + " data");
        }

    });

} 