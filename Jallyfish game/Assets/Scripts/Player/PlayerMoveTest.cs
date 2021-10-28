using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS1633 // Unrecognized #pragma directive
#pragma execution_character_set("utf-8")

public class PlayerMoveTest : MonoBehaviour
#pragma warning restore CS1633 // Unrecognized #pragma directive
{

    public float kuraga = 5f; //это курага сан и он ссыт

    private void Start()
    {
        kuraga = 6f; //курага сан не ссыт
    }
}
