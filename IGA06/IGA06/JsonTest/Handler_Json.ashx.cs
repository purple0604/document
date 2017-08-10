using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace IGA06.JsonTest
{
    /// <summary>
    /// Handler_Json 的摘要描述
    /// </summary>
    public class Handler_Json : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<Data>(context.Request.QueryString[0]);
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private class Data
        {
            public List<TableData> Tables { get; set; }
            public string test { get; set; }
        }

        private class TableData
        {
            public string Name { get; set; }
            public List<Condition> Condition { get; set; }
        }

        private class Condition
        {
            public string ColumnName { get; set; }
            public string Operation { get; set; }
            public DateTime Value { get; set; }
        }
    }
}