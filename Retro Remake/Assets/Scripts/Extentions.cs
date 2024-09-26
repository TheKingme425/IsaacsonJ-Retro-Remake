using UnityEngine;

public static class Extentions
{
    private static LayerMask LayerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 025.f;
        float distance = 0.375;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody, radius, direction, distance, LayerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
}