let form = document.getElementById('form');

form.addEventListener('submit', (e) => {
    let mensajeError = [];

    let error = document.getElementById('error');
    error.style.color = 'red';

    let tipoDcmto = document.getElementById('cbxTipoDcmtoIdentidad');
    let tipoDcmtoText = tipoDcmto.options[tipoDcmto.selectedIndex].text.substring(0, 3);
    let numeroDcmtoIdentidad = document.getElementById('NumeroDcmtoIdentidad');

    let razonSocial = document.getElementById('RazonSocial');

    let apellidoPaterno = document.getElementById('ApellidoPaterno');
    let apellidoMaterno = document.getElementById('ApellidoMaterno');
    let primerNombre = document.getElementById('PrimerNombre');

    if (tipoDcmto.value === null || tipoDcmto.value === '') {
        mensajeError.push('<p>Seleccionar tipo de documento<br></p>')
    }

    if (numeroDcmtoIdentidad.value === null || numeroDcmtoIdentidad.value === '') {
        mensajeError.push('<p>Número documento de identidad es obligatorio<br></p>')
    }

    if (tipoDcmtoText == 'RUC') {

        if (razonSocial.value === null || razonSocial.value === '') {
            mensajeError.push('<p>Razón Social es obligatorio<br></p>')
        }

    } else {


        if (apellidoPaterno.value === null || apellidoPaterno.value === '') {
            mensajeError.push('<p>Apellido Paterno es obligatorio<br></p>')
        }

        if (apellidoMaterno.value === null || apellidoMaterno.value === '') {
            mensajeError.push('<p>Apellido Materno es obligatorio<br></p>')
        }

        if (primerNombre.value === null || primerNombre.value === '') {
            mensajeError.push('<p>Primer Nombre es obligatorio<br></p>')
        }

    }

    if (mensajeError.length > 0) {
        e.preventDefault();
        error.innerHTML = mensajeError.join(', ')
    }


    //return false;   // evita que el formulario se envie por defecto



})