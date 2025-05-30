// ----------------------------------
// Validar sólo caracteres especiales
// ----------------------------------
function validateSoloCaracteresEspeciales(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toString();
    letraespecial = "$%!@.";

    especiales = [8, 13];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letraespecial.indexOf(tecla) == -1 && !tecla_especial) {
        alert("Ingresar sólo caracteres especiales");
        return false;
    }
} 