using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TextAnalyzer
{

    public partial class MainWindow : Window
    {
        GetText getText = new GetText();

        Verificator verificator = new Verificator();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// There are some inputs validations in this method in order to check ids.
        /// Also in this method downloading of a content happens with an error exception handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            GridRemove();

            if (textId.Text == "")
            {
                MessageBoxResult mbResult = MessageBox.Show("Введите идентификаторы строк", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            string formattedId = textId.Text.Replace(",", "")
                                        .Replace(";","")
                                        .Replace(" ","");

            if (formattedId.All(char.IsDigit) == false || formattedId == "")
            {
                MessageBoxResult mbResult = MessageBox.Show("Недопустимые символы в поиске", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string identificators = textId.Text;

            string[] validId = verificator.getValidated(identificators);

            List<TextStringModel> contentList = new List<TextStringModel>();

            try
            {
                foreach (string id in validId)
                {
                    {
                        int x = Int32.Parse(id);

                        if (x > 20)
                        {
                            MessageBoxResult mbResult = MessageBox.Show("Введено слишком большое значение идентификатора." +
                                                                        "\nМаксимальное значение - 20", "Error",
                                                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        var content = getText.downloadContent<TextStringModel>(id);

                        contentList.Add(content);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBoxResult mbResult = MessageBox.Show("Возникла ошибка: " + ex.Message, "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Grid dynamicGrid = GridCreator();

            for (int i = 1; i <= contentList.Count; i++)
            {
                var textStringModel = contentList[i-1];

                var textViewModel = new TextStringViewModel(textStringModel, verificator); 

                RowCreator(textViewModel, i, dynamicGrid);
            }
        }

        /// <summary>
        /// This method creates new dynamicGrid, devides it into 3 columns and add header
        /// </summary>
        /// <returns></returns>
        public Grid GridCreator()
        {
            Grid dynamicGrid = new Grid();

            dynamicGrid.Margin = new Thickness(0, 68, 0, 0);

            dynamicGrid.ShowGridLines = false;

            ColumnDefinition textColumn = new ColumnDefinition();

            ColumnDefinition wordAmountColumn = new ColumnDefinition();

            ColumnDefinition vowelAmountColumn = new ColumnDefinition();

            dynamicGrid.ColumnDefinitions.Add(textColumn);

            dynamicGrid.ColumnDefinitions.Add(wordAmountColumn);

            wordAmountColumn.Width = new GridLength(1, GridUnitType.Auto);

            dynamicGrid.ColumnDefinitions.Add(vowelAmountColumn);

            vowelAmountColumn.Width = new GridLength(1, GridUnitType.Auto);

            RowDefinition headerRow = new RowDefinition();

            dynamicGrid.RowDefinitions.Add(headerRow);

            headerRow.Height = new GridLength(1, GridUnitType.Auto);

            Label text = new Label();

            text.Content = "Текст";

            text.FontSize = 14;

            text.FontWeight = FontWeights.Bold;

            Grid.SetRow(text, 0);

            Grid.SetColumn(text, 0);

            dynamicGrid.Children.Add(text);

            Label wordAmount = new Label();

            wordAmount.Content = "Количество слов";

            wordAmount.FontSize = 14;

            wordAmount.FontWeight = FontWeights.Bold;

            Grid.SetRow(wordAmount, 0);

            Grid.SetColumn(wordAmount, 1);

            dynamicGrid.Children.Add(wordAmount);

            Label vowelAmount = new Label();

            vowelAmount.Content = "Количество гласных";

            vowelAmount.FontSize = 14;

            vowelAmount.FontWeight = FontWeights.Bold;

            Grid.SetRow(vowelAmount, 0);

            Grid.SetColumn(vowelAmount, 2);

            dynamicGrid.Children.Add(vowelAmount);

            mainGrid.Children.Add(dynamicGrid);

            this.SizeToContent = SizeToContent.WidthAndHeight;

            return dynamicGrid;
        }

        /// <summary>
        /// This method deletes the dynamicGrid in order to redo the process
        /// </summary>
        public void GridRemove()
        {
            if (mainGrid.Children.Count > 3)
            {
                mainGrid.Children.RemoveRange(3, 1);
            }
            mainWindow.MinWidth = 806;
        }

        /// <summary>
        /// This method creates rows with a string which was downloaded from server.
        /// It also inputs calculated data of vowels and words
        /// </summary>

        public void RowCreator(TextStringViewModel textStringViewModel, int i, Grid dynamicGrid)
        {
            RowDefinition dynamicRow = new RowDefinition();

            dynamicGrid.RowDefinitions.Add(dynamicRow);

            dynamicRow.Height = GridLength.Auto;

            TextBlock textBlock = new TextBlock();

            TextBlock vowelText = new TextBlock();

            TextBlock wordText = new TextBlock();

            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.Text = textStringViewModel.Text + "\n";

            wordText.Text = textStringViewModel.wordCount;

            vowelText.Text = textStringViewModel.vowelCount;

            Grid.SetRow(textBlock, i);

            Grid.SetColumn(textBlock, 0);

            dynamicGrid.Children.Add(textBlock);

            Grid.SetRow(vowelText, i);

            Grid.SetColumn(vowelText, 2);

            dynamicGrid.Children.Add(vowelText);

            Grid.SetRow(wordText, i);

            Grid.SetColumn(wordText, 1);

            dynamicGrid.Children.Add(wordText);
        }
    }
}
