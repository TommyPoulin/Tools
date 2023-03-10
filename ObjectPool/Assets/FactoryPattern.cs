using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPattern<T, EnumType> where T : IPoolable, IManagable, IGetType<EnumType>
{
    public Factory<T, EnumType> factory;
    public ObjectPool<T, EnumType> objectPool;
    public Manager<T, EnumType> manager;

    public void Initialize() {
        factory = new Factory<T, EnumType>();
        objectPool = new ObjectPool<T, EnumType>();
        manager = new Manager<T, EnumType>();

        factory.Initialize(this);
        objectPool.Initialize(this);
        manager.Initialize(this);
    }
}
