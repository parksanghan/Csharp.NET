using DataGenerator.Models;
using DataGenerator.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace DataGenerator
{
    public partial class MainWindow : FluentWindow
    {
        private readonly PacketXmlParser _xmlParser =
            new PacketXmlParser();

        private readonly PacketCodeGenerator _codeGenerator =
            new PacketCodeGenerator();

        private readonly ProjectLocator _projectLocator =
            new ProjectLocator();

        private readonly ProjectIntegrationService _integrationService =
            new ProjectIntegrationService();

        private PacketDefinitionDocument? _packetDocument;

        private CancellationTokenSource?
            _xmlParseCancellationSource;

        public MainWindow()
        {
            InitializeComponent();
            RuntimePathTextBlock.Text =
                "Runtime DLL: " + GetRuntimeLibraryPath();
        }

        private async void BrowseXmlButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "패킷 정의 XML 선택",
                Filter =
                    "Packet XML (*.xml)|*.xml|" +
                    "모든 파일 (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != true)
            {
                return;
            }

            XmlPathTextBox.Text = dialog.FileName;
            await LoadXmlPreviewAsync(dialog.FileName);
        }

        private void BrowseTargetButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "대상 솔루션 또는 C# 프로젝트 선택",
                Filter =
                    "Visual Studio 솔루션/프로젝트 (*.sln;*.csproj)|" +
                    "*.sln;*.csproj|" +
                    "솔루션 (*.sln)|*.sln|" +
                    "C# 프로젝트 (*.csproj)|*.csproj",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != true)
            {
                return;
            }

            TargetPathTextBox.Text = dialog.FileName;
            LoadProjectCandidates(dialog.FileName);
        }

        private void TargetProjectComboBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            UpdateReadyStatus();
        }

        private async void GenerateButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!TryGetGenerationInput(
                    out PacketDefinitionDocument document,
                    out ProjectCandidate project))
            {
                return;
            }

            string runtimeLibraryPath =
                GetRuntimeLibraryPath();

            GenerateButton.IsEnabled = false;
            GenerateButton.Content = "생성 중...";
            StatusTextBlock.Text =
                "XML을 파싱하고 프로젝트 파일을 생성하는 중입니다.";
            LogTextBox.Text = "생성을 시작합니다...";

            try
            {
                ProjectGenerationResult result =
                    await Task.Run(
                        () =>
                        {
                            IReadOnlyList<GeneratedSource> sources =
                                _codeGenerator.Generate(document);

                            return _integrationService.Apply(
                                project.ProjectPath,
                                runtimeLibraryPath,
                                sources);
                        });

                ShowGenerationResult(result);
            }
            catch (Exception exception)
            {
                StatusTextBlock.Text = "생성에 실패했습니다.";
                LogTextBox.Text = exception.ToString();

                System.Windows.MessageBox.Show(
                    this,
                    exception.Message,
                    "DataGenerator 오류",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                GenerateButton.IsEnabled = true;
                GenerateButton.Content = "▶  코드 생성";
            }
        }

        private async Task LoadXmlPreviewAsync(
            string xmlPath)
        {
            var cancellationSource =
                new CancellationTokenSource();

            _xmlParseCancellationSource =
                cancellationSource;

            _packetDocument = null;
            PacketPreviewGrid.ItemsSource = null;
            XmlSummaryTextBlock.Text =
                "XML 패킷 정의를 분석하는 중입니다.";

            ShowXmlLoadingOverlay();

            var progress =
                new Progress<PacketParseProgress>(
                    UpdateXmlParseProgress);

            try
            {
                PacketDefinitionDocument parsedDocument =
                    await Task.Run(
                        () =>
                            _xmlParser.Parse(
                                xmlPath,
                                progress,
                                cancellationSource.Token),
                        cancellationSource.Token);

                ApplyPacketDocument(
                    parsedDocument,
                    "XML 검증 완료");

                UpdateReadyStatus();
            }
            catch (OperationCanceledException)
            {
                _packetDocument = null;
                PacketPreviewGrid.ItemsSource = null;
                XmlSummaryTextBlock.Text =
                    "XML 분석이 취소되었습니다.";
                StatusTextBlock.Text =
                    "XML 패킷 분석을 취소했습니다.";
                LogTextBox.Text =
                    "사용자가 XML 패킷 분석을 취소했습니다.";
            }
            catch (Exception exception)
            {
                _packetDocument = null;
                PacketPreviewGrid.ItemsSource = null;
                XmlSummaryTextBlock.Text =
                    "XML을 읽을 수 없습니다.";
                StatusTextBlock.Text =
                    "XML 정의 오류가 있습니다.";
                LogTextBox.Text = exception.Message;

                System.Windows.MessageBox.Show(
                    this,
                    exception.Message,
                    "XML 검증 오류",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
            }
            finally
            {
                if (ReferenceEquals(
                        _xmlParseCancellationSource,
                        cancellationSource))
                {
                    _xmlParseCancellationSource = null;
                }

                cancellationSource.Dispose();
                XmlLoadingOverlay.Visibility =
                    Visibility.Collapsed;
            }
        }

        private void ApplyPacketDocument(
            PacketDefinitionDocument document,
            string logHeader)
        {
            _packetDocument = document;

            PacketPreviewGrid.ItemsSource = null;
            PacketPreviewGrid.ItemsSource =
                document.Packets;

            XmlSummaryTextBlock.Text =
                document.Packets.Count +
                "개 패킷 · " +
                document.TotalFieldCount +
                "개 필드 · 행 더블클릭: 필드 상세";

            LogTextBox.Text =
                logHeader +
                "\r\n파일: " +
                document.SourcePath +
                "\r\n" +
                string.Join(
                    "\r\n",
                    document.Packets.Select(
                        packet =>
                            "• " +
                            packet.Name +
                            " / " +
                            packet.DataType +
                            " / " +
                            packet.FieldCount +
                            " fields"));
        }

        private void ShowXmlLoadingOverlay()
        {
            XmlParseProgressBar.IsIndeterminate = true;
            XmlParseProgressBar.Value = 0;
            XmlParsePercentTextBlock.Text = "준비 중";
            XmlParseStatusTextBlock.Text =
                "XML 파일을 준비하는 중입니다.";
            XmlParseDetailTextBlock.Text =
                "파일 정보를 확인하고 있습니다.";
            CancelXmlLoadingButton.IsEnabled = true;
            XmlLoadingOverlay.Visibility =
                Visibility.Visible;
            StatusTextBlock.Text =
                "XML 패킷 정의를 분석하는 중입니다.";
        }

        private void UpdateXmlParseProgress(
            PacketParseProgress progress)
        {
            XmlParseProgressBar.IsIndeterminate =
                progress.IsIndeterminate;

            if (progress.IsIndeterminate)
            {
                XmlParsePercentTextBlock.Text =
                    "읽는 중";
            }
            else
            {
                XmlParseProgressBar.Value =
                    progress.Percentage;
                XmlParsePercentTextBlock.Text =
                    progress.Percentage + "%";
            }

            XmlParseStatusTextBlock.Text =
                progress.Message;

            if (progress.TotalPackets == 0 &&
                progress.TotalFields == 0)
            {
                XmlParseDetailTextBlock.Text =
                    "XML 문서 구조를 확인하고 있습니다.";
            }
            else
            {
                XmlParseDetailTextBlock.Text =
                    progress.ProcessedPackets +
                    " / " +
                    progress.TotalPackets +
                    " 패킷 · " +
                    progress.ProcessedFields +
                    " / " +
                    progress.TotalFields +
                    " 필드";
            }

            StatusTextBlock.Text =
                progress.IsIndeterminate
                    ? "XML 파일을 읽는 중입니다."
                    : "XML 패킷 분석 중 · " +
                      progress.Percentage +
                      "%";
        }

        private void CancelXmlLoadingButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (_xmlParseCancellationSource == null)
            {
                return;
            }

            CancelXmlLoadingButton.IsEnabled = false;
            XmlParseStatusTextBlock.Text =
                "분석 취소를 요청했습니다.";
            XmlParseDetailTextBlock.Text =
                "현재 처리 중인 XML 읽기가 끝나면 취소됩니다.";
            _xmlParseCancellationSource.Cancel();
        }

        private void LoadProjectCandidates(string selectedPath)
        {
            try
            {
                IReadOnlyList<ProjectCandidate> projects =
                    _projectLocator.FindProjects(selectedPath);

                TargetProjectComboBox.ItemsSource = projects;
                TargetProjectComboBox.SelectedIndex =
                    projects.Count == 1 ? 0 : -1;

                if (projects.Count > 1)
                {
                    StatusTextBlock.Text =
                        "솔루션에서 코드를 생성할 프로젝트를 선택하세요.";
                    LogTextBox.Text =
                        projects.Count +
                        "개의 C# 프로젝트를 찾았습니다.";
                }
                else
                {
                    UpdateReadyStatus();
                }
            }
            catch (Exception exception)
            {
                TargetProjectComboBox.ItemsSource = null;
                StatusTextBlock.Text =
                    "대상 프로젝트를 읽을 수 없습니다.";
                LogTextBox.Text = exception.Message;

                System.Windows.MessageBox.Show(
                    this,
                    exception.Message,
                    "프로젝트 확인 오류",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
            }
        }

        private void PacketPreviewGrid_MouseDoubleClick(
            object sender,
            MouseButtonEventArgs e)
        {
            DependencyObject? source =
                e.OriginalSource as DependencyObject;

            while (source != null &&
                   source is not DataGridRow)
            {
                source = VisualTreeHelper.GetParent(source);
            }

            if (source is not DataGridRow row ||
                row.Item is not PacketDefinition packet ||
                _packetDocument == null)
            {
                return;
            }

            int packetIndex =
                _packetDocument.Packets.IndexOf(packet);

            if (packetIndex < 0)
            {
                return;
            }

            var detailsWindow =
                new PacketFieldDetailsWindow(
                    _packetDocument,
                    packet,
                    packetIndex)
                {
                    Owner = this
                };

            bool? dialogResult =
                detailsWindow.ShowDialog();

            if (dialogResult == true &&
                detailsWindow.SavedDocument != null)
            {
                ApplyPacketDocument(
                    detailsWindow.SavedDocument,
                    "XML 수정사항 저장 및 검증 완료");

                PacketPreviewGrid.SelectedIndex =
                    packetIndex;

                if (packetIndex <
                    detailsWindow
                        .SavedDocument
                        .Packets
                        .Count)
                {
                    PacketPreviewGrid.ScrollIntoView(
                        detailsWindow
                            .SavedDocument
                            .Packets[packetIndex]);
                }

                StatusTextBlock.Text =
                    "XML 수정사항을 저장했습니다. " +
                    "코드 생성 버튼을 누르면 새 정의가 반영됩니다.";
            }

            e.Handled = true;
        }

        private bool TryGetGenerationInput(
            out PacketDefinitionDocument document,
            out ProjectCandidate project)
        {
            document = null!;
            project = null!;

            if (_packetDocument == null)
            {
                System.Windows.MessageBox.Show(
                    this,
                    "먼저 올바른 패킷 XML 파일을 선택하세요.",
                    "입력 필요",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
                return false;
            }

            if (TargetProjectComboBox.SelectedItem
                is not ProjectCandidate selectedProject)
            {
                System.Windows.MessageBox.Show(
                    this,
                    "코드를 생성할 C# 프로젝트를 선택하세요.",
                    "입력 필요",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
                return false;
            }

            string runtimePath =
                GetRuntimeLibraryPath();

            if (!File.Exists(runtimePath))
            {
                System.Windows.MessageBox.Show(
                    this,
                    "Runtime DLL을 찾을 수 없습니다.\r\n" +
                    runtimePath,
                    "NetworkGenerator.dll 없음",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                return false;
            }

            document = _packetDocument;
            project = selectedProject;
            return true;
        }

        private void ShowGenerationResult(
            ProjectGenerationResult result)
        {
            StatusTextBlock.Text =
                "코드 생성이 완료되었습니다.";

            var lines = new List<string>
            {
                "대상: " + result.ProjectPath,
                "DLL: " + result.LibraryPath,
                "DLL 복사: " +
                    (result.LibraryCopied ? "갱신됨" : "변경 없음"),
                ".csproj 참조: " +
                    (result.ProjectFileChanged
                        ? "추가/갱신됨"
                        : "이미 설정됨"),
                "생성: " + result.CreatedFileCount,
                "갱신: " + result.UpdatedFileCount,
                "변경 없음: " + result.UnchangedFileCount,
                string.Empty,
                "생성 파일:"
            };

            lines.AddRange(
                result.GeneratedFiles.Select(
                    file => "• " + file));

            LogTextBox.Text =
                string.Join("\r\n", lines);
        }

        private void UpdateReadyStatus()
        {
            bool hasXml =
                _packetDocument != null;

            bool hasProject =
                TargetProjectComboBox.SelectedItem
                is ProjectCandidate;

            if (hasXml && hasProject)
            {
                StatusTextBlock.Text =
                    "준비되었습니다. 재생 버튼을 눌러 코드를 생성하세요.";
            }
            else if (!hasXml)
            {
                StatusTextBlock.Text =
                    "패킷 정의 XML 파일을 선택하세요.";
            }
            else
            {
                StatusTextBlock.Text =
                    "대상 솔루션 또는 프로젝트를 선택하세요.";
            }
        }

        private static string GetRuntimeLibraryPath()
        {
            return Path.Combine(
                AppContext.BaseDirectory,
                "Lib",
                "NetworkGenerator.dll");
        }

        protected override void OnClosed(EventArgs e)
        {
            _xmlParseCancellationSource?.Cancel();
            base.OnClosed(e);
        }
    }
}
