using Raylib_cs;
using System.Numerics;

public class GameCamera
{
    public Camera2D Camera;

    private float screenWidth;
    private float screenHeight;

    private float worldWidth;
    private float worldHeight;

    public float MinZoom = 2f;
    public float MaxZoom = 3f;

    public float ZoomSpeed = 0.1f;
    public float Smoothness = 0.1f;

    private float deadZoneHeight = 0f;
    private float deadZoneWidth = 170f;

    public GameCamera(
        float screenWidth,
        float screenHeight,
        float worldWidth,
        float worldHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.worldWidth = worldWidth;
        this.worldHeight = worldHeight;

        Camera = new Camera2D();

        Camera.Target = Vector2.Zero;

        Camera.Offset = new Vector2(
            screenWidth / 2f,
            screenHeight / 2f
        );

        Camera.Rotation = 0f;
        Camera.Zoom = 3f;
    }

    public void Update(Vector2 targetPosition)
    {
        HandleZoom();

        DeadZoneFollow(targetPosition);

        ClampToWorld();
    }

    private void HandleZoom()
    {
        float wheel = Raylib.GetMouseWheelMove();

        Camera.Zoom += wheel * Camera.Zoom * ZoomSpeed;

        Camera.Zoom = Math.Clamp(
            Camera.Zoom,
            MinZoom,
            MaxZoom
        );
    }

    private void DeadZoneFollow(Vector2 playerPos)
    {
        float left = Camera.Target.X - deadZoneWidth / 2f;
        float right = Camera.Target.X + deadZoneWidth / 2f;

        float top = Camera.Target.Y - deadZoneHeight / 2f;
        float bottom = Camera.Target.Y + deadZoneHeight / 2f;

        Vector2 desiredPosition = new Vector2(Camera.Target.X, Camera.Target.Y);

        // Horizontal
        if (playerPos.X < left)
        {
            desiredPosition.X = playerPos.X + deadZoneWidth / 2f;
        }
        else if (playerPos.X > right)
        {
            desiredPosition.X = playerPos.X - deadZoneWidth / 2f;
        }

        // Vertical
        if (playerPos.Y < top)
        {
            desiredPosition.Y = playerPos.Y + deadZoneHeight / 2f;
        }
        else if (playerPos.Y > bottom)
        {
            desiredPosition.Y = playerPos.Y - deadZoneHeight / 2f;
        }


        Camera.Target = Vector2.Lerp(
            Camera.Target,
            desiredPosition,
            0.1f
        );
    }
    private void ClampToWorld()
    {
        float visibleWidth = screenWidth / Camera.Zoom;
        float visibleHeight = screenHeight / Camera.Zoom;

        float halfWidth = visibleWidth / 2f;
        float halfHeight = visibleHeight / 2f;

        // WORLD SMALLER THAN CAMERA
        if (visibleWidth >= worldWidth)
        {
            Camera.Target.X = worldWidth / 2f;
        }
        else
        {
            Camera.Target.X = Math.Clamp(
                Camera.Target.X,
                halfWidth,
                worldWidth - halfWidth
            );
        }

        if (visibleHeight >= worldHeight)
        {
            Camera.Target.Y = worldHeight / 2f;
        }
        else
        {
            Camera.Target.Y = Math.Clamp(
                Camera.Target.Y,
                halfHeight,
                worldHeight - halfHeight
            );
        }
    }
    public void Begin()
    {
        Raylib.BeginMode2D(Camera);
    }

    public void End()
    {
        Raylib.EndMode2D();
    }
}