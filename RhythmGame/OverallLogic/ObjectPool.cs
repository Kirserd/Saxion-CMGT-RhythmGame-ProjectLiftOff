using System.Collections.Generic;
using System;

public class ObjectPool<T> where T : class, new()
{
    private static Dictionary<Type, ObjectPool<T>> _pools = new Dictionary<Type, ObjectPool<T>>();

    private static ObjectPool<T> CreateInstance(Type type)
    {
        ObjectPool<T> pool = new ObjectPool<T>();
        _pools.Add(type, pool);
        return pool;
    }
    public static ObjectPool<T> GetInstance(Type type)
    {
        if (!_pools.ContainsKey(type))
            return CreateInstance(type);

        return _pools[type];
    }
    private List<T> _pool = new List<T>();

    public T GetObject(object owner)
    {
        if (_pool.Count > 0)
        {
            T obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
            if (obj is Bullet bullet) 
                bullet.Activate(owner as Unit);

            return obj;
        }
        else
            return null;
    }

    public void ReturnObject(T obj)
    {
        if (obj is Bullet bullet)
            bullet.Deactivate();
        _pool.Add(obj);
    }

    public void InitPool(int amount) 
    {
        for (int i = 0; i < amount; i++)
            _pool.Add(new T());
    }
    public void ClearPool() => _pool.Clear();
}


