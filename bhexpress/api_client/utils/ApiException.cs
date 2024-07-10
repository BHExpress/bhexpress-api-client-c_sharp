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

using System;
using System.Collections.Generic;

namespace bhexpress.api_client.utils
{
    public class ApiException : Exception
    {
        public int Code;
        public Dictionary<int, string> Parameters { get; set; }

        /// <summary>
        /// Excepción personalizada para errores en el cliente de la API.
        /// </summary>
        /// <param name="message" type="string">mensaje de error.</param>
        /// <param name="code" type="int">Código de error (opcional).</param>
        /// <param name="parameters" type="Dictionary<int, string>"> parámetros adicionales del error (opcional).</param>
        public ApiException(string message, int code = 0, Dictionary<int, string> parameters = null) : base(message)
        {
            this.Code = code;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Devuelve una representación en cadena del error, proporcionando un contexto claro
        /// del problema ocurrido. Esta representación incluye el prefijo "[BHExpress]",
        /// seguido del código de error si está presente, y el mensaje de error.
        /// 
        /// Si se especifica un código de error, el formato será
        /// "[BHExpress] Error {code}: {message}"
        /// 
        /// Si no se especifica un código de error, el formato será
        /// "[BHExpress] {message}"
        /// </summary>
        /// <returns type="string">Cadena que representa el error de manera clara y concisa.</returns>
        public override string ToString()
        {
            if (this.Code != 0)
            {
                return $"[BHExpress] Error {this.Code}: {this.Message}";
            }
            else
            {
                return $"[BHExpress] {this.Message}";
            }
        }
    }
}
