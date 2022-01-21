using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace YuMV.NumericalMethods
{
    internal class DlgInputSystemEquation : Window
    {
        private readonly Button btnOk;
        private readonly TextBox RangeEquation;
        private readonly TextBox txtMatrA;
        private readonly TextBox txtMatrB;

        public DlgInputSystemEquation()
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
            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            var coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);


            // Размещение элементов Label и TextBox в элементе Grid 
            //RichTextBox source = new RichTextBox();

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
            lblbText.Content = "Dimension of equation (n x n):";
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


            var lblbTextB = new Label();
            lblbTextB.Foreground = Brushes.Black;
            lblbTextB.HorizontalAlignment = HorizontalAlignment.Left;
            lblbTextB.Content = "b1[], b2[], b3[], ... ,bn[]";
            grid.Children.Add(lblbTextB);
            Grid.SetRow(lblbTextB, 2);
            Grid.SetColumn(lblbTextB, 0);

            var lblbTextA = new Label();
            lblbTextA.Foreground = Brushes.Black;
            lblbTextA.Content = "a[]";
            lblbTextA.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(lblbTextA);
            Grid.SetRow(lblbTextA, 2);
            Grid.SetColumn(lblbTextA, 1);

            var stak = new StackPanel();
            var scroll = new ScrollViewer();
            scroll.Width = 250;
            scroll.Height = 150;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrB = new TextBox();
            txtMatrB.BorderThickness = new Thickness(2);
            stak.Children.Add(txtMatrB);
            scroll.Content = stak;
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 3);
            Grid.SetColumn(scroll, 0);

            var stakA = new StackPanel();
            var scrollA = new ScrollViewer();
            scrollA.Width = 50;
            scrollA.Height = 150;
            scrollA.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrA = new TextBox();
            txtMatrA.BorderThickness = new Thickness(2);
            stakA.Children.Add(txtMatrA);
            scrollA.Content = stakA;
            grid.Children.Add(scrollA);
            Grid.SetRow(scrollA, 3);
            Grid.SetColumn(scrollA, 1);

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
            {
                Range = Convert.ToInt32(RangeEquation.Text);
            }
            else
            {
                txtMatrB.IsEnabled = false;
                txtMatrA.IsEnabled = false;
            }
        }

        public double[] MatrixA { get; set; }

        public double[,] MatrixB { get; set; }

        public int Range { get; set; }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (RangeEquation.Text != "")
            {
                Range = Convert.ToInt32(RangeEquation.Text);
                if (Range == 0 || Range == 1)
                {
                    txtMatrB.IsEnabled = false;
                    txtMatrA.IsEnabled = false;
                    txtMatrB.Text = "";
                    txtMatrA.Text = "";
                }
                else
                {
                    if (Range <= 20)
                    {
                        txtMatrB.IsEnabled = true;
                        txtMatrA.IsEnabled = true;
                        txtMatrB.Text = "";
                        txtMatrA.Text = "";
                        for (var i = 0; i < Range; i++)
                        {
                            txtMatrA.Text = txtMatrA.Text + "0\n";
                            for (var j = 0; j < Range; j++) txtMatrB.Text = txtMatrB.Text + "0 ";
                            txtMatrB.Text = txtMatrB.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.", "Error");
                        txtMatrB.IsEnabled = false;
                        txtMatrA.IsEnabled = false;
                        txtMatrB.Text = "";
                        txtMatrA.Text = "";
                    }
                }
            }
            else
            {
                txtMatrB.Text = "";
                txtMatrA.Text = "";
                txtMatrB.IsEnabled = false;
                txtMatrA.IsEnabled = false;
            }
        }

        private void inputTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            /* source = (TextBox)sender;
             string text = source.Text;
             try
             {
                 if (text.Length > 0)
                 {
                     p.Parameters.Add("x");
                     p.Parameters.Add("X");
                     p.Parameters.Add("y");
                     p.Parameters.Add("Y");
                     OutPut.Text = p.Parse(text).Tree.ToPolishInversedNotationString();
                 }
             }
             catch (ParserException exc)
             {
                 OutPut.Text = exc.Message;
             }*/
        }

        public void InitDlg()
        {
            RangeEquation.Text = Range.ToString();
            txtMatrA.Text = "";
            txtMatrB.Text = "";
            for (var i = 0; i < Range; i++)
            {
                txtMatrA.Text = txtMatrA.Text + MatrixA[i] + "\n";
                for (var j = 0; j < Range; j++) txtMatrB.Text = txtMatrB.Text + MatrixB[i, j] + " ";
                txtMatrB.Text = txtMatrB.Text + " \n";
            }
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            if (RangeEquation.Text != "")
            {
                Range = Convert.ToInt32(RangeEquation.Text);
                MatrixA = new double[2 * Range];
                MatrixB = new double[2 * Range, 2 * Range];
            }
            else
            {
                MessageBox.Show("Enter dimension of equation (n x n)", "Error");
            }

            var strLine = "";
            if (txtMatrB.Text != "")
            {
                if (txtMatrA.Text != "")
                {
                    for (var i = 0; i < Range; i++)
                    {
                        strLine = txtMatrB.GetLineText(i);
                        for (var j = 0; j < Range; j++)
                            MatrixB[i, j] = Convert.ToDouble(strLine.Split(' ')[j]);
                    }

                    for (var i = 0; i < Range; i++)
                    {
                        strLine = txtMatrA.GetLineText(i);
                        MatrixA[i] = Convert.ToDouble(strLine);
                    }

                    // MessageBox.Show(MatrA[0].ToString() + " " + MatrA[1].ToString(), "Res");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Enter matrix A", "Error");
                }
            }
            else
            {
                MessageBox.Show("Enter matrix B", "Error");
            }
        }
    }
}