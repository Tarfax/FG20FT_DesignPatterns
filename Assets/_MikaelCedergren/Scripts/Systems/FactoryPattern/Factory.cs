using System;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    private static Factory instance;

    [SerializeField] private List<FactoryDefinition> factoryDefinitions;

    private Dictionary<Type, FactoryObject> prefabs;

    private void OnEnable() {
        if (instance == null) {
            instance = this;
            prefabs = new Dictionary<Type, FactoryObject>();

            foreach (var item in factoryDefinitions) {
                prefabs.Add(FactoryDefinition.GetPrefabType(item.FactoryType), item.FactoryObject);
            }
        }
    }

    public static GameObject CreateInstance<T>(Vector3 position, Quaternion rotation) {
        return Instantiate(FetchPrefabOfType<T>(), position, rotation);
    }

    public static GameObject FetchPrefabOfType<T>() {
        if (instance.prefabs.ContainsKey(typeof(T)) == true) {
            return instance.prefabs[typeof(T)].Visual;
        }
        throw new Exception("Invalid Factory Type");
    }
    public static FactoryObject FetchWeaponDataOfType<T>() {
        if (instance.prefabs.ContainsKey(typeof(T)) == true) {
            return instance.prefabs[typeof(T)];
        }
        throw new Exception("Invalid Factory Type");
    }


    public static GameObject CreateInstance(GameObject gameObject) {
        return Instantiate(gameObject);
    }

    public static T CreateInstance<T>() {
        return Activator.CreateInstance<T>();
    }

    public static T CreateInstance<T>(params object[] args) {
        return (T)Activator.CreateInstance(typeof(T), args);
    }

    public static T CreateInstance<T>(Type type, params object[] args) {
        return (T)Activator.CreateInstance(type, args);
    }

}
