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
        static void Main(string[] args)
        {
            var multidiag = "0eNqN0NsKgzAMBuB3yXW9sPMAfZUxhofgAhqlrTKRvvvaCrIxB7tMm/9LyAZ1P+OkiS2oDagZ2YC6bmCo46oPb3adEBSQxQEEcDWESlfUgxNA3OITVOpuApAtWcI9H4v1zvNQo/YNR7KZ9YJtEgEB02h8ZuQwyDuJ71sDJ6Aljc3+lTvxBcr/wFOvOPEuh2esp7qH/bWijGT2SZbhAPFE6u2iAhbUZp+ZZbLIyjyXqXMvykl7gA==";
            File.WriteAllText("rails.json", BlueprintProcessor.JsonPrettify(BlueprintProcessor.BlueprintToJson(multidiag)));

            //Rail Factory
            //File.WriteAllText("TestTurnSegment.txt", (Rails.RailFactory.TestTurnSegment()));
            File.WriteAllText("TestCurvedRails.txt", (Rails.RailFactory.TestCurvedRails())); 
            //File.WriteAllText("TestClockwiseGeneration.txt", (Rails.RailFactory.TestClockwiseShit()));
        }
    }
}
