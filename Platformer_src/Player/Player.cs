using System;
using System.Numerics;
using Raylib_cs;

class Player
{
    // VARS
    Collision collision;
    public Vector2 pos = new Vector2(0, 0);
    public int SIZE = 16;
    public Vector2 velocity = new Vector2(0, 0);
    int direction = 0; // -1 means left, 1 means right
    int jump = 0;
    float jumpHeight = 400f;
    float maxSpeed = 400f;
    float acceleration = 2000f;
    float friction = 1800f;
    float gravity = 20f;
    public bool isGrounded = false;

//============================================================================================

    public Player(string name)
    {
        Console.WriteLine(name + " Has Spawned");
        collision = new Collision();
    }

//============================================================================================

    void HandleInput()
    {
        var keyDown = Raylib.IsKeyDown;

        if (keyDown(KeyboardKey.Left))
        {
            direction = -1;
        }
        else if (keyDown(KeyboardKey.Right))
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    
        if (keyDown(KeyboardKey.Space))
        {
            jump = 1;
        }
        else
        {
            jump = 0;
        }
    }

//============================================================================================

    void CalculateVelocity()
    {
        float dt = Raylib.GetFrameTime();

        // Calculate X velocity
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

        //Calculate Y velocity
        if (!isGrounded)
        {
            velocity.Y += gravity;
        }

        if (jump != 0 && isGrounded)
        {
            velocity.Y -= jumpHeight;
            isGrounded = false;
        }
    }

//============================================================================================

    void Move()
    {
        float dt = Raylib.GetFrameTime(); 

        pos.X += velocity.X * dt;
        pos.Y += velocity.Y * dt;
    }

//============================================================================================

    public void Update(Player player, TileMap tileMap)
    {
        HandleInput();
        CalculateVelocity();
        Move();
        collision.CheckYCollision(player, tileMap);
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, SIZE, SIZE, Color.DarkBlue);
    }
    
}