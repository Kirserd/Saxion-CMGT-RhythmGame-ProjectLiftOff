using System;
using GXPEngine;

public class Player : Unit
{
    public Player(Vector2 position, Stat hp, Stat ms, string filename, int cols, int rows) : base(position, hp, ms, "Player", 1, 1)
    {
    }
    void Update()
    {
            if (Input.GetKey(Key.W))
        {
            Move(0, -2);

            if (Input.GetKeyDown(Key.SPACE)) 
            {
                Move(0, -50);
            }
        }
        
        if (Input.GetKey(Key.A))
        {
            Move(-2, 0);

            if (Input.GetKeyDown(Key.SPACE))
            {
                Move(-50,0);
            }
        }
        
        if (Input.GetKey(Key.S))
        {
            Move(0, 2);

            if (Input.GetKeyDown(Key.SPACE))
            {
                Move(0, 50);
            }
        }
        
        if (Input.GetKey(Key.D))
        {
            Move(2, 0);

            if (Input.GetKeyDown(Key.SPACE))
            {
                Move(50, 0);
            }
        }
    }
}
