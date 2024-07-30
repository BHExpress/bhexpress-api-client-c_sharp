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
using System.Diagnostics;
using System.Net.Http;
using bhexpress.api_client.utils;

/// <summary>
/// Módulo para interactuar con Boletas de honorarios electrónicas emitidas, recibidas y anuladas en BHExpress.
/// 
/// Para más información sobre la API, consulte la `documentación completa de las BHE <https://developers.bhexpress.cl/#d2bb7582-db3f-46b0-9b1b-c23c0dca44af>`_.
/// </summary>
namespace bhexpress.api_client
{
    public class Boletas : ApiBase
    {
        /// <summary>
        /// Cliente específico para gestionar Boletas de Honorarios Electrónicas (BHE) emitidas, recibidas y anuladas.
        /// 
        /// Provee métodos para emitir, anular, y consultar información relacionada con BHEs.
        /// </summary>
        public Boletas()
        {

        }

        /// <summary>
        /// Recurso que permite obtener el listado paginado de boletas de honorarios electrónicas emitidas.
        /// 
        /// Los parámetros de entrada son filtros para obtener boletas más específicas.
        /// </summary>
        /// <param name="periodo">Período por el cuál consultar las boletas. Puede ser: "AAAAMM" o "AAAA"</param>
        /// <param name="fechaDesde">Fecha desde cuándo consultar las boletas. Formato: "AAAA-MM-DD"</param>
        /// <param name="fechaHasta">Fecha hasta cuándo consultar las boletas. Formato: "AAAA-MM-DD"</param>
        /// <param name="receptorCodigo">Código del receptor. Generalmente es el RUT del receptor sin DV.</param>
        /// <returns>HttpResponseMessage Retorna la respuesta con el listado de boletas y un filtro opcional </returns>
        /// <exception cref="ApiException">Arroja un error cuando las fechas de periodo, fechaDesde y fechaHasta no coinciden.</exception>
        public HttpResponseMessage ListadoBhe(string periodo = null, string fechaDesde = null, string fechaHasta = null, string receptorCodigo = null)
        {
            string url = "/bhe/boletas";
            List<string> parameters = new List<string>();

            if (string.IsNullOrEmpty(fechaDesde) ^ string.IsNullOrEmpty(fechaHasta))
            {
                throw new ApiException("Debe ingresar fechaDesde junto con fechaHasta.");
            }
            if ((!string.IsNullOrEmpty(fechaDesde) && !string.IsNullOrEmpty(fechaHasta)) && 
                (Convert.ToDateTime(fechaDesde) > Convert.ToDateTime(fechaHasta)))
            {
                throw new ApiException("La fecha de fechaDesde no puede ser mayor que la de fechaHasta.");
            }
            
            // Construcción de URL
            if (!string.IsNullOrEmpty(periodo))
            {
                parameters.Add($"periodo={periodo}");
            }
            else if (!string.IsNullOrEmpty(fechaDesde) && !string.IsNullOrEmpty(fechaHasta))
            {
                parameters.Add($"fecha_desde={fechaDesde}&fecha_hasta={fechaHasta}");
            }
            if (!string.IsNullOrEmpty(receptorCodigo))
            {
                parameters.Add($"receptor_codigo={receptorCodigo}");
            }
            // Concatenar al URL la lista creada
            if (parameters.Count > 0)
            {
                url += "?" + string.Join("&", parameters);
            }

            return this.client.Get(url);
        }

        /// <summary>
        /// Recurso para emitir boletas de honorarios electrónicas.
        /// </summary>
        /// <param name="boleta">Diccionario que contiene todo el detalle de la boleta.</param>
        /// <returns>HttpResponseMessage Devuelve una respuesta con los datos de la boleta emitida.</returns>
        public HttpResponseMessage EmitirBoleta(Dictionary<string, object> boleta)
        {
            string url = "/bhe/emitir";
            return this.client.Post(url, data: boleta);
        }

        /// <summary>
        /// Recurso que permite generar un PDF a partir de una boleta existente.
        /// </summary>
        /// <param name="numeroBhe">Número de boleta.</param>
        /// <returns>HttpResponseMessage Devuelve valores convertibles a ByteArray que posteriormente se guardarán en un PDF.</returns>
        public HttpResponseMessage ObtenerPdfBoleta(string numeroBhe)
        {
            string url = $"/bhe/pdf/{numeroBhe}";

            return this.client.Get(url);
        }

        /// <summary>
        /// Recurso que permite enviar un correo electronico con una boleta específica a un destinatario específico.
        /// </summary>
        /// <param name="numeroBhe">Número de boleta de BHExpress.</param>
        /// <param name="email">Correo del destinatario.</param>
        /// <returns>HttpResponseMessage Devuelve un mensaje confirmando el envío del correo y el destinatario.</returns>
        public HttpResponseMessage EnviarEmailBoleta(string numeroBhe, string email)
        {
            string url = $"/bhe/email/{numeroBhe}";
            Dictionary<string, string> correo = new Dictionary<string, string>()
            {
                {"email", email }
            };
            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "destinatario", correo }
            };

            return this.client.Post(resource: url, data: body);
        }

        /// <summary>
        /// Recurso para anular una boleta específica, determinando un motivo.
        /// </summary>
        /// <param name="numeroBhe">Número de la boleta a anular.</param>
        /// <param name="causa">Razón para anular la boleta.</param>
        /// <returns>HttpResponseMessage Devuelve la información básica de la boleta que fue anulada.</returns>
        public HttpResponseMessage AnularBoleta(string numeroBhe, int causa)
        {
            string url = $"/bhe/anular/{numeroBhe}";

            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "causa", causa }
            };

            return this.client.Post(resource: url, data: body);
        }
    }
}
