using Raylib_cs;

public static class MusicManager
{
    private static float volume = 1.0f;
    private static Music currentMusic;
    
    private static Dictionary<string, Music> music = new Dictionary <string, Music>
    {

    };

    public static void InitMusic()
    {
        Raylib.InitAudioDevice();
    }

    public static void PlayMusic(string name)
    {
        if (!music.ContainsKey(name))
        {
            Console.WriteLine("Trying to play an invalid music");
            return;
        }
        
        
        currentMusic = music[name];
        
        Raylib.SeekMusicStream(currentMusic, 0.0f);
        Raylib.SetMusicVolume(currentMusic, 1.0f); 
        Raylib.PlayMusicStream(currentMusic);
    }

    public static void Run()
    {
         Raylib.UpdateMusicStream(currentMusic);
    }
}