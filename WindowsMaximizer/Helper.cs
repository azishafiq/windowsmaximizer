using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsMaximizer
{
    public class Helper
    {
        // Window style constants
        public const int GWL_STYLE = -16;
        public const int WS_BORDER = 0x00800000;
        public const int WS_THICKFRAME = 0x00040000;

        // Window placement constants
        public const int SW_SHOWMAXIMIZED = 3;

        // Delegate for EnumWindows
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // Struct representing window placement
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
        // Struct representing window rectangle
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
