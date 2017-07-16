using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using factorioLanguage.BasicCircuitBlocks;

namespace factorioLanguage
{
    namespace BasicCircuitBlocks
    {

        ////We keep the signal stringly based
        //class Signal
        //{
        //    public string Name { get; set; }
        //}

        ////Signals that can be on a wire at a moment
        //class WireSignals
        //{
        //    private Dictionary<Signal, int> signals = new Dictionary<Signal, int>();

        //    public void AddSignal(Signal s, int count)
        //    {
        //        int newCount = count;
        //        int value;
        //        if (signals.TryGetValue(s, out value))
        //            newCount += value;

        //        signals[s] = newCount;
        //    }

        //    int SignalCount(Signal s)
        //    {
        //        int value;
        //        if (signals.TryGetValue(s, out value))
        //            return value;
        //        return 0;
        //    }

        //    IReadOnlyDictionary<Signal, int> GetSignals()
        //    {
        //        return signals as IReadOnlyDictionary<Signal, int>;
        //    }
        //}

        //class ConnectionPort
        //{
        //    List<Signal> signals { get; set; }

        //    public List<Signal> Read()
        //    {
        //        return null;
        //    }

        //    public void Write(List<Signal> signals)
        //    {

        //    }
        //}

        //class CircuitBlock
        //{
        //    ConnectionPort Input;
        //    ConnectionPort Output;

        //    void ProcessAll()
        //    {
        //        Input.Read().ForEach(new Action<Signal>(proc));
        //        Output.Write(res());
        //    }

        //    public delegate void Process(Signal s);
        //    public delegate List<Signal> Results();

        //    Process proc;
        //    Results res;

        //}

        //class ArithmeticCircuit
        //{
        //    CircuitBlock block = new CircuitBlock();

        //    ArithmeticCircuit()
        //    {

        //    }


        //    string left = "signal-0";
        //    string right = "signal-1";

        //    string output = "signal-0";

        //    int result;

        //    void Process(Signal s)
        //    {
        //        if (s.Name == left)
        //            result += s.Count;
        //        else if (s.Name == right)
        //            result += s.Count;
        //    }

        //    List<Signal> Results()
        //    {
        //        return new List<Signal>
        //        {
        //            new Signal
        //            {
        //                Name = output,
        //                Count = result
        //            }
        //        };
        //    }
        //}


        //class Node
        //{
        //    public ConnectionPort Input;
        //    public ConnectionPort Output;


        //    public int id;
        //}

        //class Subgraph
        //{
        //    public ConnectionPort Input;
        //    public ConnectionPort Output;

        //    List<Node> nodes;
        //}

        ////Graph


        //enum Channel
        //{
        //    Red = 1,
        //    Green = 2
        //}

        //interface ICircuitEntity
        //{

        //}

        //class CircuitConnection
        //{
        //    ICircuitEntity first;
        //    ICircuitEntity second;
        //    Channel channel;
        //}

        

        //class ConstantCombinator : ICircuitEntity
        //{
        //    public List<Signal> Signals { get; set; }
        //}

        //class ArithmeticCombinator : ICircuitEntity
        //{
        //    public Signal First { get; set; }
        //    public Signal Second { get; set; }

        //    public string Operation { get; set; }
        //}

    }

    class Program
    {

        static void CircuitTest()
        {
            //new ConstantCombinator
            //{
            //    Signals = new List<Signal>
            //    {
            //         new Signal
            //        {
            //            Name = "Signal-0",
            //            Count = 1
            //        }
            //    }
            //};

            //new ArithmeticCombinator
            //{
            //    First = new Signal
            //    {
            //        Name = "Signal-0",
            //        Count = 1
            //    },
            //    Second = new Signal
            //    {
            //        Name = "Constant",
            //        Count = 5
            //    },
            //    Operation = "+"
            //};
        }



        static void Main(string[] args)
        {
            //Write the json for the original blueprint
            //File.WriteAllText("original.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(BlueprintStrings.blueprint)));


            //var fb = BlueprintProcessor.ReadBlueprint(BlueprintStrings.blueprint);

            //string processedBS = BlueprintProcessor.MakeBlueprintString(fb);
            ////Write the json for the processed blueprint
            //File.WriteAllText("processed.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(processedBS)));


            //var railBp = "0eNqN0e0KgyAUBuB7Ob8NylkDb2WM0cehHSgLtSjCe58ZxMYa6+fR930UzgJFM2CvSVmQC1DZKQPytoChWuXNembnHkECWWyBgcrbddI5NeAYkKpwApm4OwNUlizh1g/D/FBDW6D2gb1ZDnrEKgoAg74zvtOp9aEpxGaQUeLlijSW29XFsS+Q76Cx3qqf9gfJN5J/kvyAvJwlxWlSnCWj7ZvxfzI9S8bHol9UWKV82zyDEbUJgUwInolrmvLEuRcjMLNV";
            //var allDirBp = "0eNqV0uGKwyAMAOB3yW8d09UNfJUxRteFLdCmRd24Unz3s7aM663H3f0zmnxK4gCX+oGdIw5gB6CqZQ/2OICnG5f1uBf6DsECBWxAAJfNGLmSaogCiK/4AVbFkwDkQIFwqs9Bf+ZHc0GXEl6VPqTa2z3ITAjoWp+qWh6vSpLUAnqw22RfyWE1Hako3kj9V3K7JpoVcfc/Uf/+xmLRMFndS2I5t/Zd3ZjsyrRY0rsV2izpn1CpZlV9Rw/jzPJU7ZdPIOCJzueEfVHofXEwRqsYPwH67LXv";
            var multidiag = "0eNqV0ksKgzAQBuC7zDpdRBOtuUopxepQBjRKjKUiuXuj0lJtFroKecyXH2ZGuFc9toa0BTUCFY3uQF1G6Oih82o6s0OLoIAs1sBA5/W0MzlV4BiQLvEFirsrA9SWLOFSP2+Gm+7rOxr/4FtZ9OaJ5WkGGLRN52saPX3knZNkMPiFc2+XZLBYLhPH/shoHxmHxTggxjtDcr6Yck3yACn2kdkHDBDyWKp4HUoExORIqI0nA156qLnZGkwD4PlIazdeNI3iPKzqZ7YZPNF0yywJESUilTLizr0BnRT8aw==";
            File.WriteAllText("rails.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(multidiag)));


            //Rail Factory
            
            File.WriteAllText("oneway.txt", Rails.RailFactory.RailBlueprintToBlueprint(Rails.RailFactory.MultiWay(20)));
            File.WriteAllText("curvedRails.txt", Rails.RailFactory.TestCurvedRails());
        }
    }
}
