using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Maker/Card Info")]
public class CardInfo : ScriptableObject
{
    [SerializeField] public CardStats card;
}
