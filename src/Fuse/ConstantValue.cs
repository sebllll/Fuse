﻿using System;
using Stride.Core.Mathematics;

namespace Fuse
{

    public static class ConstantHelper
    {
        // ReSharper disable once UnusedMember.Global
        // USED IN VL
        public static ConstantValue<T> FromFloat<T>(float theValue)
        {
            if (typeof(T) == typeof(float)) return new ConstantValue<T>((T)Convert.ChangeType(theValue, typeof(float)));
            if (typeof(T) == typeof(Vector2)) return new ConstantValue<T>((T)Convert.ChangeType(new Vector2(theValue,theValue), typeof(Vector2)));
            if (typeof(T) == typeof(Vector3)) return new ConstantValue<T>((T)Convert.ChangeType(new Vector3(theValue,theValue,theValue), typeof(Vector3)));
            if (typeof(T) == typeof(Vector4)) return new ConstantValue<T>((T)Convert.ChangeType(new Vector4(theValue,theValue,theValue,theValue), typeof(Vector4)));
            return null;
        }
    }
    
    public class ConstantValue<T>: GpuValue<T>
    {
        
        private T _myValue;
        public ConstantValue(T theValue) : base("constant")
        {
            _myValue = theValue;
        }
        
        public override string ID => TypeHelpers.GetDefaultForType<T>(_myValue);
    }
}