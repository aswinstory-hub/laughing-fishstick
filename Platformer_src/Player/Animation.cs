using System;
using System.Numerics;
using Raylib_cs;

class AnimationPlayer
{
    Texture2D spriteSheet;

    private int currentFrame = 0;
    private int currentState = 0; // 0 = Idle, 1 = Running, 2 = Jumping

    private float frameTimer = 0f;
    private float frameDuration = 0.175f;

    private int frameWidth = 32;
    private int frameHeight = 32;

    private int totalFrames = 4;

    private float SourceX;
    private float SourceY;

    public AnimationPlayer()
    {
        spriteSheet = Raylib.LoadTexture("../Assets/knight.png");
    }

    public void Update(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Idle:
            currentState = 0;
            break;

            case PlayerState.Running:
            currentState = 2;
            break;

            case PlayerState.Jumping:
            currentState = 0;
            break;
        }

        frameTimer += Raylib.GetFrameTime();

        if (frameTimer >= frameDuration)
        {
            frameTimer = 0f;

            currentFrame ++;

            if (currentFrame >= totalFrames )
            {
                currentFrame = 0;
            }
        }
    }

    public void Draw(Vector2 pos)
    {
        SourceX = currentFrame * frameWidth;
        SourceY = currentState * frameHeight;

        Rectangle source = new Rectangle(SourceX, SourceY, frameWidth, frameHeight);

        Rectangle dest = new Rectangle(
            pos.X - 12, pos.Y - 14, frameWidth, frameHeight);

        Raylib.DrawTexturePro(spriteSheet, source, dest, Vector2.Zero, 0f, Color.White);
    }
}