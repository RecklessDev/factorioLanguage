using System;
using System.IO;
using System.Linq;
using System.Text;

using Ionic.Zlib;
using Newtonsoft.Json;
using factorioLanguage.JsonClasses;

namespace factorioLanguage
{
    class DecimalJsonConverter : JsonConverter
    {
        public DecimalJsonConverter()
        {
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(float) || objectType == typeof(double));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (DecimalJsonConverter.IsWholeValue(value))
            {
                writer.WriteRawValue(JsonConvert.ToString(Convert.ToInt64(value)));
            }
            //else if (value is float)
            //{
            //writer.WriteRawValue(((float)value).ToString("0.###############"));
            //}
            else
            {
                writer.WriteRawValue(JsonConvert.ToString(value));
            }
        }

        private static bool IsWholeValue(object value)
        {
            if (value is decimal)
            {
                decimal decimalValue = (decimal)value;
                int precision = (Decimal.GetBits(decimalValue)[3] >> 16) & 0x000000FF;
                return precision == 0;
            }
            else if (value is float)
            {
                float doubleValue = (float)value;
                return doubleValue == Math.Truncate(doubleValue);
            }
            else if (value is double)
            {
                double doubleValue = (double)value;
                return doubleValue == Math.Truncate(doubleValue);
            }

            return false;
        }
    }

    class BlueprintProcessor
    {

        public static string BlueprintToJson(string blueprintString)
        {
            byte[] data = Convert.FromBase64String(blueprintString.Remove(0, 1));
            string output = null;

            using (var compressedStream = new MemoryStream(data))
            {
                //skip first two bytes
                compressedStream.ReadByte();
                compressedStream.ReadByte();

                using (var unzip = new DeflateStream(compressedStream, CompressionMode.Decompress))
                {
                    var resultStream = new MemoryStream();
                    unzip.CopyTo(resultStream);
                    output = System.Text.Encoding.Default.GetString(resultStream.ToArray());
                }
            }

            return output;
        }

        public static string JsonToBlueprint(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var zlibStream = new Ionic.Zlib.ZlibStream(stream, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                {
                    var jsonBytes = Encoding.ASCII.GetBytes(json);
                    zlibStream.Write(jsonBytes, 0, jsonBytes.Length);
                }

                return "0" + Convert.ToBase64String(stream.ToArray()).Replace("\n", "");
            }
        }

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        public static FactorioBlueprint ReadBlueprint(string blueprintString)
        {
            var json = BlueprintToJson(blueprintString);
            return JsonConvert.DeserializeObject<FactorioBlueprint>(json, new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });
        }

        public static string MakeBlueprintString(FactorioBlueprint blueprint)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new DecimalJsonConverter());
            var json = JsonConvert.SerializeObject(blueprint, Formatting.None, settings);
            return JsonToBlueprint(json);
        }
    }
}
