using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverData
{
    // Будет закидываться на то что нужно сохранить

    public int PlayerLevel;
    public float PlayerXp;
    public float PlayerRequiredXp;
    public float PlayerHP;
    public float PlayerArmor;
    public float[] PlayerPosition;

    public List<float[]> EnamyPosition;
    public List<float> EnamyHealth;
    public List<ESharkType> EnamyType;
}
