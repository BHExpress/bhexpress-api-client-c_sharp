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
using System.Text;
using Newtonsoft.Json;

namespace bhexpress.api_client.utils
{
    public class ApiClient
    {
        // Constantes
        private const string DEFAULT_URL = "https://bhexpress.cl";
        private const string DEFAULT_VERSION = "/v1";

        // Variables
        private string token;
        private string url;
        private Dictionary<string, string> headers { get; set; }
        private string version { get; set; }
        private bool raiseForStatus { get; set; }
        private HttpClient httpClient;

        // Constructor
        public ApiClient(string token = null, string url = null, string version = null, bool raiseForStatus = true)
        {
            this.token = this.ValidateToken(token);
            this.url = this.ValidateUrl(url);
            this.headers = this.GenerateHeaders();
            this.version = version ?? DEFAULT_VERSION;
            this.raiseForStatus = raiseForStatus;
            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// Valida y retorna el token de autenticación.
        /// </summary>
        /// <param name="token" type="string">¿Token de autenticación a validar.</param>
        /// <returns type="string">¿Token validado.</returns>
        /// <exception cref="ApiException">Si el token no es válido o está ausente.</exception>
        private string ValidateToken(string token)
        {
            token = token ?? Environment.GetEnvironmentVariable("BHEXPRESS_API_TOKEN");

            if (string.IsNullOrEmpty(token))
            {
                throw new ApiException("Se debe configurar la variable de entorno: BHEXPRESS_API_TOKEN.");
            }

            return token.Trim();
        }

        /// <summary>
        /// URL a validar.
        /// </summary>
        /// <param name="url" type="string">¿URL validada.</param>
        /// <returns type="string">URL validada.</returns>
        /// <exception cref="ApiException">Si la URL no es válida o está ausente.</exception>
        private string ValidateUrl(string url)
        {
            url = url ?? Environment.GetEnvironmentVariable("BHEXPRESS_API_URL") ?? DEFAULT_URL;

            return url.Trim();
        }

        /// <summary>
        /// Genera y retorna las cabeceras por omisión para las solicitudes.
        /// </summary>
        /// <returns type="Dictionary<string, string>">Dictionary Cabeceras por omisión.</returns>
        public Dictionary<string, string> GenerateHeaders()
        {
            return new Dictionary<string, string>
            {
                {"User-Agent", "BHExpress-Cliente-de-API-en-C#."},
                {"Accept", "application/json"},
                {"Authorization", $"Token {this.token}"}
            };
        }

        /// <summary>
        /// Realiza una solicitud GET a la API.
        /// </summary>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="headers" type="Dictionary<string, string>">Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Get(string resource, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Get, resource: resource, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud DELETE a la API.
        /// </summary>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="headers" type="Dictionary<string, string>">Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Delete(string resource, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Delete, resource: resource, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud POST a la API.
        /// </summary>
        /// <param name="resource" type="string">String Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, object>">Datos a enviar en la solicitud.</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Post(string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Post, resource: resource, data: data, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud PUT a la API.
        /// </summary>
        /// <param name="resource" type="string">String Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, object>">Datos a enviar en la solicitud.</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Put(string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Put, resource: resource, data: data, headers: headers);
        }

        /// <summary>
        /// Verifica la respuesta de la solicitud HTTP y maneja los errores.
        /// </summary>
        /// <param name="response" type="HTTPResponseMessage"> Objeto de respuesta de requests.</param>
        /// <returns type="HTTPResponseMessage"> Respuesta de la solicitud.</returns>
        /// <exception cref="ApiException">Si la respuesta contiene un error HTTP.</exception>
        private HttpResponseMessage CheckAndReturnResponse(HttpResponseMessage response)
        {
            if ((int)response.StatusCode != 200 && raiseForStatus)
            {
                string errorMessage = response.Content.ReadAsStringAsync().Result;
                throw new ApiException($"Error HTTP: {errorMessage}");
            }

            return response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Método privado para realizar solicitudes HTTP.
        /// </summary>
        /// <param name="method" type="HttpMethod">HttpMethod Método HTTP a utilizar.</param>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, Dictionary<string, object>">Dictionary Datos a enviar en la solicitud (opcional).</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud (opcional).</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        /// <exception cref="ApiException">Si el método HTTP no es soportado o si hay un error de conexión.</exception>
        private HttpResponseMessage SendRequest(HttpMethod method, string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            string apiPath = $"/api{this.version}{resource}";
            Trace.WriteLine(this.url + apiPath);
            Uri fullUrl = new Uri($"{this.url}{apiPath}");
            HttpRequestMessage request = new HttpRequestMessage(method, fullUrl);

            if (data != null)
            {
                var jsonData = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            foreach (var header in this.headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            try
            {
                var response = this.httpClient.SendAsync(request).Result;
                HttpResponseMessage resp = this.CheckAndReturnResponse(response);

                return resp;
            }
            catch (HttpRequestException e)
            {
                throw new ApiException($"Error en la solicitud: {e}");
            }
            catch (TimeoutException e)
            {
                throw new ApiException($"Error de timeout: {e}");
            }
            catch (Exception e)
            {
                throw new ApiException($"Error: {e}");
            }
        }
    }
}
