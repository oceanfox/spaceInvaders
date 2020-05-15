using System;
using System.Data;
using System.IO.Pipes;
using System.Net.Sockets;

namespace GameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 40);
            Game game = new Game();
        }
    }
    
    class Bullet
    {
        //private int _direction;
        private int _posY;
        private int _posX;

        public int posX { get => _posX; set => _posX = value; }
        public int posY { get => _posY; set => _posY = value; }

        public Bullet(int posY, int posX)
        {
            _posX = posX;
            _posY = posY;
        }

        public void Move()
        {
            // Moves bullet up by 1
            _posY = _posY - 1;
        }

        public bool OffScreen()
        {
            if (_posY > 0 && _posY <= 40)
            {
                return false;
            }
            return true;
        }
    }

    class Alien
    {
        private int _posY;
        private int _posX;

        public int posY { get => _posY; }
        public int posX { get => _posX; }

        public Alien(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
        }

        public void SetPosition(int posY, int posX)
        {
            _posY = posY;
            _posX = posX;
        }
    }

    class Player
    {
        public int x { get; set; }
        public int y { get; set; }

        public bool quit = false;

        public Bullet bullet;
        
        public Player (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void GetInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(x>0) x-=1;
                        break;
                    case ConsoleKey.RightArrow:
                        if(x<40) x+=1;
                        break;
                    case ConsoleKey.Escape:
                        quit = true;
                        break;
                }
                if(key.Key == ConsoleKey.Spacebar)
                {
                    bullet = new Bullet(x, y);
                }
            }
        }
    }

    class Game
    {
        private Alien[] aliens;
        private Player player;

        public Game()
        {
            player = new Player(20, 18);
            aliens = new Alien[15];
            for (int i = 0; i < aliens.Length; i++)
            {
                aliens[i] = new Alien((i % 5) * 2, (i / 5) * 2);
            }

            Run();
        }

        public void Run()
        {
            while (!player.quit)
            {
                player.GetInput();
                if (player.bullet != null)
                {
                    player.bullet.Move();
                    if (player.bullet.OffScreen()) player.bullet = null;
                }

                Draw();
            }
        }

        public void DrawPixel(int x, int y, ConsoleColor color)
        {
            Console.CursorLeft = 2 * x;
            Console.CursorTop = y;
            Console.BackgroundColor = color;
            Console.Write("  ");
        }
        
        public void Draw()
        {
            Console.Clear();
            
            for (int i = 0; i < aliens.Length; i++)
            {
                DrawPixel(aliens[i].posX, aliens[i].posY, ConsoleColor.Green);
            }

            DrawPixel(player.x, player.y, ConsoleColor.White);
        }
    }
}
