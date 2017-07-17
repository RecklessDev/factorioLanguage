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
    enum SquareSides
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }


    //A 2x2 square to be used by the rail system
    class Matcher
    {
        public Matcher(SquareSides connections)
        {
            //Position = position;
            ConnectionPoints = connections;
        }

        static Matcher()
        {
            HorizontalLeft = new Matcher(SquareSides.Left);
            HorizontalRight = new Matcher(SquareSides.Right);
            VerticalUp = new Matcher(SquareSides.Up);
            VerticalDown = new Matcher(SquareSides.Down);
            DiagonalUp = new Matcher(SquareSides.Left | SquareSides.Up);
            DiagonalDown = new Matcher(SquareSides.Right | SquareSides.Down);
            BackDiagonalUp = new Matcher(SquareSides.Right | SquareSides.Up);
            BackDiagonalDown = new Matcher(SquareSides.Left | SquareSides.Down);
        }

        public static Matcher HorizontalLeft { get; private set; }
        public static Matcher HorizontalRight { get; private set; }
        public static Matcher VerticalUp { get; private set; }
        public static Matcher VerticalDown { get; private set; }
        public static Matcher DiagonalUp { get; private set; }
        public static Matcher DiagonalDown { get; private set; }
        public static Matcher BackDiagonalUp { get; private set; }
        public static Matcher BackDiagonalDown { get; private set; }

        //public Vector2 Position { get; private set; }

        SquareSides ConnectionPoints { get; set; }

        public bool IsCompatible(Matcher other)
        {
            if (IsDiagonal(ConnectionPoints))
                return ((ConnectionPoints & other.ConnectionPoints) == 0); //both are diagonal and nothing matches
            else return ConnectionPoints == Reverse(other.ConnectionPoints);
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

    class Connection
    {
        public Connection(Vector2 position, Matcher port)
        {
            Port = port;
            Position = position;
        }

        public Matcher Port {get; private set; }
        public Vector2 Position { get; private set; }
    }

    enum AdvanceDirection
    {
        Continue,
        TurnClockwise,
        TurnCounterClockwise
    }

    

    class Rail
    {

        public Rail(Connection input, Connection output)
        {

        }

        public Rail Advance(Rail fromRail, AdvanceDirection direction)
        {


            return null;
        }

        static Rail()
        {
            new Rail(new Connection(Vector2.Zero, Matcher.HorizontalLeft), new Connection(new Vector2(2, 0), Matcher.HorizontalRight));
        }

        public Vector2 Center { get; private set; }

        public Connection Input { get; private set; }

        public Connection Output { get; private set; }
    }

 
    class Direction
    {
        Matcher Port { get; set; }
        AdvanceDirection Target { get; set; }
    }

    [Flags]
    enum RailFlags2
    {
        Horizontal = 1,
        Vertical = 2,
        DiagonalUp = 4,
        DiagonalDown = 8,
        BackDiagonalUp = 16,
        BackDiagonalDown = 32,
        Front = 64,
        Back = 128,
        Clockwise = 256,
        CounterClockwise = 512,

    }

    [Flags]
    enum RailFlags
    {
        Diagonal = 1,
        ClockwiseTurn = 1,
        Horizontal = 2,
        BackRail = 4,
        FirstHalfSquare = 4,
        IsCurvedRail = 16,
        //Up to here are the main flags - bellow are combinations to make things more readable
        DiagonalUp = FirstHalfSquare | Horizontal | Diagonal,
        DiagonalDown = SecondHalfSquare | Horizontal | Diagonal,
        BackDiagonalUp = SecondHalfSquare | Vertical | Diagonal,
        BackDiagonalDown = FirstHalfSquare | Vertical | Diagonal,
        Back = BackRail,
        Clockwise = ClockwiseTurn | IsCurvedRail,
        CounterClockwise = IsCurvedRail,
        //This is barely so the definition is readable - they don't do anything at all
        Vertical = 0,
        SecondHalfSquare = 0,
        Front = 0
    }

    struct RailPiece
    {
        
        static RailPiece()
        {
            
        }

        public static bool IsCurvedRail(RailFlags flags)
        {
            return (flags & RailFlags.IsCurvedRail) != 0;
        }

        public static int Direction(RailFlags flags)
        {
            //This would had been shorter in C++
            //return
            //    (((flags & RailFlags.Clockwise) != 0) ? 1 : 0) | 
            //    (((flags & RailFlags.Horizontal) != 0) ? 2 : 0) | 
            //    (((flags & RailFlags.Back) != 0) ? 4 : 0);

            return (int)flags & ~(1 << 4);
        }

    }


    class Testing
    {
        public static void Test()
        {
            var HorizontalRail = RailFlags.Horizontal;
            var HorizontalRailFrontClockwise = RailFlags.Horizontal | RailFlags.Clockwise | RailFlags.Front;
            var HorizontalRailFrontCounterClockwise = RailFlags.Horizontal | RailFlags.CounterClockwise | RailFlags.Front;
            var HorizontalRailBackClockwise = RailFlags.Horizontal | RailFlags.Clockwise | RailFlags.Back;
            var HorizontalRailBackCounterClockwise = RailFlags.Horizontal | RailFlags.CounterClockwise | RailFlags.Back;

            var VerticalRail = RailFlags.Vertical;
            var VerticalRailFrontClockwise = RailFlags.Vertical | RailFlags.Clockwise | RailFlags.Front;
            var VerticalRailFrontCounterClockwise = RailFlags.Vertical | RailFlags.CounterClockwise | RailFlags.Front;
            var VerticalRailBackClockwise = RailFlags.Vertical | RailFlags.Clockwise | RailFlags.Back;
            var VerticalRailBackCounterClockwise = RailFlags.Vertical | RailFlags.CounterClockwise | RailFlags.Back;

            var DiagonalUpRail = RailFlags.DiagonalUp;
            var DiagonalUpRailFrontClockwise = RailFlags.DiagonalUp | RailFlags.Clockwise | RailFlags.Front;
            var DiagonalUpRailBackCounterClockwise = RailFlags.DiagonalUp | RailFlags.CounterClockwise | RailFlags.Back;
            var DiagonalDownRail = RailFlags.DiagonalDown;
            var DiagonalDownRailFrontCounterClockwise = RailFlags.DiagonalDown | RailFlags.CounterClockwise | RailFlags.Front;
            var DiagonalDownRailBackClockwise = RailFlags.DiagonalDown | RailFlags.Clockwise | RailFlags.Back;

            var BackDiagonalUpRail = RailFlags.BackDiagonalUp;
            var BackDiagonalUpRailFrontCounterClockwise = RailFlags.BackDiagonalUp | RailFlags.CounterClockwise | RailFlags.Front;
            var BackDiagonalUpRailBackClockwise = RailFlags.BackDiagonalUp | RailFlags.Clockwise | RailFlags.Back;
            var BackDiagonalDownRail = RailFlags.BackDiagonalDown;
            var BackDiagonalDownRailFrontClockwise = RailFlags.BackDiagonalDown | RailFlags.Clockwise | RailFlags.Front;
            var BackDiagonalDownRailBackCounterClockwise = RailFlags.BackDiagonalDown | RailFlags.CounterClockwise | RailFlags.Back;

            Dictionary<RailFlags, RailFlags> mapRailToOutputType = new Dictionary<RailFlags, RailFlags>
            {
                { HorizontalRail, RailFlags.Horizontal },
                { HorizontalRailFrontClockwise, RailFlags.BackDiagonalUp },
                { HorizontalRailFrontCounterClockwise, RailFlags.DiagonalDown },
                { HorizontalRailBackClockwise, RailFlags.BackDiagonalDown },
                { HorizontalRailBackCounterClockwise, RailFlags.DiagonalUp },

                { VerticalRail, RailFlags.Vertical },
                { VerticalRailFrontClockwise, RailFlags.DiagonalUp },
                { VerticalRailFrontCounterClockwise, RailFlags.BackDiagonalUp },
                { VerticalRailBackClockwise, RailFlags.DiagonalDown },
                { VerticalRailBackCounterClockwise, RailFlags.BackDiagonalDown },

                { DiagonalUpRail, RailFlags.DiagonalDown },
                { DiagonalUpRailFrontClockwise, RailFlags.Horizontal },
                { DiagonalUpRailBackCounterClockwise, RailFlags.Vertical },
                { DiagonalDownRail, RailFlags.DiagonalUp },
                { DiagonalDownRailFrontCounterClockwise, RailFlags.Vertical },
                { DiagonalDownRailBackClockwise, RailFlags.Horizontal },

                { BackDiagonalUpRail, RailFlags.DiagonalDown },
                { BackDiagonalUpRailFrontCounterClockwise, RailFlags.Horizontal },
                { BackDiagonalUpRailBackClockwise, RailFlags.Vertical },
                { BackDiagonalDownRail, RailFlags.DiagonalUp },
                { BackDiagonalDownRailFrontClockwise, RailFlags.Vertical },
                { BackDiagonalDownRailBackCounterClockwise, RailFlags.Horizontal }
            };




        }
    }
}
