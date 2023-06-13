using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WindowsMaximizer;
using static WindowsMaximizer.Helper;

class Program
{
    static void Main()
    {

        Windows.EnumWindows((IntPtr hWnd, IntPtr lParam) =>
        {
            if (Windows.IsWindowVisible(hWnd))
            {
                int length = Windows.GetWindowTextLength(hWnd);
                if (length == 0)
                    return true;

                StringBuilder sb = new StringBuilder(length + 1);
                Windows.GetWindowText(hWnd, sb, sb.Capacity);

                string windowTitle = sb.ToString();
                IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

                if (hWnd != handle && !string.IsNullOrEmpty(windowTitle))
                {
                    RECT windowRect;
                    Windows.GetWindowRect(hWnd, out windowRect);

                    int windowWidth = windowRect.Right - windowRect.Left;
                    int windowHeight = windowRect.Bottom - windowRect.Top;

                    // Check if window is smaller than a threshold size
                    if (windowWidth > 100 && windowHeight > 100)
                    {
                        WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                        placement.length = Marshal.SizeOf(placement);
                        Windows.GetWindowPlacement(hWnd, out placement);

                        placement.showCmd = SW_SHOWMAXIMIZED;
                        Windows.SetWindowPlacement(hWnd, ref placement);

                        int style = Windows.GetWindowLong(hWnd, GWL_STYLE);
                        if ((style & WS_BORDER) != 0 || (style & WS_THICKFRAME) != 0)
                            Windows.SetWindowLong(hWnd, GWL_STYLE, style & ~(WS_BORDER | WS_THICKFRAME));
                    }
                }
            }
            return true;
        }, IntPtr.Zero);

        Console.WriteLine("Program is running in the background. Press any key to exit...");
        Console.ReadLine();
    }
}