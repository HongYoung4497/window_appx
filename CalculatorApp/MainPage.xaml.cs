using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowAppxCalculator
{
    public sealed partial class MainPage : Page
    {
        private double _storedValue;
        private string _pendingOperator;
        private bool _resetDisplay;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs args)
        {
            var value = (sender as Button)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (_resetDisplay || DisplayText.Text == "0")
            {
                DisplayText.Text = value;
                _resetDisplay = false;
                return;
            }

            DisplayText.Text += value;
        }

        private void Decimal_Click(object sender, RoutedEventArgs args)
        {
            if (_resetDisplay)
            {
                DisplayText.Text = "0";
                _resetDisplay = false;
            }

            if (!DisplayText.Text.Contains("."))
            {
                DisplayText.Text += ".";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs args)
        {
            _storedValue = 0;
            _pendingOperator = null;
            _resetDisplay = false;
            DisplayText.Text = "0";
        }

        private void ToggleSign_Click(object sender, RoutedEventArgs args)
        {
            if (DisplayText.Text == "0")
            {
                return;
            }

            if (DisplayText.Text.StartsWith("-", StringComparison.Ordinal))
            {
                DisplayText.Text = DisplayText.Text.Substring(1);
            }
            else
            {
                DisplayText.Text = "-" + DisplayText.Text;
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs args)
        {
            if (TryParseDisplay(out var current))
            {
                DisplayText.Text = (current / 100d).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs args)
        {
            var op = (sender as Button)?.Content?.ToString();
            if (string.IsNullOrWhiteSpace(op))
            {
                return;
            }

            if (_pendingOperator != null)
            {
                CalculateResult();
            }
            else if (TryParseDisplay(out var current))
            {
                _storedValue = current;
            }

            _pendingOperator = op;
            _resetDisplay = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs args)
        {
            CalculateResult();
            _pendingOperator = null;
        }

        private void CalculateResult()
        {
            if (_pendingOperator == null || !TryParseDisplay(out var current))
            {
                return;
            }

            var result = _pendingOperator switch
            {
                "+" => _storedValue + current,
                "−" => _storedValue - current,
                "×" => _storedValue * current,
                "÷" => current == 0 ? double.NaN : _storedValue / current,
                _ => current
            };

            DisplayText.Text = double.IsNaN(result)
                ? "NaN"
                : result.ToString(CultureInfo.InvariantCulture);
            _storedValue = result;
            _resetDisplay = true;
        }

        private bool TryParseDisplay(out double value)
        {
            return double.TryParse(DisplayText.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}
