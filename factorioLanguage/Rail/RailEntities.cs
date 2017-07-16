using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internal;

using System.Numerics;

namespace Internal
{
   
}

namespace Factorio.Rail
{

    public class RailDefinition
    {
        Dictionary<int, ConnectionType> incomingConnections = new Dictionary<int, ConnectionType>()
        {
            {45, ConnectionType.HalfUp },
            {135, ConnectionType.HalfUp },
            {225, ConnectionType.HalfDown },
            {315,  ConnectionType.HalfDown}
        };

        Dictionary<int, ConnectionType> outgoingConnections = new Dictionary<int, ConnectionType>()
        {
            {45, ConnectionType.HalfDown },
            {135, ConnectionType.HalfDown },
            {225, ConnectionType.HalfUp },
            {315,  ConnectionType.HalfUp}
        };

        public RailDefinition(int inputAngle, int outputAngle)
        {
            InputAngle = inputAngle;
            OutputAngle = outputAngle;

            InputConnection = incomingConnections.SingleOrDefault(x => x.Key == InputAngle).Value;
            OutputConnection = outgoingConnections.SingleOrDefault(x => x.Key == OutputAngle).Value;
        }

        public int InputAngle { get; private set; }
        public int OutputAngle { get; private set; }

        ConnectionType InputConnection { get; set; }
        ConnectionType OutputConnection { get; set; }
    }

    class Rail
    {
        

        public Vector2 Position { get; private set; }

        public RailDefinition Definition { get; private set; }

    }

    interface IRail
    {
        Vector2 Position { get; }

        //Vector2 Orientation { get; }
    }
     
    class StraightRail : IRail
    {
        public Vector2 Position { get; set; }
    }

    class DiagonalRailDown : IRail
    {
        public Vector2 Position { get; set; }
    }

    class DiagonalRailUp : IRail
    {
        public Vector2 Position { get; set; }
    }

    class CurvedRail : IRail
    {
        public Vector2 Position { get; set; }

        public CurvedRail()
        {
            Vector2 startPoint = new Vector2(0, 0);
            Connection startDirection;
            Connection endDirection;


        }
    }


    enum Side
    {
        Up,
        Right,
        Down,
        Left
    }

    enum ConnectionType
    {
        Full,
        HalfUp,
        HalfDown
    }

    class Connection
    {
        public Side Way { get; set; }

        public ConnectionType Type  { get; set; }
    }

    class Square
    {
        public Square(SquareSides connections)
        {
            ConnectionPoints = connections;
        }

        SquareSides ConnectionPoints { get; set; }

        public bool IsCompatible(Square other)
        {
            if (IsDiagonal(ConnectionPoints))
                return ((ConnectionPoints & other.ConnectionPoints) == 0); //both are diagonal and nothing matches
            else return ConnectionPoints == other.ConnectionPoints;
        }

        private bool IsDiagonal(SquareSides sides)
        {
            return !(sides == (SquareSides.Up | SquareSides.Down) || sides == (SquareSides.Left | SquareSides.Right));
        }
        
        public static SquareSides Reverse(SquareSides sides)
        {
            var j = (uint)sides;
            return (SquareSides)((j << 2) % 16 | j >> (4 - 2));
        }
    }

    enum SquareSides
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }

    class Testing
    {
        public static void Test()
        {
            new Square(SquareSides.Down | SquareSides.Right);
        }
    }
}
