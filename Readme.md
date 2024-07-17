BHExpress: Cliente de API en C#
================================

[![NuGet version](https://img.shields.io/nuget/v/bhexpress.svg)](https://www.nuget.org/packages/bhexpress/)
[![NuGet downloads](https://img.shields.io/nuget/dt/bhexpress.svg)](https://www.nuget.org/packages/bhexpress/)

Cliente para realizar la integración con los servicios web de [BHExpress](https://www.bhexpress.cl) desde C#.

Instalación y actualización
---------------------------

Instalación mediante el Administrador de Paquetes NuGet
-------------------------------------------------------

1.  Abre tu proyecto en Visual Studio.

2.  Haz clic derecho en el proyecto en el Explorador de Soluciones y 
    selecciona "Administrar paquetes NuGet...".

3.  En la pestaña "Examinar", busca `bhexpress`.

4.  Selecciona el paquete `bhexpress` y haz clic en "Instalar".

Instalación desde la línea de comandos (cmd)
------------------------------------------------------

1.  Abre la línea de comandos desde Herramientas, Administrador de paquetes NuGet,
    Consola del administrador de paquetes.

2.  Ejecuta el siguiente comando para instalar `bhexpress`:

```sh
    nuget install bhexpress
```

Modo de uso
-----------

Se recomienda ver los ejemplos para más detalles. Lo que se muestra aquí es sólo
una idea, y muy resumida:

Lo más simple, y recomendado, es usar una variable de entorno con el [token del usuario](https://bhexpress.cl/usuarios/perfil#token),
el cual será reconocido automáticamente por el cliente:

```C#
using bhexpress.api_client;
using System.Net.Http;
using System.Collections.Generic;

Boletas boleta = new Boletas();
// Encabezado contiene la información de la boleta, del emisor y del receptor.
// Detalle contiene una lista con el detalle de los items.

HttpResponseMessage response = boleta.ListadoBhe(); // Respuesta del cliente de API.

var jsonResponse = response.Content.ReadAsStringAsync().Result; // Resultado convertido en JSON
Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse); // Resultado convertido en diccionario.
```

Lo que hizo el ejemplo anterior es listar boletas emitidas en un resultado.

Ejemplos
--------

Estos ejemplos provienen de la versión PHP. Para crear la versión C# de BHExpress,
se tomó en cuenta dichos ejemplos.
Los ejemplos cubren los siguientes casos:

- `001-boletas_listado.php`: obtener las boletas de un período.
- `002-boleta_emitir.php`: emisitir una BHE.
- `003-boleta_pdf.php`: descargar el PDF de una BHE.
- `004-boleta_email.php`: enviar por email una BHE.
- `005-boleta_anular.php`: anular una BHE.

Los ejemplos, por defecto, hacen uso de variables de entornos, si quieres usar
esto debes tenerlas creadas, por ejemplo, en Windows 10, con:

```shell
set BHEXPRESS_API_URL="https://bhexpress.cl"
set BHEXPRESS_API_TOKEN="" # aquí el token obtenido en https://bhexpress.cl/usuarios/perfil#token
set BHEXPRESS_EMISOR_RUT="" # aquí el RUT del emisor de las BHE
```

Luego, para probar los ejemplos, lo que se necesita es Visual Studio 2019, y descargar este proyecto
desde NuGet, o descargar desde GitHub y añadir un proyecto desde tu explorador de soluciones.

Licencia
--------

Este programa es software libre: usted puede redistribuirlo y/o modificarlo
bajo los términos de la GNU Lesser General Public License (LGPL) publicada
por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
o (a su elección) cualquier versión posterior de la misma.

Este programa se distribuye con la esperanza de que sea útil, pero SIN
GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
Public License (LGPL) para obtener una información más detallada.

Debería haber recibido una copia de la GNU Lesser General Public License
(LGPL) junto a este programa. En caso contrario, consulte
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html).

Enlaces
-------

- [Sitio web BHExpress](https://www.bhexpress.cl).
- [Código fuente en GitHub](https://github.com/BHExpress/bhexpress-api-client-c_sharp).
- [Paquete en NuGet](https://www.nuget.org/packages/bhexpress).