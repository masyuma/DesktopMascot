﻿/**
 * WindowController class
 * 
 * Author: Kirurobo, http://twitter.com/kirurobo
 * Copyright: (c) 2014 Kirurobo
 * License: Unlicense
 * 
 * This is free and unencumbered software released into the public domain.
 * 
 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.
 * 
 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 * 
 * For more information, please refer to <http://unlicense.org/>
 */

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Text;

public class WindowController : IDisposable {
	private int WindowVectorX = 768;
	private int WindowVectorY = 576;

	/// <summary>
	/// このウィンドウのハンドル
	/// </summary>
	private IntPtr hWnd = IntPtr.Zero;

	/// <summary>
	/// ウィンドウ操作ができる状態ならtrueを返す
	/// </summary>
	/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
	public bool IsActive { get{return hWnd != IntPtr.Zero;} }

	/// <summary>
	/// ウィンドウが最大化されていればtrue
	/// </summary>
	/// <value><c>true</c> if this instance is maximized; otherwise, <c>false</c>.</value>
	public bool IsMaximized { get {return (IsActive && WinApi.IsZoomed(hWnd));}}

	/// <summary>
	/// ウィンドウが最小化されていればtrue
	/// </summary>
	/// <value><c>true</c> if this instance is minimized; otherwise, <c>false</c>.</value>
	public bool IsMinimized { get {return (IsActive && WinApi.IsIconic(hWnd));}}
	
	/// <summary>
	/// ウィンドウ透過時に最前面に表示するかどうか
	/// </summary>
	public bool TopmostWhenTransparent = false;

    /// <summary>
    /// ウィンドウ位置
    /// </summary>
    public Vector2 NormalWindowPosition;

	/// <summary>
	/// 標準ウィンドウサイズの指定
	/// </summary>
	public Vector2 NormalWindowSize; 

	/// <summary>
	/// 標準ウィンドウクライアント領域のみのサイズ指定
	/// </summary>
	private Vector2 NormalClientSize;

	/// <summary>
	/// 元のウィンドウスタイル
	/// </summary>
	private long NormalWindowStyle;

	/// <summary>
	/// 現在のウィンドウスタイル
	/// </summary>
	private long CurrentWindowStyle;

    /// <summary>
    /// Original extended window style
    /// </summary>
    private long NormalWindowExStyle;

    /// <summary>
    /// Current extended window style
    /// </summary>
    private long CurrentWindowExStyle;

    /// <summary>
    /// 起動時のデスクトップコンポジション状態を記憶。終了時に戻すため
    /// </summary>
    private bool IsCompositionEnabled;


	/// <summary>
	/// ウィンドウ制御のコンストラクタ
	/// </summary>
	public WindowController() {
//		//// ウィンドウハンドル取得
//		////	現在アクティブな物と仮定。アクティブなのが替わってしまうと困る。
//		//FindActiveWindow();
//		
		//// デスクトップコンポジションを有効化（Unityだと強制終了する模様）
		//IsCompositionEnabled = DwmApi.DwmIsCompositionEnabled();
		//DwmApi.DwmEnableComposition (true);
	}

	~WindowController() {
        //// デスクトップコンポジションを起動時の状態に戻す
        //DwmApi.DwmEnableComposition(IsCompositionEnabled);
        Dispose();
	}

	/// <summary>
	/// Stores the size and position of the window.
	/// </summary>
	private void StoreWindowSize() {
		// 今の状態を記憶
		if (IsActive && !WinApi.IsZoomed(hWnd) && !WinApi.IsIconic(hWnd)) {
			// 
			WinApi.RECT rect = new WinApi.RECT();
			WinApi.RECT clientRect = new WinApi.RECT();
			
			// ウィンドウ位置とサイズ
			WinApi.GetWindowRect(hWnd, out rect);
			this.NormalWindowPosition = new Vector2(rect.left, rect.top);
			this.NormalWindowSize = new Vector2(rect.right - rect.left, rect.bottom - rect.top);
			
			// クライアント領域のサイズ
			WinApi.GetClientRect(hWnd, out clientRect);
			this.NormalClientSize = new Vector2(clientRect.right, clientRect.bottom);

			// ウィンドウスタイルを記憶
			this.NormalWindowStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
            this.NormalWindowExStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
		}
	}

	/// <summary>
	/// Sets the window z-order (Topmost or not).
	/// </summary>
	/// <param name="isTop">If set to <c>true</c> is top.</param>
	public void EnableTopmost(bool isTop)
	{
		if (!IsActive) return;
		WinApi.SetWindowPos (
			hWnd,
			(isTop ? WinApi.HWND_TOPMOST : WinApi.HWND_NOTOPMOST),
			0, 0, 0, 0,
			WinApi.SWP_NOSIZE | WinApi.SWP_NOMOVE
			| WinApi.SWP_FRAMECHANGED | WinApi.SWP_NOOWNERZORDER
			| WinApi.SWP_NOACTIVATE | WinApi.SWP_ASYNCWINDOWPOS
			);
	}

    /// <summary>
    /// Sets the window size.
    /// </summary>
    /// <param name="isTop">If set to <c>true</c> is top.</param>
    /// <param name="size">Size.</param>
    public void SetSize(Vector2 size)
	{
		if (!IsActive) return;
		WinApi.SetWindowPos (
			hWnd,
			IntPtr.Zero,
			0, 0, (int)size.x, (int)size.y,
			WinApi.SWP_NOMOVE | WinApi.SWP_NOZORDER
			| WinApi.SWP_FRAMECHANGED | WinApi.SWP_NOOWNERZORDER
			| WinApi.SWP_NOACTIVATE | WinApi.SWP_ASYNCWINDOWPOS
			);
	}

	/// <summary>
	/// Sets the window position.
	/// </summary>
	/// <param name="isTop">If set to <c>true</c> is top.</param>
	/// <param name="position">Position.</param>
	/// <param name="size">Size.</param>
	public void SetPosition(Vector2 position)
	{
		if (!IsActive) return;
		WinApi.SetWindowPos (
			hWnd,
			IntPtr.Zero,
			(int)position.x, (int)position.y, 0, 0,
			WinApi.SWP_NOSIZE | WinApi.SWP_NOZORDER
			| WinApi.SWP_FRAMECHANGED | WinApi.SWP_NOOWNERZORDER
			| WinApi.SWP_NOACTIVATE | WinApi.SWP_ASYNCWINDOWPOS
			);
	}

	/// <summary>
	/// Gets the window size.
	/// </summary>
	/// <returns>The size.</returns>
	public Vector2 GetSize()
	{
		if (!IsActive) return Vector2.zero;
		WinApi.RECT rect = new WinApi.RECT();
		WinApi.GetWindowRect(hWnd, out rect);
		return new Vector2(rect.right - rect.left, rect.bottom - rect.top);
	}

	/// <summary>
	/// Gets the window position.
	/// </summary>
	/// <returns>The position.</returns>
	public Vector2 GetPosition()
	{
		if (!IsActive) return Vector2.zero;
		WinApi.RECT rect = new WinApi.RECT();
		WinApi.GetWindowRect(hWnd, out rect);
		return new Vector2(rect.left, rect.top);
	}

	/// <summary>
	/// 現在のウィンドウスタイルを記憶
	/// </summary>
	private void MemorizeWindowState()
	{
		StoreWindowSize();
		this.CurrentWindowStyle = this.NormalWindowStyle;
        this.CurrentWindowExStyle = this.NormalWindowExStyle;
    }

    /// <summary>
    /// アクティブウィンドウのハンドルを取得
    /// </summary>
    /// <returns><c>true</c>, if window handle was set, <c>false</c> otherwise.</returns>
    public bool FindHandle()
	{
        RestoreWndProc();
		hWnd = WinApi.GetActiveWindow();
        InitWndProc();
		MemorizeWindowState();
		return IsActive;
	}

	/// <summary>
	/// ウィンドウタイトルを元にハンドルを取得
	/// </summary>
	/// <returns><c>true</c>, if handle by title was found, <c>false</c> otherwise.</returns>
	/// <param name="title">Title.</param>
	public bool FindHandleByTitle(string title)
	{
        RestoreWndProc();
		hWnd = WinApi.FindWindow(null, title);
        InitWndProc();
		MemorizeWindowState();
		return IsActive;
	}
	
	/// <summary>
	/// ウィンドウクラスを元にハンドルを取得
	/// </summary>
	/// <returns><c>true</c>, if handle by title was found, <c>false</c> otherwise.</returns>
	/// <param name="title">Title.</param>
	public bool FindHandleByClass(string title)
	{
        RestoreWndProc();
		hWnd = WinApi.FindWindow(title, null);
        InitWndProc();
		MemorizeWindowState();
		return IsActive;
	}

	void Start()
	{
		SetSize(new Vector2(WindowVectorX, WindowVectorY));
		SetPosition(new Vector2(WindowVectorX / 2, WindowVectorY / 2));
	}

	/// <summary>
	/// ウィンドウスタイルを監視して、替わっていれば戻す
	/// </summary>
	public void Update() {
		if (!IsActive) return;
		long style = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
        long exstyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
        if (!WinApi.IsIconic(hWnd) && !WinApi.IsZoomed(hWnd)) {
			if (style != this.CurrentWindowStyle) {
				WinApi.SetWindowLong (hWnd, WinApi.GWL_STYLE, this.CurrentWindowStyle);
				WinApi.ShowWindow(hWnd, WinApi.SW_SHOW);
			}
            if (exstyle != this.CurrentWindowExStyle)
            {
                WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, this.CurrentWindowExStyle);
                WinApi.ShowWindow(hWnd, WinApi.SW_SHOW);
            }
        }
    }

	/// <summary>
	/// ウィンドウ透過をON/OFF
	/// </summary>
	public void EnableTransparency(bool enable) {
		if (!IsActive) return;

		if (enable) {
			// 現在のウィンドウ情報を記憶
			StoreWindowSize();

            // ウィンドウサイズ変更
            if (this.TopmostWhenTransparent)
            {
                EnableTopmost(true);
            }
			SetSize(this.NormalClientSize);

			// 全面をGlassにする
			DwmApi.DwmExtendIntoClientAll (hWnd);

            // 枠無しウィンドウにする
            EnableBorderless(true);
		} else {
			// ウィンドウスタイルを戻す
			EnableBorderless(false);

            // 操作の透過をやめる
            EnableUnfocusable(false);

            // 枠のみGlassにする
            //	※ 本来のウィンドウが枠のみで無かった場合は残念ながら表示が戻りません
            DwmApi.MARGINS margins = new DwmApi.MARGINS (0, 0, 0, 0);
			DwmApi.DwmExtendFrameIntoClientArea (hWnd, margins);

			// ウィンドウサイズ変更
            if (this.TopmostWhenTransparent)
            {
			    EnableTopmost(false);
            }
			SetSize(this.NormalWindowSize);
		}
	
		// ウィンドウ再描画
		WinApi.ShowWindow(hWnd, WinApi.SW_SHOW);
	}

	/// <summary>
	/// ウィンドウの枠を消去/戻す
	/// </summary>
	/// <description></description>
	/// <param name="enable">trueだと枠無し。falseだと標準</param>
	public void EnableBorderless(bool enable) {
		if (!IsActive) return;

		if (enable) {
			// 枠無しウィンドウにする
			//long val = WinApi.GetWindowLong (hWnd, WinApi.GWL_STYLE) & ~WinApi.WS_OVERLAPPEDWINDOW;
			//long val = WinApi.WS_VISIBLE | WinApi.WS_POPUP;
			this.CurrentWindowStyle = this.NormalWindowStyle & ~WinApi.WS_OVERLAPPEDWINDOW;
			WinApi.SetWindowLong (hWnd, WinApi.GWL_STYLE, this.CurrentWindowStyle);
		} else {
			// ウィンドウスタイルを戻す
			this.CurrentWindowStyle = this.NormalWindowStyle;
			WinApi.SetWindowLong (hWnd, WinApi.GWL_STYLE, this.CurrentWindowStyle);
		}
	}

    /// <summary>
    /// Extended window style で操作の透過/戻す
    /// </summary>
    /// <param name="isUnfocusable">If set to <c>true</c> is top.</param>
    public void EnableUnfocusable(bool isUnfocusable)
    {
        if (!IsActive) return;

        if (isUnfocusable)
        {
            long exstyle = this.CurrentWindowExStyle;
            exstyle |= WinApi.WS_EX_TRANSPARENT;
            exstyle |= WinApi.WS_EX_LAYERED;
            this.CurrentWindowExStyle = exstyle;
            WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, this.CurrentWindowExStyle);
        }
        else
        {
            this.CurrentWindowExStyle = this.NormalWindowExStyle;
            WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, this.CurrentWindowExStyle);
        }
    }

	/// <summary>
	/// ウィンドウ最大化
	/// </summary>
	public void Maximize() {
		if (!IsActive) return;
		this.CurrentWindowStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
		WinApi.ShowWindow(hWnd, WinApi.SW_MAXIMIZE);
	}

	/// <summary>
	/// ウィンドウ最小化
	/// </summary>
	public void Minimize() {
		if (!IsActive) return;
		this.CurrentWindowStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
		WinApi.ShowWindow(hWnd, WinApi.SW_MINIMIZE);
	}
	
	/// <summary>
	/// 最大化または最小化したウィンドウを元に戻す
	/// </summary>
	public void Restore() {
		if (!IsActive) return;
		WinApi.ShowWindow(hWnd, WinApi.SW_RESTORE);
		this.CurrentWindowStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
	}

	/// <summary>
	/// Gets the product-name
	/// </summary>
	/// <description>http://gamedev.stackexchange.com/questions/68784/how-do-i-access-the-product-name-in-unity-4</description>
	/// <returns>The project name.</returns>
	public static string GetProjectName() {
		string[] s = Application.dataPath.Split('/');
		string projectName = s[s.Length - 2];
		return projectName;
	}

	#region マウス操作関連
	/// <summary>
	/// マウスカーソルを指定座標へ移動させる
	/// </summary>
	/// <param name="screenPosition">スクリーン上の絶対座標。（Unityのウィンドウが基準では無い）</param>
	public void SetCursorPosition(Vector2 screenPosition) {
		WinApi.SetCursorPos((int)screenPosition.x, (int)screenPosition.y);
	}

	/// <summary>
	/// マウスカーソル座標を取得
	/// </summary>
	/// <returns>スクリーン上の絶対座標。（Unityのウィンドウが基準では無い）</returns>
	public Vector2 GetCursorPosition() {
		WinApi.POINT point;
		WinApi.GetCursorPos(out point);
		return new Vector2(point.x, point.y);
	}

	/// <summary>
	/// マウスの左ボタンが離されたイベントを発生させます
	/// </summary>
	public void SendMouseUp() {
		SendMouseUp(0);
	}

	/// <summary>
	/// マウスのボタンが離されたイベントを発生させます
	/// </summary>
	/// <param name="button">0:左, 1:右, 2:中</param>
	public void SendMouseUp(int button) {
		WinApi.mouse_event(
			button == 1 ? WinApi.MOUSEEVENTF_RIGHTUP
			: button == 2 ? WinApi.MOUSEEVENTF_MIDDLEUP
			: WinApi.MOUSEEVENTF_LEFTUP,
			0, 0, 0, IntPtr.Zero
			);
	}

	/// <summary>
	/// マウスの左ボタンが押されたイベントを発生させます
	/// </summary>
	public void SendMouseDown() {
		SendMouseDown(0);
	}
	
	/// <summary>
	/// マウスのボタンが押されたイベントを発生させます
	/// </summary>
	/// <param name="button">0:左, 1:右, 2:中</param>
	public void SendMouseDown(int button) {
		WinApi.mouse_event(
			button == 1 ? WinApi.MOUSEEVENTF_RIGHTDOWN
			: button == 2 ? WinApi.MOUSEEVENTF_MIDDLEDOWN
			: WinApi.MOUSEEVENTF_LEFTDOWN,
			0, 0, 0, IntPtr.Zero
			);
	}
    #endregion

    #region キー操作関連
    /// <summary>
    /// キーコードを送ります
    /// </summary>
    public void SendKey(KeyCode code)
    {
        WinApi.PostMessage(this.hWnd, WinApi.WM_IME_CHAR, (long)code, IntPtr.Zero);
    }
    #endregion

    #region ファイルドロップ関連

    /// <summary>
    /// 現在のウィンドウがファイルのドラッグアンドドロップを受け入れるかどうかを設定します
    /// </summary>
    /// <param name="accept"></param>
    public void SetDragAcceptFiles(bool accept)
    {
        if (this.IsActive)
        {
            WinApi.DragAcceptFiles(this.hWnd, true);
        }
    }

    public delegate void FilesDropped(string[] files);
    public event FilesDropped OnFilesDropped;

    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    private IntPtr newWndProcPtr = IntPtr.Zero;
    private IntPtr oldWndProcPtr = IntPtr.Zero;
    private WndProcDelegate newWndProc = null;

    // 参考 https://qiita.com/DandyMania/items/d1404c313f67576d395f
    private IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WinApi.WM_DROPFILES)
        {
            IntPtr hDrop = wParam;
            uint num = WinApi.DragQueryFile(hDrop, 0xFFFFFFFF, null, 0);
            string[] files = new string[num];

            uint bufferSize = 1024;
            StringBuilder path = new StringBuilder((int)bufferSize);
            for (uint i = 0; i < num; i++)
            {
                uint size = WinApi.DragQueryFile(hDrop, i, path, bufferSize);
                files[i] = path.ToString();
                path.Length = 0;
            }

            WinApi.DragFinish(hDrop);

            if (OnFilesDropped != null)
            {
                OnFilesDropped(files);
            }
        }

        //try {
        //    return WinApi.CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
        //} catch
        //{
        //    //return WinApi.DefWindowProc(hWnd, msg, wParam, lParam);
        //    return IntPtr.Zero;
        //}

        return WinApi.CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
    }

    /// <summary>
    /// ウィンドウプロシージャをフック
    /// </summary>
    private void InitWndProc()
    {
        if (this.IsActive)
        {
            newWndProc = new WndProcDelegate(wndProc);
            newWndProcPtr = Marshal.GetFunctionPointerForDelegate(newWndProc);
            oldWndProcPtr = WinApi.SetWindowLongPtr(this.hWnd, WinApi.GWLP_WNDPROC, newWndProcPtr);

            WinApi.DragAcceptFiles(this.hWnd, true);
        }
    }

    /// <summary>
    /// ウィンドウプロシージャを戻す
    /// </summary>
    private void RestoreWndProc()
    {
        if (newWndProc != null && oldWndProcPtr != IntPtr.Zero)
        {
            WinApi.SetWindowLongPtr(this.hWnd, WinApi.GWLP_WNDPROC, oldWndProcPtr);
            oldWndProcPtr = IntPtr.Zero;
            newWndProcPtr = IntPtr.Zero;
            newWndProc = null;

            // Unityだと通常はドラッグ不可
            //SetDragAcceptFiles(false);
        }
    }

    /// <summary>
    /// 破棄時にはウィンドウプロシージャを戻す必要がある
    /// </summary>
    public void Dispose()
    {
        RestoreWndProc();
    }

    #endregion
}
