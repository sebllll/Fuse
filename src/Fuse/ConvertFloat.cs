﻿using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse
{
    public class Float2Join : ShaderNode<Vector2>
    {
        private ShaderNode<float> _x;
        private ShaderNode<float> _y;
        
        private readonly ShaderNode<float> _default;

        public Float2Join(NodeContext nodeContext) : base(nodeContext, "float2Join")
        {
            _default = new ConstantValue<float>(ShaderNodesUtil.GetContext(nodeContext,0),0);
            
            _x = _default;
            _y = _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y});
        }

        public void SetInputs(ShaderNode<float> x, ShaderNode<float> y)
        {
            _x = x ?? _default;
            _y = y ?? _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y});
        }
        
        protected override string SourceTemplate()
        {
            return ShaderNodesUtil.Evaluate("float2 ${resultName} = float2(${x},${y});", 
                new Dictionary<string, string>
                {
                    {"x", _x.ID},
                    {"y", _y.ID}
                });
        }
    }
    
    public class Float3Join : ShaderNode<Vector3>
    {
        
        private ShaderNode<float> _x;
        private ShaderNode<float> _y;
        private ShaderNode<float> _z;
        
        private readonly ShaderNode<float> _default;

        public Float3Join(NodeContext nodeContext) : base(nodeContext, "Float3Join")
        {
            _default = new ConstantValue<float>(ShaderNodesUtil.GetContext(nodeContext,0),0);
            
            _x = _default;
            _y = _default;
            _z = _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y,_z});
        }

        public void SetInputs(ShaderNode<float> x, ShaderNode<float> y, ShaderNode<float> z)
        {
            _x = x ?? _default;
            _y = y ?? _default;
            _z = z ?? _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y,_z});
        }
        
        protected override string SourceTemplate()
        {
            return ShaderNodesUtil.Evaluate("float3 ${resultName} = float3(${x},${y},${z});", 
                new Dictionary<string, string>
                {
                    {"x", _x.ID},
                    {"y", _y.ID},
                    {"z", _z.ID}
                });
        }
    }
    
    public class Float4Join : ShaderNode<Vector4>
    {
        private ShaderNode<float> _x;
        private ShaderNode<float> _y;
        private ShaderNode<float> _z;
        private ShaderNode<float> _w;
        
        private readonly ShaderNode<float> _default;

        public Float4Join(NodeContext nodeContext) : base(nodeContext, "Float4Join")
        {
            _default = new ConstantValue<float>(ShaderNodesUtil.GetContext(nodeContext,0),0);

            _x = _default;
            _y = _default;
            _z = _default;
            _w = _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y,_z,_w});
        }

        public void SetInputs(ShaderNode<float> x, ShaderNode<float> y, ShaderNode<float> z, ShaderNode<float> w)
        {
            _x = x ?? _default;
            _y = y ?? _default;
            _z = z ?? _default;
            _w = w ?? _default;
            
            SetInputs( new List<AbstractShaderNode>{_x,_y,_z,_w});
        }

        protected override string SourceTemplate()
        {
            return ShaderNodesUtil.Evaluate("float4 ${resultName} = float4(${x},${y},${z},${w});", 
                new Dictionary<string, string>
                {
                    {"x", _x.ID},
                    {"y", _y.ID},
                    {"z", _z.ID},
                    {"w", _w.ID}
                });
        }
    }
    
    public class FromFloat<T> : ShaderNode<T> 
    {

        private ShaderNode<float> _x;
        
        private readonly ShaderNode<float> _default;
        
        public FromFloat(NodeContext nodeContext, ShaderNode<float> x) : base(nodeContext, "fromFloat")
        {
            _default = new ConstantValue<float>(ShaderNodesUtil.GetContext(nodeContext,0),0);
            
            _x =_default;
            
            SetInputs(new List<AbstractShaderNode>{x});
        }

        public void SetInput(ShaderNode<float> x)
        {
            _x = x ?? _default;
            
            SetInputs(new List<AbstractShaderNode>{x});
        }

        protected override string SourceTemplate()
        {
            var shader = TypeHelpers.GetDimension(typeof(T)) switch
            {
                1 => "float ${resultName} = ${x};",
                2 => "float2 ${resultName} = float2(${x},${x});",
                3 => "float3 ${resultName} = float3(${x},${x},${x});",
                4 => "float4 ${resultName} = float4(${x},${x},${x},${x});",
                _ => ""
            };

            return ShaderNodesUtil.Evaluate(shader, 
                new Dictionary<string, string>
                {
                    {"x", _x.ID}
                });
        }
    }
    
    public class FloatJoinAdaptive<TIn, TExtension, TOut> : ShaderNode<TOut> where TOut : struct
    {

        private ShaderNode<TIn> _input;
        private ShaderNode<TExtension> _extension;

        private readonly ShaderNode<TIn> _defaultIn;
        private readonly ShaderNode<TExtension> _defaultExtension;
        
        public FloatJoinAdaptive(NodeContext nodeContext) : base(nodeContext, "join")
        {
            _input = _defaultIn = ConstantHelper.FromFloat<TIn>(ShaderNodesUtil.GetContext(nodeContext, 0), 0);
            _extension = _defaultExtension = ConstantHelper.FromFloat<TExtension>(ShaderNodesUtil.GetContext(nodeContext, 1), 0);

            SetInputs(new List<AbstractShaderNode>{_input, _extension});
        }

        public void SetInput(ShaderNode<TIn> theInput,ShaderNode<TExtension> theExtension)
        {
            _input = theInput ?? _defaultIn;
            _extension = theExtension ?? _defaultExtension;
            
            SetInputs(new List<AbstractShaderNode>{_input, _extension});
        }

        protected override string SourceTemplate()
        {
            var shader = TypeHelpers.GetDimension(typeof(TOut)) switch
            {
                2 => "float2 ${resultName} = float2(${input},${extension});",
                3 => "float3 ${resultName} = float3(${input},${extension});",
                4 => "float4 ${resultName} = float4(${input},${extension});",
                _ => ""
            };
           

            return ShaderNodesUtil.Evaluate(shader, 
                new Dictionary<string, string>
                {
                    {"input", _input.ID},
                    {"extension", _extension.ID}
                });
        }
    }
    
    public class ToFloat4 : ShaderNode<Vector4>
    {

        private AbstractShaderNode _x;

        private readonly ShaderNode<float> _default;

        public ToFloat4(NodeContext nodeContext, AbstractShaderNode x) : base(nodeContext, "ToFloat")
        {
            _default = new ConstantValue<float>(ShaderNodesUtil.GetContext(nodeContext,0),0);
            _x = x ?? _default;
            
            SetInputs(new List<AbstractShaderNode>{x});
        }

        public void SetInput(AbstractShaderNode x)
        {
            _x = x ?? _default;
            
            SetInputs(new List<AbstractShaderNode>{x});
        }

        protected override string SourceTemplate()
        {
            var shader = _x.Dimension() switch
            {
                1 => "float4 ${resultName} = float4(${x},${x},${x},1.0);",
                2 => "float4 ${resultName} = float4(${x}.xy,0.0,1.0);",
                3 => "float4 ${resultName} = float4(${x}.xyz,1.0);",
                4 => "float4 ${resultName} = ${x};",
                _ => ""
            };

            return ShaderNodesUtil.Evaluate(shader, 
                new Dictionary<string, string>
                {
                    {"x", _x.ID}
                });
        }
    }
}