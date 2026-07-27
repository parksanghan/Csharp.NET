using DataGenerator.Models;
using DataGenerator.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace DataGenerator
{
    public partial class PacketFieldDetailsWindow : FluentWindow
    {
        private readonly string _sourcePath;
        private readonly int _packetIndex;
        private readonly string _originalPacketName;
        private readonly string _originalMessageId;
        private readonly PacketDefinition _originalPacket;
        private readonly PacketXmlEditor _xmlEditor =
            new PacketXmlEditor();

        private PacketXmlEditDefinition _editDefinition;

        public PacketFieldDetailsWindow(
            PacketDefinitionDocument document,
            PacketDefinition packet,
            int packetIndex)
        {
            if (document == null)
            {
                throw new ArgumentNullException(
                    nameof(document));
            }

            if (packet == null)
            {
                throw new ArgumentNullException(
                    nameof(packet));
            }

            if (packetIndex < 0 ||
                packetIndex >= document.Packets.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(packetIndex));
            }

            _sourcePath = document.SourcePath;
            _packetIndex = packetIndex;
            _originalPacketName = packet.Name;
            _originalMessageId = packet.MessageId;
            _originalPacket = packet;
            _editDefinition =
                PacketXmlEditDefinition.FromPacket(packet);

            InitializeComponent();
            LoadEditDefinition();
            SetEditMode(false);
        }

        public PacketDefinitionDocument?
            SavedDocument { get; private set; }

        private void LoadEditDefinition()
        {
            _editDefinition =
                PacketXmlEditDefinition.FromPacket(
                    _originalPacket);

            Title =
                _editDefinition.Name +
                " 필드 정보";
            PacketNameTextBox.Text =
                _editDefinition.Name;
            MessageIdTextBox.Text =
                _editDefinition.MessageId;
            MessageIdValueTextBox.Text =
                _editDefinition.MessageIdValue;
            SyncTextBox.Text =
                _editDefinition.Sync;
            DataTypeTextBox.Text =
                _editDefinition.DataType;
            FieldCountTextBlock.Text =
                "필드 목록 · " +
                _editDefinition.Fields.Count +
                "개";

            FieldGrid.ItemsSource =
                _editDefinition.Fields;
        }

        private void SetEditMode(bool isEditing)
        {
            PacketNameTextBox.IsReadOnly = !isEditing;
            MessageIdTextBox.IsReadOnly = !isEditing;
            MessageIdValueTextBox.IsReadOnly = !isEditing;
            SyncTextBox.IsReadOnly = !isEditing;
            DataTypeTextBox.IsReadOnly = !isEditing;
            FieldGrid.IsReadOnly = !isEditing;

            EditModeInfoBorder.Visibility =
                isEditing
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            EditButton.Visibility =
                isEditing
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            CloseButton.Visibility =
                isEditing
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            CancelEditButton.Visibility =
                isEditing
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            SaveButton.Visibility =
                isEditing
                    ? Visibility.Visible
                    : Visibility.Collapsed;

            SaveStatusTextBlock.Text =
                isEditing
                    ? "수정할 셀을 더블클릭하여 값을 입력하세요."
                    : "조회 모드";
        }

        private void EditButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            SetEditMode(true);
            PacketNameTextBox.Focus();
            PacketNameTextBox.SelectAll();
        }

        private void CancelEditButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            LoadEditDefinition();
            SetEditMode(false);
        }

        private async void SaveButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            FieldGrid.CommitEdit(
                DataGridEditingUnit.Cell,
                true);
            FieldGrid.CommitEdit(
                DataGridEditingUnit.Row,
                true);

            _editDefinition.Name =
                PacketNameTextBox.Text;
            _editDefinition.MessageId =
                MessageIdTextBox.Text;
            _editDefinition.MessageIdValue =
                MessageIdValueTextBox.Text;
            _editDefinition.Sync =
                SyncTextBox.Text;
            _editDefinition.DataType =
                DataTypeTextBox.Text;

            SetSavingState(true);

            try
            {
                PacketDefinitionDocument saved =
                    await Task.Run(
                        () =>
                            _xmlEditor.SavePacket(
                                _sourcePath,
                                _packetIndex,
                                _originalPacketName,
                                _originalMessageId,
                                _editDefinition));

                SavedDocument = saved;
                DialogResult = true;
            }
            catch (Exception exception)
            {
                SaveStatusTextBlock.Text =
                    "저장하지 못했습니다.";

                System.Windows.MessageBox.Show(
                    this,
                    exception.Message,
                    "XML 저장 오류",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
            }
            finally
            {
                if (IsVisible)
                {
                    SetSavingState(false);
                }
            }
        }

        private void SetSavingState(bool isSaving)
        {
            PacketNameTextBox.IsEnabled = !isSaving;
            MessageIdTextBox.IsEnabled = !isSaving;
            MessageIdValueTextBox.IsEnabled = !isSaving;
            SyncTextBox.IsEnabled = !isSaving;
            DataTypeTextBox.IsEnabled = !isSaving;
            FieldGrid.IsEnabled = !isSaving;
            CancelEditButton.IsEnabled = !isSaving;
            SaveButton.IsEnabled = !isSaving;
            SaveButton.Content =
                isSaving
                    ? "저장 중..."
                    : "저장";
            SaveProgressBar.Visibility =
                isSaving
                    ? Visibility.Visible
                    : Visibility.Collapsed;

            if (isSaving)
            {
                SaveStatusTextBlock.Text =
                    "XML 전체 규칙을 검증하고 저장하는 중입니다.";
            }
        }

        private void CloseButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            Close();
        }
    }
}
