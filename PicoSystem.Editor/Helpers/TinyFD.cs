using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Editor.Helpers
{
    public class TinyFD
    {
        public enum MessageDialogType
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum MessageDialogIconType
        {
            Info,
            Warning,
            Error,
            Question
        }

        public enum MessageDialogDefaultButton
        {
            Cancel_No = 0,
            Ok_Yes = 1,
            No_In_YesNoCancel = 2
        }


        private const string LibDLL = "tinyfiledialogs64.dll";

        // cross platform utf8
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int tinyfd_messageBox(string aTitle, string aMessage, string aDialogTyle, string aIconType, int aDefaultButton);
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tinyfd_inputBox(string aTitle, string aMessage, string aDefaultInput);
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tinyfd_saveFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription);
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tinyfd_openFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription, int aAllowMultipleSelects);
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tinyfd_selectFolderDialog(string aTitle, string aDefaultPathAndFile);
        [DllImport(LibDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tinyfd_colorChooser(string aTitle, string aDefaultHexRGB, byte[] aDefaultRGB, byte[] aoResultRGB);


        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="message">May contain \n.</param>
        /// <param name="dialogType">Ok, OkCancel, YesNo, YesNoCancel.</param>
        /// <param name="iconType">Info, Warning, Error, Question.</param>
        /// <param name="defaultButton">Cancel_No, Ok_Yes, No_In_Yes_No_Cancel.</param>
        /// <returns>If Type is OkCancel or YesNo returns true when clicking OK or YES otherwise returns false.</returns>
        public static bool MessageBox(string title, string message, MessageDialogType dialogType,
            MessageDialogIconType iconType, MessageDialogDefaultButton defaultButton)
        {
            string dialogTypeStr;
            string iconTypeStr;
           

            switch (dialogType)
            {
                default:
                case MessageDialogType.Ok: dialogTypeStr = "ok"; break;
                case MessageDialogType.OkCancel: dialogTypeStr = "okcancel"; break;
                case MessageDialogType.YesNo: dialogTypeStr = "yesno"; break;
                case MessageDialogType.YesNoCancel: dialogTypeStr = "yesnocancel"; break;
            }

            switch (iconType)
            {
                default:
                case MessageDialogIconType.Info: iconTypeStr = "info"; break;
                case MessageDialogIconType.Warning: iconTypeStr = "warning"; break;
                case MessageDialogIconType.Error: iconTypeStr = "error"; break;
                case MessageDialogIconType.Question: iconTypeStr = "question"; break;
            }

            int result = tinyfd_messageBox(title, message, dialogTypeStr, iconTypeStr, (int)defaultButton);

            return result == 1;
        }


        /// <summary>
        /// InputBox
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="message">Dialog Message</param>
        /// <param name="defaultInput">Default Input when opening</param>
        /// <returns>Returns the contents of the input field or NULL on cancel</returns>
        public static string InputBox(string title, string message, string defaultInput)
        {
            IntPtr result = tinyfd_inputBox(title, message, defaultInput);

            return (result != IntPtr.Zero) ? Marshal.PtrToStringAnsi(result) : null;
        }

        /// <summary>
        /// SaveFileDialog
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="defaultPathAndFile">Default Path and File</param>
        /// <param name="numOfFilterPatterns">Number of filter patterns</param>
        /// <param name="filterPatterns">Array of filter patterns</param>
        /// <param name="singleFilterDescription">Filter description</param>
        /// <returns>Path of file to save or NULL on cancel.</returns>
        public static string SaveFileDialog(string title, string defaultPathAndFile, int numOfFilterPatterns, string[] filterPatterns, string singleFilterDescription)
        {
            IntPtr result = tinyfd_saveFileDialog(title, defaultPathAndFile, numOfFilterPatterns, filterPatterns,
                singleFilterDescription);

            return (result != IntPtr.Zero) ? Marshal.PtrToStringAnsi(result) : null;
        }

        /// <summary>
        /// OpenFileDialog
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="defaultPathAndFile">Default path and file</param>
        /// <param name="numOfFilterPatterns">Number of filter patterns</param>
        /// <param name="filterPatterns">Array of filter patterns</param>
        /// <param name="singleFilterDescription">Single filter description</param>
        /// <param name="allowMultipleSelects">Allow mutliple file selects</param>
        /// <returns>Path of file to open. On case of multiple files , paths are separated with '|'. In case of cancel returns NULL</returns>
        public static string OpenFileDialog(string title, string defaultPathAndFile, int numOfFilterPatterns, string[] filterPatterns, string singleFilterDescription, bool allowMultipleSelects)
        {
            IntPtr result = tinyfd_openFileDialog(title, defaultPathAndFile, numOfFilterPatterns, filterPatterns,
                singleFilterDescription, allowMultipleSelects ? 1 : 0);

            return (result != IntPtr.Zero) ? Marshal.PtrToStringAnsi(result) : null;
        }

        /// <summary>
        /// SelectFolderDialog
        /// </summary>
        /// <param name="title">Folder Title</param>
        /// <param name="defaultPathAndFile">Default Path</param>
        /// <returns>Selected Folder Path or NULL on cancel.</returns>
        public static string SelectFolderDialog(string title, string defaultPathAndFile)
        {
            IntPtr result = tinyfd_selectFolderDialog(title, defaultPathAndFile);

            return (result != IntPtr.Zero) ? Marshal.PtrToStringAnsi(result) : null;
        }

        /// <summary>
        /// ColorChooser
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="defaultHexRGB">Default HEX RGB , NULL or "#FF0000"</param>
        /// <param name="defaultRGB"> {0, 255, 255}. Only used if defaultHexRGB is NULL</param>
        /// <param name="resultRGB">Contains the results as an array of bytes</param>
        /// <returns>Returns the hexcolor as string '#Ff0000' or NULL on cancel.</returns>
        public static string ColorChooser(string title, string defaultHexRGB, byte[] defaultRGB, byte[] resultRGB)
        {
            IntPtr result = tinyfd_colorChooser(title, defaultHexRGB, defaultRGB, resultRGB);

            return (result != IntPtr.Zero) ? Marshal.PtrToStringAnsi(result) : null;
        }

    }
}
