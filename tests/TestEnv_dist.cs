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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    class TestEnv_dist
    {
        public TestEnv_dist()
        {

        }

        public void SetVariablesDeEntorno()
        {
            // VARIABLES DE ENTORNO BASE
            string BHEXPRESS_API_URL = "https://bhexpress.cl"; // URL base.
            string BHEXPRESS_API_VERSION = "v1"; // Versión de la API.
            string BHEXPRESS_API_TOKEN = ""; // Token de API, obtenido desde tu cuenta de BHExpress.
            // VARIABLES DE ENTORNO DE HEADER
            string BHEXPRESS_EMISOR_RUT = ""; // RUT del emisor para el header, con DV.
            // VARIABLES DE ENTORNO DE PRUEBA
            // LISTAR BOLETAS
            string TEST_LISTAR_ANIO = ""; // Año de periodo, formato "AAAA"
            string TEST_LISTAR_PERIODO = ""; // Año y mes de periodo, formato "AAAAMM"
            string TEST_LISTAR_FECHADESDE = ""; // Fecha desde cuándo listar boletas, formato "AAAA-MM-DD".
            string TEST_LISTAR_FECHAHASTA = ""; // Fecha hasta cuándo listar boletas, formato "AAAA-MM-DD".
            string TEST_LISTAR_CODIGORECEPTOR = ""; // Código de receptor. Generalmente su RUT sin DV.
            // EMITIR BOLETAS
            string TEST_EMITIR_FECHA_EMIS = ""; // Fecha de emisión de la boleta a emitir, formato "AAAA-MM-DD".
            string TEST_EMITIR_EMISOR = ""; // RUT del emisor, con DV.
            string TEST_EMITIR_RECEPTOR = ""; // RUT del receptor, con DV.
            string TEST_EMITIR_RZNSOC_REC = ""; // Razón social del receptor.
            string TEST_EMITIR_DIR_REC = ""; // Dirección del receptor.
            string TEST_EMITIR_COM_REC = ""; // Comuna del receptor.
            // PDF BOLETA
            string TEST_PDF_NUMEROBHE = ""; // Número del BHE para convertir a PDF.
            // EMAIL BOLETA
            string TEST_EMAIL_NUMEROBHE = ""; // Número del BHE para enviar por correo.
            string TEST_EMAIL_CORREO = ""; // Correo electrónico del destinatario.
            // ANULAR BOLETAS
            string TEST_ANULAR_NUMEROBHE = ""; // Número del BHE a anular.
            // DEFINICIÓN DE VARIABLES DE ENTORNO
            // VARIABLES DE ENTORNO BASE

            Environment.SetEnvironmentVariable("BHEXPRESS_API_URL", BHEXPRESS_API_URL);
            Environment.SetEnvironmentVariable("BHEXPRESS_API_VERSION", BHEXPRESS_API_VERSION);
            Environment.SetEnvironmentVariable("BHEXPRESS_API_TOKEN", BHEXPRESS_API_TOKEN);
            // VARIABLES DE ENTORNO DE HEADER
            Environment.SetEnvironmentVariable("BHEXPRESS_EMISOR_RUT", BHEXPRESS_EMISOR_RUT);
            // VARIABLES DE ENTORNO DE PRUEBA
            // LISTAR BOLETAS
            Environment.SetEnvironmentVariable("TEST_LISTAR_ANIO", TEST_LISTAR_ANIO);
            Environment.SetEnvironmentVariable("TEST_LISTAR_PERIODO", TEST_LISTAR_PERIODO);
            Environment.SetEnvironmentVariable("TEST_LISTAR_FECHADESDE", TEST_LISTAR_FECHADESDE);
            Environment.SetEnvironmentVariable("TEST_LISTAR_FECHAHASTA", TEST_LISTAR_FECHAHASTA);
            Environment.SetEnvironmentVariable("TEST_LISTAR_CODIGORECEPTOR", TEST_LISTAR_CODIGORECEPTOR);
            // EMITIR BOLETAS
            Environment.SetEnvironmentVariable("TEST_EMITIR_FECHA_EMIS", TEST_EMITIR_FECHA_EMIS);
            Environment.SetEnvironmentVariable("TEST_EMITIR_EMISOR", TEST_EMITIR_EMISOR);
            Environment.SetEnvironmentVariable("TEST_EMITIR_RECEPTOR", TEST_EMITIR_RECEPTOR);
            Environment.SetEnvironmentVariable("TEST_EMITIR_RZNSOC_REC", TEST_EMITIR_RZNSOC_REC);
            Environment.SetEnvironmentVariable("TEST_EMITIR_DIR_REC", TEST_EMITIR_DIR_REC);
            Environment.SetEnvironmentVariable("TEST_EMITIR_COM_REC", TEST_EMITIR_COM_REC);
            // PDF BOLETA
            Environment.SetEnvironmentVariable("TEST_PDF_NUMEROBHE", TEST_PDF_NUMEROBHE);
            // EMAIL BOLETA
            Environment.SetEnvironmentVariable("TEST_EMAIL_NUMEROBHE", TEST_EMAIL_NUMEROBHE);
            Environment.SetEnvironmentVariable("TEST_EMAIL_CORREO", TEST_EMAIL_CORREO);
            // ANULAR BOLETAS
            Environment.SetEnvironmentVariable("TEST_ANULAR_NUMEROBHE", TEST_ANULAR_NUMEROBHE);
        }
    }
}
