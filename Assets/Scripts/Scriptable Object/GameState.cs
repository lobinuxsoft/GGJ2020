using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameStat", menuName = "Scriptable Object/GameStat")]
public class GameState : BaseVariable
{
    public enum PlayableEntities
    {
        Ship,
        Bob,
        Count
    }

    public PlayableEntities currentEntity = PlayableEntities.Ship;
    public Cams cams;
}

[System.Serializable]
public struct Cams
{
    public GameObject flyCam;
    public GameObject radarCam;
    public GameObject introCam;
    public GameObject bobCam;
}
