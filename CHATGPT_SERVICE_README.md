# Servicio Genérico de ChatGPT

## Descripción

Se ha implementado un servicio genérico para realizar consultas a la API de ChatGPT de OpenAI. Este servicio permite integrar capacidades de IA en tu aplicación de manera fácil y reutilizable.

## Archivos Creados

### 1. **IChatGPTService.cs** (Interface)
Ubicación: `FolderViewWeb/Services/IChatGPTService.cs`

Define el contrato del servicio con tres métodos principales:
- `SendPromptAsync`: Envía un prompt simple y retorna la respuesta
- `SendPromptWithHistoryAsync`: Envía una conversación completa con historial
- `SendPromptDetailedAsync`: Envía un prompt y retorna información detallada incluyendo tokens utilizados

### 2. **ChatGPTService.cs** (Implementación)
Ubicación: `FolderViewWeb/Services/ChatGPTService.cs`

Implementación del servicio que:
- Se conecta a la API de OpenAI
- Maneja autenticación con API Key
- Procesa respuestas y errores
- Soporta diferentes modelos (gpt-4, gpt-3.5-turbo, etc.)
- Controla temperatura y máximo de tokens

### 3. **ChatGPTController.cs** (API Controller)
Ubicación: `FolderViewWeb/Controllers/ChatGPTController.cs`

Controlador de API con tres endpoints:
- `POST /api/chatgpt/send`: Envío simple de prompts
- `POST /api/chatgpt/send-detailed`: Envío con control completo de parámetros
- `POST /api/chatgpt/send-with-history`: Envío con historial de conversación

## Configuración

### 1. Configuración completa en appsettings.json

Todos los parámetros del bot están configurables en `appsettings.json`:

```json
{
  "ChatGPT": {
    "ApiKey": "sk-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "ApiUrl": "https://api.openai.com/v1/chat/completions",
    "DefaultModel": "gpt-4",
    "DefaultTemperature": 0.7,
    "DefaultMaxTokens": 1000,
    "RequestTimeoutSeconds": 120,
    "EnableLogging": true,
    "DefaultSystemMessage": "Eres un asistente útil y profesional."
  }
}
```

**IMPORTANTE**: Reemplaza `YOUR_OPENAI_API_KEY_HERE` con tu API Key real de OpenAI.

### Descripción de parámetros:

- **ApiKey**: Tu API Key de OpenAI (REQUERIDO)
- **ApiUrl**: URL del endpoint de ChatGPT (default: https://api.openai.com/v1/chat/completions)
- **DefaultModel**: Modelo a usar por defecto (gpt-4, gpt-3.5-turbo, etc.)
- **DefaultTemperature**: Creatividad de las respuestas (0.0 - 2.0)
- **DefaultMaxTokens**: Longitud máxima de respuesta en tokens
- **RequestTimeoutSeconds**: Timeout para las peticiones HTTP
- **EnableLogging**: Habilitar logs detallados (true/false)
- **DefaultSystemMessage**: Mensaje de sistema por defecto para todas las consultas

Para obtener una API Key:
1. Ve a https://platform.openai.com/
2. Inicia sesión o crea una cuenta
3. Ve a API Keys
4. Crea una nueva API Key

### 2. Registro de Servicio (Ya configurado)

El servicio ya está registrado en `Program.cs`:

```csharp
builder.Services.AddHttpClient<IChatGPTService, ChatGPTService>();
```

### 3. Ventajas de la configuración centralizada

✅ **Todos los parámetros están en appsettings.json**
✅ **No hay valores hardcodeados en el código**
✅ **Fácil de modificar sin recompilar**
✅ **Diferentes configuraciones por ambiente** (Development, Production, etc.)
✅ **Valores por defecto configurables**

Si no especificas parámetros al llamar al servicio, se usarán los valores configurados en appsettings.json automáticamente.

## Uso del Servicio

### Desde un Controller

```csharp
using FolderView.Services;

public class MiController : Controller
{
    private readonly IChatGPTService _chatGPTService;

    public MiController(IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
    }

    public async Task<IActionResult> GenerarTexto()
    {
        // Uso simple - Usa todos los valores del appsettings.json
        var respuesta = await _chatGPTService.SendPromptAsync(
            "Escribe un párrafo sobre inteligencia artificial"
        );

        // Uso con mensaje de sistema personalizado - Otros valores del config
        var respuesta2 = await _chatGPTService.SendPromptAsync(
            "Escribe un párrafo sobre inteligencia artificial",
            "Eres un experto en tecnología"
        );

        // Uso con detalles y valores personalizados (sobrescribe el config)
        var respuestaDetallada = await _chatGPTService.SendPromptDetailedAsync(
            prompt: "Explica qué es ASP.NET Core",
            systemMessage: "Eres un profesor de programación",
            model: "gpt-3.5-turbo",  // Sobrescribe DefaultModel del config
            temperature: 0.5,         // Sobrescribe DefaultTemperature del config
            maxTokens: 500            // Sobrescribe DefaultMaxTokens del config
        );

        // Uso con valores del config por defecto
        var respuesta3 = await _chatGPTService.SendPromptDetailedAsync(
            prompt: "Explica qué es Dapper"
            // No se especifican otros parámetros, usa los del appsettings.json
        );

        // Uso con historial - Usa DefaultModel, DefaultTemperature y DefaultMaxTokens del config
        var mensajes = new List<ChatMessage>
        {
            new ChatMessage { Role = "system", Content = "Eres un asistente útil" },
            new ChatMessage { Role = "user", Content = "¿Qué es C#?" },
            new ChatMessage { Role = "assistant", Content = "C# es un lenguaje de programación..." },
            new ChatMessage { Role = "user", Content = "Dame un ejemplo de código" }
        };

        var respuestaConHistorial = await _chatGPTService.SendPromptWithHistoryAsync(mensajes);

        return Ok(respuesta);
    }
}
```

## Ejemplos de Uso de la API

### 1. Envío Simple

```bash
POST /api/chatgpt/send
Content-Type: application/json

{
  "prompt": "Escribe un saludo cordial",
  "systemMessage": "Eres un asistente amigable"
}
```

**Respuesta:**
```json
{
  "success": true,
  "response": "¡Hola! Es un placer saludarte..."
}
```

### 2. Envío Detallado (con control de parámetros)

```bash
POST /api/chatgpt/send-detailed
Content-Type: application/json

{
  "prompt": "Explica qué es Dapper",
  "systemMessage": "Eres un experto en .NET",
  "model": "gpt-4",
  "temperature": 0.5,
  "maxTokens": 300
}
```

**Respuesta:**
```json
{
  "success": true,
  "response": {
    "content": "Dapper es un micro ORM...",
    "model": "gpt-4",
    "usage": {
      "promptTokens": 25,
      "completionTokens": 150,
      "totalTokens": 175
    },
    "finishReason": "stop"
  }
}
```

### 3. Envío con Historial

```bash
POST /api/chatgpt/send-with-history
Content-Type: application/json

{
  "messages": [
    {
      "role": "system",
      "content": "Eres un experto en SQL Server"
    },
    {
      "role": "user",
      "content": "¿Qué es un stored procedure?"
    },
    {
      "role": "assistant",
      "content": "Un stored procedure es un conjunto de instrucciones SQL..."
    },
    {
      "role": "user",
      "content": "Dame un ejemplo"
    }
  ]
}
```

## Parámetros Disponibles

### Models (Modelos)
- `gpt-4`: Modelo más potente y preciso
- `gpt-3.5-turbo`: Modelo rápido y económico
- `gpt-4-turbo`: Versión optimizada de GPT-4

### Temperature (Creatividad)
- Rango: 0.0 - 2.0
- `0.0`: Respuestas más deterministas y consistentes
- `0.7`: Balance entre creatividad y coherencia (recomendado)
- `1.5-2.0`: Respuestas más creativas y variadas

### MaxTokens (Longitud máxima)
- Define el número máximo de tokens en la respuesta
- Aprox. 1 token = 4 caracteres en español
- Recomendado: 500-2000 para respuestas normales

## Manejo de Errores

El servicio lanza excepciones cuando:
- La API Key no está configurada
- La API de OpenAI retorna un error
- Hay problemas de red

Ejemplo de manejo:

```csharp
try
{
    var respuesta = await _chatGPTService.SendPromptAsync("Hola");
}
catch (HttpRequestException ex)
{
    // Error de API o red
    _logger.LogError(ex, "Error al llamar a OpenAI");
}
catch (ArgumentNullException ex)
{
    // API Key no configurada
    _logger.LogError(ex, "API Key no configurada");
}
```

## Configuración por Ambiente

Puedes tener diferentes configuraciones para Development y Production:

### appsettings.Development.json
```json
{
  "ChatGPT": {
    "ApiKey": "sk-dev-key-xxxxxxxxx",
    "DefaultModel": "gpt-3.5-turbo",
    "DefaultMaxTokens": 500,
    "EnableLogging": true
  }
}
```

### appsettings.Production.json
```json
{
  "ChatGPT": {
    "ApiKey": "sk-prod-key-xxxxxxxxx",
    "DefaultModel": "gpt-4",
    "DefaultMaxTokens": 2000,
    "EnableLogging": false,
    "RequestTimeoutSeconds": 180
  }
}
```

## Seguridad

### Recomendaciones:

1. **Nunca compartas tu API Key** en repositorios públicos
2. **Usa variables de entorno** en producción:
   - En appsettings.json NO incluyas la API Key real
   - Configúrala como variable de entorno del sistema
   - O usa Azure Key Vault / AWS Secrets Manager
3. **Implementa rate limiting** para evitar abusos
4. **Valida y sanitiza** los inputs de usuarios
5. **Monitorea el uso** para controlar costos
6. **Agrega la API Key al .gitignore**: Crea un `appsettings.local.json` para desarrollo local

## Costos

OpenAI cobra por tokens:
- GPT-4: ~$0.03 por 1K tokens (prompt) + $0.06 por 1K tokens (completion)
- GPT-3.5-turbo: ~$0.0015 por 1K tokens (prompt) + $0.002 por 1K tokens (completion)

Consulta precios actualizados en: https://openai.com/pricing

## Próximos Pasos

Posibles mejoras:
- [ ] Implementar caché de respuestas
- [ ] Agregar streaming de respuestas
- [ ] Soporte para imágenes (DALL-E)
- [ ] Implementar function calling
- [ ] Agregar límite de rate
- [ ] Sistema de logging detallado
- [ ] Soporte para embeddings

## Referencias

- [Documentación oficial de OpenAI](https://platform.openai.com/docs)
- [API Reference](https://platform.openai.com/docs/api-reference)
- [Best Practices](https://platform.openai.com/docs/guides/production-best-practices)
