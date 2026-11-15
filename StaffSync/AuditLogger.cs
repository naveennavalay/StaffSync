using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace StaffSync
{
    /// <summary>
    /// Common UI audit helper.
    /// Uses Control.Tag as the audit field name when supplied; otherwise uses Control.Name.
    /// </summary>
    public static class AuditLogger
    {
        /// <summary>
        /// Captures the current values of supported input controls recursively,
        /// including controls inside TabPages, GroupBoxes, Panels and other containers.
        /// This method is used both for the original snapshot and the current UI values.
        /// </summary>
        public static Dictionary<string, object> getOriginalValues(Control parent)
        {
            return CaptureValues(parent);
        }

        public static Dictionary<string, object> getCurrentValues(Control parent)
        {
            return CaptureValues(parent);
        }

        private static Dictionary<string, object> CaptureValues(Control parent)
        {
            var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (parent == null)
                return values;

            foreach (Control ctrl in GetAllControls(parent))
            {
                string fieldName = GetAuditFieldName(ctrl);
                if (string.IsNullOrWhiteSpace(fieldName))
                    continue;

                object value;
                if (!TryGetControlValue(ctrl, out value))
                    continue;

                // Preserve the chosen Tag/Name exactly. If duplicate audit field names
                // exist, keep the first occurrence rather than silently substituting a
                // different field name. Each control should have a unique Tag when set.
                if (!values.ContainsKey(fieldName))
                    values[fieldName] = value;
            }

            return values;
        }

        private static string GetAuditFieldName(Control ctrl)
        {
            if (ctrl == null)
                return string.Empty;

            // Tag is the preferred audit field name. If it is missing or blank,
            // use the exact WinForms control Name so the control can be identified.
            string tagName = Convert.ToString(ctrl.Tag, CultureInfo.InvariantCulture);
            if (!string.IsNullOrWhiteSpace(tagName))
                return tagName.Trim();

            return ctrl.Name ?? string.Empty;
        }

        private static bool TryGetControlValue(Control ctrl, out object value)
        {
            value = null;
            if (ctrl == null)
                return false;

            string typeName = ctrl.GetType().Name;

            // Text input controls (WinForms and Krypton).
            if (ctrl is TextBoxBase || typeName.IndexOf("KryptonTextBox", StringComparison.OrdinalIgnoreCase) >= 0
                || typeName.IndexOf("RichTextBox", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                value = (ctrl.Text ?? string.Empty).Trim();
                return true;
            }

            // Check boxes (read Checked through reflection so both WinForms and Krypton work).
            if (ctrl is CheckBox || typeName.IndexOf("KryptonCheckBox", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                object checkedValue = ReadProperty(ctrl, "Checked");
                value = checkedValue ?? false;
                return true;
            }

            // Dropdowns: use the displayed selection text for a human-readable audit statement.
            // SelectedValue is deliberately not used because it is commonly an internal ID.
            if (ctrl is ComboBox || typeName.IndexOf("KryptonComboBox", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                value = (ctrl.Text ?? string.Empty).Trim();
                return true;
            }

            // Date/time picker controls, including Krypton variants.
            if (ctrl is DateTimePicker || typeName.IndexOf("DateTimePicker", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                object dateValue = ReadProperty(ctrl, "Value");
                if (dateValue is DateTime)
                {
                    value = (DateTime)dateValue;
                    return true;
                }

                value = (ctrl.Text ?? string.Empty).Trim();
                return true;
            }

            // NumericUpDown and Krypton numeric controls.
            if (ctrl is NumericUpDown || typeName.IndexOf("NumericUpDown", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                object numericValue = ReadProperty(ctrl, "Value");
                value = numericValue ?? (object)(ctrl.Text ?? string.Empty).Trim();
                return true;
            }

            // Radio buttons.
            if (ctrl is RadioButton)
            {
                value = ((RadioButton)ctrl).Checked;
                return true;
            }

            // Other input-like controls can still be audited by their displayed text.
            // Container controls, labels and buttons are intentionally excluded.
            return false;
        }

        private static object ReadProperty(Control ctrl, string propertyName)
        {
            try
            {
                PropertyInfo property = ctrl.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                return property != null && property.CanRead ? property.GetValue(ctrl, null) : null;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> getUpdatedValues1(Dictionary<string, object> original, Dictionary<string, object> current)
        {
            return getUpdatedValues(original, current, false);
        }

        public static List<string> getUpdatedValues111(Control parent, Dictionary<string, object> original, Dictionary<string, object> current)
        {
            return getUpdatedValues(original, current, false);
        }

        /// <summary>
        /// Compares the original snapshot with the current UI values.
        /// Audit text format: "ControlTagOrName" changed from "old" to "new".
        /// </summary>
        public static List<string> getUpdatedValues(Dictionary<string, object> original, Dictionary<string, object> current, bool isNewRecord)
        {
            var changes = new List<string>();
            original = original ?? new Dictionary<string, object>();
            current = current ?? new Dictionary<string, object>();

            foreach (var item in current)
            {
                string newValue = ConvertToString(item.Value);

                if (isNewRecord)
                {
                    if (!string.IsNullOrWhiteSpace(newValue))
                        changes.Add($"\"{item.Key}\" set to \"{newValue}\"");
                    continue;
                }

                object originalValue;
                string oldValue = original.TryGetValue(item.Key, out originalValue)
                    ? ConvertToString(originalValue)
                    : string.Empty;

                if (!string.Equals(oldValue, newValue, StringComparison.Ordinal))
                    changes.Add($"\"{item.Key}\" changed from \"{oldValue}\" to \"{newValue}\"");
            }

            return changes;
        }

        private static string ConvertToString(object value)
        {
            if (value == null)
                return string.Empty;

            if (value is DateTime)
                return ((DateTime)value).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

            if (value is bool)
                return (bool)value ? "True" : "False";

            if (value is IFormattable)
                return ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);

            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                yield return ctrl;
                foreach (Control child in GetAllControls(ctrl))
                    yield return child;
            }
        }
    }
}
