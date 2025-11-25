// Item base class
//
// Similar story to the base entity, just a generic base to build on
// to save sanity from this base being insanely long

using System;
using System.IO;
using System.Reflection;
using Raylib_cs;

namespace Starfall
{
    public abstract class ItemBase
    {
        // ID and name
        public int Id { get; private set; }
        public string? Name { get; protected set; }

        public int Durability { get; protected set; }

        // Resource paths
        public string SpritePath { get; private set; }
        public string? Sprite { get; private set; }
        private Texture2D Texture;
        private bool TextureLoaded = false;

        // Flags that are used across all item subtypes
        public bool PartOfQuest { get; protected set; }
        public bool Unbreakable { get; protected set; }

        public int NeededForQuestID { get; private set; }

        // Constructor
        protected ItemBase(int id, string name, string sprite, int dur, bool cantBreak, bool questItem, int qid)
        {
            Id = id;
            Name = name;
            
            // Is this for a quest?
            if (questItem)
            {
                PartOfQuest = questItem;
                NeededForQuestID = qid;
            } else {
                PartOfQuest = false;
                NeededForQuestID = -1;
            }

            // Can the item break?
            if (cantBreak)
            {
                Unbreakable = cantBreak;
                Durability = -1;
            } else {
                Durability = dur;
                Unbreakable = false;
            }

            // Get the actual sprite path
            SpritePath = GetSpritePath();
        }

        private string GetSpritePath()
        {
            string? CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string ActualSpritePath = CurrentDir + "/" + Sprite;
            return ActualSpritePath;
        }
    }
}