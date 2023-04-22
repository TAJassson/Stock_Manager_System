using System.Xml;
using System;
using System.IO;
using System.Data;
using System.Xml.Serialization;

namespace stock_manager_system
{
    internal class XMLManager
    {
        public string xmlfile { get; set; }
        public class XMLReadLine
        {
            [XmlElement("Database")]
            public string SQLType { get; set; } //SQLite, MSSQL, PGSQL
            public string FileNamedb { get; set; }  //SQL, DB, CSV
            public string ConnectIPAddress { get; set; } // 0 = not in use
            public string ConnectionString { get; set; }
            public string NetworkCredential { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            // Database Setup

            [XmlElement("PasswordProtectSetting")]
            public string PasswordProtection { get; set; }
            public string PasswordProtectType { get; set; }
            public string MD5FileProtect { get; set; }
            public string FileProtectType { get; set; }
            public string DailyEncryptDatabase { get; set; }
            public string AllowLoadSQLOnLocalHost { get; set; }

            [XmlElement("DateTimeSetting")]
            public string loginTime { get; set; }
            public string loginDate { get; set; }
            public string EncodingDateTime { get; set; }

            [XmlElement("Domain")]
            public string GetUserConnectionDomain { get; set; }

            [XmlElement("METADATA")]
            public string LoadMetadata { get; set; }
            public string MetadataType { get; set; }

            [XmlElement("HTTPServer")]
            public string ConnectHTTP { get; set; }
            public string AuthHTTPServer { get; set; }
            public string AuthHTTPAddress { get; set; }
            public string HTTPCredential { get; set; }
            public string HTTPConnectedPort { get; set; }

            [XmlElement("CrosshairSystem")]
            public string GetClientHWINFO { get; set; }
            public string HwidInfoHashProtect { get; set; }
            public string HwidFileType { get; set; }

            [XmlElement("TCPServer")]
            public string TCPServerListen { get; set; }
            public string TCPPort { get; set; }
            public string UDPPort { get; set; }
            public string ServerRecevidedByte { get; set; }
        }


    }
}
