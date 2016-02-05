/*  [ConsoleWindow]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Win32
{
    /// <summary>The console window.</summary>
    public static class ConsoleWindow
    {
        /// <summary>コンソールウィンドウを表示する<para>
        /// コンソールウィンドウが既に開いているときは何もしない</para></summary>
        /// <returns>コンソールウィンドウのウィンドウハンドル</returns>
        public static IntPtr Open()
        {
            return Open(IntPtr.Zero);
        }

        /// <summary>コンソールウィンドウを表示する<para>
        /// コンソールウィンドウが既に開いているときは何もしない</para></summary>
        /// <param name="hParent">親のウィンドウハンドル</param>
        /// <returns>コンソールウィンドウのウィンドウハンドル</returns>
        public static IntPtr Open(IntPtr hParent)
        {
            IntPtr hWnd = Win32.GetConsoleWindow();
            if (hWnd == IntPtr.Zero)
            {
                // プロセスにコンソールを割り当て、標準出力を接続する
                Win32.AllocConsole();
                var stdout = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
                stdout.AutoFlush = true;
                Console.SetOut(stdout);

                hWnd = Win32.GetConsoleWindow();
                if (hWnd != IntPtr.Zero)
                {
                    ////［閉じる］ボタンの無効化
                    const uint SC_CLOSE = 0x0000F060;
                    const uint MF_BYCOMMAND = 0x00000000;
                    IntPtr hMenu = Win32.GetSystemMenu(hWnd, 0);
                    Win32.RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
                }
            }

            // ウィンドウを背面に移動
            if (hWnd != IntPtr.Zero && hParent != IntPtr.Zero)
            {
                Win32.SetWindowPos(hWnd, hParent, 0, 0, 0, 0,
                    Win32.SetWindowPosFlags.NOMOVE | Win32.SetWindowPosFlags.NOSIZE);
            }

            return hWnd;
        }

        /// <summary>コンソールウィンドウを閉じる<para>
        /// コンソールウィンドウが開いていないときは何もしない</para></summary>
        public static void Close()
        {
            if (Win32.GetConsoleWindow() != IntPtr.Zero)
            {
                Console.SetOut(StreamWriter.Null);
                Win32.FreeConsole();
            }
        }

        /// <summary>コンソールウィンドウが表示されているか？</summary>
        /// <returns>true:コンソールウィンドウが表示されている</returns>
        public static bool IsShow()
        {
            return Win32.GetConsoleWindow() != IntPtr.Zero;
        }

        /// <summary>クラス内で使用するWin32の宣言</summary>
        private class Win32
        {
            /// <summary>ウィンドウの位置オプション(Win32.SetWindowPosメソッドの引数)</summary>
            [Flags]
            public enum SetWindowPosFlags
            {
                /// <summary>現在のサイズを維持する(cxパラメータとcyパラメータを無視する)。</summary>
                NOSIZE = 0x0001,

                /// <summary>現在の位置を維持する(XパラメータとYパラメータを無視する)。</summary>
                NOMOVE = 0x0002,

                /// <summary>現在のZオーダーを維持する(hWndInsertAfterパラメータを無視する)。</summary>
                NOZORDER = 0x0004,

                /// <summary>変更結果を再描画しない。</summary>
                NOREDRAW = 0x0008,

                /// <summary>ウィンドウをアクティブ化しない。</summary>
                NOACTIVATE = 0x0010,

                /// <summary>SetWindowLong関数を使って新しいフレームスタイルの設定を適用する</summary>
                FRAMECHANGED = 0x0020,

                /// <summary>ウィンドウを表示する。</summary>
                SHOWWINDOW = 0x0040,

                /// <summary>ウィンドウを非表示にする。</summary>
                HIDEWINDOW = 0x0080,

                /// <summary>クライアント領域の内容全体を破棄する。</summary>
                NOCOPYBITS = 0x0100,

                /// <summary>オーナーウィンドウのZオーダーを変更しない。</summary>
                NOOWNERZORDER = 0x0200,

                /// <summary>ウィンドウにWM_WINDOWPOSCHANGINGメッセージが送られないようにする。</summary>
                NOSENDCHANGING = 0x400
            }

            [DllImport("kernel32.dll")]
            public static extern bool AllocConsole();

            [DllImport("kernel32.dll")]
            public static extern bool AttachConsole(uint dwProcessId);

            [DllImport("kernel32.dll")]
            public static extern bool FreeConsole();

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetConsoleWindow();

            [DllImport("USER32.DLL")]
            public static extern IntPtr GetSystemMenu(IntPtr hWnd, uint bRevert);

            [DllImport("USER32.DLL")]
            public static extern uint RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

            [DllImport("USER32.DLL")]
            public static extern uint SetWindowPos(
                IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags);
        }
    }
}
