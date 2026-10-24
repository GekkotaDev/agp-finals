using Raylib_cs;

public static class TextureManager
{
    private static Dictionary<string, Texture2D> textures = new Dictionary <string, Texture2D>
    {
        
    };


    public static Texture2D? GetTexture(string name)
    {
        if (!textures.ContainsKey(name))
        {
            Console.WriteLine("Trying to get an invalid texture");
            return null;
        }
        
        return textures[name];
    }
}