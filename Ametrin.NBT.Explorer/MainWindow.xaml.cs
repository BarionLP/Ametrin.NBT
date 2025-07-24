using System.IO;
using Microsoft.Win32;

namespace Ametrin.NBT.Explorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Tag? tag;

    public MainWindow()
    {

        InitializeComponent();

        Loaded += (sender, args) =>
        {
            NbtViewer.ItemsSource = (ImmutableArray<string>)["Please open a file"];
        };
    }


    private void FileOpen_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            ValidateNames = true,
            CheckFileExists = true,
            Multiselect = false,
        };

        if(dialog.ShowDialog() is not true)
        {
            return;
        }

        using var reader = NbtReader.CreateFromFile(dialog.FileName);

        tag = reader.ReadTag();
        NbtViewer.ItemsSource = (ImmutableArray<Tag>) [tag];
    }

    private void FileSave_Click(object sender, RoutedEventArgs e)
    {
        if (tag is null) return;

        var dialog = new SaveFileDialog
        {
            ValidateNames = true,
        };

        if (dialog.ShowDialog() is not true)
        {
            return;
        }

        using var stream = File.Create(dialog.FileName);
        using var writer = NbtWriter.CreateCompressed(stream);

        writer.WriteTag(tag);
    }
}