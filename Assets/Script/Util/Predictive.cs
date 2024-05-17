using UnityEngine;

public class Predictive {
    public static Vector3 PredictTarget(
            Vector3 startPos,
            Vector3 targetPos, Vector3 targetVel,
            float bulletVel
    ) {

        float a = Vector3.Dot(targetVel, targetVel) - bulletVel * bulletVel;
        float b = 2f * Vector3.Dot(targetVel, targetPos - startPos);
        float c = Vector3.Dot(targetPos - startPos, targetPos - startPos);

        float discriminant = b * b - 4f * a * c;

        // If we were to shoot in the past, just shoot at the player
        if (discriminant < 0) {
            return targetPos;
        }

        // t is the time it will take for the bullet to reach the player
        float t = 2f * c / Mathf.Sqrt(discriminant - b);

        // We can now predict where the player will be in t seconds
        return targetPos + targetVel * t;
    }
}