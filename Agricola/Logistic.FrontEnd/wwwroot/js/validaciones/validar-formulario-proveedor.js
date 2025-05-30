window.addEventListener('load', () => {

    let form = document.querySelector('#formulario')

    let tipoDcmto = document.getElementById('cbxTipoDcmtoIdentidad');
    var tipoDcmtoValue = tipoDcmto.value;
    var tipoDcmtoText = tipoDcmto.options[tipoDcmto.selectedIndex].text.substring(0, 3);

    let numeroDcmtoIdentidad = document.getElementById('numeroDcmtoIdentidad')
    //let razonSocial = document.getElementById('razonSocial')
    //let apellidoPaterno = document.getElementById('apellidoPaterno')
    //let apellidoMaterno = document.getElementById('apellidoMaterno')
    //let primerNombre = document.getElementById('primerNombre')
    //let segundoNombre = document.getElementById('segundoNombre')
    let email = document.getElementById('email')

    //console.log('tipoDcmtoValue => ' + tipoDcmtoValue);
    //console.log('tipoDcmtoText => ' + tipoDcmtoText);
    //console.log('numeroDcmtoIdentidad => ' + numeroDcmtoIdentidad);
    //console.log('razonSocial => ' + razonSocial);
    //console.log('apellidoPaterno => ' + apellidoPaterno);
    //console.log('apellidoMaterno => ' + apellidoMaterno);
    //console.log('primerNombre => ' + primerNombre);
    //console.log('segundoNombre => ' + segundoNombre);
    //console.log('email => ' + email);


    form.addEventListener('submit', (e) => {
        e.preventDefault()
        validaCampos()
    })

    const validaCampos = () => {

        const emailValor = email.value.trim()

        console.log('emailValor => ' + emailValor);

        //validando campo email
        if (!emailValor) {
            validaFalla(email, 'Campo vacío')
        } else if (!validaEmail(emailValor)) {
            validaFalla(email, 'El e-mail no es válido')
        } else {
            validaOk(email)
        }
        //validando campo password



    }

    const validaFalla = (input, msje) => {
        const formControl = input.parentElement
        const aviso = formControl.querySelector('invalid-email')
        aviso.innerText = msje

        formControl.className = 'form-control falla'
    }
    //const validaOk = (input, msje) => {
    //    const formControl = input.parentElement
    //    formControl.className = 'form-control ok'
    //}

    const validaEmail = (email) => {
        return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
    }

})