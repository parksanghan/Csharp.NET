using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfComObjectSample;

public partial class MainWindow : Window
{
    private object? _comObject;

    public MainWindow()
    {
        InitializeComponent();
        AppendLog("The default sample uses the Windows COM object Scripting.Dictionary.");
        AppendLog("Enter another ProgID and click Create to test a different COM object.");
    }

    private void CreateComObjectButton_Click(object sender, RoutedEventArgs e)
    {
        var progId = ProgIdTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(progId))
        {
            AppendLog("Enter a ProgID.");
            return;
        }

        ReleaseComObject();

        try
        {
            var comType = Type.GetTypeFromProgID(progId, throwOnError: true);
            _comObject = Activator.CreateInstance(comType!);

            StatusTextBlock.Text = $"{progId} created";
            AppendLog($"COM object created: {progId}");
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = "COM object creation failed";
            AppendLog($"COM object creation failed: {ex.Message}");
        }
    }

    private void AddValueButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryGetComDictionary(out dynamic dictionary))
        {
            return;
        }

        var key = KeyTextBox.Text;
        var value = ValueTextBox.Text;

        try
        {
            if (dictionary.Exists(key))
            {
                dictionary.Item(key) = value;
                AppendLog($"Updated: {key} = {value}");
            }
            else
            {
                dictionary.Add(key, value);
                AppendLog($"Added: {key} = {value}");
            }

            StatusTextBlock.Text = $"Dictionary Count = {dictionary.Count}";
        }
        catch (Exception ex)
        {
            AppendLog($"Add failed: {ex.Message}");
        }
    }

    private void ReadValueButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryGetComDictionary(out dynamic dictionary))
        {
            return;
        }

        var key = KeyTextBox.Text;

        try
        {
            if (!dictionary.Exists(key))
            {
                AppendLog($"Key not found: {key}");
                return;
            }

            var value = dictionary.Item(key);
            AppendLog($"Read: {key} = {value}");
        }
        catch (Exception ex)
        {
            AppendLog($"Read failed: {ex.Message}");
        }
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryGetComDictionary(out dynamic dictionary))
        {
            return;
        }

        try
        {
            dictionary.RemoveAll();
            StatusTextBlock.Text = "Dictionary Count = 0";
            AppendLog("Dictionary cleared.");
        }
        catch (Exception ex)
        {
            AppendLog($"Clear failed: {ex.Message}");
        }
    }

    private bool TryGetComDictionary(out dynamic dictionary)
    {
        if (_comObject is null)
        {
            CreateComObjectButton_Click(this, new RoutedEventArgs());
        }

        dictionary = _comObject!;

        if (_comObject is not null)
        {
            return true;
        }

        AppendLog("COM object is missing.");
        return false;
    }

    private void AppendLog(string message)
    {
        LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        LogTextBox.ScrollToEnd();
    }

    private void ReleaseComObject()
    {
        if (_comObject is null)
        {
            return;
        }

        if (Marshal.IsComObject(_comObject))
        {
            Marshal.FinalReleaseComObject(_comObject);
        }

        _comObject = null;
    }

    protected override void OnClosed(EventArgs e)
    {
        ReleaseComObject();
        base.OnClosed(e);
    }
}
