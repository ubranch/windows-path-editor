using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsPathEditor
{
    /// <summary>
    /// Interaction logic for AutoCompleteBox.xaml
    /// </summary>
    public partial class AutoCompleteBox
    {
        private CancellationTokenSource _searchCts;
        private TextChangedEventHandler _textChangedHandler;

        public AutoCompleteBox()
        {
            InitializeComponent();
        }

        public void SetCompleteProvider(Func<string, IEnumerable<object>> provider)
        {
            if (_textChangedHandler != null)
                textBox.TextChanged -= _textChangedHandler;

            _textChangedHandler = async (s, e) =>
            {
                var text = ((TextBox)s).Text;
                if (string.IsNullOrEmpty(text))
                    return;

                _searchCts?.Cancel();
                var cts = new CancellationTokenSource();
                _searchCts = cts;

                try
                {
                    var res = await Task.Run(() => provider(text), cts.Token);
                    if (cts.Token.IsCancellationRequested) return;

                    popup.IsOpen = true;
                    if (res.Count() > 0)
                        suggestionList.ItemsSource = res;
                    else
                        suggestionList.ItemsSource = new string[] { "(no matches)" };
                }
                catch (OperationCanceledException) { }
            };

            textBox.TextChanged += _textChangedHandler;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) { textBox.Text = ""; popup.IsOpen = false; }
            if (textBox.Text == "") popup.IsOpen = false;
        }
    }
}
