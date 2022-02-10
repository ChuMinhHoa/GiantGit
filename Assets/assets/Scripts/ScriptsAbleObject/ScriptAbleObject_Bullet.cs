using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet/NewBullet")]
public class ScriptAbleObject_Bullet : ScriptableObject
{
    public string nameID;
    public GameObject objShow;
    public int exp;
    public bool enable;
}
