#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using Mono.Cecil;
using UnityEngine;

public static class PlatformUserStats
{
    private static IPlatformUserStats platform;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize() 
    {
        Debug.Log("Initialize Platform");
        
#if !DISABLESTEAMWORKS
        platform = new SteamUserStats();
        return;
#endif
        
#if UNITY_ANDROID
        platform = new AndroidUserStats();
        return;
#endif
    }
    
    public static bool SetAchievement(string achievementId) 
    {
        Debug.Log($"Achievement: {achievementId}");
        
        
        return platform.SetAchievement(achievementId);
    }
}

public interface IPlatformUserStats
{
    public bool SetAchievement(string achievementId);
}

#if !DISABLESTEAMWORKS
public class SteamUserStats : IPlatformUserStats
{
    public bool SetAchievement(string achievementId) 
    {
        return Steamworks.SteamUserStats.SetAchievement(achievementId);
    }
}
#endif

#if UNITY_ANDROID
public class AndroidUserStats : IPlatformUserStats
{
    public bool SetAchievement(string achievementId) 
    {
        Social.ReportProgress("achivementId",100f,null);
        return true;
    }
}
#endif