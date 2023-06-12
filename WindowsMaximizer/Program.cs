using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WindowsMaximizer;

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
                Helper.WINDOWPLACEMENT placement = new Helper.WINDOWPLACEMENT();
                Windows.GetWindowPlacement(hWnd, out placement);

                placement.length = Marshal.SizeOf(placement);
                placement.showCmd = Helper.SW_SHOWMAXIMIZED;
                Windows.SetWindowPlacement(hWnd, ref placement);

                int style = Windows.GetWindowLong(hWnd, Helper.GWL_STYLE);
                if ((style & Helper.WS_BORDER) != 0 || (style & Helper.WS_THICKFRAME) != 0)
                    Windows.SetWindowLong(hWnd, Helper.GWL_STYLE, style & ~(Helper.WS_BORDER | Helper.WS_THICKFRAME));
            }
        }
        return true;
    }, IntPtr.Zero);

    Console.WriteLine("Program is running in the background. Press any key to exit...");
    Console.ReadKey();
}