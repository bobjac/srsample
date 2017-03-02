using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TRex.Metadata;
using CSVtoJSON.Models;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Data;

namespace CSVtoJSON.Controllers
{
    public class CovertItController : ApiController
    {
        /// <summary>
        /// Convert JSON to CSV
        /// </summary>
        /// <param name="body">JSON string</param>
        /// <returns>CSV string</returns>
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [Metadata("ConvertIt", "Convert JSON to CSV2")]
        public HttpResponseMessage Post([FromBody] string body)
        {
            string csv = JSONtoCSVCall(body);

            //string endpointAddress = null;
            //if (System.Configuration.ConfigurationManager.AppSettings[endpoint] != null)
            //{
            //    endpointAddress = new EndPointAddress(System.Configuration.ConfigurationManager.AppSettings[endpoint]);
            //    srv.Endpoint.Address = endpointAddress;
            //}

            return Request.CreateResponse<string>(csv);
        }

        private string JSONtoCSVCall(string req)
        {
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + req + "}}");

            XmlDocument xmldoc = new XmlDocument();
            //Create XmlDoc Object
            xmldoc.LoadXml(xml.InnerXml);
            //Create XML Steam
            var xmlReader = new XmlNodeReader(xmldoc);
            DataSet dataSet = new DataSet();
            //Load Dataset with Xml
            dataSet.ReadXml(xmlReader);
            //return single table inside of dataset
            var csv = dataSet.Tables[0];
            return ToCSV(csv, ",");
        }


        public static string ToCSV(DataTable table, string delimator)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : delimator);
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : delimator);
                }
            }
            return result.ToString().TrimEnd(new char[] { '\r', '\n' });
            //return result.ToString();
        }
    }
}
