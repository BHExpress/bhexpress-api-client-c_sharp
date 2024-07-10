/*
* BHExpress: Cliente de API en C#.
* Copyright (C) BHExpress <https://www.bhexpress.cl>
*
* Este programa es software libre: usted puede redistribuirlo y/o modificarlo
* bajo los términos de la GNU Lesser General Public License (LGPL) publicada
* por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
* o (a su elección) cualquier versión posterior de la misma.
*
* Este programa se distribuye con la esperanza de que sea útil, pero SIN
* GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
* PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
* Public License (LGPL) para obtener una información más detallada.
*
* Debería haber recibido una copia de la GNU Lesser General Public License
* (LGPL) junto a este programa. En caso contrario, consulte
* <http://www.gnu.org/licenses/lgpl.html>.
*/

using System.Collections.Generic;

namespace bhexpress.api_client.utils
{
    public class ApiBase
    {
        public ApiClient client;

        /// <summary>
        /// Clase base para las clases que consumen la API (wrappers).
        /// </summary>
        /// <param name="apiToken" type="string">Token de autenticación para la API.</param>
        /// <param name="apiUrl" type="string">URL base para la API.</param>
        /// <param name="apiVersion" type="string">¿Versión de la API.</param>
        /// <param name="apiRaiseForStatus" type="bool">Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por omisión es true.</param>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales para la autenticación.</param>
        public ApiBase(string apiToken = null, string apiUrl = null, string apiVersion = null, bool apiRaiseForStatus = true, Dictionary<string, string> kwargs = null)
        {
            this.client = new ApiClient(apiToken, apiUrl, apiVersion, apiRaiseForStatus);
        }
    }
}
