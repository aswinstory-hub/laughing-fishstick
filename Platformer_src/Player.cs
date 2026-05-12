using System;
using System.Numerics;
using Raylib_cs;

class Player
{
    // VARS
    Vector2 pos = new Vector2(360, 360);
    int SIZE = 50;
    Vector2 velocity = new Vector2(0, 0);
    int direction = 0; // -1 means left, 1 means right
    float maxSpeed = 400f;
    float acceleration = 2000f;
    float friction = 1800f;



    public Player()
    {
        Console.WriteLine("Player Has Spawned");
    }

    void HandleInput()
    {
        var key = Raylib.IsKeyDown;

        if (key(KeyboardKey.Left))
        {
            direction = -1;
        }
        else if (key(KeyboardKey.Right))
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    }

    void CalculateVelocity()
    {
        // Calculate X velocity
        float dt = Raylib.GetFrameTime();

            // Accelerate
            if (direction != 0)
            {
                velocity.X += direction * acceleration * dt;
            }
            else
            {
                // Friction / deceleration
                if (velocity.X > 0)
                {
                    velocity.X -= friction * dt;

                    if (velocity.X < 0)
                        velocity.X = 0;
                }
                else if (velocity.X < 0)
                {
                    velocity.X += friction * dt;

                    if (velocity.X > 0)
                        velocity.X = 0;
                }
            }

            // Clamp max speed
            velocity.X = Math.Clamp(velocity.X, -maxSpeed, maxSpeed);        
    }

    public void Move()
    {
        float dt = Raylib.GetFrameTime(); 

        HandleInput();
        CalculateVelocity();

        pos.X += velocity.X * dt;
    }


    public void Draw()
    {
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, SIZE, SIZE, Color.DarkBlue);
    }
    
}