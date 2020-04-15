using System;
using System.Collections.Generic;
using System.Runtime;
#if __SHARPDX__
using SharpDX;
using Matrix4 = SharpDX.Matrix;
#else
using OpenTK;
#endif

namespace NSTech2D.RenderEngine
{
    public partial class Window
    {
        private Matrix4 projectionMatrix;
        private float _aspectRatio;

        public Matrix4 ProjectionMatrix
        {
            get
            {
                return this.projectionMatrix;
            }
        }
        public float aspectRatio
        {
            get
            {
                return this._aspectRatio;
            }
        }

        private int width;
        private int height;

        private Vector2 viewportPosition;
        private Vector2 viewportSize;

        public Vector2 CurrentViewportPosition
        {
            get
            {
                return viewportPosition;
            }
        }
        public Vector2 CurrentViewportSize
        {
            get
            {
                return viewportSize;
            }
        }

        private float currentOrthographicSize;

        public float CurrentOrthographicSize
        {
            get
            {
                return currentOrthographicSize;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int ScaledWidth
        {
            get
            {
                return (int)(this.width * this.scaleX);
            }
        }
        public int ScaledHeight
        {
            get
            {
                return (int)(this.height * this.scaleY);
            }
        }

        public float OrthoWidth
        {
            get
            {
                if (this.currentOrthographicSize > 0)
                    return this.currentOrthographicSize * this._aspectRatio;
                return this.width;
            }
        }
        public float OrthoHeight
        {
            get
            {
                if (this.currentOrthographicSize > 0)
                    return this.currentOrthographicSize;
                return this.height;
            }
        }

        public string Version
        {
            get
            {
                return Graphics.Version;
            }
        }
        public string Vendor
        {
            get
            {
                return Graphics.Vendor;
            }
        }

        public string SLVersion
        {
            get
            {
                return Graphics.SLVersion;
            }
        }
        public string Renderer
        {
            get
            {
                return Graphics.Renderer;
            }
        }
        public string Extensions
        {
            get
            {
                return Graphics.Extensions;
            }
        }

        private int defaultFrameBuffer;
        private bool collectedDefaultFrameBuffer;

        protected float zNear;
        protected float zFar;

        public void SetZNearZFar(float near, float far)
        {
            zNear = near;
            zFar = far;
            this.SetViewport(0, 0, this.width, this.height);
        }
        public float ZFar { get { return zFar; } }
        public float ZNear { get { return zNear; } }

        public void EnableDepthTest()
        {
            Graphics.EnableDepthTest();
        }
        public void DisableDepthTest()
        {
            Graphics.DisableDepthTest();
        }

        public void CullFrontFaces()
        {
            Graphics.CullFrontFaces();
        }
        public void CullBackFaces()
        {
            Graphics.CullBackFaces();
        }
        public void DisableCullFaces()
        {
            Graphics.CullFacesDisable();
        }

        private float defaultOrthographicSize;
        public void SetDefaultOrthographicSize(float value)
        {
            defaultOrthographicSize = value;
            this.SetViewport(0, 0, this.width, this.height);
        }

        private static Window current;
        public static Window Current { get { return Window.current; } }
        public static void SetCurrent(Window targetWindow)
        {
            current = targetWindow;
            Graphics.SetContext(current);
        }
        public void SetCurrent()
        {
            Window.SetCurrent(this);
        }

        public bool opened = true;
        public bool IsOpened
        {
            get
            {
                return opened;
            }
        }

        public void Exit(int code = 0)
        {
            System.Environment.Exit(code);
        }

        //used for dpi management
        private float scaleX;
        private float scaleY;

        private float _deltaTime;
        public float deltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

        #region Log
        private ILogger logger;

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }
        public void Log(string message)
        {
            if (logger == null)
                return;
            logger.Log(message);
        }
        #endregion

        #region Camera
        // TODO: create camera object
        #endregion

        #region Obsolete Mode
        private static bool obsoleteMode;
        public static void SetObsoleteMode()
        {
            obsoleteMode = true;
        }
        public static bool IsObsolete
        {
            get
            {
                return obsoleteMode;
            }
        }
        #endregion

        #region Post Processing
        // TODO: Create post processing
        #endregion

        public void SetClearColor(float r, float g, float b, float a = 1)
        {
            Graphics.SetClearColor(r, g, b, a);
        }
        public void SetClearColor(int r, int g, int b, int a = 255)
        {
            SetClearColor(r / 255f, g / 255f, b / 255f, a / 255f);
        }
        public void SetClearColor(Vector4 color)
        {
            SetClearColor(color.X, color.Y, color.Z, color.W);
        }

        public void ClearColor()
        {
            Graphics.ClearColor();
        }

        #region Scissor
        public void SetScissorTest(bool enabled)
        {
            if (enabled)
            {
                Graphics.EnableScissorTest();
            }
            else
            {
                Graphics.DisableScissorTest();
            }
        }
        public void SetScissorTest(int x, int y, int width, int height)
        {
            SetScissorTest(true);
            Graphics.Scissor((int)(x * this.scaleX),
                                (int)(((this.height - y) - height) * this.scaleY),
                                (int)(width * this.scaleX),
                                (int)(height * this.scaleY));
        }
        public void SetScissorTest(float x, float y, float width, float height)
        {
            SetScissorTest((int)x, (int)y, (int)width, (int)height);
        }
        #endregion

        private int GetDefaultFrameBuffer()
        {
            //Post Processing stuff
            if (!collectedDefaultFrameBuffer)
            {
                defaultFrameBuffer = Graphics.GetDefaultFrameBuffer();
                collectedDefaultFrameBuffer = true;
            }
            return defaultFrameBuffer;
        }
        public void BindTextureToUnit(Texture texture, int unit)
        {
            Graphics.BindTextureToUnit(texture.Id, unit);
        }

        #region Garbage Collector
        public List<int> textureGC = new List<int>();
        public List<int> bufferGC = new List<int>();
        public List<int> vaoGC = new List<int>();
        public List<int> shaderGC = new List<int>();

        private void RunGraphicsGC()
        {
            // Destroy useless resources
            for (int i = 0; i < bufferGC.Count; i++)
            {
                int _id = this.bufferGC[i];
                Graphics.DeleteBuffer(_id);
                this.Log($"buffer {_id} deleted");
            }
            this.bufferGC.Clear();

            for (int i = 0; i < vaoGC.Count; i++)
            {
                int _id = this.vaoGC[i];
                Graphics.DeleteArray(_id);
                this.Log($"Array {_id} deleted");
            }
            this.vaoGC.Clear();

            for (int i = 0; i < textureGC.Count; i++)
            {
                int _id = this.textureGC[i];
                Graphics.DeleteTexture(_id);
                this.Log($"texture {_id} deleted");
            }
            this.textureGC.Clear();

            for (int i = 0; i < shaderGC.Count; i++)
            {
                int _id = this.shaderGC[i];
                Graphics.DeleteShader(_id);
                this.Log($"shader {_id} deleted");
            }
            this.shaderGC.Clear();
        }
        #endregion

        public void ResetFrameBuffer()
        {
            Graphics.BindFrameBuffer(GetDefaultFrameBuffer());
            ClearColor();
        }

        public void SetViewport(int x, int y, int width, int height, float orthoSize = 0, bool virtualScreen = false)
        {
            if (zNear == 0 && zFar == 0)
            {
                zNear = -1;
                zFar = -1;
            }
            // Store values before changes
            this.viewportPosition = new Vector2(x, y);
            this.viewportSize = new Vector2(width, height);
            // Fix y as it is downsized in OpenGL
            y = (this.height - y) - height;
            if (virtualScreen)
            {
                Graphics.Viewport((int)(x * this.scaleX),
                                    (int)(y * this.scaleY),
                                    (int)(width * this.scaleX),
                                    (int)(height * this.scaleY));
            }
            this._aspectRatio = (float)width / (float)height;

            if (orthoSize == 0)
                orthoSize = this.defaultOrthographicSize;

            // use units instead of pixels

#if __SHARPDX__
            if (orthoSize > 0) 
            {
                Matrix4.OrthoOffCenterRH(0, orthoSize * this._aspectRatio, orthoSize, 0, zNear, zFar, out this.projectionMatrix);
            } else {
                Matrix4.OrthoOffCenterRH(0, width, height, 0, zNear, zFar, out this.projectionMatrix);
            }
#else
            if (orthoSize > 0)
            {
                this.projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, orthoSize * this._aspectRatio, orthoSize, 0, zNear, zFar);
            } else
            {
                this.projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, zNear, zFar);
            }
#endif
            this.currentOrthographicSize = orthoSize;
        }

        public void RenderTo(RenderTexture renderTexture, bool clear = true, float orthoSize = 0)
        {
            if (renderTexture == null)
            {
                Graphics.BindFrameBuffer(GetDefaultFrameBuffer());
                SetViewport(0, 0, this.width, this.height);
                return;
            }
            else
            {
                Graphics.BindFrameBuffer(renderTexture.FrameBuffer);
                SetViewport(0, 0, renderTexture.Width, renderTexture.Height, orthoSize, true);
            }

            if (clear)
                this.ClearColor();
        }

        private void FinalizeSetup()
        {
            // more gentle GC
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;
            //Post Pocessing

            Window.SetCurrent(this);
        }

        public void SetAlphaBlending()
        {
            Graphics.SetAlphaBlending();
        }

        public void SetMaskedBlending()
        {
            SetMaskedBlending();
        }
    }
}