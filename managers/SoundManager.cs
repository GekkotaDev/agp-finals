using Raylib_cs;

public static class SoundManager
{
    private static Dictionary<string, Sound> sounds = new Dictionary <string, Sound>
    {

    };


    public static void PlaySound(string name)
    {
        if (!sounds.ContainsKey(name))
        {
            Console.WriteLine("Trying to play an invalid sound");
            return;
        }
        
        Raylib.PlaySound(sounds[name]);
    }
}