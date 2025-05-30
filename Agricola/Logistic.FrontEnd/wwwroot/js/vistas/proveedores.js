
$(document).ready(function () {


    $("#btnNuevo").click(function () {
        //cargarRoles();
        //mostarModal();
        $("#modalData").modal("show");
        showhide();

    });



    function showhide() {
        let tipoDcmto = document.getElementById('cbxTipoDcmtoIdentidad');
        let value = tipoDcmto.value;
        let text = tipoDcmto.options[tipoDcmto.selectedIndex].text.substring(0, 3);

        console.log("1. text: " + text + "   value: " + text);

        if (text == 'RUC') {
            hideApellidosAndNombre();
            showRazonSocial();

        } else {
            hideRazonSocial();
            showApellidosAndNombre();
        }
    }

    function hideApellidosAndNombre() {
        var elem = document.getElementById('tipoDcmtoNoRUC');
        elem.style.display = 'none';
    }

    function showApellidosAndNombre() {
        var elem = document.getElementById('tipoDcmtoNoRUC');
        elem.style.display = 'inline-flex';
    }

    function hideRazonSocial() {
        var elem = document.getElementById('tipoDcmtoRUC');
        elem.style.display = 'none';
    }

    function showRazonSocial() {
        var elem = document.getElementById('tipoDcmtoRUC');
        elem.style.display = 'inline';
    }

});


