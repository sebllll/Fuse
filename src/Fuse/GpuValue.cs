﻿namespace Fuse
{
    public abstract class AbstractGpuValue
    {

        public abstract string ID { get; }

        public abstract string TypeName(); 

        public AbstractShaderNode ParentNode { get; set; }
    }
    
    public class GpuValue<T> : AbstractGpuValue
    {
        protected internal string name;

        public GpuValue(string theName)
        {
            name = theName;
        }
        
        public override string TypeName()
        {
            return TypeHelpers.GetNameForType<T>().ToLower();
        }

        public override string ID => name + "_" + GetHashCode();

    }

    public class GpuNumericValue<T> : GpuValue<T> where T : struct
    {
        public GpuNumericValue(string theName) : base(theName)
        {
        }
    }
}