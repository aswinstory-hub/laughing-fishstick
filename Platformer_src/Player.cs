using System;
using System.Numerics;
using Raylib_cs;

class Player
{
    // VARS
    Vector2 pos = new Vector2(360, 360);
    int SIZE = 50;



    public Player()
    {
        Console.WriteLine("Player Has Spawned");
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, SIZE, SIZE, Color.DarkBlue);
    }
    
}