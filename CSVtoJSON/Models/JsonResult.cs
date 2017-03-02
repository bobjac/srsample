using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSVtoJSON.Models
{
    public class JsonResult
    {
        public JsonResult()
        {
            rows = new List<object>();
        }
        public List<object> rows { get; set; }
    }

    public class CompositeJsonResult
    {
        public JsonResult Result1 { get; set; }
        public JsonResult Result2 { get; set; }
        public JsonResult Result3 { get; set; }
    }
}