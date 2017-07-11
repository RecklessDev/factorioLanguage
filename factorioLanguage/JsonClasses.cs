using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace factorioLanguage.JsonClasses
{
    class Signal
    {
        public string type { get; set; }

        public string name { get; set; }
    }

    class Icon
    {
        public Signal signal { get; set; }
        public int index { get; set; }
    }

    class Blueprint
    {
        public List<Icon> icons { get; set; }
        public List<Entity> entities { get; set; }
        public List<Tile> tiles { get; set; }

        public string item { get; set; }
        public string label { get; set; }
        public long version { get; set; }
    }

    class Position
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    class ArithmeticConditions
    {
        public Signal first_signal { get; set; }
        public Signal second_signal { get; set; }
        public int? constant { get; set; }
        public string operation { get; set; }
        public Signal output_signal { get; set; }
    }

    class DeciderConditions
    {
        public Signal first_signal { get; set; }
        public Signal second_signal { get; set; }
        public int? constant { get; set; }
        public string comparator { get; set; }
        public Signal output_signal { get; set; }
        public bool copy_count_from_input { get; set; }
    }

    class Filter
    {
        public Signal signal { get; set; }
        public int? count { get; set; }
        public int index { get; set; }
        public string name { get; set; }
    }

    class CircuitCondition
    {
        public Signal first_signal { get; set; }
        public int? constant { get; set; }
        public string comparator { get; set; }
    }

    class StackControlInputSignal
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    
    class ControlBehavior
    {
        public ArithmeticConditions arithmetic_conditions { get; set; }
        public DeciderConditions decider_conditions { get; set; }
        public bool? circuit_close_signal { get; set; }
        public bool? circuit_read_signal { get; set; }
        public CircuitCondition circuit_condition { get; set; }
        public int? circuit_mode_of_operation { get; set; }
        public bool? circuit_read_hand_contents { get; set; }
        public int? circuit_hand_read_mode { get; set; }
        public Signal train_stopped_signal { get; set; }
        public StackControlInputSignal stack_control_input_signal { get; set; }
        public List<Filter> filters { get; set; }
    }

    class ConnectionTarget
    {
        public int entity_id { get; set; }
        public int? circuit_id { get; set; }
    }

    class Connection
    {
        public List<ConnectionTarget> red { get; set; }
        public List<ConnectionTarget> green { get; set; }
    }

    class Connections
    {
        [JsonProperty("1")]
        Connection first { get; set; }
        [JsonProperty("2")]
        Connection second { get; set; }
    }

    class Color
    {
        public decimal r { get; set; }
        public decimal g { get; set; }
        public decimal b { get; set; }
        public decimal a { get; set; }
    }

    class Entity
    {
        public int entity_number { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public int? direction { get; set; }
        public string recipe { get; set; }
        public bool? auto_launch { get; set; }
        public Dictionary<string, int> items { get; set; }
        public string type { get; set; }
        public ControlBehavior control_behavior { get; set; }
        public List<Filter> filters { get; set; }
        public Connections connections { get; set; }
        public Color color { get; set; }
        public int? override_stack_size { get; set; }
        public int? bar { get; set; }
    }

    class Tile
    {
        public Position position { get; set; }
        public string name { get; set; }
    }

    class FactorioBlueprint
    {
        public Blueprint blueprint { get; set; }
    }
}
