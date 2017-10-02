﻿using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ReportApplication.Common
{
    public static class EasyDialog
    {
        /// <summary>
        /// Hiện msg box (yes/no)
        /// </summary>
        /// <param name="caption">Nội dung cần thông báo</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowYesNoDialog(string caption)
        {
            return XtraMessageBox.Show(caption, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Hiện msg box thông báo thao tác thành công
        /// </summary>
        /// <param name="action">Lưu/Xóa/Sửa/...</param>
        /// <returns>DialogResult</returns>
        public static void ShowSuccessfulDialog(string action)
        {
            XtraMessageBox.Show(action, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hiện msg box thông báo thao tác không thành công
        /// </summary>
        /// <param name="action">Lưu/Xóa/Sửa/...</param>
        /// <returns>DialogResult</returns>
        public static void ShowUnsuccessfulDialog(string action)
        {
            XtraMessageBox.Show(action, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        ///  Hiện msg box thông báo lỗi
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static DialogResult ShowErrorDialog(string caption)
        {
            return XtraMessageBox.Show(caption, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hiện msg box cảnh báo
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static DialogResult ShowWarningDialog(string caption)
        {
            return XtraMessageBox.Show(caption, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowInfoDialog(string caption)
        {
            return XtraMessageBox.Show(caption, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowYesNoCancelDialog(string caption)
        {
            return XtraMessageBox.Show(caption, "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}