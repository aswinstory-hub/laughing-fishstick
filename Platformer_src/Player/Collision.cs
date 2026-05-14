using System;
using Raylib_cs;

class Collision
{
    public Collision()
    {
        
    }

    public void ResolveYCollision(Player player, TileMap tileMap)
    {
        float dt = Raylib.GetFrameTime();

        Rectangle playerRect = new Rectangle(
            
            (float)player.pos.X, 
            (float)player.pos.Y + (player.velocity.Y * dt), 
            (float)player.SIZE, 
            (float)player.SIZE
            
        );

        int TILE_SIZE = tileMap.TILE_SIZE;

        int left   = (int)(playerRect.X / tileMap.TILE_SIZE);
        int right  = (int)((playerRect.X + player.SIZE - 1) / tileMap.TILE_SIZE);

        int top    = (int)(playerRect.Y / tileMap.TILE_SIZE);
        int bottom = (int)((playerRect.Y + player.SIZE - 1) / tileMap.TILE_SIZE);

        player.isGrounded = false;

        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                if (tileMap.collisionTiles[y, x] == 1)
                {
                    Rectangle tileRect = new Rectangle(
                        x * TILE_SIZE,
                        y * TILE_SIZE,
                        TILE_SIZE,
                        TILE_SIZE
                    );

                    if (Raylib.CheckCollisionRecs(playerRect, tileRect))
                    {
                        // falling down
                        if (player.velocity.Y > 0)
                        {
                            player.pos.Y = tileRect.Y - playerRect.Height;
                            player.isGrounded = true;
                        }

                        // hitting ceiling
                        else if (player.velocity.Y < 0 )
                        {
                            player.pos.Y = tileRect.Y + tileRect.Height;
                        }

                        player.velocity.Y = 0;

                        return;
                    }
                }
            }
        }
    }

    public void ResolveXCollision(Player player, TileMap tileMap)
    {
        float dt = Raylib.GetFrameTime();

        Rectangle playerRect = new Rectangle(
            
            (float)player.pos.X + (player.velocity.X * dt), 
            (float)player.pos.Y, 
            (float)player.SIZE, 
            (float)player.SIZE
            
        );

        int TILE_SIZE = tileMap.TILE_SIZE;

        int left   = (int)(playerRect.X / tileMap.TILE_SIZE);
        int right  = (int)((playerRect.X + player.SIZE - 1) / tileMap.TILE_SIZE);

        int top    = (int)(playerRect.Y / tileMap.TILE_SIZE);
        int bottom = (int)((playerRect.Y + player.SIZE - 1) / tileMap.TILE_SIZE);


        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                if (
                        x >= 0 &&
                        y >= 0 &&
                        y < tileMap.collisionTiles.GetLength(0) &&
                        x < tileMap.collisionTiles.GetLength(1)
                    )
                {    
                    if (tileMap.collisionTiles[y, x] == 1)
                    {
                        Rectangle tileRect = new Rectangle(
                            x * TILE_SIZE,
                            y * TILE_SIZE,
                            TILE_SIZE,
                            TILE_SIZE
                        );

                        if (Raylib.CheckCollisionRecs(playerRect, tileRect))
                        {
                            // falling down
                            if (player.velocity.X > 0)
                            {
                                player.pos.X = tileRect.X - playerRect.Width;
                            }

                            // hitting ceiling
                            else if (player.velocity.X < 0 )
                            {
                                player.pos.X = tileRect.X + tileRect.Width;
                            }

                            player.velocity.X = 0;

                            return;
                        }
                    }
                }   
            }
        }
    }

}