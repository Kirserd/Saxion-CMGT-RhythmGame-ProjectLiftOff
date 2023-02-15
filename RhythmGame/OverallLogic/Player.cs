using System;
using GXPEngine;

public class Player : Unit
{
    public Player(Vector2 position, Stat hp, Stat ms, string filename, int cols, int rows) : base(position, hp, ms, "Player", 1, 1)
    {
    }

    protected override void SafeUpdate()
    {
        if (Input.GetKey(Key.W))
        {
            Move(0, -2);
            if (Input.GetKey(Key.SPACE)) 
            {
              for(int i = 0; i <= 7; i ++)
                {
                    y--;
                }
            }
        }
        
        if (Input.GetKey(Key.A))
        {
            SetXY(x--, y);

            if (Input.GetKey(Key.SPACE))
            {
                for (int i = 0; i <= 7; i++)
                {
                    x--;
                }
            }
        }
        
        if (Input.GetKey(Key.S))
        {
            SetXY(x, y++);

            if (Input.GetKey(Key.SPACE))
            {
                for (int i = 0; i <= 7; i++)
                {
                    y++;
                }
            }
        }
        
        if (Input.GetKey(Key.D))
        {
            SetXY(x++, y);

            if (Input.GetKey(Key.SPACE))
            {
                for (int i = 0; i <= 7; i++)
                {
                    x++;
                }
            }
        }
    }
}
