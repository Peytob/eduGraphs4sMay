using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Graph m_graph;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация графа
            m_graph = new Graph();

            GraphUserControl.Content = new GraphControlRect(m_graph, 40f);
            ((GraphControlRect)(GraphUserControl.Content)).GenerateGraph();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            ((GraphControlRect)(GraphUserControl.Content)).GenerateGraph();
        }

        private void OnlyNumbersFilter(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void FieldTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
                ((GraphControlRect)(GraphUserControl.Content)).ToggleDeltaShow();
        }
    }
}
