using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipo4
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GamerOver { get; private set; }

        private readonly LinkedList<Position> snakePosition = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Righ;

            AddSnake();
        }
        private void AddSnake()
        {
            int r = Rows / 2;
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePosition.AddLast(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    if (Grid[r, c] == GridValue.Empty) {
                        yield return new Position(r, c);
                    }
                }
            }
        }
        private void AddFoot()
        {
            List<Position> Emty = new List<Position>(EmptyPositions());
            if (Emty.Count == 0)
            {
                return;
            }

            Position pos = Emty[random.Next(Emty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;

        }


        public Position HeadPosition()
        {
            return snakePosition.First.Value;
        }

        public Position TaiPosition()
        {
            return snakePosition.Last.Value;
        }

        public IEnumerable<Position> SnakePosition()
        {
            return snakePosition;
        }

        private void AddHead(Position pos)
        {
            snakePosition.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tall = snakePosition.Last.Value;
            Grid[tall.Row, tall.Col] = GridValue.Empty;
            snakePosition.RemoveLast();
        }
        public void ChangeDirection(Direction dir)
        {
            Dir = dir;
        } 

        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }
        private GridValue Wi11Hit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            if (newHeadPos == TaiPosition())
            {
                return GridValue.Empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Col];
        }


        public void Move()
        {
            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = Wi11Hit(newHeadPos);

            if ( hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GamerOver = true;
            }
            else if(hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if(hit == GridValue.Food)
            {
                AddHead(newHeadPos);
                Score++;
                AddFoot();
            }
        }

        
    }
}

