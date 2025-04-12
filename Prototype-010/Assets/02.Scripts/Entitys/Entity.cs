using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public bool IsDead { get; set; }
    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();
        AddComponents();
        InitializeComponents();
    }
    private void InitializeComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }
    private void AddComponents()
    {
        GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component => _components.Add(component.GetType(), component));
    }

    public T GetCompo<T>() where T : IEntityComponent
        => (T)_components.GetValueOrDefault(key: typeof(T));
}