using System;
using OpenTK;
#if !__MOBILE__
using OpenTK.Graphics.OpenGL;
#else
using OpenTK.Graphics.ES30;
#endif

namespace NSTech2D.RenderEngine
{
    using Vector2 = OpenTK.Vector2;
    public static class Graphics
    {
        public static void SetContext(Window window)
        {
            // on mobile devices, multiple contexts not available
#if !__MOBILE__
            window.context.MakeCurrent();
#endif
        }

        public static void BindFrameBuffer(int frameBufferId)
        {
#if !__MOBILE__
            if (Window.IsObsolete)
            {
                GL.Ext.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferId);
                return;
            }
#endif
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferId);
        }

        internal static void DepthTexture(int width, int height, int depthSize)
        {
            throw new NotImplementedException();
        }

        internal static void FrameBufferDisableDraw()
        {
            throw new NotImplementedException();
        }

        internal static void FrameBufferTexture(int id)
        {
            throw new NotImplementedException();
        }

        internal static void FrameBufferDepthTexture(int id)
        {
            throw new NotImplementedException();
        }

        internal static void TextureSetNearest()
        {
            throw new NotImplementedException();
        }

        internal static int NewFrameBuffer()
        {
            throw new NotImplementedException();
        }

        public static string GetError()
        {
#if !__MOBILE__
            return GL.GetError().ToString();
#else
            return GL.GetErrorCode().ToString();
#endif
        }

        public static void SetAlphaBlending()
        {
            // enable alpha blending
            GL.Enable(EnableCap.Blend);
            GL.BlendEquationSeparate(BlendEquationMode.FuncAdd, BlendEquationMode.FuncAdd);
            GL.BlendFuncSeparate(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);
            GL.ColorMask(true, true, true, true);
        }

        internal static int NewTexture()
        {
            throw new NotImplementedException();
        }

        public static void SetMaskedBlending()
        {
            // enable alpha blending
            GL.Enable(EnableCap.Blend);
            GL.BlendEquationSeparate(BlendEquationMode.FuncAdd, BlendEquationMode.FuncAdd);
            GL.BlendFuncSeparate(BlendingFactorSrc.DstAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.OneMinusSrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.ColorMask(true, true, true, true);
        }

        internal static void TextureSetRepeatX(bool repeat)
        {
            throw new NotImplementedException();
        }

        internal static void TextureSetLinear(bool mipMap)
        {
            throw new NotImplementedException();
        }

        internal static void TextureBitmap(int width, int height, byte[] bitmap, int mipMap)
        {
            throw new NotImplementedException();
        }

        internal static void TextureSetNearest(bool mipMap)
        {
            throw new NotImplementedException();
        }

        internal static void TextureSetRepeatY(bool repeat)
        {
            throw new NotImplementedException();
        }

        public static void Setup()
        {
            SetAlphaBlending();

            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.ScissorTest);

#if !__MOBILE__
            GL.Disable(EnableCap.Multisample);
#endif

            GL.Disable(EnableCap.DepthTest);
        }


        private static bool depthTestEnabled;

        public static string Vendor { get; internal set; }

        public static void EnableDepthTest()
        {
            GL.Enable(EnableCap.DepthTest);
            depthTestEnabled = true;
        }
        public static void DisableDepthTest()
        {
            GL.Disable(EnableCap.DepthTest);
            depthTestEnabled = false;
        }

        public static void TextureGetPixels(int mipMap, byte[] data)
        {
            GL.GetTexImage<byte>(TextureTarget.Texture2D, mipMap, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        }
        public static void DepthTextureGetPixels(int id, int mipMap, float[] data)
        {
            GL.GetTexImage<float>(TextureTarget.Texture2D, mipMap, PixelFormat.Rgba, PixelType.Float, data);
        }

        public static void CullBackFaces()
        {
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
        }
        public static void CullFrontFaces()
        {
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
        }

        public static void EnableMultiSampling()
        {
#if !__MOBILE__
            GL.Enable(EnableCap.Multisample);
#endif
        }

        public static void CullFacesDisable()
        {
            GL.Disable(EnableCap.CullFace);
        }

        public static void ClearColor()
        {
            if (!depthTestEnabled)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);
            }
            else
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            }
        }

        public static void DeleteBuffer(int id)
        {
#if !__MOBILE__
            GL.DeleteBuffer(id);
#else
            GL.DeleteBuffers(1, new int[] {id});
#endif
        }
        public static void DeleteTexture(int id)
        {
            GL.DeleteTexture(id);
        }
        public static void DeleteShader(int id)
        {
            GL.DeleteShader(id);
        }
        public static void DeleteArray(int id)
        {
#if !__MOBILE__
            GL.DeleteVertexArray(id);
#else
            GL.DeleteVertexArrays(1, new int[] { id });
#endif
        }

        public static void Viewport(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        public static void EnableScissorTest()
        {
            GL.Enable(EnableCap.ScissorTest);
        }
        public static void DisableScissorTest()
        {
            GL.Disable(EnableCap.ScissorTest);
        }
        public static void Scissor(int x, int y, int width, int height)
        {
            GL.Scissor(x, y, width, height);
        }

        public static void SetClearColor(float r, float g, float b, float a)
        {
            GL.ClearColor(r, g, b, a);
        }

        public static int GetDefaultFrameBuffer()
        {
            int id = 0;
            //iOS does not use default framebuffer 0
            GL.GetInteger(GetPName.FramebufferBinding, out id);
            return id;
        }
        public static void BindTextureToUnit(int textureId, int unit)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
        }

        public static int NewBuffer()
        {
#if __MOBILE__
            int[] tmpStore = new int[1];
            GL.GenBuffers(1, tmpStore);
            return tmpStore[0];
# else
            return GL.GenBuffer();
#endif
        }
        public static int NewArray()
        {
            int vao = -1;
            // use VAO on modern OpenGL
            if (!Window.IsObsolete)
            {
#if !__MOBILE__
                vao = GL.GenVertexArray();
#else
                int[] tmpStore = new int[1];
				GL.GenVertexArrays(1, tmpStore);
				vao = tmpStore[0];
#endif

            }
            return vao;
        }
        public static void DrawArrays(int amount)
        {
#if !__MOBILE__
            GL.DrawArrays(PrimitiveType.Triangles, 0, amount);
#else
			GL.DrawArrays(BeginMode.Triangles, 0, amount);
#endif
        }

        public static void MapBufferToArray(int bufferId, int index, int elementSize)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, elementSize, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
        }
        public static void MapBufferToIntArray(int bufferId, int index, int elementSize)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, elementSize, VertexAttribPointerType.Int, false, 0, IntPtr.Zero);   
        }

        
    }
}