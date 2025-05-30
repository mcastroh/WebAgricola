// ------------------------------------------------
// Validar sólo números incluido espacios en blanco
// ------------------------------------------------
function validateSoloNumerosIncluidoEspaciosEnBlanco(evt) {
    if (window.event) {
        keynum = evt.keyCode;
    }
    else {
        keynum = evt.which;
    }

    if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 32) {
        return true;
    }
    else {
        // console.log('Ingresar sólo números"');
        return false;
    }
}

// --------------------
// Validar sólo números 
// --------------------
function validateSoloNumeros(evt) {
    if (window.event) {
        keynum = evt.keyCode;
    }
    else {
        keynum = evt.which;
    }

    if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 32) {
        return true;
    }
    else {
        // console.log('Ingresar sólo números"');
        return false;
    }
}

// --------------------------
// Validar un número decimal
// --------------------------
function validateUnDecimal(valor) {
    let RE = /^\d*\.?\d*$/;

    if (RE.test(valor)) {
        return true;
    } else {
        return false;
    }
}

// ------------------------------------------------------
// Validar un número decimal con dos dígitos de precisión
// ------------------------------------------------------
function validateDosDecimales(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    let key = window.Event ? evt.which : evt.keyCode;
    let chark = String.fromCharCode(key);
    let tempValue = input.value + chark;
    let isNumber = (key >= 48 && key <= 57);
    let isSpecial = (key == 8 || key == 13 || key == 0 || key == 46);

    if (isNumber || isSpecial) {
        return filter(tempValue);
    }

    return false;

}

function filter(__val__) {
    let preg = /^([0-9]+\.?[0-9]{0,2})$/;
    return (preg.test(__val__) === true);
}
