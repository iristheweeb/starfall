// Entity base class.
//
// All entities in this game are derived from this class and its
// methods. It's pretty cool and saves me time from re-writing
// a pile of code over and over and over.

using System.IO;
using System.Reflection;
using Raylib_cs;

namespace Starfall;

public abstract class Entity
{
    // ENT identifiers, an ID and name
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

    // Constructor
    protected Entity(int id, string name, string sprite, float x, float y)
    {
        Id = id;
        Name = name;
        Sprite = sprite;
        Px = x;
        Py = y;

        SpritePath = GetSpritePath();
    }

    // Internal!
    private string GetSpritePath()
    {
        string? CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string ActualSpritePath = CurrentDir + "/" + Sprite;
        return ActualSpritePath;
    }

    // TODO: Virutal funcs for
    //  - movement
    //  - boundary collision
}