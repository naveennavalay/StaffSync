using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public static class TextBoxHelper
    {
        private static Dictionary<Krypton.Toolkit.KryptonTextBox, ListBox> _attachedListBoxes = new Dictionary<Krypton.Toolkit.KryptonTextBox, ListBox>();
        private static Dictionary<Krypton.Toolkit.KryptonTextBox, List<string>> _attachedData = new Dictionary<Krypton.Toolkit.KryptonTextBox, List<string>>();
        // store per-textbox settings; handlers read values from here
        private static readonly Dictionary<Krypton.Toolkit.KryptonTextBox, (TextCaseMode caseMode, InputType allowedInput, bool selectAllOnFocus)> _settings
            = new Dictionary<Krypton.Toolkit.KryptonTextBox, (TextCaseMode, InputType, bool)>();
        private static readonly ConditionalWeakTable<Krypton.Toolkit.KryptonTextBox, State> _states = new ConditionalWeakTable<Krypton.Toolkit.KryptonTextBox, State>();
        private static bool _suppressAutoComplete = false;

        public enum TextCaseMode
        {
            None,
            UpperCase,
            LowerCase,
            CamelCase,   // helloWorld (no spaces)
            ProperCase   // Hello World (title case)
        }

        public enum InputType
        {
            AnyChar,
            TextOnly,
            TextAndSpecial,
            NumberOnly,
            NumberAndSpecial
        }

        private class State
        {
            public TextCaseMode CaseMode;
            public InputType InputType;
            public bool SelectAllOnFocus;

            // handlers (store instances so we can attach only once)
            public KeyPressEventHandler KeyPressHandler;
            public EventHandler EnterHandler;
            public MouseEventHandler MouseUpHandler;
            public KeyEventHandler KeyDownHandler;
            public EventHandler TextChangedHandler;
            public EventHandler DisposedHandler;

            public bool IsApplyingText; // guard for re-entrancy
        }

        /// <summary>
        /// Attach autocomplete from a DataTable.
        /// Only one column will be used for search/display.
        /// </summary>
        public static void EnableAutoCompleteFromDataTable(this Krypton.Toolkit.KryptonTextBox textBox, DataTable dataTable, string columnName)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            if (!dataTable.Columns.Contains(columnName))
                throw new ArgumentException($"Column '{columnName}' not found in DataTable.");

            var list = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[columnName] != DBNull.Value)
                    list.Add(row[columnName].ToString());
            }

            EnableAutoComplete(textBox, list);
        }

        public static void EnableAutoComplete(this Krypton.Toolkit.KryptonTextBox textBox, List<string> dataList)
        {
            if (_attachedListBoxes.ContainsKey(textBox))
                return; // already attached

            _attachedData[textBox] = dataList;

            var listBox = new ListBox
            {
                Visible = false
            };

            // Store the listbox for this specific textbox
            _attachedListBoxes[textBox] = listBox;

            listBox.Click += (s, e) =>
            {
                AssignSelectedItemToTextBox(textBox, listBox);
            };

            // Add to the same parent container as the textbox
            textBox.Parent.Controls.Add(listBox);
            listBox.BringToFront();

            textBox.GotFocus += (s, e) =>
            {
                // Hide all other listboxes
                foreach (var kv in _attachedListBoxes)
                {
                    if (kv.Key != textBox)
                        kv.Value.Visible = false;
                }
            };

            textBox.TextChanged += (s, e) =>
            {
                var txt = textBox.Text;
                if (string.IsNullOrWhiteSpace(txt))
                {
                    listBox.Visible = false;
                    return;
                }

                var filtered = _attachedData[textBox]
                    .Where(x => x.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filtered.Count > 0)
                {
                    listBox.BeginUpdate();
                    listBox.Items.Clear();
                    listBox.Items.AddRange(filtered.ToArray());
                    listBox.EndUpdate();

                    // Select first item for nice UI
                    if (listBox.Items.Count > 0)
                        listBox.SelectedIndex = 0;

                    // Position below the current textbox
                    listBox.Location = new Point(textBox.Left, textBox.Bottom);
                    listBox.Width = textBox.Width;
                    listBox.Height = Math.Min(150, listBox.ItemHeight * (filtered.Count + 1));
                    listBox.Visible = true;
                }
                else
                {
                    listBox.Visible = false;
                }
            };

            textBox.KeyDown += (s, e) =>
            {
                if (!listBox.Visible) return;

                if (e.KeyCode == Keys.Down)
                {
                    if (listBox.SelectedIndex < listBox.Items.Count - 1)
                        listBox.SelectedIndex++;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (listBox.SelectedIndex > 0)
                        listBox.SelectedIndex--;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    AssignSelectedItemToTextBox(textBox, listBox);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    listBox.Visible = false;
                    e.Handled = true;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                // Delay to allow click selection
                Timer t = new Timer { Interval = 200 };
                t.Tick += (s2, e2) =>
                {
                    t.Stop();
                    if (!listBox.Focused)
                        listBox.Visible = false;
                };
                t.Start();
            };
        }

        private static void AssignSelectedItemToTextBox(Krypton.Toolkit.KryptonTextBox textBox, ListBox listBox)
        {
            if (listBox.SelectedItem != null)
            {
                textBox.Text = listBox.SelectedItem.ToString();
                textBox.SelectionStart = textBox.Text.Length;
            }
            listBox.Visible = false;
        }

        public static void ApplySettings(this Krypton.Toolkit.KryptonTextBox tb, TextCaseMode caseMode, InputType inputType, bool selectAllOnFocus = false)
        {
            if (tb == null) throw new ArgumentNullException(nameof(tb));

            var state = _states.GetOrCreateValue(tb);

            // update settings (handlers will read values from state)
            state.CaseMode = caseMode;
            state.InputType = inputType;
            state.SelectAllOnFocus = selectAllOnFocus;

            // if handlers are already attached, we're done (they use up-to-date state fields)
            if (state.KeyPressHandler != null) return;

            // Create and store handlers
            state.KeyPressHandler = new KeyPressEventHandler((s, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (!IsCharAllowed(e.KeyChar, state.InputType))
                    e.Handled = true;
            });

            state.EnterHandler = new EventHandler((s, e) =>
            {
                // Use BeginInvoke so it runs after default focus behaviour (fixes mouse caret placement issue)
                if (state.SelectAllOnFocus)
                {
                    tb.BeginInvoke(new Action(() =>
                    {
                        if (tb.Focused) tb.SelectAll();
                    }));
                }
            });

            state.MouseUpHandler = new MouseEventHandler((s, e) =>
            {
                // Mouse click can move caret; selecting on MouseUp ensures select-all on mouse focus too
                if (state.SelectAllOnFocus && tb.Focused)
                {
                    // call later to ensure Windows doesn't override selection
                    tb.BeginInvoke(new Action(() =>
                    {
                        if (tb.Focused) tb.SelectAll();
                    }));
                }
            });

            state.KeyDownHandler = new KeyEventHandler((s, e) =>
            {
                // Paste handling (Ctrl+V or Shift+Insert)
                if ((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.Insert))
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;

                    if (!Clipboard.ContainsText()) return;

                    string pasted = Clipboard.GetText();

                    // filter pasted chars according to input type
                    pasted = new string(pasted.Where(ch => IsCharAllowed(ch, state.InputType)).ToArray());

                    // apply case transform
                    pasted = TransformText(pasted, state.CaseMode);

                    int selStart = tb.SelectionStart;
                    int selLen = tb.SelectionLength;

                    // build new text
                    string newText = tb.Text.Remove(selStart, selLen).Insert(selStart, pasted);

                    // set text safely (guarding re-entrancy)
                    state.IsApplyingText = true;
                    tb.Text = newText;
                    state.IsApplyingText = false;

                    tb.SelectionStart = Math.Min(selStart + pasted.Length, tb.Text.Length);
                    tb.SelectionLength = 0;
                }
            });

            state.TextChangedHandler = new EventHandler((s, e) =>
            {
                if (state.IsApplyingText) return; // avoid recursion

                if (state.CaseMode == TextCaseMode.None) return;

                int selStart = tb.SelectionStart;
                int selLen = tb.SelectionLength;

                string transformed = TransformText(tb.Text, state.CaseMode);

                if (!string.Equals(tb.Text, transformed, StringComparison.Ordinal))
                {
                    state.IsApplyingText = true;
                    tb.Text = transformed;
                    state.IsApplyingText = false;

                    // restore selection as best as possible
                    tb.SelectionStart = Math.Min(selStart, tb.Text.Length);
                    tb.SelectionLength = Math.Min(selLen, Math.Max(0, tb.Text.Length - tb.SelectionStart));
                }
            });

            state.DisposedHandler = new EventHandler((s, e) =>
            {
                // cleanup table entry when control is disposed
                _states.Remove(tb);
            });

            // Attach handlers once
            tb.KeyPress += state.KeyPressHandler;
            tb.Enter += state.EnterHandler;
            tb.MouseUp += state.MouseUpHandler;
            tb.KeyDown += state.KeyDownHandler;
            tb.TextChanged += state.TextChangedHandler;
            tb.Disposed += state.DisposedHandler;
        }

        private static bool IsCharAllowed(char ch, InputType inputType)
        {
            switch (inputType)
            {
                case InputType.TextOnly:
                    return char.IsLetter(ch) || char.IsWhiteSpace(ch);
                case InputType.TextAndSpecial:
                    return char.IsLetter(ch) || char.IsWhiteSpace(ch) || char.IsPunctuation(ch) || char.IsSymbol(ch);
                case InputType.NumberOnly:
                    return char.IsDigit(ch);
                case InputType.NumberAndSpecial:
                    return char.IsDigit(ch) || char.IsPunctuation(ch) || char.IsSymbol(ch);
                case InputType.AnyChar:
                default:
                    return true;
            }
        }

        private static string TransformText(string input, TextCaseMode mode)
        {
            if (string.IsNullOrEmpty(input)) return input;

            switch (mode)
            {
                case TextCaseMode.UpperCase:
                    return input.ToUpper(CultureInfo.CurrentCulture);
                case TextCaseMode.LowerCase:
                    return input.ToLower(CultureInfo.CurrentCulture);
                case TextCaseMode.ProperCase:
                    // preserve spaces (so multiple spaces produce empty entries)
                    return string.Join(" ",
                        input.Split(' ')
                             .Select(w => w.Length > 0 ? char.ToUpper(w[0], CultureInfo.CurrentCulture) + (w.Length > 1 ? w.Substring(1).ToLower(CultureInfo.CurrentCulture) : string.Empty) : string.Empty)
                    );
                case TextCaseMode.CamelCase:
                    // helloWorld (remove spaces)
                    var words = input.Split(' ').Where(x => x.Length > 0).ToArray();
                    if (words.Length == 0) return string.Empty;
                    var titleWords = words.Select(w => char.ToUpper(w[0], CultureInfo.CurrentCulture) + (w.Length > 1 ? w.Substring(1).ToLower(CultureInfo.CurrentCulture) : string.Empty)).ToArray();
                    var joined = string.Concat(titleWords);
                    return char.ToLower(joined[0], CultureInfo.CurrentCulture) + (joined.Length > 1 ? joined.Substring(1) : string.Empty);
                case TextCaseMode.None:
                default:
                    return input;
            }
        }

        public static void EnableSelectAllOnFocus(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox textBox)
                {
                    textBox.GotFocus -= TextBox_GotFocus; // avoid duplicate subscription
                    textBox.GotFocus += TextBox_GotFocus;
                }
                if (ctrl is Krypton.Toolkit.KryptonTextBox kTextBox)
                {
                    kTextBox.GotFocus -= TextBox_GotFocus; // avoid duplicate subscription
                    kTextBox.GotFocus += TextBox_GotFocus;
                }
                if (ctrl is MaskedTextBox mTextBox)
                {
                    mTextBox.GotFocus -= TextBox_GotFocus; // avoid duplicate subscription
                    mTextBox.GotFocus += TextBox_GotFocus;
                }

                // If control contains child controls, process recursively
                if (ctrl.HasChildren)
                    EnableSelectAllOnFocus(ctrl);
            }
        }

        private static void TextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is TextBox txt && txt.Enabled && !txt.ReadOnly)
            {
                txt.SelectAll();
            }
            if (sender is Krypton.Toolkit.KryptonTextBox ktxt && ktxt.Enabled && !ktxt.ReadOnly)
            {
                ktxt.SelectAll();
            }
            if (sender is MaskedTextBox mtxt && mtxt.Enabled && !mtxt.ReadOnly)
            {
                mtxt.SelectAll();
            }
        }
    }
}
