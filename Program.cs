using System;
using Raylib_cs;

namespace Starfall;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(800, 600, "Starfall");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            Raylib.DrawText("This is text (woah!)", 0, 0, 20, Color.Black);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}