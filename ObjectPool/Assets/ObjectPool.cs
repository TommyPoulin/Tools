using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using System.Linq;

public class ObjectPool<T, EnumType> where T : IPoolable, IManagable, IGetType<EnumType>
{
    FactoryPattern<T, EnumType> wraper;

    Dictionary<EnumType, Queue<T>> oPool;
    public void Initialize(FactoryPattern<T, EnumType> factoryPattern) {
        wraper = factoryPattern;

        oPool = new Dictionary<EnumType, Queue<T>>();

        List<EnumType> list = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToList();

        foreach (EnumType type in list)
            oPool.Add(type, new Queue<T>());
    }

    public void Pool(EnumType objType, T o) {
        oPool[objType].Enqueue(o);
        o.Pool();
    }

    public T Depool(EnumType objType) {
        T o;
        try {
            o = oPool[objType].Dequeue();
            o.Depool();
            return o;
        } catch (System.Exception e) {
            throw new System.InvalidOperationException("Queue is empty.");
        }
    }
}
