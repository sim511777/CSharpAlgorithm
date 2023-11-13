using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using System.Xml;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.ComponentModel;
using System.Text.Json;
using System.Drawing;

namespace _06_05_LinqTree {
    public enum JsonSerializerType {
        DataContractJsonSerializer,
        JavaScriptSerializer,
        JsonDotNet,
        Xml,
        JsonSerializer,
        JsonSerializer_Opt,
        YamlDotNet,
    }
    public class JsonUtil {
        public static string ObjectToJson(object obj, JsonSerializerType serializerType, bool indent) {
            if (serializerType == JsonSerializerType.DataContractJsonSerializer) {
                var ser = new DataContractJsonSerializer(obj.GetType());
                using (var ms = new MemoryStream())
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, true, indent)) {
                    ser.WriteObject(writer, obj);
                    writer.Flush();
                    var json = Encoding.UTF8.GetString(ms.ToArray());
                    return json;
                }
            } else if (serializerType == JsonSerializerType.JavaScriptSerializer) {
                var jss = new JavaScriptSerializer();
                var json = jss.Serialize(obj);
                return json;
            } else if (serializerType == JsonSerializerType.JsonDotNet) {
                var settings = new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Auto
                };
                var json = JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None, settings);
                return json;
            } else if (serializerType == JsonSerializerType.Xml) {
                var ser = new XmlSerializer(obj.GetType());
                using (var ms = new MemoryStream()) {
                    ser.Serialize(ms, obj);
                    var json = Encoding.UTF8.GetString(ms.ToArray());
                    return json;
                }
            } else if (serializerType == JsonSerializerType.JsonSerializer) {
                var opt = new JsonSerializerOptions();
                opt.WriteIndented = indent;
                var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj, opt);
                return Encoding.UTF8.GetString(bytes);
            } else if (serializerType == JsonSerializerType.JsonSerializer_Opt) {
                var opt = new JsonSerializerOptions() {
                    WriteIndented = indent,
                    Converters = { new ColorJsonConverter() },
                };

                var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj, opt);
                return Encoding.UTF8.GetString(bytes);
            } else if (serializerType == JsonSerializerType.YamlDotNet) {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var yaml = serializer.Serialize(obj);
                return yaml;
            } else {
                throw new Exception("Invalid Serializer");
            }
        }

        public static T JsonToObject<T>(string json, JsonSerializerType serializerType) {
            if (serializerType == JsonSerializerType.DataContractJsonSerializer) {
                var ser = new DataContractJsonSerializer(typeof(T));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                    var obj = (T)ser.ReadObject(ms);
                    return obj;
                }
            } else if (serializerType == JsonSerializerType.JavaScriptSerializer) {
                var jss = new JavaScriptSerializer();
                var obj = jss.Deserialize<T>(json);
                return obj;
            } else if (serializerType == JsonSerializerType.JsonDotNet) {
                JsonSerializerSettings a = new JsonSerializerSettings();
                var obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            } else if (serializerType == JsonSerializerType.Xml) {
                var ser = new XmlSerializer(typeof(T));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                    var obj = (T)ser.Deserialize(ms);
                    return obj;
                }
            } else if (serializerType == JsonSerializerType.JsonSerializer) {
                var obj = System.Text.Json.JsonSerializer.Deserialize<T>(json);
                return obj;
            } else if (serializerType == JsonSerializerType.JsonSerializer_Opt) {
                var opt = new JsonSerializerOptions() {
                    WriteIndented = true,
                    Converters = { new ColorJsonConverter() },
                };

                var obj = System.Text.Json.JsonSerializer.Deserialize<T>(json, opt);
                return obj;
            } else if (serializerType == JsonSerializerType.YamlDotNet) {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml
                    .Build();

                //yml contains a string containing your YAML
                var obj = deserializer.Deserialize<T>(json);
                return obj;
            } else {
                throw new Exception("Invalid Serializer");
            }
        }
    }

    public class ColorJsonConverter : System.Text.Json.Serialization.JsonConverter<Color> {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Color));
            return (Color)tc.ConvertFromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Color));
            writer.WriteStringValue(tc.ConvertToString(value));
        }
    }
}


