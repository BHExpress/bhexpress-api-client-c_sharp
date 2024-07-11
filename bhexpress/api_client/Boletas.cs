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
            string rut = Environment.GetEnvironmentVariable("BHEXPRESS_EMISOR_RUT");
            List<string> parameters = new List<string>();

            // Revisión de variables
            // Periodo dividido en año y mes (si es que viene con mes) convertido en enteros
            int perAnio = (!string.IsNullOrEmpty(periodo)) ? Convert.ToInt32(periodo.Substring(0, 4)) : 0;
            int perMes = (!string.IsNullOrEmpty(periodo) && periodo.Length > 4) ? Convert.ToInt32(periodo.Substring(4, 2)) : 0;
            // Fechas divididas y convertidas en enteros
            int fechaDesdeAnio = (!string.IsNullOrEmpty(fechaDesde)) ? Convert.ToInt32(fechaDesde.Split('-')[0]) : 0;
            int fechaDesdeMes = (!string.IsNullOrEmpty(fechaDesde)) ? Convert.ToInt32(fechaDesde.Split('-')[1]) : 0;
            int fechaDesdeDia = (!string.IsNullOrEmpty(fechaDesde)) ? Convert.ToInt32(fechaDesde.Split('-')[2]) : 0;
            int fechaHastaAnio = (!string.IsNullOrEmpty(fechaHasta)) ? Convert.ToInt32(fechaHasta.Split('-')[0]) : 0;
            int fechaHastaMes = (!string.IsNullOrEmpty(fechaHasta)) ? Convert.ToInt32(fechaHasta.Split('-')[1]) : 0;
            int fechaHastaDia = (!string.IsNullOrEmpty(fechaHasta)) ? Convert.ToInt32(fechaHasta.Split('-')[2]) : 0;
            Trace.WriteLine(fechaDesdeAnio + "-" + fechaDesdeMes + "-" + fechaDesdeDia);
            Trace.WriteLine(fechaHastaAnio + "-" + fechaHastaMes + "-" + fechaHastaDia);
            Trace.WriteLine(perAnio + "-" + perMes);

            if ((string.IsNullOrEmpty(fechaDesde) && !string.IsNullOrEmpty(fechaHasta)) || 
                (!string.IsNullOrEmpty(fechaDesde) && string.IsNullOrEmpty(fechaHasta)))
            {
                throw new ApiException("Debe ingresar fechaDesde junto con fechaHasta.");
            }
            if ((fechaDesdeAnio > fechaHastaAnio && fechaDesdeAnio != 0 && fechaHastaAnio != 0) ||
                ((fechaDesdeAnio <= fechaHastaAnio && fechaDesdeMes > fechaHastaMes) && fechaDesdeMes != 0 && fechaHastaMes != 0) ||
                ((fechaDesdeAnio <= fechaHastaAnio && fechaDesdeMes <= fechaHastaMes && fechaDesdeDia > fechaHastaDia) && fechaDesdeDia != 0 && fechaHastaDia != 0))
            {
                throw new ApiException("La fecha de fechaDesde no puede ser mayor que la de fechaHasta.");
            }
            if ((fechaDesdeAnio != perAnio && fechaDesdeAnio != 0 && perAnio != 0) || (fechaDesdeMes != perMes && fechaDesdeMes != 0 && perMes != 0))
            {
                throw new ApiException("Se generó un conflicto entre fechaDesde y periodo: Las fechas no coinciden.");
            }
            if ((fechaHastaAnio != perAnio && fechaHastaAnio != 0 && perAnio != 0) || (fechaHastaMes != perMes && fechaHastaMes != 0 && perMes != 0))
            {
                throw new ApiException("Se generó un conflicto entre fechaHasta y periodo: Las fechas no coinciden.");
            }
            
            // Construcción de URL
            if (!string.IsNullOrEmpty(periodo))
            {
                parameters.Add($"periodo={periodo}");
            }
            if (!string.IsNullOrEmpty(fechaDesde) && !string.IsNullOrEmpty(fechaHasta))
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
            // Crear cabecera para el request
            Dictionary<string, string> header = new Dictionary<string, string>(){
                { "X-Bhexpress-Emisor", rut }
            };

            return this.client.Get(url, headers: header);
        }

        /// <summary>
        /// Recurso para emitir boletas de honorarios electrónicas.
        /// </summary>
        /// <param name="boleta">Diccionario que contiene todo el detalle de la boleta.</param>
        /// <returns>HttpResponseMessage Devuelve una respuesta con los datos de la boleta emitida.</returns>
        public HttpResponseMessage EmitirBoleta(Dictionary<string, object> boleta)
        {
            string url = "/bhe/emitir";
            string rut = Environment.GetEnvironmentVariable("BHEXPRESS_EMISOR_RUT");
            Dictionary<string, string> header = new Dictionary<string, string>(){
                { "X-Bhexpress-Emisor", rut }
            };

            return this.client.Post(resource: url, data: boleta, headers: header);
        }

        /// <summary>
        /// Recurso que permite generar un PDF a partir de una boleta existente.
        /// </summary>
        /// <param name="numeroBhe">Número de boleta.</param>
        /// <returns>HttpResponseMessage Devuelve valores convertibles a ByteArray que posteriormente se guardarán en un PDF.</returns>
        public HttpResponseMessage ObtenerPdfBoleta(string numeroBhe)
        {
            string url = $"/bhe/pdf/{numeroBhe}";
            string rut = Environment.GetEnvironmentVariable("BHEXPRESS_EMISOR_RUT");
            Dictionary<string, string> header = new Dictionary<string, string>(){
                { "X-Bhexpress-Emisor", rut }
            };

            return this.client.Get(url, headers: header);
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
            string rut = Environment.GetEnvironmentVariable("BHEXPRESS_EMISOR_RUT");
            Dictionary<string, string> correo = new Dictionary<string, string>()
            {
                {"email", email }
            };
            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "destinatario", correo }
            };

            Dictionary<string, string> header = new Dictionary<string, string>(){
                { "X-Bhexpress-Emisor", rut }
            };

            return this.client.Post(resource: url, data: body, headers: header);
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
            string rut = Environment.GetEnvironmentVariable("BHEXPRESS_EMISOR_RUT");

            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "causa", causa }
            };

            Dictionary<string, string> header = new Dictionary<string, string>(){
                { "X-Bhexpress-Emisor", rut }
            };

            return this.client.Post(resource: url, data: body, headers: header);
        }
    }
}
