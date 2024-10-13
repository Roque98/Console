// Función para hacer una petición AJAX GET
function fetchData(url) {
    return fetch(url)
        .then(response => response.json())
        .catch(error => console.error('Error al obtener los datos:', error));
}

// Función para hacer una petición AJAX POST
function sendPost(url, body) {
    return fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    }).then(response => response.json())
        .catch(error => console.error('Error al generar el archivo:', error));
}

// Funcion para devoler un elemento del dom de un template string
function generateDomNodeFromHtmlString(stringTemplate) {
    const container = document.createElement('div');

    container.innerHTML = stringTemplate;

    return container.firstElementChild;
}


/**
     * getParam - Helper function to retrieve a parameter from the URL.
     * @@param {string} keyParam - The name of the parameter to retrieve from the URL.
     * @@returns {number} - The value of the parameter or 0 if not found.
     */
function getParam(keyParam) {
    const urlParams = new URLSearchParams(window.location.search); // Object to handle URL parameters
    return urlParams.has(keyParam) ? parseInt(urlParams.get(keyParam)) : 0; // Return the parameter value as an integer, or 0 if not present
}