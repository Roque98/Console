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


/**
 * Realiza una petición HTTP GET a la URL proporcionada y devuelve los datos en formato JSON.
 * 
 * @async
 * @function HttpGet
 * @param {string} url - La URL a la que se hará la petición.
 * @returns {Promise<Object>} - Los datos obtenidos en formato JSON.
 * @throws {Error} - Lanza un error si la respuesta HTTP no es exitosa o si ocurre algún problema en la petición.
 * 
 * @example
 * // Ejemplo de uso:
 * (async () => {
 *     try {
 *         const data = await HttpGet("https://api.example.com/resource");
 *         console.log(data);
 *     } catch (error) {
 *         console.error(error);
 *     }
 * })();
 */
async function HttpGet(url) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP Error: ${response.status} ${response.statusText}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error al obtener los datos:', error);
        throw error;
    }
}

/**
 * Realiza una petición HTTP POST a la URL proporcionada con un cuerpo de datos especificado.
 * 
 * @async
 * @function httpPost
 * @param {string} url - La URL a la que se hará la petición.
 * @param {Object} body - Los datos que se enviarán en el cuerpo de la solicitud.
 * @param {Object} [headers={ 'Content-Type': 'application/json' }] - Encabezados HTTP opcionales para la petición.
 * @returns {Promise<Object>} - La respuesta del servidor en formato JSON.
 * @throws {Error} - Lanza un error si la respuesta HTTP no es exitosa o si ocurre algún problema en la petición.
 * 
 * @example
 * // Ejemplo de uso:
 * (async () => {
 *     try {
 *         const response = await httpPost("https://api.example.com/resource", { key: "value" });
 *         console.log(response);
 *     } catch (error) {
 *         console.error(error);
 *     }
 * })();
 */
async function httpPost(url, body, headers = { 'Content-Type': 'application/json' }) {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers,
            body: JSON.stringify(body),
        });
        if (!response.ok) {
            throw new Error(`HTTP Error: ${response.status} ${response.statusText}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error al generar el archivo:', error);
        throw error;
    }
}
