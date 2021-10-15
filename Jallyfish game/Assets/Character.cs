using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //класс который содержит все наши оборудования
{
    public enum BodyArmor //список для бронижелета
    {
        None,
        Tier_1,
        Tier_2,
        Tier_3
    }

    private BodyArmor bodyArmor; //поле для хранения желета

    //делаем конструктор
    public Character()
    {
        bodyArmor = BodyArmor.None; //Установим желет по умолчанию
    }

    public BodyArmor GetEquippedBodyArmor() //возврат желетов
    {
        return bodyArmor;
    }
    public void SetEquippedBodyArmor(BodyArmor bodyArmor) //установка желета
    {
        this.bodyArmor = bodyArmor;
    }
}
 