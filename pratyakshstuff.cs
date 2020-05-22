using System;
using System.Threading;

namespace spaceInvaders
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
        // Direction down is +ve
        private int _direction;
        private int _posY;
        private int _posX;

        public int posX { get => _posX; set => _posX = value; }
        public int posY { get => _posY; set => _posY = value; }

        public Bullet(int posX, int posY, int direction)
        {
            _posX = posX;
            _posY = posY;
            _direction = direction;
        }

        public void Move()
        {
            // Moves bullet by 1
            _posY = _posY + _direction;
        }

        public bool OffScreen()
        {
            if (_posY >= 0 && _posY <= 40)
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

        public void SetPosition(int posX, int posY)
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

        public bool GetInput()
        {
            bool moved = false;
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 0) x -= 1;
                        moved = true;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x < 39) x += 1;
                        moved = true;
                        break;
                    case ConsoleKey.A:
                        if (x > 0) x -= 1;
                        moved = true;
                        break;
                    case ConsoleKey.D:
                        if (x < 39) x += 1;
                        moved = true;
                        break;
                    case ConsoleKey.Escape:
                        quit = true;
                        break;
                }
                if (key.Key == ConsoleKey.Spacebar && bullet == null)
                {
                    bullet = new Bullet(x, y, -1);
                }
            }

            while (Console.KeyAvailable) Console.ReadKey(true);
            return moved;
        }
    }

    class Game
    {
        private Alien[] aliens;
        private Player player;
        private int dt;

        public Game()
        {
            player = new Player(20, 38);
            aliens = new Alien[15];
            for (int i = 0; i < aliens.Length; i++)
            {
                aliens[i] = new Alien((i % 5) * 2, (i / 5) * 2);
            }

            Run();
        }

        public void Run()
        {
            bool win = false;
            while (!player.quit)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorVisible = false;
                if (dt % 10 == 0)
                {
                    int prevX = player.x;
                    int prevY = player.y;
                    if (player.GetInput())
                    {
                        DrawPixel(prevX, prevY, ConsoleColor.Black);
                    }

                    if (player.bullet != null)
                    {
                        DrawPixel(player.bullet.posX, player.bullet.posY, ConsoleColor.Black);
                        player.bullet.Move();
                        if (player.bullet.OffScreen()) player.bullet = null;
                    }

                    int alive = 0;
                    for (int i = 0; i < aliens.Length; i++)
                    {
                        if (aliens[i] != null)
                        {
                            alive++;
                            if (dt % 50 == 0)
                            {
                                DrawPixel(aliens[i].posX, aliens[i].posY, ConsoleColor.Black);
                                if (aliens[i].posX >= 39)
                                {
                                    aliens[i].SetPosition(0, aliens[i].posY + 1);
                                    if (aliens[i].posY >= 38) player.quit = true;
                                }
                                else
                                {
                                    aliens[i].SetPosition(aliens[i].posX + 1, aliens[i].posY);
                                }
                            }

                            if (player.bullet != null)
                            {
                                if (aliens[i].posX == player.bullet.posX && aliens[i].posY == player.bullet.posY)
                                {
                                    DrawPixel(aliens[i].posX, aliens[i].posY, ConsoleColor.Black);
                                    aliens[i] = null;
                                    player.bullet = null;
                                }
                            }
                        }
                    }

                    if (alive == 0)
                    {
                        win = true;
                        player.quit = true;
                    }
                    
                    Draw();
                }
                
                Thread.Sleep(5);
                dt++;
            }

            Console.BackgroundColor = ConsoleColor.Black;

            if (win)
            {
                Console.Clear();
                Console.WriteLine("You win");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You lose");
            }

            Console.ReadLine();
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
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i] != null) DrawPixel(aliens[i].posX, aliens[i].posY, ConsoleColor.Green);
            }

            DrawPixel(player.x, player.y, ConsoleColor.White);
            if (player.bullet != null) DrawPixel(player.bullet.posX, player.bullet.posY, ConsoleColor.Red);
        }
    }
}
