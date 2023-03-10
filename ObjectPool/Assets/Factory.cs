using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Factory<T, EnumType> where T : IPoolable, IManagable, IGetType<EnumType>
{
    FactoryPattern<T, EnumType> wraper;

    Dictionary<EnumType, GameObject> prefabResourceDict;

    public void Initialize(FactoryPattern<T, EnumType> factoryPattern) {
        wraper = factoryPattern;

        prefabResourceDict = new Dictionary<EnumType, GameObject>();

        List<EnumType> enumValuesList = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToList();
        foreach (EnumType type in enumValuesList)
            prefabResourceDict.Add(type, Resources.Load<GameObject>(type.ToString() + "Prefab"));
    }

    public T Create(EnumType type) {
        T obj = wraper.objectPool.Depool(type);
        if (obj == null) {
            GameObject go = GameObject.Instantiate(prefabResourceDict[type]);
            obj = go.GetComponent<T>();
            wraper.manager.Add(obj);
        }
        return obj;
    }

}