let opcionOriginal = [];

function seleccionarOpcionesPorTexto(texto, idSelect) {
    texto = texto.toUpperCase();

    var selectElement = document.getElementById(idSelect);
    if(opcionOriginal.length === 0) {
        for(var i = 0; i < selectElement.options.length; i++){
            opcionOriginal[i] = selectElement.options[i];
        }
    }

    while(selectElement.options.length){
        selectElement.removeChild(selectElement.options[0]);
    }

    for (var i = 0; i < opcionOriginal.length; i++) {
        if (opcionOriginal[i].value.toUpperCase().includes(texto)) {
            selectElement.appendChild(opcionOriginal[i]);
        }
    }
}

function obtenerValorInput(idInput) {
    // Obtener el valor actual del input
    var valorInput = document.getElementById(idInput).value;
    // Mostrar el valor en la consola (puedes hacer lo que desees con este valor)
    console.log("Valor del input: " + valorInput);
}