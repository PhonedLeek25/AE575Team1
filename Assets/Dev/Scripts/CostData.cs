using System.Collections.Generic;
using UnityEngine;
public enum TextureTypes
{
    None, Wall, Ceiling, Floor, Furniture
}
public class Texture
{
    public string name;
    public int cost;
    public TextureTypes TextureType;

    public Texture(string name, int cost)
    {
        this.name = name;
        this.cost = cost;
    }
}