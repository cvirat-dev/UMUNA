using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Umuna.Ui.Interop
{
    public class ExternalAppHost : HwndHost
    {
        private IntPtr _containerHandle = IntPtr.Zero; // the child HWND owned by HwndHost
        private IntPtr _appHandle = IntPtr.Zero;       // the external app's main window
        private Process? _process;

        public string? ExecutablePath { get; set; }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            if (string.IsNullOrWhiteSpace(ExecutablePath))
                throw new InvalidOperationException("ExecutablePath must be set.");

            // 1) Create a WS_CHILD container owned by the hwndParent
            _containerHandle = CreateWindowEx(
                0,
                "Static",
                string.Empty,
                WS_CHILD | WS_VISIBLE,
                0, 0, 0, 0,
                hwndParent.Handle,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            if (_containerHandle == IntPtr.Zero)
                throw new InvalidOperationException("Failed to create host container window.");

            // 2) Start external process and wait for its main window
            _process = Process.Start(new ProcessStartInfo
            {
                FileName = ExecutablePath!,
                UseShellExecute = true
            }) ?? throw new InvalidOperationException("Failed to start process.");

            WaitForMainWindow(_process, timeoutMs: 10000);
            _appHandle = _process.MainWindowHandle;

            if (_appHandle == IntPtr.Zero)
                throw new InvalidOperationException("External process did not create a main window.");

            // 3) Make the external window a child and re-parent to our container
            MakeChildWindow(_appHandle);
            SetParent(_appHandle, _containerHandle);

            // 4) Show and size to container
            ShowWindow(_appHandle, SW_SHOW);
            MoveWindow(_appHandle, 0, 0, 1, 1, true); // will be resized in OnWindowPositionChanged

            // Return the CHILD container (not the external window) -> satisfies HwndHost checks
            return new HandleRef(this, _containerHandle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            try
            {
                if (_appHandle != IntPtr.Zero)
                {
                    SendMessage(_appHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    _appHandle = IntPtr.Zero;
                }
            }
            catch { /* ignore best-effort */ }

            if (hwnd.Handle != IntPtr.Zero)
            {
                DestroyWindow(hwnd.Handle);
            }

            if (_process != null && !_process.HasExited)
            {
                try { _process.WaitForExit(3000); } catch { /* ignore */ }
                try { _process.Kill(entireProcessTree: true); } catch { /* ignore */ }
            }
            _process?.Dispose();
            _process = null;
        }

        // Keep hosted app sized to the WPF host
        protected override void OnWindowPositionChanged(Rect rcBoundingBox)
        {
            base.OnWindowPositionChanged(rcBoundingBox);

            if (_appHandle != IntPtr.Zero && _containerHandle != IntPtr.Zero)
            {
                int width = Math.Max(0, (int)Math.Round(rcBoundingBox.Width));
                int height = Math.Max(0, (int)Math.Round(rcBoundingBox.Height));
                MoveWindow(_appHandle, 0, 0, width, height, true);
            }
        }

        private static void WaitForMainWindow(Process process, int timeoutMs)
        {
            var sw = Stopwatch.StartNew();
            try { process.WaitForInputIdle(Math.Min(timeoutMs, 5000)); } catch { /* ignore */ }

            // Repeatedly refresh MainWindowHandle until available or timeout
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                process.Refresh();
                if (process.MainWindowHandle != IntPtr.Zero)
                    break;

                Thread.Sleep(50);
            }
        }

        private static void MakeChildWindow(IntPtr hwnd)
        {
            // Remove top-level decorations and add WS_CHILD
            var style = GetWindowLongPtr(hwnd, GWL_STYLE);
            long newStyle = style.ToInt64();

            newStyle |= WS_CHILD;
            newStyle &= ~WS_POPUP;
            newStyle &= ~WS_CAPTION;
            newStyle &= ~WS_THICKFRAME;
            newStyle &= ~WS_MINIMIZE;
            newStyle &= ~WS_MAXIMIZE;
            newStyle &= ~WS_SYSMENU;

            SetWindowLongPtr(hwnd, GWL_STYLE, new IntPtr(newStyle));
        }

        // Win32

        private const int GWL_STYLE = -16;

        private const int WS_CHILD      = 0x40000000;
        private const int WS_POPUP      = unchecked((int)0x80000000);
        private const int WS_CAPTION    = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_MINIMIZE   = 0x20000000;
        private const int WS_MAXIMIZE   = 0x01000000;
        private const int WS_SYSMENU    = 0x00080000;
        private const int WS_VISIBLE    = 0x10000000;

        private const int SW_SHOW = 5;
        private const int WM_CLOSE = 0x0010;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr CreateWindowEx(
            int dwExStyle,
            string lpClassName,
            string lpWindowName,
            int dwStyle,
            int x, int y, int nWidth, int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}
