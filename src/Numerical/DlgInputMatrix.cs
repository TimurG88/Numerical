using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace YuMV.NumericalMethods
{
    internal class DlgInputMatrix : Window
    {
        private readonly Button btnOk;

        private readonly TextBox RangeEquation;
        private readonly TextBox txtMatrix;

        public DlgInputMatrix()
        {
            Title = " Input Equation";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;
            // Создание объекта StackPanel 
            // и его назначение содержимым окна 
            var stack = new StackPanel();
            Content = stack;
            // Создание GroupBox как дочернего объекта StackPanel 
            var grpbox = new GroupBox();
            grpbox.BorderBrush = Brushes.Gray;
            grpbox.BorderThickness = new Thickness(2);
            grpbox.Header = "Parameters:";
            grpbox.Margin = new Thickness(20);
            stack.Children.Add(grpbox);
            // Назначение объекта Grid содержимым GroupBox 
            var grid = new Grid();
            grid.Margin = new Thickness(15);
            grpbox.Content = grid;
            // Две строки и четыре столбца 
            var rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(10, GridUnitType.Pixel);
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            var coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            // Размещение элементов Label и TextBox в элементе Grid 
            var grpboxIn = new GroupBox();
            grpboxIn.BorderThickness = new Thickness(2);
            grpboxIn.Header = "Matrix data:";
            grpboxIn.Margin = new Thickness(3);
            grid.Children.Add(grpboxIn);
            Grid.SetRow(grpboxIn, 0);
            Grid.SetColumn(grpboxIn, 0);

            var gridIn = new Grid();
            gridIn.Margin = new Thickness(5);
            grpboxIn.Content = gridIn;
            // Две строки и четыре столбца 
            var rowdefIn = new RowDefinition();
            rowdefIn.Height = GridLength.Auto;
            gridIn.RowDefinitions.Add(rowdefIn);
            var coldefIn = new ColumnDefinition();
            coldefIn.Width = GridLength.Auto;
            gridIn.ColumnDefinitions.Add(coldefIn);
            coldefIn = new ColumnDefinition();
            coldefIn.Width = GridLength.Auto;
            gridIn.ColumnDefinitions.Add(coldefIn);

            var lblbText = new Label();
            lblbText.Foreground = Brushes.Black;
            lblbText.Content = "Dimension of matrix (n x n):";
            lblbText.HorizontalAlignment = HorizontalAlignment.Left;
            gridIn.Children.Add(lblbText);
            Grid.SetRow(lblbText, 0);
            Grid.SetColumn(lblbText, 0);

            RangeEquation = new TextBox();
            RangeEquation.Margin = new Thickness(2);
            RangeEquation.TextChanged += TextBoxOnTextChanged;
            RangeEquation.Width = 50;
            gridIn.Children.Add(RangeEquation);
            Grid.SetRow(RangeEquation, 0);
            Grid.SetColumn(RangeEquation, 1);

            var stak = new StackPanel();
            var scroll = new ScrollViewer();
            scroll.Width = 300;
            scroll.Height = 200;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrix = new TextBox();
            txtMatrix.BorderThickness = new Thickness(2);
            stak.Children.Add(txtMatrix);
            scroll.Content = stak;
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);
            Grid.SetColumn(scroll, 0);

            // Создание элемента UniformGrid для кнопок OK и Cancel 
            var unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 2;
            stack.Children.Add(unigrid);
            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            // btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Right;
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);
            var btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.IsCancel = true;
            btnCancel.MinWidth = 60;
            btnCancel.Margin = new Thickness(12);
            btnCancel.HorizontalAlignment = HorizontalAlignment.Left;
            unigrid.Children.Add(btnCancel);

            if (RangeEquation.Text != "")
                Range = Convert.ToInt32(RangeEquation.Text);
            else
                txtMatrix.IsEnabled = false;
        }

        public double[,] Matrix { get; set; }

        public int Range { get; set; }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (RangeEquation.Text != "")
            {
                Range = Convert.ToInt32(RangeEquation.Text);
                if (Range == 0 || Range == 1)
                {
                    txtMatrix.IsEnabled = false;
                    txtMatrix.Text = "";
                }
                else
                {
                    if (Range <= 20)
                    {
                        txtMatrix.IsEnabled = true;
                        txtMatrix.Text = "";
                        for (var i = 0; i < Range; i++)
                        {
                            for (var j = 0; j < Range; j++) txtMatrix.Text = txtMatrix.Text + "0 ";
                            txtMatrix.Text = txtMatrix.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.", "Error");
                        txtMatrix.IsEnabled = false;
                        txtMatrix.Text = "";
                    }
                }
            }
            else
            {
                txtMatrix.Text = "";
                txtMatrix.IsEnabled = false;
            }
        }

        private void inputTb_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        public void InitDlg()
        {
            RangeEquation.Text = Range.ToString();
            txtMatrix.Text = "";
            for (var i = 0; i < Range; i++)
            {
                for (var j = 0; j < Range; j++) txtMatrix.Text = txtMatrix.Text + Matrix[i, j] + " ";
                txtMatrix.Text = txtMatrix.Text + " \n";
            }
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            if (RangeEquation.Text != "")
            {
                Range = Convert.ToInt32(RangeEquation.Text);
                Matrix = new double[2 * Range, 2 * Range];
            }
            else
            {
                MessageBox.Show("Enter dimension of matrix (n x n)", "Error");
            }

            var strLine = "";

            if (txtMatrix.Text != "")
            {
                for (var i = 0; i < Range; i++)
                {
                    strLine = txtMatrix.GetLineText(i);
                    for (var j = 0; j < Range; j++)
                        Matrix[i, j] = Convert.ToDouble(strLine.Split(' ')[j]);
                }

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Enter matrix", "Error");
            }
        }
    }
}