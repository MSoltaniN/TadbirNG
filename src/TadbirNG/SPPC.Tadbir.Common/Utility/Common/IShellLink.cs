using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// عملیات مورد نیاز برای کار با پوسته سیستم عامل را تعریف می کند
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    public interface IShellLink
    {
        /// <summary>
        /// 
        /// </summary>
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);

        /// <summary>
        /// 
        /// </summary>
        void GetIDList(out IntPtr ppidl);

        /// <summary>
        /// 
        /// </summary>
        void SetIDList(IntPtr pidl);

        /// <summary>
        /// 
        /// </summary>
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);

        /// <summary>
        /// 
        /// </summary>
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        /// <summary>
        /// 
        /// </summary>
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);

        /// <summary>
        /// 
        /// </summary>
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        /// <summary>
        /// 
        /// </summary>
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);

        /// <summary>
        /// 
        /// </summary>
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        /// <summary>
        /// 
        /// </summary>
        void GetHotkey(out short pwHotkey);

        /// <summary>
        /// 
        /// </summary>
        void SetHotkey(short wHotkey);

        /// <summary>
        /// 
        /// </summary>
        void GetShowCmd(out int piShowCmd);

        /// <summary>
        /// 
        /// </summary>
        void SetShowCmd(int iShowCmd);

        /// <summary>
        /// 
        /// </summary>
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

        /// <summary>
        /// 
        /// </summary>
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

        /// <summary>
        /// 
        /// </summary>
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

        /// <summary>
        /// 
        /// </summary>
        void Resolve(IntPtr hwnd, int fFlags);

        /// <summary>
        /// 
        /// </summary>
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}
