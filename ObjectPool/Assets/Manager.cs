using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T, EnumType> where T : IPoolable, IManagable, IGetType<EnumType>
{
    FactoryPattern<T, EnumType> wraper;

    List<T> objectList;
    List<T> toRemoveList;

    public void Initialize(FactoryPattern<T, EnumType> factory) {
        wraper = factory;

        objectList = new List<T>();
        toRemoveList = new List<T>();
    }

    public void Add(T obj) {
        objectList.Add(obj);
    }

    public void Remove(T toRemove) {
        toRemoveList.Add(toRemove);
    }

    public void Refresh() {

        foreach (T item in toRemoveList) {
            objectList.Remove(item);
            wraper.objectPool.Pool(item.GetEnumType(), item);
        }
        toRemoveList.Clear();

        foreach (T obj in objectList) {
            if (obj.IsAlive())
                obj.Refresh();
            else
                Remove(obj);
        }
    }

    public void FixedRefresh() {
        foreach (T obj in objectList) {
            obj.FixedRefresh();
        }
    }
}
