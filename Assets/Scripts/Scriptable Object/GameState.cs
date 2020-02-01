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
}
