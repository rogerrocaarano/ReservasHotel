let opcionOriginal = [];

function seleccionarOpcionesPorTexto(texto, idSelect) {
    texto = texto.toUpperCase();

    let selectElement = document.getElementById(idSelect);
    if(opcionOriginal.length === 0) {
        for(let i = 0; i < selectElement.options.length; i++){
            opcionOriginal[i] = selectElement.options[i];
        }
    }

    while(selectElement.options.length){
        selectElement.removeChild(selectElement.options[0]);
    }

    for (let i = 0; i < opcionOriginal.length; i++) {
        if (opcionOriginal[i].value.toUpperCase().includes(texto)) {
            selectElement.appendChild(opcionOriginal[i]);
        }
    }
}
