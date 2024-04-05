using UnityEngine;

public static class ComponentUtility
{
    // Generic method to get or add a component
    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            Debug.LogWarning($"Component {typeof(T).Name} not found on {gameObject.name}, adding one.");
            component = gameObject.AddComponent<T>();
        }
        return component;
    }

    // Specific method to assign a Rigidbody2D component
    public static void AssignRigidbody2D(GameObject gameObject, out Rigidbody2D rb2d)
    {
        rb2d = GetOrAddComponent<Rigidbody2D>(gameObject);
    }
}
