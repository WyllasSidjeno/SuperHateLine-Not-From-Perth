using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rigidbody2DExt {

    public static void AddExplosionForce(
            this Rigidbody2D rb,
            float explosionForce,
            Vector2 explosionPosition,
            float explosionRadius,
            float upwardsModifier = 0.0f,
            ForceMode2D mode = ForceMode2D.Force
    ) {
        Vector2 explosionDir = rb.position - explosionPosition;
        float explosionDistance = explosionDir.magnitude;

        if (upwardsModifier != 0) {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
        }
        explosionDir.Normalize();

        // Halfway from the radius gives half force
        float forceRatio = 1 - (explosionDistance / explosionRadius);

        rb.AddForce(Mathf.Lerp(0, explosionForce, forceRatio) * explosionDir, mode);
    }
}


public class ExplosionForce : MonoBehaviour {
    public float _Force = 5f;
    public float _Radius = 10f;
    public LayerMask _Mask;
    public bool _AutoDestroy = false;

    private IEnumerator Start() {
        yield return null;  // Wait a frame before applying force

        var cols = Physics2D.OverlapCircleAll(gameObject.transform.position, _Radius, _Mask);
        var rigidbodies = new HashSet<Rigidbody2D>();
        foreach (var col in cols) {
            if (col.attachedRigidbody != null) {
                rigidbodies.Add(col.attachedRigidbody);
            }
        }
        foreach (var rb in rigidbodies) {
            rb.AddExplosionForce(_Force, transform.position, _Radius, 0, ForceMode2D.Impulse);
        }
        yield return null;
        if ( _AutoDestroy ) { Destroy(gameObject); }
    }
}
