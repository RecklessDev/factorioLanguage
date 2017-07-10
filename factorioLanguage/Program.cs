using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using System.Text;



namespace factorioLanguage
{
    class Program
    {

        private static string BlueprintToJson(string blueprintString)
        {
            byte[] data = Convert.FromBase64String(blueprintString);
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

        static void Main(string[] args)
        {

            string blueprint = "eNrFWVFvmzAQ/iuTH1eyYeymCZr2GybtpV0VIQKXxBIY5JhqWcV/nw1pSkNDOSdt+1CJ4Dvuvu/uuyN5JMusglIJqUn4SERSyC0J7x/JVqxlnNnP9K4EEhKhIScekXFur2Il9CYHLZJJUuRLIWNdKFJ7RMgU/pKQ1t6bPlJIRArqdQdBvfAISC20gDai5mIXySpfgjJPeCMWj5TF1lgX0gZgPXpkR8IJ+3ZtnpIKBUl7k3vEZK1VkUVL2MQPwhgbi2evkbmdNp629sZKqK2Oerk9CKUr88khrPbEJLBJWVh1bDH2PVKUoOL22eTKnC8qXVYIj3ekrlufss2hCSuw/xSkXaxE2qSdCJVUQjeXtF5Y6x6cARZO2sLpfzCcdxbOLVgf443+WKMu7F8vBjs9ATt9CXvQgdxeM0ODd5Kz+TjO2CHEp/oa0wC0fp2hlcg0qBPdP4hsUlS2tINO9y/GI9Ur0JdITXtYeGStAOSxn9k40PiQ/vQwY7gq37s8r8Rve4phIixj1UQYkp8Otbt3We6ihqtopYo8EtL4IKFWFSAKe35c2EOFPBvmlvrjOLtGcWYlvqHNf8nZ9B05o5fnjF6Os5sTuE6xom8boYGWjoL2UqJ/exHR/+5GAgJndlTfM0yvnCLp5hBTDqmo8glkJhRlmCqLDPoc+YfaHxn1dHhUXY/r0ZnbAkE/eH+4TClduWkwopR4r3ROjL3hTWI6jrw5SmCDDnPvOwWxRN21mtlR3h+fOy2D4d46HoDDCsHHkWmd4lqRfV4rPs9M+vkthl3JTU9qke3fT49HJT/o8D7ITfwvVqkhQiYKNEyUWG80sQT2Fpi3TDNYvW653/EDl6d23g8cbX130zOeekayzMWUnoEx7WH8ZDRw3EedxvkOUKdd8PLd4fJRaPkYsHxM9r5z8mfUCq5UUJWCot6d+b2gcHdThpfAMxQwQCEeYBAPMIi7axNzT56hkmeY5BlGlxgGKeaMFHdHirvPSfedgDtPSe48JPlIdM0a1HyzH3Z+TPBIFi/BLG3kt25Wui+/hFyD+nL/9Ga9MGceQG3bLxA4D6aMm795Xf8HXkdMtw==";

            var output = BlueprintToJson(blueprint);
            //Console.WriteLine(output);

            var processedBlueprint = JsonToBlueprint(output);
            Console.WriteLine(processedBlueprint);
        }
    }
}
