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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using bhexpress.api_client;
using bhexpress.api_client.utils;

namespace tests
{
    [TestClass]
    public class TestBoletas
    {

        /// <summary>
        /// Test para probar ListadoBhe sin parámetros (default).
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasDefault()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();

            // Creación de clase para su uso
            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe();
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe sólo con periódo en formato AAAA (anio)
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasAnio()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string anio = Environment.GetEnvironmentVariable("TEST_LISTAR_ANIO");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(periodo: anio);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                
                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }
                
                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe sólo con periodo en formato AAAAMM.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasPeriodo()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string periodo = Environment.GetEnvironmentVariable("TEST_LISTAR_PERIODO");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(periodo: periodo);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe con fechaDesde y fechaHasta.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasRango()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string fechaDesde = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHADESDE");
            string fechaHasta = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHAHASTA");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(fechaDesde: fechaDesde, fechaHasta: fechaHasta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }
        
        /// <summary>
        /// Test para probar ListadoBhe con periodo, fechaDesde y fechaHasta correctos.
        /// 
        /// Retornará las boletas con el filtro de periodo, ya que tiene prioridad sobre fechaDesde y fechaHasta.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasPeriodoRango()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas
            string periodo = Environment.GetEnvironmentVariable("TEST_LISTAR_PERIODO");
            string fechaDesde = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHADESDE");
            string fechaHasta = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHAHASTA");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(periodo: periodo, fechaDesde: fechaDesde, fechaHasta: fechaHasta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe sólo con receptorCodigo.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasReceptor()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string receptorCodigo = Environment.GetEnvironmentVariable("TEST_LISTAR_CODIGORECEPTOR");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(receptorCodigo: receptorCodigo);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe sólo con periodo, fechaDesde, fechaHasta y receptorCodigo.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasPeriodoReceptor()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas
            string periodo = Environment.GetEnvironmentVariable("TEST_LISTAR_PERIODO");
            string receptorCodigo = Environment.GetEnvironmentVariable("TEST_LISTAR_CODIGORECEPTOR");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(periodo: periodo, receptorCodigo: receptorCodigo);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe sólo con fechaDesde, fechaHasta y receptorCodigo.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasRangoReceptor()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas
            string fechaDesde = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHADESDE");
            string fechaHasta = Environment.GetEnvironmentVariable("TEST_LISTAR_FECHAHASTA");
            string receptorCodigo = Environment.GetEnvironmentVariable("TEST_LISTAR_CODIGORECEPTOR");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(fechaDesde: fechaDesde, fechaHasta: fechaHasta, receptorCodigo: receptorCodigo);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe con fechaDesde, fechaHasta, y periodo en un año diferente.
        /// 
        /// ESTA PRUEBA DEBE FALLAR Y ENTREGAR MENSAJE DE QUE LOS AÑOS NO COINCIDEN.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasRangoErrorAnio()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables definidas para probar errores
            string fechaDesde = "2024-06-01";
            string fechaHasta = "2023-06-02";

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(fechaDesde: fechaDesde, fechaHasta: fechaHasta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"ApiException: {e}");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe con fechaDesde, fechaHasta con fecha anterior a fechaDesde, y periodo.
        /// 
        /// ESTA PRUEBA DEBE FALLAR Y ENTREGAR MENSAJE DE QUE LAS FECHAS SON INCORRECTAS.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasRangoErrorDia()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables definidas para probar errores+
            string fechaDesde = "2024-06-03";
            string fechaHasta = "2024-06-02";

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(fechaDesde: fechaDesde, fechaHasta: fechaHasta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"ApiException: {e}");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar ListadoBhe con fechaDesde y fechaHasta con fecha anterior a fechaDesde.
        /// 
        /// ESTA PRUEBA DEBE FALLAR Y ENTREGAR MENSAJE DE QUE LAS FECHAS SON INCORRECTAS.
        /// </summary>
        [TestMethod]
        public void TestListadoBoletasRangoErrorMes()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables definidas para probar errores
            string fechaDesde = "2024-06-03";
            string fechaHasta = "2024-02-05";

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.ListadoBhe(fechaDesde: fechaDesde, fechaHasta: fechaHasta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(resultado.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar ninguna boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"ApiException: {e}");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test para probar EmitirBoleta. Los valores insertados siguen una estructura en múltiples 
        /// diccionarios y listas anidados en un diccionario mayor que sería la boleta, o body.
        /// 
        /// Para mayor información de cómo armar el diccionario, revisar el ejemplo provisto aquí, o
        /// referirse al body en: <https://developers.bhexpress.cl/#56ce2cab-f2e3-433b-b81c-937d24765191>`_.
        /// </summary>
        [TestMethod]
        public void TestEmitirBoleta()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string fechaEmision = Environment.GetEnvironmentVariable("TEST_EMITIR_FECHA_EMIS");
            string emisor = Environment.GetEnvironmentVariable("TEST_EMITIR_EMISOR");
            string receptor = Environment.GetEnvironmentVariable("TEST_EMITIR_RECEPTOR");
            string nombreRecep = Environment.GetEnvironmentVariable("TEST_EMITIR_RZNSOC_REC");
            string dirRecep = Environment.GetEnvironmentVariable("TEST_EMITIR_DIR_REC");
            string comunaRecep = Environment.GetEnvironmentVariable("TEST_EMITIR_COM_REC");

            // CREACIÓN DE ENCABEZADO
            // Emisión y tipo de retención
            Dictionary<string, object> IdDoc = new Dictionary<string, object>()
            {
                {"FchEmis", fechaEmision},
                {"TipoRetencion", 2 }
            };
            // Emisor
            Dictionary<string, string> Emisor = new Dictionary<string, string>()
            {
                {"RUTEmisor", emisor}
            };
            // Receptor
            Dictionary<string, string> Receptor = new Dictionary<string, string>()
            {
                {"RUTRecep", receptor},
                {"RznSocRecep", nombreRecep},
                {"DirRecep", dirRecep},
                {"CmnaRecep", comunaRecep}
            };
            // Encabezado completo
            Dictionary<string, object> Encabezado = new Dictionary<string, object>()
            {
                {"IdDoc", IdDoc},
                {"Emisor", Emisor},
                {"Receptor", Receptor}
            };

            // CREACIÓN DE DETALLE
            // Items
            Dictionary<string, object> item1 = new Dictionary<string, object>()
            {
                {"CdgItem", 0},
                {"NmbItem", "Se agrega código y cantidad al item (se indica precio unitario)"},
                {"QtyItem", 1},
                {"PrcItem", 1000}
            };
            Dictionary<string, object> item2 = new Dictionary<string, object>()
            {
                {"NmbItem", "Se agrega cantidad al item (se indica precio unitario)"},
                {"QtyItem", 2},
                {"PrcItem", 2500}
            };
            Dictionary<string, object> item3 = new Dictionary<string, object>()
            {
                {"CdgItem", 2},
                {"NmbItem", "Caso más completo, con código, cantidad, precio y descuento porcentual"},
                {"QtyItem", 2},
                {"PrcItem", 700},
                {"DescuentoPct", 10 }
            };
            Dictionary<string, object> item4 = new Dictionary<string, object>()
            {
                {"CdgItem", 3},
                {"NmbItem", "Caso más completo, con código, cantidad, precio y descuento fijo"},
                {"QtyItem", 2},
                {"PrcItem", 700},
                {"DescuentoMonto", 600 }
            };
            // Lista de items
            List<Dictionary<string, object>> Detalle = new List<Dictionary<string, object>>()
            {
                item1, item2, item3, item4
            };

            // DTE TEMPORAL COMPLETO
            Dictionary<string, object> boleta = new Dictionary<string, object>()
            {
                {"Encabezado", Encabezado},
                {"Detalle", Detalle}
            };

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.EmitirBoleta(boleta);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                
                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }
                

                Assert.AreEqual(jsonResponse.Length > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido crear la boleta. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }


        /// <summary>
        /// Test para probar ObtenerPdfBoleta desde BHExpress.
        /// 
        /// El valor obtenido se convierte a ByteArray, y el ByteArray se transforma en un PDF que se guarda en 
        /// bin/debug/ dentro del proyecto tests.
        /// </summary>
        [TestMethod]
        public void TestPdfBoleta()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string numeroBhe = Environment.GetEnvironmentVariable("TEST_PDF_NUMEROBHE");

            Boletas boleta = new Boletas();

            try
            {
                HttpResponseMessage response = boleta.ObtenerPdfBoleta(numeroBhe);
                byte[] respuesta = response.Content.ReadAsByteArrayAsync().Result;

                if (respuesta.Length == 0)
                {
                    Trace.WriteLine($"La boleta no existe.");
                }
                else
                {
                    System.IO.File.WriteAllBytes($@"test_pdf_{numeroBhe}.pdf", respuesta);
                    Trace.WriteLine("El PDF se ha generado exitosamente (Guardado en tests/bin/Debug/test_pdf_{numeroBhe}.pdf).");
                }

                Assert.AreEqual(respuesta.Length >= 0, true);
            }
            catch (AssertFailedException e)
            {
                // Si arroja un mensaje de error, es porque no estás conectado
                Trace.WriteLine($"No se ha podido encontrar la boleta. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error de búsqueda. Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test que prueba EnviarEmailBoleta con un nro de boleta y email existentes.
        /// 
        /// Una vez efectuada la prueba, revisar correo al que fue mandado.
        /// </summary>
        [TestMethod]
        public void TestEmailBoleta()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string numeroBhe = Environment.GetEnvironmentVariable("TEST_EMAIL_NUMEROBHE");
            string email = Environment.GetEnvironmentVariable("TEST_EMAIL_CORREO");

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.EnviarEmailBoleta(numeroBhe, email);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(jsonResponse.Length > 0, true);
            }
            catch (AssertFailedException e)
            {
                throw new ApiException($"Error al enviar la boleta. Error: {e}");
            }
            catch (JsonSerializationException e)
            {
                throw new ApiException($"Error de serialización json. Error: {e}");
            }
            catch (Exception e)
            {
                throw new ApiException($"Error: {e}");
            }
        }

        /// <summary>
        /// Test que prueba AnularBoleta con una boleta existente.
        /// 
        /// La boleta debe tener menos de 2 meses de antigüedad y no haber sido cobrada ni previamente anulada.
        /// </summary>
        [TestMethod]
        public void TestAnularBoleta()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            // Variables de entorno definidas desde test_env
            string numeroBhe = Environment.GetEnvironmentVariable("TEST_ANULAR_NUMEROBHE");
            int causa = 3;

            Boletas boletas = new Boletas();

            try
            {
                HttpResponseMessage response = boletas.AnularBoleta(numeroBhe, causa);
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                foreach (var informacion in resultado)
                {
                    Trace.WriteLine(informacion.ToString());
                }

                Assert.AreEqual(jsonResponse.Length > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido encontrar contribuyente. Error: {e}");
                Assert.Fail();
            }
            catch (JsonSerializationException e)
            {
                Trace.WriteLine($"Error de serialización json. Error: {e}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }
    }
}
