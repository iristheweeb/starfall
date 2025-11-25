// Entity base class.
//
// All entities in this game are derived from this class and its
// methods. It's pretty cool and saves me time from re-writing
// a pile of code over and over and over.

using System;
using System.IO;
using System.Reflection;
using Raylib_cs;

namespace Starfall
{
    public abstract class Entity
    {
        // Entity identifiers, an ID and name
        public int Id { get; private set; }
        public string? Name { get; protected set; }

        // Position and movement (and some physics)
        public float Px { get; protected set; }
        public float Py { get; protected set; }
        public float VelX { get; protected set; }
        public float VelY { get; protected set; }
        public float Friction { get; private set; }

        // Living (it's important to know if we're alive)
        public bool IsAlive { get; private set; } = true;

        // Resource paths!
        public string SpritePath { get; private set; }
        public string? Sprite { get; private set; }
        private Texture2D Texture;
        private bool TextureLoaded = false;

        // Collision box
        public (float, float) CollisionBox { get; private set; }

        // Constructor
        protected Entity(int id, string name, string sprite, float x, float y)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
            Px = x;
            Py = y;
            CollisionBox = (32, 32);
            Friction = 0.98f;

            SpritePath = GetSpritePath();
            LoadTexture();
        }

        // Internal use only and simplifies the process of getting the sprite paths
        private string GetSpritePath()
        {
            string? CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string ActualSpritePath = CurrentDir + "/" + Sprite;
            return ActualSpritePath;
        }

        // Load the texture into the texture var
        protected void LoadTexture()
        {
            if (!TextureLoaded && File.Exists(SpritePath))
            {
                Texture = Raylib.LoadTexture(SpritePath);
            }
        }

        public virtual void Update(float deltaTime)
        {
            Console.WriteLine("This entity does not have an update function implemented!");
        }

        public virtual void Draw()
        {
            if (!TextureLoaded)
                LoadTexture();

            if (TextureLoaded)
            {
                float drawX = Px - (Texture.Width / 2f);
                float drawY = Py - (Texture.Height / 2f);
                Raylib.DrawTexture(Texture, (int)drawX, (int)drawY, Color.White);
            }
        }

        public virtual void Unload()
        {
            if (TextureLoaded)
            {
                Raylib.UnloadTexture(Texture);
                TextureLoaded = false;
            }
        }

        // Checks collision with the bounds of the window
        public virtual bool CheckCollisionWithBoundary(int HeightMax, int WidthMax)
        {
            Console.WriteLine("Bounds collision hasn't been implemented yet!");
            return false;
        }

        // Simple but possibly inefficient AABB collision
        public bool CheckCollisionWithEntity(Entity target)
        {
            // Calculating half-w and half-h
            float thisHWidth = CollisionBox.Item1 / 2f;
            float thisHHeight = CollisionBox.Item2 / 2f;
            float targetHWidth = target.CollisionBox.Item1 / 2f;
            float targetHHeight = target.CollisionBox.Item2 / 2f;

            // Get the boundaries of this
            float thisLeft = Px - thisHWidth;
            float thisRight = Px + thisHWidth;
            float thisTop = Py - thisHHeight;
            float thisBottom = Py + thisHHeight;

            // Get the bounds of the target entity
            float targetLeft = target.Px - targetHWidth;
            float targetRight = target.Px + targetHWidth;
            float targetTop = target.Py - targetHHeight;
            float targetBottom = target.Py + targetHHeight;

            // Actuall AA/BB collision
            bool collX = thisRight >= targetLeft && thisLeft <= targetRight;
            bool collY = thisBottom >= targetTop && thisTop <= targetBottom;

            return collX && collY;
        }
    }
}