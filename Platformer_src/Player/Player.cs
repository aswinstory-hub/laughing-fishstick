using System;
using System.Numerics;
using Raylib_cs;

public enum PlayerState
{
    Idle,
    Running,
    Jumping

}


class Player
{
    // VARS
    Collision collision;
    AnimationPlayer animationPlayer;

    public Vector2 pos = new Vector2(96, 96);
    
    public int height = 12;
    public int width = 8; 
    public Vector2 velocity = new Vector2(0, 0);
    int direction = 0; // -1 means left, 1 means right

    int jump = 0;
    float jumpHeight = 400f;
    public bool isGrounded = false;

    float maxSpeed = 260f;
    float maxGroundSpeed = 200f;
    float acceleration = 1800f;
    float friction = 2400f;
    float gravity = 1400f;

    public PlayerState state;


//============================================================================================

    public Player(string name)
    {
        Console.WriteLine(name + " Has Spawned");
        collision = new Collision();
        animationPlayer = new AnimationPlayer();
        state = new PlayerState();
        state = PlayerState.Idle;
    }

//============================================================================================

    void HandleInput()
    {
        var keyDown = Raylib.IsKeyDown;

        if (keyDown(KeyboardKey.Left))
        {
            direction = -1;
            state = PlayerState.Running;
        }
        else if (keyDown(KeyboardKey.Right))
        {
            direction = 1;
            state = PlayerState.Running;
        }
        else
        {
            direction = 0;
            state = PlayerState.Idle;
        }
    
        if (keyDown(KeyboardKey.Space))
        {
            jump = 1;
            state = PlayerState.Jumping;
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
        if (!isGrounded)
        {
            velocity.X = Math.Clamp(velocity.X, -maxSpeed, maxSpeed);
        }
        else if (isGrounded)
        {
            velocity.X = Math.Clamp(velocity.X, -maxGroundSpeed, maxGroundSpeed);
        }        

        if (jump != 0  && isGrounded)
        {
            velocity.Y = -jumpHeight;
            isGrounded = false;
        }

        //Calculate Y velocity
        velocity.Y += gravity * dt;



        if (velocity.Y != 0)
        {
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
        collision.ResolveYCollision(player, tileMap);
        collision.ResolveXCollision(player, tileMap);
        Move();
        animationPlayer.Update(state, direction);
    }

    public void Draw()
    {
        animationPlayer.Draw(pos);
        // Raylib.DrawRectangle((int)pos.X, (int)pos.Y, width, height, Color.Blue);
    }
    
}