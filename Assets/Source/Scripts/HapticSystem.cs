using MoreMountains.NiceVibrations;

public static class HapticSystem
{
    public static void Haptic(bool hapticState, EHapticType haptic)
    {
        if (hapticState)
        {
            if (haptic == EHapticType.death)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            }
            else if (haptic == EHapticType.bombPlant)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            }
            else if (haptic == EHapticType.explosion)
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
        }
    }

}