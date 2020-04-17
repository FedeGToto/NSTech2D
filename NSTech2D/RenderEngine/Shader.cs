using System;
using System.Collections.Generic;
#if __SHARPDX__
using SharpDX;
using Matrix4 = SharpDX.Matrix;
#else
using OpenTK;
#endif

namespace NSTech2D.RenderEngine
{
    public class Shader : IDisposable
    {
        public class CompilationException : Exception
        {
            public CompilationException(string message) : base(message) { }
        }
        public class UnsupportedVersionException : Exception
        {
            public UnsupportedVersionException(string message) : base(message) { }
        }

        private int programId;
        
        private Dictionary<string, int> uniformChace;
        
        private bool disposed;
        
        public Shader(string vertexModern, string fragmentModern, string vertexObsolete = null, string fragmentObsolete = null, string[] attribs = null, int[]attribsSize = null, string[] vertexUniforms = null, string[] fragmentUniforms = null)
        {
            this.programId = Graphics.CompileShader(vertexModern, fragmentModern, vertexObsolete, fragmentObsolete, attribs, attribsSize, vertexUniforms, fragmentUniforms);
            this.uniformChace = new Dictionary<string, int>();
        }

        public void Use()
        {
            Graphics.BindShader(this.programId);
        }

        private int GetUniform(string name)
        {
            int uid = -1;
            if (this.uniformChace.ContainsKey(name))
            {
                uid = this.uniformChace[name];
            }
            else
            {
                uid = Graphics.GetShaderUniformId(this.programId, name);
                this.uniformChace[name] = uid;
            }
            return uid;
        }

        public void SetUniform(string name, Matrix4 m)
        {
            this.Use();
            int uid = this.GetUniform(name);
            Graphics.SetShaderUniform(uid, m);
        }
        public void SetUniform(string name, int n)
        {
            this.Use();
            int uid = this.GetUniform(name);
            Graphics.SetShaderUniform(uid, n);
        }

        public void SetUniform(string name, float n)
        {
            this.Use();
            int uid = this.GetUniform(name);
            Graphics.SetShaderUniform(uid, n);
        }
        public void SetUniform(string name, Vector4 value)
        {
            this.Use();
            int uid = this.GetUniform(name);
            Graphics.SetShaderUniform(uid, value);
        }
        public void SetUniform(string name, Vector3 value)
        {
            this.Use();
            int uid = this.GetUniform(name);
            Graphics.SetShaderUniform(uid, value);
        }

        public void Dispose()
        {
            if (disposed)
                return;
            Graphics.DeleteShader(this.programId);
            Window.Current.Log($"Shader {this.programId} deleted");
        }
        ~Shader()
        {
            if (disposed)
                return;
            Window.Current.shaderGC.Add(this.programId);
            disposed = true;
        }
    }
}
