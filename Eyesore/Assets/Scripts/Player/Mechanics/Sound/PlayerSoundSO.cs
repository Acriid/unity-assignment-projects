using UnityEngine;

[CreateAssetMenu(fileName ="SoundObject",menuName ="SoundMechanic/PlayerSound")]
public class PlayerSoundSO : ScriptableObject
{
    public float SoundRadius;
    public float SoundLingerDuration;
    public float SoundTravelTime;
    public Vector2 SoundPosition;
}
