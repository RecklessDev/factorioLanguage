using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;


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

    class Program
    {

        private static string BlueprintToJson(string blueprintString)
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

        private static string JsonToBlueprint(string json)
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
            return JsonConvert.DeserializeObject<FactorioBlueprint>(json, new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal});
        }

        public static string MakeBlueprintString(FactorioBlueprint blueprint)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new DecimalJsonConverter());
            var json = JsonConvert.SerializeObject(blueprint, Formatting.None, settings);
            return JsonToBlueprint(json);
        }


        static void Main(string[] args)
        {
            string blueprint_simple = "0eNrFWVFvmzAQ/iuTH1eyYeymCZr2GybtpV0VIQKXxBIY5JhqWcV/nw1pSkNDOSdt+1CJ4Dvuvu/uuyN5JMusglIJqUn4SERSyC0J7x/JVqxlnNnP9K4EEhKhIScekXFur2Il9CYHLZJJUuRLIWNdKFJ7RMgU/pKQ1t6bPlJIRArqdQdBvfAISC20gDai5mIXySpfgjJPeCMWj5TF1lgX0gZgPXpkR8IJ+3ZtnpIKBUl7k3vEZK1VkUVL2MQPwhgbi2evkbmdNp629sZKqK2Oerk9CKUr88khrPbEJLBJWVh1bDH2PVKUoOL22eTKnC8qXVYIj3ekrlufss2hCSuw/xSkXaxE2qSdCJVUQjeXtF5Y6x6cARZO2sLpfzCcdxbOLVgf443+WKMu7F8vBjs9ATt9CXvQgdxeM0ODd5Kz+TjO2CHEp/oa0wC0fp2hlcg0qBPdP4hsUlS2tINO9y/GI9Ur0JdITXtYeGStAOSxn9k40PiQ/vQwY7gq37s8r8Rve4phIixj1UQYkp8Otbt3We6ihqtopYo8EtL4IKFWFSAKe35c2EOFPBvmlvrjOLtGcWYlvqHNf8nZ9B05o5fnjF6Os5sTuE6xom8boYGWjoL2UqJ/exHR/+5GAgJndlTfM0yvnCLp5hBTDqmo8glkJhRlmCqLDPoc+YfaHxn1dHhUXY/r0ZnbAkE/eH+4TClduWkwopR4r3ROjL3hTWI6jrw5SmCDDnPvOwWxRN21mtlR3h+fOy2D4d46HoDDCsHHkWmd4lqRfV4rPs9M+vkthl3JTU9qke3fT49HJT/o8D7ITfwvVqkhQiYKNEyUWG80sQT2Fpi3TDNYvW653/EDl6d23g8cbX130zOeekayzMWUnoEx7WH8ZDRw3EedxvkOUKdd8PLd4fJRaPkYsHxM9r5z8mfUCq5UUJWCot6d+b2gcHdThpfAMxQwQCEeYBAPMIi7axNzT56hkmeY5BlGlxgGKeaMFHdHirvPSfedgDtPSe48JPlIdM0a1HyzH3Z+TPBIFi/BLG3kt25Wui+/hFyD+nL/9Ga9MGceQG3bLxA4D6aMm795Xf8HXkdMtw==";


            //Write the json for the original blueprint
            File.WriteAllText("original.json", JsonPrettify(BlueprintToJson(BlueprintStrings.blueprint)));


            var fb = ReadBlueprint(BlueprintStrings.blueprint);
            string processedBS = MakeBlueprintString(fb);

            //Write the json for the processed blueprint
            File.WriteAllText("processed.json", JsonPrettify(BlueprintToJson(processedBS)));
        }

        static void x2Main(string[] args)
        {
            string blueprint = "0eNrFWVFvmzAQ/iuTH1eyYeymCZr2GybtpV0VIQKXxBIY5JhqWcV/nw1pSkNDOSdt+1CJ4Dvuvu/uuyN5JMusglIJqUn4SERSyC0J7x/JVqxlnNnP9K4EEhKhIScekXFur2Il9CYHLZJJUuRLIWNdKFJ7RMgU/pKQ1t6bPlJIRArqdQdBvfAISC20gDai5mIXySpfgjJPeCMWj5TF1lgX0gZgPXpkR8IJ+3ZtnpIKBUl7k3vEZK1VkUVL2MQPwhgbi2evkbmdNp629sZKqK2Oerk9CKUr88khrPbEJLBJWVh1bDH2PVKUoOL22eTKnC8qXVYIj3ekrlufss2hCSuw/xSkXaxE2qSdCJVUQjeXtF5Y6x6cARZO2sLpfzCcdxbOLVgf443+WKMu7F8vBjs9ATt9CXvQgdxeM0ODd5Kz+TjO2CHEp/oa0wC0fp2hlcg0qBPdP4hsUlS2tINO9y/GI9Ur0JdITXtYeGStAOSxn9k40PiQ/vQwY7gq37s8r8Rve4phIixj1UQYkp8Otbt3We6ihqtopYo8EtL4IKFWFSAKe35c2EOFPBvmlvrjOLtGcWYlvqHNf8nZ9B05o5fnjF6Os5sTuE6xom8boYGWjoL2UqJ/exHR/+5GAgJndlTfM0yvnCLp5hBTDqmo8glkJhRlmCqLDPoc+YfaHxn1dHhUXY/r0ZnbAkE/eH+4TClduWkwopR4r3ROjL3hTWI6jrw5SmCDDnPvOwWxRN21mtlR3h+fOy2D4d46HoDDCsHHkWmd4lqRfV4rPs9M+vkthl3JTU9qke3fT49HJT/o8D7ITfwvVqkhQiYKNEyUWG80sQT2Fpi3TDNYvW653/EDl6d23g8cbX130zOeekayzMWUnoEx7WH8ZDRw3EedxvkOUKdd8PLd4fJRaPkYsHxM9r5z8mfUCq5UUJWCot6d+b2gcHdThpfAMxQwQCEeYBAPMIi7axNzT56hkmeY5BlGlxgGKeaMFHdHirvPSfedgDtPSe48JPlIdM0a1HyzH3Z+TPBIFi/BLG3kt25Wui+/hFyD+nL/9Ga9MGceQG3bLxA4D6aMm795Xf8HXkdMtw==";

            var output = BlueprintToJson(blueprint);
            //Console.WriteLine(output);
            File.WriteAllText("original.json", JsonPrettify(output));

            var processedBlueprint = JsonToBlueprint(output);
            //Console.WriteLine(processedBlueprint);


            //Json Test Code

            
            var traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            traceWriter.LevelFilter = System.Diagnostics.TraceLevel.Warning | System.Diagnostics.TraceLevel.Error | System.Diagnostics.TraceLevel.Verbose;
            var data = JsonConvert.DeserializeObject<FactorioBlueprint>(output, new JsonSerializerSettings { TraceWriter = traceWriter });

            //Console.WriteLine(traceWriter.ToString());
            File.WriteAllText("tracer.txt", traceWriter.ToString());

            //var fb = new FactorioBlueprint();
            //fb.blueprint.icons.Add(new Icon
            //{
            //    signal = new Signal
            //    {
            //        type = "item",
            //        name = "arithmetic-combinator"
            //    },
            //    index = 1
            //});

            //fb.blueprint.icons.Add(new Icon
            //{
            //    signal = new Signal
            //    {
            //        type = "item",
            //        name = "decider-combinator"
            //    },
            //    index = 2
            //});



            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new DecimalJsonConverter());
            File.WriteAllText("processed.json", JsonConvert.SerializeObject(data, Formatting.Indented, settings));

        }
    }
}
