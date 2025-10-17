# ShoppingCartIDM API

API de carrito de compras desarrollada con .NET 9. La API proporciona endpoints para gestionar un carrito de compras, con una lógica de validación compleja para añadir y actualizar productos basada en atributos y reglas de grupo.

## Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/es-es/download)
- Un editor de código como [Visual Studio Code](https://code.visualstudio.com/) o [Visual Studio](https://visualstudio.microsoft.com/).
- Una herramienta para realizar peticiones HTTP como [Postman](https://www.postman.com/), [Insomnia](https://insomnia.rest/) o la extensión [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) para VS Code (utilizando el archivo `ShoppingCartIDM.http`).

## Cómo Ejecutar la API Localmente

1.  **Clona el repositorio:**
    ```bash
    git clone <URL_DEL_REPOSITORIO>
    cd ShoppingCartIDM
    ```

2.  **Ejecuta la aplicación:**
    Abre una terminal en la raíz del proyecto y ejecuta el siguiente comando:
    ```bash
    dotnet run
    ```
    La API se ejecutará en la URL que se muestre en la consola (generalmente `http://localhost:5215` o `https://localhost:5000`).  
    Si quieres ver los endpoints en Swagger usar la url http://localhost:5215/swagger/index.html

## Endpoints de la API

-   **`POST /api/cart/items`**: Añade un nuevo producto al carrito.
-   **`PUT /api/cart/items/{cartItemId}`**: Actualiza la cantidad y atributos de un producto existente en el carrito.
-   **`PATCH /api/cart/items/{cartItemId}/quantity`**: Ajusta la cantidad de un producto (incrementa o decrementa).
-   **`DELETE /api/cart/items/{cartItemId}`**: Elimina un producto del carrito.
-   **`GET /api/cart`**: Obtiene el contenido completo del carrito.

## Casos de Prueba

A continuación se muestran varios escenarios de prueba para los endpoints.

### Añadir Producto (`POST /api/cart/items`)

#### Prueba 1: Grupo Obligatorio Faltante

-   **Descripción**: Se intenta añadir un producto sin todos los grupos de atributos obligatorios.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: Un mensaje de error indicando que faltan grupos obligatorios.

---

#### Prueba 2: Cantidad de Grupo `EQUAL_THAN` Inválida

-   **Descripción**: Se envían 2 atributos para un grupo que requiere exactamente 1.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [
            {"attributeId": 968670, "quantity": 1},
            {"attributeId": 968671, "quantity": 1}
          ]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: `El grupo 'Elige tu bebida' requiere exactamente 1 selección(es), pero se enviaron 2`

---

#### Prueba 3: Cantidad de Grupo `LOWER_EQUAL_THAN` Excedida

-   **Descripción**: Se envían 5 atributos para un grupo que permite un máximo de 4.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        },
        {
          "groupAttributeId": "241888",
          "attributes": [
            {"attributeId": 968639, "quantity": 1},
            {"attributeId": 968640, "quantity": 1},
            {"attributeId": 968641, "quantity": 1},
            {"attributeId": 968642, "quantity": 1},
            {"attributeId": 968643, "quantity": 1}
          ]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [{"attributeId": 968670, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: `El grupo '¿Deseas más carnes? (opcional)' permite máximo 4 selección(es), pero se enviaron 5`

---

#### Prueba 4: Cantidad de Atributo Excedida

-   **Descripción**: Se envían 5 unidades de un atributo que permite un máximo de 4.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        },
        {
          "groupAttributeId": "241888",
          "attributes": [
            {"attributeId": 968639, "quantity": 5}
          ]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [{"attributeId": 968670, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: `El atributo 'Carne XT' permite máximo 4 unidad(es), pero se enviaron 5`

---

#### Prueba 5: Grupo Inexistente

-   **Descripción**: Se envía un `groupAttributeId` que no existe en la configuración del producto.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "999999",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: `El grupo 999999 no existe en el producto`

---

#### Prueba 6: Atributo Inexistente

-   **Descripción**: Se envía un `attributeId` que no pertenece al grupo especificado.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 999999, "quantity": 1}]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [{"attributeId": 968670, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Error 400 Bad Request)**: `El atributo 999999 no existe en el grupo 241887`

---

#### Prueba 7: Caso Exitoso

-   **Descripción**: Se envía una solicitud que cumple con todas las reglas de validación.
-   **Payload**:
    ```json
    {
      "productId": 3546345,
      "quantity": 1,
      "groupAttributes": [
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        },
        {
          "groupAttributeId": "241888",
          "attributes": [
            {"attributeId": 968639, "quantity": 2},
            {"attributeId": 968643, "quantity": 1}
          ]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [{"attributeId": 968670, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Éxito 200 OK)**: Un objeto de resultado con `isSuccess: true` y los datos del `cartItem` creado.

---

### Actualizar Producto (`PUT /api/cart/items/{cartItemId}`)

-   **Descripción**: Actualiza un producto existente en el carrito, cambiando su cantidad y/o atributos. Se debe proporcionar el `cartItemId` en la URL.
-   **Payload**:
    ```json
    {
      "quantity": 2, // Cantidad actualizada
      "groupAttributes": [
        // Mismos o nuevos atributos que cumplan las reglas
        {
          "groupAttributeId": "241887",
          "attributes": [{"attributeId": 968636, "quantity": 1}]
        },
        {
          "groupAttributeId": "241891",
          "attributes": [{"attributeId": 968663, "quantity": 1}]
        },
        {
          "groupAttributeId": "241892",
          "attributes": [{"attributeId": 968670, "quantity": 1}]
        }
      ]
    }
    ```
-   **Respuesta Esperada (Éxito 200 OK)**: Un objeto con `isSuccess: true` y los datos del `cartItem` actualizado.

---

### Ajustar Cantidad (`PATCH /api/cart/items/{cartItemId}/quantity`)

-   **Descripción**: Incrementa o decrementa la cantidad de un producto existente. Se debe proporcionar el `cartItemId` en la URL.
-   **Payload** (para incrementar en 1):
    ```json
    {
      "adjustment": 1
    }
    ```
-   **Respuesta Esperada (Éxito 200 OK)**: Un objeto con `isSuccess: true` y los datos del `cartItem` con la cantidad ajustada.

---

### Eliminar Producto (`DELETE /api/cart/items/{cartItemId}`)

-   **Descripción**: Elimina un producto del carrito. Se debe proporcionar el `cartItemId` en la URL.
-   **Payload**: No se requiere.
-   **Respuesta Esperada (Éxito 200 OK)**: Un objeto con `isSuccess: true`.

---

### Obtener Carrito (`GET /api/cart`)

-   **Descripción**: Devuelve todos los productos y el estado actual del carrito.
-   **Payload**: No se requiere.
-   **Respuesta Esperada (Éxito 200 OK)**: Un objeto con `isSuccess: true` y los datos del `cart`, incluyendo la lista de `items` y el `totalPrice`.