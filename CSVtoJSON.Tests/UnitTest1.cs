using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using CSVtoJSON.Models;
using Newtonsoft.Json;

namespace CSVtoJSON.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void XmlTest()
        {
            string filePath = @"C:\dev\customers\silkroad\Integration\GetUserProfileEx2_Sample.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            var nodeList = xmlDoc.SelectSingleNode("//*[local-name()='Data']");
            string innerText = nodeList.InnerText;

            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(innerText);
            var usersElement = doc2.FirstChild.FirstChild;

            var xmlString = usersElement.InnerText;

            Assert.IsNotNull(nodeList);
        }

        [TestMethod]
        public void CompositeJsonResultTest()
        {
            var result1 = new Models.JsonResult();
            for (int i=0; i < 50; i++)
            {
                JObject jObj = new JObject();
                for(int j =0; j< 10; j++)
                {
                    string keyName = string.Format("AttributeKey1-{0}", j);
                    string keyValue = string.Format("AttributeValue1-{0}", j);
                    jObj[keyName] = keyValue;
                }
                result1.rows.Add(jObj);
            }

            var result2 = new Models.JsonResult();
            for (int i = 0; i < 20; i++)
            {
                JObject jObj = new JObject();
                for (int j = 0; j < 7; j++)
                {
                    string keyName = string.Format("AttributeKey2-{0}", j);
                    string keyValue = string.Format("AttributeValue2-{0}", j);
                    jObj[keyName] = keyValue;
                }
                result2.rows.Add(jObj);
            }

            var result3 = new Models.JsonResult();
            for (int i = 0; i < 10; i++)
            {
                JObject jObj = new JObject();
                for (int j = 0; j < 5; j++)
                {
                    string keyName = string.Format("AttributeKey3-{0}", j);
                    string keyValue = string.Format("AttributeValue3-{0}", j);
                    jObj[keyName] = keyValue;
                }
                result3.rows.Add(jObj);
            }

            var composite = new CompositeJsonResult { Result1 = result1, Result2 = result2, Result3 = result3 };
            string output = JsonConvert.SerializeObject(composite);

            Assert.IsFalse(String.IsNullOrEmpty(output));
        }
    }
}
