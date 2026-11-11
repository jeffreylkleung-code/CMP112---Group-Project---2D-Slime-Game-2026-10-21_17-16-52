using UnityEngine;

public interface IAbsorbable
{
    // Concept:
    // Take the player's size value > if player bigger than object, absorb, return the size the player got
    // float abosrb(): returns the size player got from absorbing
    // float PlayerSizeValue: Size of player
    float Absorb(float PlayerSizeValue);
}
