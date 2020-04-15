using System;
using System.Collections.Generic;
using OpenTK;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OpenTK.Input;
using System.Runtime.InteropServices;

namespace NSTech2D.RenderEngine
{
    public enum KeyCode
    {
        A = Key.A,
        B = Key.B,
        C = Key.C,
        D = Key.D,
        E = Key.E,
        F = Key.F,
        G = Key.G,
        H = Key.H,
        I = Key.I,
        J = Key.J,
        K = Key.K,
        L = Key.L,
        M = Key.M,
        N = Key.N,
        O = Key.O,
        P = Key.P,
        Q = Key.Q,
        R = Key.R,
        S = Key.S,
        T = Key.T,
        U = Key.U,
        V = Key.V,
        W = Key.W,
        X = Key.X,
        Y = Key.Y,
        Z = Key.Z,

        SpaceBar = Key.Space,
        Enter = Key.Enter,
        Esc = Key.Escape,

        ArrowUp = Key.Up,
        ArrowDown = Key.Down,
        ArrowLeft = Key.Left,
        ArrowRight = Key.Right,

        LShift = Key.ShiftLeft,
        RShift = Key.ShiftRight,

        LCtrl = Key.ControlLeft,
        RCtrl = Key.ControlRight,

        Keypad0 = Key.Keypad0,
        Keypad1 = Key.Keypad1,
        Keypad2 = Key.Keypad2,
        Keypad3 = Key.Keypad3,
        Keypad4 = Key.Keypad4,
        Keypad5 = Key.Keypad5,
        Keypad6 = Key.Keypad6,
        Keypad7 = Key.Keypad7,
        Keypad8 = Key.Keypad8,
        Keypad9 = Key.Keypad9,

        Number0 = Key.Number0,
        Number1 = Key.Number1,
        Number2 = Key.Number2,
        Number3 = Key.Number3,
        Number4 = Key.Number4,
        Number5 = Key.Number5,
        Number6 = Key.Number6,
        Number7 = Key.Number7,
        Number8 = Key.Number8,
        Number9 = Key.Number9,

        F1 = Key.F1,
        F2 = Key.F2,
        F3 = Key.F3,
        F4 = Key.F4,
        F5 = Key.F5,
        F6 = Key.F6,
        F7 = Key.F7,
        F8 = Key.F8,
        F9 = Key.F9,
        F10 = Key.F10,
        F11 = Key.F11,
        F12 = Key.F12,

        Tab = Key.Tab,
    }
    public partial class Window
    {
        public GameWindow context;

        private Stopwatch watch;

        public static string[] Displays
        {
            get
            {
                List<string> displays = new List<string>();
                for (int i = 0; i < (int)DisplayIndex.Sixth; i++)
                {
                    DisplayDevice device = DisplayDevice.GetDisplay((DisplayIndex)i);
                    if (device != null)
                        displays.Add(device.ToString());
                }
                return displays.ToArray();
            }
        }

        public static Vector2[] Resolutions
        {
            get
            {
                List<Vector2> resolutions = new List<Vector2>();
                foreach (DisplayResolution resolution in DisplayDevice.Default.AvailableResolutions)
                {
                    resolutions.Add(new Vector2(resolution.Width, resolution.Height));
                }
                return resolutions.ToArray();
            }
        }

        public bool SetResolution(int screenWidth, int screenHeight)
        {
            return SetResolution(new Vector2(screenWidth, screenHeight));
        }
        public bool SetResolution(Vector2 newResolution)
        {
            foreach (DisplayResolution resolution in DisplayDevice.Default.AvailableResolutions)
            {
                if (resolution.Width == newResolution.X && resolution.Height == newResolution.Y)
                {
                    DisplayDevice.Default.ChangeResolution(resolution);
                    this.FixDimensions(resolution.Width, resolution.Height);
                    return true;
                }
            }
            return false;
        }

        public Vector2 Position
        {
            get
            {
                Point location = this.context.Location;
                return new Vector2(location.X, location.Y);
            }

            set
            {
                this.context.Location = new Point((int)value.X, (int)value.Y);
            }
        }

        public Window(string title, int depthSize = 16, int antialiasingSamples = 0, int stencilBuffers = 0) : this(DisplayDevice.Default.Width, DisplayDevice.Default.Height, title, true, depthSize, antialiasingSamples, stencilBuffers)
        {

        }

        public Window(int width, int height, string title, bool fullscreen = false, int depthSize = 16, int antialiasingSamples = 0, int stencilBuffers = 0)
        {
            // force OpenGL 3.3 this is a good compromise
            int major = 3;
            int minor = 3;
            if (obsoleteMode)
            {
                major = 2;
                minor = 2;
            }

            this.context = new GameWindow(width, height, new OpenTK.Graphics.GraphicsMode(32, depthSize, stencilBuffers, antialiasingSamples), title,
                fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow,
                DisplayDevice.Default, major, minor, OpenTK.Graphics.GraphicsContextFlags.Default);

            if (fullscreen)
            {
                this.context.Location = new Point(0, 0);
            }

            watch = new Stopwatch();

            // Enable VSync by default
            this.SetVSync(true);

            FixDimensions(width, height, true);

            this.context.Closed += new EventHandler<EventArgs>(this.Close);
            this.context.Move = +(sender, e) =>
            {
                // avoid delta time to become huge while move
                this.watch.Stop();
            };
            this.context.Visible = true;

            // initialize graphics subsystem
            Graphics.Setup();

            if (antialiasingSamples > 0)
            {
                Graphics.EnableMultisampling();
            }

            FinalizeSetup();

            // hack for getting the default framebuffer
            ResetFrameBuffer();
        }

        public void SetVSync(bool enable)
        {
            if (enable)
            {
                this.context.VSync = VSyncMode.On;
            } else
            {
                this.context.VSync = VSyncMode.Off;
            }
        }

        private void Close(object sender, EventArgs args)
        {
            this.opened = false;
        }

        public bool HasFocus
        {
            get
            {
                return this.context.Focused;
            }
        }

        public static byte[] LoadImage(string fileName, bool premultiplied, out int width, out int height)
        {
            throw new NotImplementedException();
        }
        public static byte[] LoadImage(Stream imageStream, bool premultiplied, out int width, out int height)
        {
            throw new NotImplementedException();
        }
    }
}
