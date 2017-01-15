﻿using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public struct WeakAction<T1, T2>:IEquatable<WeakAction<T1, T2>>
{
    public WeakAction(Action<T1, T2> listener)
    {
        ObjectRef = GCHandle.Alloc(listener.Target, GCHandleType.Weak);
        Method = listener.Method;

        if (Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length != 0)
            throw new ArgumentException("Cannot create weak event to anonymous method with closure.");
    }

    public bool Invoke(T1 data1, T2 data2)
    {
        if (ObjectRef.IsAllocated && ObjectRef.Target != null)
        {
            Method.Invoke(ObjectRef.Target, new object[] { data1, data2 });
            return true;
        }

        return false;
    }

    public bool Equals(WeakAction<T1, T2> other)
    {
        return (Method.Equals(other.Method));
    }

    readonly GCHandle   ObjectRef;
    readonly MethodInfo Method;
}

public struct WeakAction<T>:IEquatable<WeakAction<T>>
{
    public WeakAction(Action<T> listener) 
    {
        ObjectRef = GCHandle.Alloc(listener.Target, GCHandleType.Weak);
        Method = listener.Method;

        if (Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length != 0)
            throw new ArgumentException("Cannot create weak event to anonymous method with closure.");
    }

    public bool Invoke(T data)
    {
        if (ObjectRef.IsAllocated && ObjectRef.Target != null)
        {
            Method.Invoke(ObjectRef.Target, new object[] { data });
            return true;
        }

        return false;
    }

    public bool Equals(WeakAction<T> other)
    {
        return (Method.Equals(other.Method));
    }

    readonly GCHandle   ObjectRef;
    readonly MethodInfo Method;
}

public struct WeakAction:IEquatable<WeakAction>
{
    public WeakAction(Action listener) 
    {
        ObjectRef = GCHandle.Alloc(listener.Target, GCHandleType.Weak);
        Method = listener.Method;

        if (Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length != 0)
            throw new ArgumentException("Cannot create weak event to anonymous method with closure.");
    }

    public bool Invoke()
    {
        if (ObjectRef.IsAllocated && ObjectRef.Target != null)
        {
            Method.Invoke(ObjectRef.Target, null);
            return true;
        }

        return false;
    }

    public bool Equals(WeakAction other)
    {
        return (Method.Equals(other.Method));
    }

    readonly GCHandle   ObjectRef;
    readonly MethodInfo Method;
}

