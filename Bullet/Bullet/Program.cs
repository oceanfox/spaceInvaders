using System;

namespace Bullet
{
    class Bullet
    {
        // Direction down is +ve
        private int _direction;
        private int _posY;
        private int _posX;

        public int posX { get => _posX; set => _posX = value; }
        public int posY { get => _posY; set => _posY = value; }

        public Bullet(int posY, int posX, int direction)
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

        public void Shoot()
        {
            // creates bullet at that aliens position and moving down
            Bullet bullet1 = new Bullet(_posY, _posX, 1);
        }

        public void SetPosition(int posY, int posX)
        {
            _posY = posY;
            _posX = posX;
        }
    }
}
