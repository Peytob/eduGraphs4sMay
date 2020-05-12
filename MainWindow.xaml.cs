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
        int m_currentH;
        int m_currentS;

        public MainWindow()
        {
            InitializeComponent();

            SquareSideTextBox.Text = "500";
            VertexesDeltaTextBox.Text = "50";
            VertexesNumber.Text = "12";

            GraphUserControl.Content = new GraphControlRect(50, 500, 12);
            (GraphUserControl.Content as GraphControl).GenerateGraph();

            int sSharpHelpMe = (int)(GraphUserControl.Content as GraphControl).S(500);
            AreaTextBox.Text = $"{sSharpHelpMe}";

            m_currentH = int.Parse(SquareSideTextBox.Text);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            int delta = int.Parse(VertexesDeltaTextBox.Text);
            int vertexes = int.Parse(VertexesNumber.Text);
            bool showRadiis = (GraphUserControl.Content as GraphControl).DeltaShow;

            if (FieldTypeComboBox.Text == "Квадрат")
            {
                int H = (int)GraphControlRect.HStatic(int.Parse(AreaTextBox.Text));
                GraphUserControl.Content = new GraphControlRect(delta, H, vertexes);
            }

            else if (FieldTypeComboBox.Text == "Круг")
            {
                int H = (int)GraphControlCircle.HStatic(int.Parse(AreaTextBox.Text));
                GraphUserControl.Content = new GraphControlCircle(delta, H, vertexes);
            }

            else if (FieldTypeComboBox.Text == "Треугольник")
            {
                int H = (int)GraphControlTrinagle.HStatic(int.Parse(AreaTextBox.Text));
                GraphUserControl.Content = new GraphControlTrinagle(delta, H, vertexes);
            }

            if (showRadiis)
                (GraphUserControl.Content as GraphControl).ToggleDeltaShow();

            (GraphUserControl.Content as GraphControl).GenerateGraph();
            SquareSideTextBox.Text = ((int)(GraphUserControl.Content as GraphControl).H()).ToString();
        }

        private void OnlyNumbersFilter(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
                (GraphUserControl.Content as GraphControl).ToggleDeltaShow();
        }

        private void OnlyNumbersLess100Filter(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || (sender as TextBox).Text.Length >= 2)
                e.Handled = true;
        }

        private void SquareSideTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SquareSideTextBox.Text.Length == 0)
            {
                SquareSideTextBox.Text = "0";
                return;
            }

            int h = int.Parse(SquareSideTextBox.Text);

            int sSharpHelpMe = (int)(GraphUserControl.Content as GraphControl).S(h);
            AreaTextBox.Text = $"{sSharpHelpMe}";
        }
    }
}
