using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VSON.Windows
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("8680ed94-2a84-4b75-9dd1-d889d6473a67")]
    public class VSONPreview : IPreviewHandler
    {
        private IntPtr PreviewWindowHandle;
        private RECT PreviewWindowRect;

        public void SetWindow(IntPtr hwnd, ref RECT rect)
        {
            // Set up the preview window

            // Save the handle to the preview window
            PreviewWindowHandle = hwnd;

            // Save the rectangle information (if needed)
            PreviewWindowRect = rect;

            /*// Create a child window
            IntPtr childWindowHandle = CreateWindowEx(
                0,                  // Extended window style
                "STATIC",           // Window class name (STATIC for a label)
                "Preview Content",  // Window caption
                WS_CHILD | WS_VISIBLE,  // Window style (child, visible)
                rect.Left,          // X coordinate
                rect.Top,           // Y coordinate
                rect.Right - rect.Left, // Width
                rect.Bottom - rect.Top, // Height
                hwnd,               // Parent window handle
                IntPtr.Zero,        // Menu handle
                IntPtr.Zero,        // Instance handle
                IntPtr.Zero         // Param for WM_CREATE
            );

            // Set the background color of the child window (optional)
            SetClassLong(childWindowHandle, GCL_HBRBACKGROUND, (IntPtr)GetStockObject(WHITE_BRUSH));

            // Display the label in the child window (optional)
            SendMessage(childWindowHandle, WM_SETTEXT, 0, "Hello, Preview!");

            // Perform any additional setup or initialization here*/
        }

        // Constants for window styles and messages
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int GCL_HBRBACKGROUND = -10;
        private const int WM_SETTEXT = 0x000C;

        // Constants for stock objects
        private const int WHITE_BRUSH = 0;

        // External methods for window creation and manipulation
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateWindowEx(
            int dwExStyle, string lpClassName, string lpWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight,
            IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetClassLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr GetStockObject(int fnObject);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);


        public void SetRect(ref RECT rect)
        {
            PreviewWindowRect = rect;
        }

        public void DoPreview()
        {
            // Assuming that you have access to the file content
            //string fileContent = GetFileContent(); // Implement this method to retrieve file content

            // Display "Hello World" in the preview pane
            DisplayTextInPreview("Hello World");
        }

        // Method to display text in the preview pane
        private void DisplayTextInPreview(string text)
        {
            // Assuming you have the handle to the preview window
            // (saved in the SetWindow method)
            if (PreviewWindowHandle != IntPtr.Zero)
            {
                // Send a message to set the text in the preview pane
                SendMessage(PreviewWindowHandle, WM_SETTEXT, 0, text);
            }
        }

        public void Unload()
        {
            // Clean up resources
        }

        public void SetFocus()
        {
            // Set Focus to Preview
        }

        public void QueryFocus(out IntPtr phwnd)
        {
            // Return the handle of the window that has focus (if any)
            phwnd = IntPtr.Zero; // Set to IntPtr.Zero if no specific window has focus

            // Implement your logic to determine the focused window handle
            // For example, you might get the focused control within your preview handler
            // and retrieve its handle using the Handle property.

            // Example:
            // Control focusedControl = GetFocusedControl();
            // phwnd = focusedControl.Handle;
        }

        public uint TranslateAccelerator(ref MSG pmsg)
        {
            // Handle keyboard accelerators here
            // Return S_OK (0) if the accelerator is processed; otherwise, return S_FALSE (1)

            // Example: Handle Ctrl+C as a copy command
            if (pmsg.message == 0x100) // WM_KEYDOWN
            {
                bool ctrlKeyPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                bool cKeyPressed = (int)pmsg.wParam == (int)Keys.C;

                if (ctrlKeyPressed && cKeyPressed)
                {
                    // Process Ctrl+C as a copy command
                    // Implement your copy logic here

                    // Return S_OK to indicate that the accelerator was processed
                    return 0; // S_OK
                }
            }

            // Return S_FALSE to indicate that the accelerator was not processed
            return 1; // S_FALSE
        }

    }
}
