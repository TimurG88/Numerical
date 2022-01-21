using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;

namespace YuMV.NumericalMethods
{
    public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
    {
        private static bool m_bPaint = true;
        private int m_nContentLength;
        private int m_nCurSelection;
        private int m_nLineEnd;
        private int m_nLineLength;
        private int m_nLineStart;
        private string m_strKeywords = "";
        private string m_strLine = "";

        /// <summary>
        ///     The settings.
        /// </summary>
        public SyntaxSettings Settings { get; } = new SyntaxSettings();

        /// <summary>
        ///     WndProc
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x00f)
            {
                if (m_bPaint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        /// <summary>
        ///     OnTextChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            // Calculate shit here.
            m_nContentLength = this.TextLength;

            int nCurrentSelectionStart = SelectionStart;
            int nCurrentSelectionLength = SelectionLength;

            m_bPaint = false;

            // Find the start of the current line.
            m_nLineStart = nCurrentSelectionStart;
            while (m_nLineStart > 0 && Text[m_nLineStart - 1] != '\n')
                m_nLineStart--;
            // Find the end of the current line.
            m_nLineEnd = nCurrentSelectionStart;
            while (m_nLineEnd < Text.Length && Text[m_nLineEnd] != '\n')
                m_nLineEnd++;
            // Calculate the length of the line.
            m_nLineLength = m_nLineEnd - m_nLineStart;
            // Get the current line.
            m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

            // Process this line.
            ProcessLine();

            m_bPaint = true;
        }

        /// <summary>
        ///     Process a line.
        /// </summary>
        private void ProcessLine()
        {
            // Save the position and make the whole line black
            int nPosition = SelectionStart;
            SelectionStart = m_nLineStart;
            SelectionLength = m_nLineLength;
            SelectionColor = Color.Black;

            // Process the keywords
            ProcessRegex(m_strKeywords, Settings.KeywordColor);
            // Process numbers
            if (Settings.EnableIntegers)
                ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", Settings.IntegerColor);
            // Process strings
            if (Settings.EnableStrings)
                ProcessRegex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", Settings.StringColor);
            // Process comments
            if (Settings.EnableComments && !string.IsNullOrEmpty(Settings.Comment))
                ProcessRegex(Settings.Comment + ".*$", Settings.CommentColor);

            SelectionStart = nPosition;
            SelectionLength = 0;
            SelectionColor = Color.Black;

            m_nCurSelection = nPosition;
        }

        /// <summary>
        ///     Process a regular expression.
        /// </summary>
        /// <param name="strRegex">The regular expression.</param>
        /// <param name="color">The color.</param>
        private void ProcessRegex(string strRegex, Color color)
        {
            var regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match regMatch;

            for (regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                var nStart = m_nLineStart + regMatch.Index;
                var nLenght = regMatch.Length;
                SelectionStart = nStart;
                SelectionLength = nLenght;
                SelectionColor = color;
            }
        }

        /// <summary>
        ///     Compiles the keywords as a regular expression.
        /// </summary>
        public void CompileKeywords()
        {
            for (var i = 0; i < Settings.Keywords.Count; i++)
            {
                var strKeyword = Settings.Keywords[i];

                if (i == Settings.Keywords.Count - 1)
                    m_strKeywords += "\\b" + strKeyword + "\\b";
                else
                    m_strKeywords += "\\b" + strKeyword + "\\b|";
            }
        }

        public void ProcessAllLines()
        {
            m_bPaint = false;

            var nStartPos = 0;
            var i = 0;
            int nOriginalPos = SelectionStart;
            while (i < Lines.Length)
            {
                m_strLine = Lines[i];
                m_nLineStart = nStartPos;
                m_nLineEnd = m_nLineStart + m_strLine.Length;

                ProcessLine();
                i++;

                nStartPos += m_strLine.Length + 1;
            }

            m_bPaint = true;
        }
    }

    /// <summary>
    ///     Class to store syntax objects in.
    /// </summary>
    public class SyntaxList
    {
        public Color m_color = new Color();
        public List<string> m_rgList = new List<string>();
    }

    /// <summary>
    ///     Settings for the keywords and colors.
    /// </summary>
    public class SyntaxSettings
    {
        private readonly SyntaxList m_rgKeywords = new SyntaxList();

        #region Properties

        /// <summary>
        ///     A list containing all keywords.
        /// </summary>
        public List<string> Keywords => m_rgKeywords.m_rgList;

        /// <summary>
        ///     The color of keywords.
        /// </summary>
        public Color KeywordColor
        {
            get { return m_rgKeywords.m_color; }
            set { m_rgKeywords.m_color = value; }
        }

        /// <summary>
        ///     A string containing the comment identifier.
        /// </summary>
        public string Comment { get; set; } = "";

        /// <summary>
        ///     The color of comments.
        /// </summary>
        public Color CommentColor { get; set; } = Color.Green;

        /// <summary>
        ///     Enables processing of comments if set to true.
        /// </summary>
        public bool EnableComments { get; set; } = true;

        /// <summary>
        ///     Enables processing of integers if set to true.
        /// </summary>
        public bool EnableIntegers { get; set; } = true;

        /// <summary>
        ///     Enables processing of strings if set to true.
        /// </summary>
        public bool EnableStrings { get; set; } = true;

        /// <summary>
        ///     The color of strings.
        /// </summary>
        public Color StringColor { get; set; } = Color.Gray;

        /// <summary>
        ///     The color of integers.
        /// </summary>
        public Color IntegerColor { get; set; } = Color.Red;

        #endregion
    }
}