using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace StaffSync
{
    public static class FocusManager
    {
        public static bool EnableHighlighting { get; set; } = true;

        public static bool ShowNavigationError { get; set; } = true;

        private static readonly Dictionary<Control, Color> _originalColors =
            new Dictionary<Control, Color>();

        #region Registration

        public static void Register(Control parentControl)
        {
            if (parentControl == null)
                return;

            foreach (Control control in GetAllControls(parentControl))
            {
                RegisterControl(control);
            }
        }

        private static void RegisterControl(Control control)
        {
            if (control == null)
                return;

            control.KeyDown -= CommonNavigation;
            control.KeyDown += CommonNavigation;

            if (EnableHighlighting)
            {
                control.Enter -= Control_Enter;
                control.Enter += Control_Enter;

                control.Leave -= Control_Leave;
                control.Leave += Control_Leave;
            }
        }

        #endregion

        #region Navigation

        private static void CommonNavigation(
            object sender,
            KeyEventArgs e)
        {
            try
            {
                if (!(sender is Control currentControl))
                    return;

                switch (e.KeyCode)
                {
                    case Keys.Enter:

                        if (e.Shift)
                            SetPreviousFocus(currentControl);
                        else
                            SetNextFocus(currentControl);

                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        break;

                    case Keys.Down:

                        if (CanNavigateUsingArrow(currentControl))
                        {
                            SetNextFocus(currentControl);

                            e.SuppressKeyPress = true;
                            e.Handled = true;
                        }

                        break;

                    case Keys.Up:

                        if (CanNavigateUsingArrow(currentControl))
                        {
                            SetPreviousFocus(currentControl);

                            e.SuppressKeyPress = true;
                            e.Handled = true;
                        }

                        break;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Public Methods

        public static void SetFocus(Control targetControl)
        {
            if (!IsFocusable(targetControl))
                return;

            ActivateTab(targetControl);

            targetControl.Focus();

            SelectControlText(targetControl);
        }

        public static void SetNextFocus(Control currentControl)
        {
            if (currentControl == null)
                return;

            Form form = currentControl.FindForm();

            if (form == null)
                return;

            Control nextControl = currentControl;

            while (true)
            {
                nextControl =
                    form.GetNextControl(nextControl, true);

                if (nextControl == null)
                {
                    currentControl.Focus();
                    NavigationFailed();
                    return;
                }

                if (IsFocusable(nextControl))
                {
                    ActivateTab(nextControl);

                    nextControl.Focus();

                    if (nextControl is TextBox txt)
                    {
                        txt.SelectionStart = 
                            txt.Text.Length;
                    }

                    return;
                }
            }
        }

        private static void SelectControlText(Control control)
        {
            switch (control)
            {
                case TextBox txt:
                    txt.SelectAll();
                    break;

                case MaskedTextBox mtxt:
                    mtxt.SelectAll();
                    break;

                case RichTextBox rtxt:
                    rtxt.SelectAll();
                    break;

                case ComboBox cbo:
                    if (cbo.DropDownStyle == ComboBoxStyle.DropDown)
                        cbo.SelectAll();
                    break;
            }
        }

        public static void SetPreviousFocus(Control currentControl)
        {
            if (currentControl == null)
                return;

            Form form = currentControl.FindForm();

            if (form == null)
                return;

            List<Control> controls =
                GetAllControls(form)
                .Where(IsFocusable)
                .OrderBy(x => x.TabIndex)
                .ToList();

            int index =
                controls.IndexOf(currentControl);

            if (index <= 0)
            {
                currentControl.Focus();
                NavigationFailed();
                return;
            }

            Control previousControl =
                controls[index - 1];

            ActivateTab(previousControl);

            previousControl.Focus();
        }

        #endregion

        #region Validation

        public static bool IsFocusable(Control control)
        {
            if (control == null)
                return false;

            if (!control.Visible)
                return false;

            if (!control.Enabled)
                return false;

            if (!control.TabStop)
                return false;

            if (control is Label)
                return false;

            if (control is Panel)
                return false;

            if (control is GroupBox)
                return false;

            if (control is TextBox txt &&
                txt.ReadOnly)
                return false;

            return true;
        }

        public static bool CanNavigateUsingArrow(
            Control control)
        {
            if (control == null)
                return false;

            if (control is ComboBox)
                return false;

            if (control is DataGridView)
                return false;

            if (control is ListBox)
                return false;

            if (control is TreeView)
                return false;

            if (control is MonthCalendar)
                return false;

            if (control is TextBox txt &&
                txt.Multiline)
                return false;

            return true;
        }

        #endregion

        #region Tab Handling

        private static void ActivateTab(Control control)
        {
            if (control == null)
                return;

            Control parent = control.Parent;

            while (parent != null)
            {
                if (parent is TabPage tabPage)
                {
                    if (tabPage.Parent is TabControl tabControl)
                    {
                        tabControl.SelectedTab = tabPage;

                        Application.DoEvents();
                    }
                }

                parent = parent.Parent;
            }
        }

        #endregion

        #region Highlighting

        private static void Control_Enter(
            object sender,
            EventArgs e)
        {
            if (!(sender is Control control))
                return;

            try
            {
                if (!_originalColors.ContainsKey(control))
                {
                    _originalColors.Add(
                        control,
                        control.BackColor);
                }

                control.BackColor =
                    Color.LightYellow;
            }
            catch
            {
            }
        }

        private static void Control_Leave(
            object sender,
            EventArgs e)
        {
            if (!(sender is Control control))
                return;

            try
            {
                if (_originalColors.ContainsKey(control))
                {
                    control.BackColor =
                        _originalColors[control];
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Utility

        private static IEnumerable<Control>
            GetAllControls(Control parent)
        {
            foreach (Control child
                in parent.Controls)
            {
                foreach (Control grandChild
                    in GetAllControls(child))
                {
                    yield return grandChild;
                }

                yield return child;
            }
        }

        private static void NavigationFailed()
        {
            if (!ShowNavigationError)
                return;

            try
            {
                SystemSounds.Beep.Play();
            }
            catch
            {
            }
        }

        #endregion
    }
}