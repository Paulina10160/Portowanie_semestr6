using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start() 
    {
        PlatformUserStats.SetAchievement("welcomeAchievement");
    }
}