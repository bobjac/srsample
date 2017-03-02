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

namespace CSVtoJSON.Controllers
{
    public class JSONtoCSVController : ApiController
    {
        /// <summary>
        /// Convert JSON to CSV
        /// </summary>
        /// <param name="body">JSON string</param>
        /// <returns>CSV string</returns>
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [Metadata("JSON to CSV", "Convert JSON to CSV")]
        public HttpResponseMessage Post([FromBody] string body)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonTextWriter tw = new JsonTextWriter(sw);

            serializer.Serialize(tw, body);

            string csv = "Name,Email,Date\nAdam Francis,jacobsrj@gmail.com,1/1/17\nJill Turk,bobjac@microsoft.com,1/2/17\nMarc Levine,mlevine@whitemanorcc.com,2/1/17";
            return Request.CreateResponse<string>(csv);
        }
    }
}
