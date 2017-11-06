//Copyright © MadPiranha 2012-2013

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MadPiranha.Plugster.Util
{
    //http://www.liensberger.it/web/blog/?p=207

    [Flags]
    public enum ModifierKeys : uint
    {
        MOD_ALT = 0x1,
        MOD_CONTROL = 0x2,
        MOD_SHIFT = 0x4,
        MOD_WIN = 0x8
    }

    public sealed class HotKeyRegister : IDisposable
    {
        //Disabling this so that only the consumer will consume the key press.
        //public event EventHandler<KeyPressedEventArgs> AnyHotKeyPressed;

        private HotKeyWindow hkWindow;
        private int currentId;
        private System.Collections.Hashtable register;

        #region Constructor

        private static HotKeyRegister hotKeyRegister = new HotKeyRegister();
        public static HotKeyRegister GetHotKeyRegister()
        {
            return hotKeyRegister;
        }

        private HotKeyRegister()
        {
            hkWindow = new HotKeyWindow();
            register = new System.Collections.Hashtable();

            hkWindow.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                IHotKeyConsumer ihkc = (IHotKeyConsumer) register[GetKey(args.Modifier, args.Key)];
                if (ihkc != null)
                {
                    ihkc.HotKeyPressed(args);
                }

                //Disabling this so that only the consumer will consume the key press.
                //if (AnyHotKeyPressed != null)
                //    AnyHotKeyPressed(this, args);
            };
        }

        #endregion

        public bool RegisterHotKey(ModifierKeys modifier, Keys key, IHotKeyConsumer action)
        {
            currentId = currentId + 1;
            bool success = RegisterHotKey(hkWindow.Handle, currentId, (uint)modifier, (uint)key);
            if (success && action != null)
            {

                register.Add(GetKey(modifier, key), action);
            }
            return success;
        }

        private string GetKey(ModifierKeys modifier, Keys key)
        {
            return modifier + ":" + key;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #region IDisposable Members

        public void Dispose()
        {
            for (int i = currentId; i > 0; i--)
            {
                UnregisterHotKey(hkWindow.Handle, i);
            }

            hkWindow.Dispose();
        }

        #endregion


        #region HotKeyWindow class

        private class HotKeyWindow : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;
            private const int WM_ACTIVATEAPP = 0x001C;

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public HotKeyWindow()
            {
                this.CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));

                }

                base.WndProc(ref m);
            }

            public void Dispose()
            {
                this.DestroyHandle();
            }

        }

        #endregion

    }

    #region KeyPressedEventArgs class

    public class KeyPressedEventArgs : EventArgs
    {
        private ModifierKeys _modifier;
        private Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public Keys Key
        {
            get { return _key; }
        }
    }

    #endregion

    #region HotKeyAction Interface

    public interface IHotKeyConsumer
    {
        void HotKeyPressed(KeyPressedEventArgs args);
    }

    #endregion
}
