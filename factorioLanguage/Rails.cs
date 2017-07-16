using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using factorioLanguage.JsonClasses;

namespace factorioLanguage
{
    namespace Rails
    {

        enum Signaling
        {
            None, //The signaling is infered from the previous blocks
            SingleWay, //The signaling is just one way, with signals on right side
            DoubleWay //The signaling is both ways and it will have signals on both sides
        }

        enum SignalType
        {
            Single,
            Chain
        }

        class Node
        {
            
            public Vector2 Position;

            //public List<NodeConnection> to = new List<NodeConnection>();
            //public List<NodeConnection> from = new List<NodeConnection>();
        }

        class RailBlueprint
        {
            public List<Node> nodes = new List<Node>();
            public List<NodeConnection> connections = new List<NodeConnection>();
        }


        public static class Vector2Extension
        {

            public static Vector2 Rotate(this Vector2 v, float degrees)
            {
                var sin = Math.Sin(degrees * Math.PI * 180);
                var cos = Math.Cos(degrees * Math.PI * 180);

                float tx = v.X;
                float ty = v.Y;
                v.X = (float)((cos * tx) - (sin * ty));
                v.Y = (float)((sin * tx) + (cos * ty));
                return v;
            }
        }

        //Each direction represents a 45 degrees of a turn
        //  This enum maps them with hours as so:
        //      It starts from the horizontal position going 45 degrees down-left
        //          Each rail afterwards represents the next curve on the circle
        //
        enum CurvedRailDirection
        {
            From0To45Degrees = 3,
            From45To90Degrees = 0,
            From90to135Degrees = 5,
            From135To180Degrees = 2,
            From180To225Degrees = 7,
            From225To270Degrees = 4,
            From270To315Degrees = 1,
            From315To360Degrees = 6
        }

        interface IRailSegment
        {
            Vector2 FromPosition { get; }

            Vector2 EndPosition { get; }

            List<JsonClasses.Entity> GetRailEntities();
        }

        //A segment that turns a rail from its initial angle to a desired target angle
        class TurnRailSegment : IRailSegment
        {
            private static Dictionary<int, CurvedRailDirection> angleToDirection = new Dictionary<int, CurvedRailDirection>()
            {
                { 0  , CurvedRailDirection.From0To45Degrees   },
                { 45 , CurvedRailDirection.From45To90Degrees  },
                { 90 , CurvedRailDirection.From90to135Degrees },
                { 135, CurvedRailDirection.From135To180Degrees },
                { 180, CurvedRailDirection.From180To225Degrees },
                { 225, CurvedRailDirection.From225To270Degrees },
                { 270, CurvedRailDirection.From270To315Degrees },
                { 315, CurvedRailDirection.From315To360Degrees }
            };


            public TurnRailSegment(Vector2 startPosition, int startAngle, int endAngle)
            {
                for (int i = startAngle + 45; i <= endAngle; i+=45)
                {
                    //new CurvedRailSegment(startPosition, )
                }
            }

            public Vector2 EndPosition
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public Vector2 FromPosition
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public List<Entity> GetRailEntities()
            {
                throw new NotImplementedException();
            }
        }

        enum TurnDirection
        {
            Clockwise,
            Counterclockwise
        }

        class CurvedRailSegment : IRailSegment
        {
            private static Dictionary<CurvedRailDirection, int> degreesFromZero = new Dictionary<CurvedRailDirection, int>()
            {
                {CurvedRailDirection.From0To45Degrees, 0 },
                {CurvedRailDirection.From45To90Degrees, 45 },
                {CurvedRailDirection.From90to135Degrees, 90 },
                {CurvedRailDirection.From135To180Degrees, 135 },
                {CurvedRailDirection.From180To225Degrees, 180 },
                {CurvedRailDirection.From225To270Degrees, 225 },
                {CurvedRailDirection.From270To315Degrees, 270 },
                {CurvedRailDirection.From315To360Degrees, 315 }
            };

            //Hardcoded values extracted from the game JSON. There is a pattern here but initially I didn't figured it out so since there are just 8 directions
            //  decided to hardcode everything. First Vector2 represents the offset of the center position for the curved rail and second Vector2 is where the segment ends.
            private static Dictionary<CurvedRailDirection, Tuple<Vector2, Vector2>> offsets = new Dictionary<CurvedRailDirection, Tuple<Vector2, Vector2>>
            {
                {CurvedRailDirection.From0To45Degrees,    Tuple.Create(new Vector2(5, 1), new Vector2(8, 4)) },
                {CurvedRailDirection.From45To90Degrees,   Tuple.Create(new Vector2(3, 3), new Vector2(4, 8)) },
                {CurvedRailDirection.From90to135Degrees,  Tuple.Create(new Vector2(-1, 5), new Vector2(-4, 8)) },
                {CurvedRailDirection.From135To180Degrees, Tuple.Create(new Vector2(-3, 3), new Vector2(-8, 4)) },
                {CurvedRailDirection.From180To225Degrees, Tuple.Create(new Vector2(-5, -1), new Vector2(-8, -4)) },
                {CurvedRailDirection.From225To270Degrees, Tuple.Create(new Vector2(-3, -3), new Vector2(-4, -8)) },
                {CurvedRailDirection.From270To315Degrees, Tuple.Create(new Vector2(1, -5), new Vector2(4, -8)) },
                {CurvedRailDirection.From315To360Degrees, Tuple.Create(new Vector2(3, -3), new Vector2(4, -8)) }
            };

            private static Dictionary<int, Tuple<Vector2, Vector2>> angleTurnOffsets = new Dictionary<int, Tuple<Vector2, Vector2>>
            {
                {0,    Tuple.Create(new Vector2(5, 1), new Vector2(8, 4)) },
                {45,   Tuple.Create(new Vector2(3, 3), new Vector2(4, 8)) },
                {90,  Tuple.Create(new Vector2(-1, 5), new Vector2(-4, 8)) },
                {135, Tuple.Create(new Vector2(-3, 3), new Vector2(-8, 4)) },
                {180, Tuple.Create(new Vector2(-5, -1), new Vector2(-8, -4)) },
                {225, Tuple.Create(new Vector2(-3, -3), new Vector2(-4, -8)) },
                {270, Tuple.Create(new Vector2(1, -5), new Vector2(4, -8)) },
                {315, Tuple.Create(new Vector2(3, -3), new Vector2(4, -8)) }
            };

            private static Dictionary<int, CurvedRailDirection> angleToDirection = new Dictionary<int, CurvedRailDirection>()
            {
                { 0  , CurvedRailDirection.From0To45Degrees   },
                { 45 , CurvedRailDirection.From45To90Degrees  },
                { 90 , CurvedRailDirection.From90to135Degrees },
                { 135, CurvedRailDirection.From135To180Degrees },
                { 180, CurvedRailDirection.From180To225Degrees },
                { 225, CurvedRailDirection.From225To270Degrees },
                { 270, CurvedRailDirection.From270To315Degrees },
                { 315, CurvedRailDirection.From315To360Degrees }
            };

            Tuple<Vector2, Vector2> GetOffset(int fromAngle, TurnDirection direction)
            {
                if (direction == TurnDirection.Clockwise)
                    return angleTurnOffsets[fromAngle];
                else
                {
                    var newAngle = (fromAngle - 45) % 360;
                    if (newAngle < 0) newAngle += 360;
                    
                    var offsets = angleTurnOffsets[newAngle];
                    return Tuple.Create(Vector2.Multiply(offsets.Item1, -1), Vector2.Multiply(offsets.Item2, -1));
                }
            }

            public CurvedRailSegment(Vector2 startPos, int angle) : this(startPos, degreesFromZero.Single(x => x.Value == angle).Key)
            {
            }

            public CurvedRailSegment(Vector2 startPos, CurvedRailDirection direction)
            {
                var degrees = degreesFromZero[direction];

                var offsetTuple = MinimizeRailPosition(offsets[direction], degrees);

                FromPosition = startPos;
                Center = startPos + offsetTuple.Item1;
                EndPosition = startPos + offsetTuple.Item2;

                Direction = direction;
            }

            public CurvedRailSegment(Vector2 startPos, int startAngle, TurnDirection direction)
            {
                Tuple<Vector2, Vector2> offsetTuple;

                {
                    if (direction == TurnDirection.Clockwise)
                    {
                        Direction = angleToDirection[startAngle];
                        offsetTuple = angleTurnOffsets[startAngle];
                    }
                    else
                    {
                        var newAngle = (startAngle - 45) % 360;
                        if (newAngle < 0) newAngle += 360;

                        var offsets = angleTurnOffsets[newAngle];
                        offsetTuple = Tuple.Create(Vector2.Multiply(offsets.Item1, -1), Vector2.Multiply(offsets.Item2, -1));

                        Direction = angleToDirection[newAngle];
                    }
                }

                FromPosition = startPos;
                Center = startPos + offsetTuple.Item1;
                EndPosition = startPos + offsetTuple.Item2;
            }

            //Rails that end on horizontal or vertical can be one segment shorter so this function fixes that; it should be added in the hardcoded values
            private Tuple<Vector2, Vector2> MinimizeRailPosition(Tuple<Vector2, Vector2> tuple, int angle)
            {
                if (angle == 45)
                    return Tuple.Create(tuple.Item1, new Vector2(tuple.Item2.X, tuple.Item2.Y - 2));
                if (angle == 135)
                    return Tuple.Create(tuple.Item1, new Vector2(tuple.Item2.X + 2, tuple.Item2.Y));
                if (angle == 225)
                    return Tuple.Create(tuple.Item1, new Vector2(tuple.Item2.X, tuple.Item2.Y + 2));
                if (angle == 315)
                    return Tuple.Create(tuple.Item1, new Vector2(tuple.Item2.X - 2, tuple.Item2.Y));
                return tuple;
            }

            public void TranslateSegment(Vector2 offset)
            {
                FromPosition += offset;
                Center += offset;
                EndPosition += offset;
            }

            public JsonClasses.Entity MakeEntity()
            {
                var rail = new JsonClasses.Entity();
                rail.direction = (int)Direction;
                rail.position = Center;
                rail.entity_number = EntityIds.GenerateId();
                rail.name = "curved-rail";
                return rail;
            }

            public List<Entity> GetRailEntities()
            {
                return new List<Entity> { MakeEntity() };
            }

            public Vector2 FromPosition { get; private set; }

            public Vector2 EndPosition { get; private set; }

            public Vector2 Center { get; private set; }

            public CurvedRailDirection Direction { get; private set; }
        }

        class EntityIds
        {
            private static int FreeId = 0;

            public static int GenerateId()
            {
                return FreeId++;
            }

            public static void Reset()
            {
                FreeId = 0;
            }
        }

        class StraightRailSegment : IRailSegment
        {
            //Maps the increment value to the direction enumeration
            private static Dictionary<Vector2, ConnectionDirection> DIRECTIONS = new Dictionary<Vector2, ConnectionDirection>
            {
                { new Vector2(0, 2),  ConnectionDirection.Vertical },
                { new Vector2(0, -2),  ConnectionDirection.Vertical },
                { new Vector2(2, 0),  ConnectionDirection.Horizontal },
                { new Vector2(-2, 0),  ConnectionDirection.Horizontal },
                { new Vector2(-2, 2),  ConnectionDirection.Diagonal },
                { new Vector2(2, -2),  ConnectionDirection.Diagonal },
                { new Vector2(2, 2),  ConnectionDirection.BackDiagonal },
                { new Vector2(-2, -2),  ConnectionDirection.BackDiagonal }
            };


            public StraightRailSegment(Vector2 fromPosition, Vector2 toPosition)
            {
                FromPosition = fromPosition;
                EndPosition = toPosition;

                var fromZero = Vector2.Subtract(FromPosition, EndPosition);
                var posIncrementStep = new Vector2(Math.Sign(fromZero.X) * 2, Math.Sign(fromZero.Y) * 2);
                direction = DIRECTIONS[posIncrementStep];

                int numRails = (int)(Vector2.Distance(FromPosition, EndPosition) / posIncrementStep.Length());

                railPositions = new List<Vector2>(numRails);
                for (int idx = 0; idx < numRails; idx++)
                {
                    railPositions.Add(FromPosition + posIncrementStep * idx);
                }
            }

            private static List<JsonClasses.Entity> MakeRail(Vector2 at, ConnectionDirection direction)
            {
                if (direction == ConnectionDirection.Horizontal || direction == ConnectionDirection.Vertical)
                {
                    var rail = new JsonClasses.Entity();
                    rail.direction = (int)direction;
                    rail.position = at;
                    rail.entity_number = EntityIds.GenerateId();
                    rail.name = "straight-rail";
                    return new List<JsonClasses.Entity> { rail };
                }
                //Diagonals have to generate two rails
                else if (direction == ConnectionDirection.Diagonal)
                {
                    var railDown = new JsonClasses.Entity();
                    railDown.direction = (int)RailDirection.FrontDiagonalDown;
                    railDown.position = at;
                    railDown.entity_number = EntityIds.GenerateId();
                    railDown.name = "straight-rail";


                    var railUp = new JsonClasses.Entity();
                    railUp.direction = (int)RailDirection.FrontDiagonalUp;
                    railUp.position = at + new Vector2(0, 2);
                    railUp.entity_number = EntityIds.GenerateId();
                    railUp.name = "straight-rail";
                    return new List<JsonClasses.Entity> { railDown, railUp };
                }
                else
                {
                    var railUp = new JsonClasses.Entity();
                    railUp.direction = (int)RailDirection.BackDiagonalUp;
                    railUp.position = at;
                    railUp.entity_number = EntityIds.GenerateId();
                    railUp.name = "straight-rail";


                    var railDown = new JsonClasses.Entity();
                    railDown.direction = (int)RailDirection.BackDiagonalDown;
                    railDown.position = at + new Vector2(0, -2);
                    railDown.entity_number = EntityIds.GenerateId();
                    railDown.name = "straight-rail";
                    return new List<JsonClasses.Entity> { railUp, railDown };
                }
            }

            private ConnectionDirection direction { get; set; }

            private List<Vector2> railPositions { get; set; }

            public List<Entity> GetRailEntities()
            {
                var entities = new List<JsonClasses.Entity>();
                railPositions.ForEach(x => entities.AddRange(MakeRail(x, direction)));
                return entities;
            }

            public Vector2 FromPosition { get; private set; }

            public Vector2 EndPosition { get; private set; }
        }

        class NodeConnection
        {
            //Maps the increment value to the direction enumeration
            private static Dictionary<Vector2, ConnectionDirection> DIRECTIONS = new Dictionary<Vector2, ConnectionDirection>
            {
                { new Vector2(0, 2),  ConnectionDirection.Vertical },
                { new Vector2(0, -2),  ConnectionDirection.Vertical },
                { new Vector2(2, 0),  ConnectionDirection.Horizontal },
                { new Vector2(-2, 0),  ConnectionDirection.Horizontal },
                { new Vector2(-2, 2),  ConnectionDirection.Diagonal },
                { new Vector2(2, -2),  ConnectionDirection.Diagonal },
                { new Vector2(2, 2),  ConnectionDirection.BackDiagonal },
                { new Vector2(-2, -2),  ConnectionDirection.BackDiagonal }
            };

            public NodeConnection(Node source, Node target)
            {
                Source = source;
                Target = target;

                var fromZero = Vector2.Subtract(Target.Position, Source.Position);
                var posIncrementStep = new Vector2(Math.Sign(fromZero.X) * 2, Math.Sign(fromZero.Y) * 2);
                Direction = DIRECTIONS[posIncrementStep];

                int numRails = (int)(Vector2.Distance(Source.Position, target.Position) / posIncrementStep.Length());

                RailPositions = new List<Vector2>(numRails);
                for (int idx = 0; idx < numRails; idx++)
                {
                    RailPositions.Add(FromPosition + posIncrementStep * idx);
                }
            }

            
            public void Segments(Vector2 from, int incomingAngle, Vector2 to, int outgoingAngle)
            {
                List<IRailSegment> segments = new List<IRailSegment>();

                bool isDiagonal = (Math.Abs(from.X - to.X) == Math.Abs(from.Y - to.Y));
                bool isHorizontal = (from.X == to.X);
                bool isVertical = (from.Y == to.Y);
                if (isDiagonal || isHorizontal || isVertical)
                {
                    segments.Add(new StraightRailSegment(from, to));
                }

                //var curved = new CurvedRailSegment(from, )
                //var dot = Vector2.Dot(Vector2.Normalize(new Vector2(from.X + 1, from.Y)), Vector2.Normalize(to));
                //var angle = Math.Acos(dot);
                //Math.Cos(angle)
            }

            public Node Source { get; private set; }
            public Node Target { get; private set; }
            public Signaling Way { get; set; }
            public ConnectionDirection Direction { get; private set; }
            public List<Vector2> RailPositions { get; private set; }

            public Vector2 FromPosition
            {
                get
                {
                    return Source.Position;
                }
            }

            public Vector2 ToPosition
            {
                get
                {
                    return Target.Position;
                }
            }
        }

        enum RailDirection
        {
            Vertical = 0,
            BackDiagonalUp = 1,
            Horizontal = 2,
            FrontDiagonalDown = 3,
            BackDiagonalDown = 5,
            FrontDiagonalUp = 7
        }

        enum ConnectionDirection
        {
            Vertical = 0, // will be: -
            Horizontal = 2, // will be: |
            Diagonal = 4, // will be: /
            BackDiagonal = 8 // will be \
        }


        class RailFactory
        {

            private static List<JsonClasses.Entity> MakeRail(Vector2 at, ConnectionDirection direction)
            {
                if (direction == ConnectionDirection.Horizontal || direction == ConnectionDirection.Vertical)
                {
                    var rail = new JsonClasses.Entity();
                    rail.direction = (int)direction;
                    rail.position = at;
                    rail.entity_number = EntityIds.GenerateId();
                    rail.name = "straight-rail";
                    return new List<JsonClasses.Entity> { rail };
                }
                //Diagonals have to generate two rails
                else if (direction == ConnectionDirection.Diagonal)
                {
                    var railDown = new JsonClasses.Entity();
                    railDown.direction = (int)RailDirection.FrontDiagonalDown;
                    railDown.position = at;
                    railDown.entity_number = EntityIds.GenerateId();
                    railDown.name = "straight-rail";


                    var railUp = new JsonClasses.Entity();
                    railUp.direction = (int)RailDirection.FrontDiagonalUp;
                    railUp.position = at + new Vector2(0, 2);
                    railUp.entity_number = EntityIds.GenerateId();
                    railUp.name = "straight-rail";
                    return new List<JsonClasses.Entity> { railDown, railUp };
                }
                else
                {
                    var railUp = new JsonClasses.Entity();
                    railUp.direction = (int)RailDirection.BackDiagonalUp;
                    railUp.position = at;
                    railUp.entity_number = EntityIds.GenerateId();
                    railUp.name = "straight-rail";


                    var railDown = new JsonClasses.Entity();
                    railDown.direction = (int)RailDirection.BackDiagonalDown;
                    railDown.position = at + new Vector2(0, -2);
                    railDown.entity_number = EntityIds.GenerateId();
                    railDown.name = "straight-rail";
                    return new List<JsonClasses.Entity> { railUp, railDown };
                }
            }

            private static JsonClasses.Entity MakeSignal(Vector2 at, SignalType type, RailDirection direction)
            {
                var signal = new JsonClasses.Entity();
                signal.direction = (int)direction;
                signal.position = at;
                signal.entity_number = EntityIds.GenerateId();
                signal.name = type == SignalType.Single ? "rail-signal" : "chain-signal";

                return signal;
            }

            

            public static List<JsonClasses.Entity> MakeRailEntities(NodeConnection connection)
            {
                var entities = new List<JsonClasses.Entity>();
                connection.RailPositions.ForEach(x => entities.AddRange(MakeRail(x, connection.Direction)));
                return entities;
            }

            public static string RailBlueprintToBlueprint(RailBlueprint rb)
            {
                List<JsonClasses.Entity> entities = new List<JsonClasses.Entity>();

                foreach (var connection in rb.connections)
                {
                    entities.AddRange(MakeRailEntities(connection));

                }

                JsonClasses.FactorioBlueprint fb = new JsonClasses.FactorioBlueprint();
                fb.blueprint = new JsonClasses.Blueprint();
                fb.blueprint.label = "one way rail";
                fb.blueprint.version = 64426475521;
                fb.blueprint.item = "blueprint";
                fb.blueprint.icons = new List<JsonClasses.Icon> {
                    new JsonClasses.Icon
                    {
                        signal = new JsonClasses.Signal
                        {
                            type = "item",
                            name = "rail"
                        },
                        index = 1
                    }
                };

                fb.blueprint.entities = entities;

                return BlueprintProcessor.MakeBlueprintString(fb);
            }

            public static RailBlueprint OneWay(uint blockTiles)
            {
                var rb = new RailBlueprint();
                float length = (float)Math.Ceiling((decimal)blockTiles / 2) * 4;


                var start = new Node();
                start.Position = new Vector2 { X = 0, Y = 0 };
                
                var end = new Node();
                end.Position = new Vector2 { X = length, Y = 0 };

                var connection = new NodeConnection(start, end);
                connection.Way = Signaling.SingleWay;

                rb.nodes.Add(start);
                rb.nodes.Add(end);
                rb.connections.Add(connection);

                return rb;
            }


            public static RailBlueprint MultiWay(uint blockTiles)
            {
                var directions = new List<Vector2> {
                    new Vector2(1, 0),
                    new Vector2(-1, 0),
                    new Vector2(0, 1),
                    new Vector2(0, -1),
                    new Vector2(1, 1),
                    new Vector2(-1, -1),
                    new Vector2(-1, 1),
                    new Vector2(1, -1),
                };

                var rb = new RailBlueprint();
                float length = (float)Math.Ceiling((decimal)blockTiles / 2) * 4;


                var start = new Node();
                start.Position = new Vector2 { X = 0, Y = 0 };

                foreach (var dir in directions)
                {
                    var end = new Node() { Position = new Vector2 { X = dir.X * length, Y = dir.Y * length } };

                    var connection = new NodeConnection(start, end);
                    connection.Way = Signaling.SingleWay;

                    rb.nodes.Add(start);
                    rb.nodes.Add(end);
                    rb.connections.Add(connection);
                }



                return rb;
            }





            public static string TestCurvedRails()
            {
                List<JsonClasses.Entity> entities = new List<JsonClasses.Entity>();
                {
                    List<int> angles = new List<int>
                    {

                    };

                    Vector2 startPos = new Vector2(0, 0);
                    for (int i = 360; i > 0; i -= 45)
                    {
                        var rail = new CurvedRailSegment(startPos, i, TurnDirection.Counterclockwise);                        
                        entities.Add(rail.MakeEntity());
                        startPos = rail.EndPosition;
                    }
                }

                JsonClasses.FactorioBlueprint fb = new JsonClasses.FactorioBlueprint();
                fb.blueprint = new JsonClasses.Blueprint();
                fb.blueprint.label = "one way rail";
                fb.blueprint.version = 64426475521;
                fb.blueprint.item = "blueprint";
                fb.blueprint.icons = new List<JsonClasses.Icon> {
                    new JsonClasses.Icon
                    {
                        signal = new JsonClasses.Signal
                        {
                            type = "item",
                            name = "rail"
                        },
                        index = 1
                    }
                };

                fb.blueprint.entities = entities;

                return BlueprintProcessor.MakeBlueprintString(fb);
            }










        }

    }
}
