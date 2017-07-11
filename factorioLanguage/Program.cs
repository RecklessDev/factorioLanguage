using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using factorioLanguage.BasicCircuitBlocks;

namespace factorioLanguage
{
    namespace BasicCircuitBlocks
    {

        class ConnectionPort
        {
            List<Signal> signals { get; set; }

            public List<Signal> Read()
            {
                return null;
            }

            public void Write(List<Signal> signals)
            {

            }
        }

        class CircuitBlock
        {
            ConnectionPort Input;
            ConnectionPort Output;

            void ProcessAll()
            {
                Input.Read().ForEach(new Action<Signal>(proc));
                Output.Write(res());
            }

            public delegate void Process(Signal s);
            public delegate List<Signal> Results();

            Process proc;
            Results res;

        }

        class ArithmeticCircuit
        {
            CircuitBlock block = new CircuitBlock();

            ArithmeticCircuit()
            {

            }


            string left = "signal-0";
            string right = "signal-1";

            string output = "signal-0";

            int result;

            void Process(Signal s)
            {
                if (s.Name == left)
                    result += s.Count;
                else if (s.Name == right)
                    result += s.Count;
            }

            List<Signal> Results()
            {
                return new List<Signal>
                {
                    new Signal
                    {
                        Name = output,
                        Count = result
                    }
                };
            }
        }


        class Node
        {
            public ConnectionPort Input;
            public ConnectionPort Output;


            public int id;
        }

        class Subgraph
        {
            public ConnectionPort Input;
            public ConnectionPort Output;

            List<Node> nodes;
        }

        //Graph


        enum Channel
        {
            Red = 1,
            Green = 2
        }

        //Something like "Signal-0" with count of 50 or "Iron Plate" with count of 3000
        class Signal
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        interface ICircuitEntity
        {

        }

        class CircuitConnection
        {
            ICircuitEntity first;
            ICircuitEntity second;
            Channel channel;
        }

        

        class ConstantCombinator : ICircuitEntity
        {
            public List<Signal> Signals { get; set; }
        }

        class ArithmeticCombinator : ICircuitEntity
        {
            public Signal First { get; set; }
            public Signal Second { get; set; }

            public string Operation { get; set; }
        }

    }

    class Program
    {

        static void CircuitTest()
        {
            new ConstantCombinator
            {
                Signals = new List<Signal>
                {
                     new Signal
                    {
                        Name = "Signal-0",
                        Count = 1
                    }
                }
            };

            new ArithmeticCombinator
            {
                First = new Signal
                {
                    Name = "Signal-0",
                    Count = 1
                },
                Second = new Signal
                {
                    Name = "Constant",
                    Count = 5
                },
                Operation = "+"
            };
        }

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
