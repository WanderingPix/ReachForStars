using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReachForStars.Utilities;

// Original code from: https://github.com/D1GQ/AmongUsPrefabsAPI

/// <summary>
/// A base class for handling prefab management within Among Us.
/// This class provides functionality to load, cache, retrieve, and remove prefabs dynamically.
/// It ensures that prefabs can be instantiated, stored, and managed efficiently without requiring Unity Editor access.
/// About any Among Us MonoBehavior/Component prefab can be loaded from the game.
/// </summary>
public static class PrefabManager
{
    private static readonly List<string> CachedTypes = [];
    private static readonly Dictionary<string, GameObject?> Cached = [];
    private static readonly Dictionary<string, GameObject?> TempCached = [];

    private static T? LoadPrefab<T>(Transform? parent = null, int cacheType = 0) where T : Component
    {
        string name = typeof(T).Name;
        Component? obj = Resources.FindObjectsOfTypeAll(Il2CppType.Of<T>()).FirstOrDefault(com => com.Cast<T>().GetIl2CppType() == Il2CppType.Of<T>())?.Cast<T>();
        if (obj != null)
        {
            var instance = parent != null ? UnityEngine.Object.Instantiate(obj.gameObject, parent) : UnityEngine.Object.Instantiate(obj.gameObject);

            instance.name = instance.name.Replace("(Clone)", string.Empty);
            if (cacheType == 1)
            {
                instance.name += "(Prefab)";
                CachedTypes.Add(name);
                Cached[name] = instance.gameObject;
                UnityEngine.Object.DontDestroyOnLoad(instance.gameObject);
            }
            else if (cacheType == 2)
            {
                instance.name += "(Temp)";
                TempCached[name] = instance;
            }

            return instance.GetComponent<T>();
        }

        return null;
    }

    /// <summary>
    /// Copies a prefab of type <typeparamref name="T"/> without caching it.
    /// </summary>
    /// <typeparam name="T">The component type of the prefab.</typeparam>
    /// <param name="parent">The parent Transform to attach the instantiated prefab to (optional).</param>
    /// <returns>An instance of the requested prefab component if found, otherwise null.</returns>
    public static T? CopyPrefab<T>(Transform? parent = null) where T : Component
    {
        return LoadPrefab<T>(parent);
    }

    /// <summary>
    /// Retrieves a temporarily cached prefab of type <typeparamref name="T"/>.
    /// If the prefab is not already cached, it will be loaded and temporarily cached for future use.
    /// Note: Temporarily cached prefabs can be destroyed on load, unlike cached prefabs.
    /// </summary>
    /// <typeparam name="T">The component type of the prefab.</typeparam>
    /// <returns>An instance of the requested prefab component if found, otherwise null.</returns>
    public static T? GetTempPrefab<T>() where T : Component
    {
        if (TempCached.TryGetValue(typeof(T).Name, out var obj) && obj != null)
        {
            return obj.GetComponent<T>();
        }

        return LoadPrefab<T>(null, 2);
    }

    /// <summary>
    /// Retrieves a cached prefab of type <typeparamref name="T"/>.
    /// If the prefab is not already cached, an exception is thrown.
    /// </summary>
    /// <typeparam name="T">The component type of the prefab.</typeparam>
    /// <returns>The cached instance of the requested prefab.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the requested prefab is not cached.</exception>
    public static T? GetCachedPrefab<T>() where T : Component
    {
        if (!CachedTypes.Contains(typeof(T).Name))
            throw new InvalidOperationException("Unable to get a prefab that hasn't been cached!");

        if (Cached.TryGetValue(typeof(T).Name, out var obj) && obj != null)
        {
            return obj.GetComponent<T>();
        }

        return null;
    }

    /// <summary>
    /// Caches a prefab of type <typeparamref name="T"/> to allow retrieval later,
    /// placing it into DontDestroyOnLoad to persist across scene changes.
    /// If the prefab is already cached, an exception is thrown.
    /// </summary>
    /// <typeparam name="T">The component type of the prefab.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the prefab is already cached.
    /// </exception>
    public static void CatchPrefab<T>() where T : Component
    {
        if (CachedTypes.Contains(typeof(T).Name))
            throw new InvalidOperationException("Unable to cache a prefab that's already been cached!");

        LoadPrefab<T>(null, 1);
    }

    /// <summary>
    /// Removes a cached prefab of type <typeparamref name="T"/>.
    /// If the prefab is not cached, an exception is thrown.
    /// </summary>
    /// <typeparam name="T">The component type of the prefab.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if the prefab is not cached.</exception>
    public static void UncachePrefab<T>() where T : Component
    {
        if (!CachedTypes.Contains(typeof(T).Name))
            throw new InvalidOperationException("Unable to uncache a prefab that hasn't been cached!");

        if (Cached.TryGetValue(typeof(T).Name, out var obj))
        {
            CachedTypes.Remove(typeof(T).Name);
            Cached.Remove(typeof(T).Name);
            UnityEngine.Object.Destroy(obj);
        }
    }

    /// <summary>
    /// Clears all cached prefabs, removing their references and destroying their instances.
    /// </summary>
    public static void UncacheAll()
    {
        CachedTypes.Clear();
        foreach (var obj in Cached.Values)
        {
            UnityEngine.Object.Destroy(obj);
        }
        Cached.Clear();
    }
}