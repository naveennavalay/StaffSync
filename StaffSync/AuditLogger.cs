using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class AuditLogger
    {
        // Capture values from controls (using Tag as field name)
        public static Dictionary<string, object> getOriginalValues(Control parent)
        {
            var values = new Dictionary<string, object>();

            foreach (Control ctrl in GetAllControls(parent))
            {
                if (ctrl.Tag == null) continue;

                string fieldName = ctrl.Tag.ToString();

                if (ctrl is TextBox || ctrl.GetType().Name.Contains("KryptonTextBox"))
                    values[fieldName] = ctrl.Text?.Trim();

                else if (ctrl is CheckBox || ctrl.GetType().Name.Contains("KryptonCheckBox"))
                    values[fieldName] = ((CheckBox)ctrl).Checked;

                else if (ctrl is ComboBox || ctrl.GetType().Name.Contains("KryptonComboBox"))
                    values[fieldName] = ctrl.Text;

                else if (ctrl is DateTimePicker || ctrl.GetType().Name.Contains("KryptonDateTimePicker"))
                {
                    var dt = (DateTimePicker)ctrl;
                    values[fieldName] = dt.Format == DateTimePickerFormat.Custom ? (DateTime?)null : dt.Value;
                }
            }

            return values;
        }

        public static Dictionary<string, object> getCurrentValues(Control parent)
        {
            var values = new Dictionary<string, object>();

            foreach (Control ctrl in GetAllControls(parent))
            {
                if (ctrl.Tag == null) continue;

                string fieldName = ctrl.Tag.ToString();

                if (ctrl is TextBox || ctrl.GetType().Name.Contains("KryptonTextBox"))
                    values[fieldName] = ctrl.Text?.Trim();

                else if (ctrl is CheckBox || ctrl.GetType().Name.Contains("KryptonCheckBox"))
                    values[fieldName] = ((CheckBox)ctrl).Checked;

                else if (ctrl is ComboBox || ctrl.GetType().Name.Contains("KryptonComboBox"))
                    values[fieldName] = ctrl.Text;

                else if (ctrl is DateTimePicker || ctrl.GetType().Name.Contains("KryptonDateTimePicker"))
                {
                    var dt = (DateTimePicker)ctrl;
                    values[fieldName] = dt.Format == DateTimePickerFormat.Custom ? (DateTime?)null : dt.Value;
                }
            }

            return values;
        }

        // Compare old vs new values
        public static List<string> getUpdatedValues1(Dictionary<string, object> original, Dictionary<string, object> current)
        {
            var changes = new List<string>();

            foreach (var key in current.Keys)
            {
                string oldValue = original.ContainsKey(key) ? ConvertToString(original[key]) : "";
                string newValue = ConvertToString(current[key]);

                if (oldValue != newValue)
                {
                    changes.Add($"\"{key}\" changed from \"{oldValue}\" to \"{newValue}\"");
                }
            }

            return changes;
        }

        public static List<string> getUpdatedValues111(Control parent, Dictionary<string, object> original, Dictionary<string, object> current)
        {
            var changes = new List<string>();

            foreach (Control ctrl in GetAllControls(parent))
            {
                if (ctrl.Tag == null) continue;

                string key = ctrl.Tag.ToString();

                string oldValue = original.ContainsKey(key) ? ConvertToString(original[key]) : "";
                string newValue = current.ContainsKey(key) ? ConvertToString(current[key]) : "";

                if (oldValue != newValue)
                {
                    changes.Add($"\"{key}\" changed from \"{oldValue}\" to \"{newValue}\"");
                }
            }

            return changes;
        }

        public static List<string> getUpdatedValues(Dictionary<string, object> original, Dictionary<string, object> current, bool isNewRecord)
        {
            var changes = new List<string>();

            foreach (var key in current.Keys)
            {
                string newValue = ConvertToString(current[key]);

                if (isNewRecord)
                {
                    // Only log new values (no old value)
                    if (!string.IsNullOrWhiteSpace(newValue))
                    {
                        changes.Add($"\"{key}\" set to \"{newValue}\"");
                    }
                }
                else
                {
                    string oldValue = original.ContainsKey(key)
                        ? ConvertToString(original[key])
                        : "";

                    if (oldValue != newValue)
                    {
                        changes.Add($"\"{key}\" changed from \"{oldValue}\" to \"{newValue}\"");
                    }
                }
            }

            return changes;
        }

        // Convert safely
        private static string ConvertToString(object value)
        {
            if (value == null) return "";

            if (value is DateTime dt)
                return dt.ToString("dd-MMM-yyyy");

            return value.ToString();
        }

        // Get all controls recursively
        private static IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                foreach (Control child in GetAllControls(ctrl))
                    yield return child;

                yield return ctrl;
            }
        }
    }
}
