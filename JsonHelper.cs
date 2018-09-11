using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace cpplog.Common
{
    public class JsonHelper
    {
        public static string ConvertToJson(object dataObject)
        {
            string convertedJson = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(dataObject.GetType());
                jsonSerializer.WriteObject(stream, dataObject);
                convertedJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return convertedJson;
        }

        public static object ParseDetailResponse(string jsonStr, Type objectType)
        {
            object jsonObject = null;
            using (MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(objectType);
                jsonObject = jsonSerializer.ReadObject(mStream);
            }
            return jsonObject;
        }
    }

    [DataContract]
    public class WifiParameterJsonObject
    {
        [DataMember(IsRequired = false)]
        public string ssid { get; set; }

        [DataMember(IsRequired = false)]
        public string rssi { get; set; }

        public string ToJson()
        {
            return JsonHelper.ConvertToJson(this);
        }

        public static WifiParameterJsonObject ParseFromJson(string jsonStr)
        {
            return JsonHelper.ParseDetailResponse(jsonStr, typeof(WifiParameterJsonObject)) as WifiParameterJsonObject;
        }
    }
}
