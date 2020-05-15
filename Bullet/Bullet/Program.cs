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
}
