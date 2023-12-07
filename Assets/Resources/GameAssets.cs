using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 *  GameAssets contains assets needed to be changed on runtime.
 *  It is a component of an object in the scene, and the object MUST be a prefab.
    Also, the prefab MUST be saved in the following path: Assets/Resources, otherwise it won't work.
 */
public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
    /*
    #region Arm animations [Deprecated]
    public AnimatorOverrideController walk_arm1;
    public AnimatorOverrideController walk_arm2;
    #endregion
    */
    [Header("Simboli")]
    public Sprite[] mapSymbols;

    [Header("Prefab")]
    public GameObject locationPrefab;
    public GameObject buttonPrefab;
    public GameObject partyPrefab;
}
