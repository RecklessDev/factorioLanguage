using System;
using System.IO;

using Newtonsoft.Json;

namespace factorioLanguage
{
    class Program
    {

        static void Main(string[] args)
        {
            //Write the json for the original blueprint
            File.WriteAllText("original.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(BlueprintStrings.blueprint)));


            var fb = BlueprintProcessor.ReadBlueprint(BlueprintStrings.blueprint);
            string processedBS = BlueprintProcessor.MakeBlueprintString(fb);

            //Write the json for the processed blueprint
            File.WriteAllText("processed.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(processedBS)));
        }
    }
}
