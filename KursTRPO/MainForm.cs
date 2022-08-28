using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace KursTRPO
{
    partial class MainForm : Form
    {
        #region Поля
        readonly List<string> PictureWords = new List<string> { "рисунок", "рис." }; //только строчными буквами
        string secNum;
        int section;
        int number;
        PictureNumber prevPicRefNum;
        PictureNumber prevPicNameNum;
        #endregion
        #region Конструкторы
        public MainForm() => InitializeComponent();
        #endregion
        #region Методы
        private void BtnStart_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application wordApp = new Application();
            Document document;
            try
            {
                document = wordApp.Documents.Open(tbPath.Text, null, true);
            }
            catch (Exception)
            {
                ShowError("Путь к документу указан неверно!");
                wordApp.Quit();
                Cursor = Cursors.Default;
                return;
            }
            File.Delete("РезультатНормоконтроля.txt");
            File.Create("РезультатНормоконтроля.txt").Close();
            CheckPicRefsAndNames(document);
            document.Close();
            wordApp.Quit();

            bool empty = false;
            using(var reader = new StreamReader("РезультатНормоконтроля.txt"))
            {
                if (reader.EndOfStream)
                    empty = true;
            }
            if (empty)
            {
                using (StreamWriter writer = new StreamWriter("РезультатНормоконтроля.txt"))
                {
                    writer.Write("Ошибки не обнаружены.");
                }
            }

            MessageBox.Show("Поиск завершён!", "Поиск",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Cursor = Cursors.Default;
            Process.Start("РезультатНормоконтроля.txt");
        }
        private void CheckPicRefsAndNames(Document document)
        {
            string text;
            int index;
            if (RdBtnBySections.Checked)
                section = 1;
            else
                section = 0;
            prevPicRefNum = new PictureNumber(section, 0);
            prevPicNameNum = new PictureNumber(section, 0);
            foreach (Paragraph item in document.Paragraphs)
            {
                if (PageNumber(item.Range) < 7)
                    continue;
                text = item.Range.Text.ToLower();
                index = IndexOfWord(text, out int length);
                if (index >= 0)
                {
                    secNum = text.Substring(index + length + 1);
                    if (item.Range.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphCenter)
                    {
                        if (!CheckRefSequence(item))
                            break;
                        if (!CheckName(item))
                            break;
                    }
                    else if (!CheckNameSequence(item))
                        break;
                }
            }
        }
        private int IndexOfWord(string text, out int length)
        {
            int index;
            foreach (var item in PictureWords)
            {
                index = text.IndexOf(item);
                if (index >= 0)
                {
                    length = item.Length;
                    return index;
                }
            }
            length = -1;
            return -1;
        }
        private bool CheckNameSequence(Paragraph item)
        {
            try
            {
                secNum = secNum.Substring(0, secNum.IndexOf(' '));
            }
            catch (ArgumentOutOfRangeException)
            {
                WriteError($"Отсутствует пробел после номера:\n{secNum}!", item.Range);
                return true;
            }
            if (!GetSecAndNum(item))
                return true;
            PictureNumber picNameNum = new PictureNumber(section, number);
            if (!prevPicNameNum.IsBefore(picNameNum))
                if (WriteWarning($"Название рисунка {secNum} идёт сразу " +
                    $"после названия рисунка {prevPicNameNum}!", item.Range))
                    return false;
            prevPicNameNum = picNameNum;
            return true;
        }
        /// <summary>
        /// Проверка наличия и правильности подписи рисунка через 2 абзаца после его упоминания.
        /// </summary>
        /// <param name="item">Параграф</param>
        /// <param name="secNum">{Номер раздела}.{Номер рисунка}</param>
        /// <returns>True - продолжить проверку, false - прекратить.</returns>
        private bool CheckName(Paragraph item)
        {
            Range picNameRange = item.Next().Next().Range;
            string picName = picNameRange.Text;
            if (!picName.StartsWith("Рисунок "))
            {
                if (WriteWarning($"После упоминания рисунка {secNum} отсутствует его название!\n" +
                    $"Название должно начинаться со слова \"Рисунок\" и располагаться на втором абзаце " +
                    $"после упоминания", picNameRange))
                    return true;
            }
            else
            {
                string secNum2;
                try
                {
                    secNum2 = picName.Substring(picName.IndexOf(' ') + 1);
                    secNum2 = secNum2.Substring(0, secNum2.IndexOf(' '));
                }
                catch (ArgumentOutOfRangeException)
                {
                    WriteError($"Отсутствует пробел после номера:\n{picName}!", item.Range);
                    return true;
                }
                if (picNameRange.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphCenter)
                    if (WriteWarning($"Название рисунка {secNum2} выровнено не по центру!", picNameRange))
                        return false;
                if (picNameRange.Font.Name != "Times New Roman")
                    if (WriteWarning($"Название рисунка {secNum2} не использует шрифт Times New Roman!", picNameRange))
                        return false;
                if (picNameRange.Font.Size != 12)
                    if (WriteWarning($"Название рисунка {secNum2} не использует размер шрифта 12 пт!", picNameRange))
                        return false;
                if (secNum != secNum2)
                    if (WriteWarning($"Номер рисунка в названии {secNum2} не совпадает с номером " +
                        $"при упоминании {secNum}!", picNameRange))
                        return false;
            }
            return true;
        }
        /// <summary>
        /// Проверка правильности последовательности упоминаний рисунков.
        /// </summary>
        /// <param name="secNum">{Номер раздела}.{Номер рисунка}</param>
        /// <param name="prevPicRefNum">Предыдущая ссылка на рисунок.</param>
        /// <param name="item">Параграф.</param>
        /// <returns>True - продолжить проверку, false - прекратить.</returns>
        private bool CheckRefSequence(Paragraph item)
        {
            try
            {
                secNum = secNum.Substring(0, IndexOfEndOfNumber(secNum));
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return true;
            }
            if (!GetSecAndNum(item))
                return true;
            PictureNumber picRefNum = new PictureNumber(section, number);
            if (!prevPicRefNum.IsBefore(picRefNum))
                if (WriteWarning($"Упоминание рисунка {secNum} идёт сразу после " +
                    $"упоминания рисунка {prevPicRefNum}!", item.Range))
                    return false;
            prevPicRefNum = picRefNum;
            return true;
        }
        private bool GetSecAndNum(Paragraph item)
        {
            if (RdBtnBySections.Checked)
                try
                {
                    if (!int.TryParse(secNum.Substring(0, secNum.IndexOf('.')), out section))
                    {
                        WriteError($"Не удалось преобразовать символы до точки в число:\n{secNum}", item.Range);
                        return false;
                    }
                    if (!int.TryParse(secNum.Substring(secNum.IndexOf('.') + 1), out number))
                    {
                        WriteError($"Не удалось преобразовать символы после точки в число:\n{secNum}", item.Range);
                        return false;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    WriteError($"Отсутствует точка-разделитель в номере:\n{secNum}!", item.Range);
                    return false;
                }
            else
            {
                section = 0;
                if (!int.TryParse(secNum, out number))
                {
                    WriteError("Не удалось преобразовать символы в число:", item.Range);
                    return false;
                }
            }
            return true;
        }
        int PageNumber(Range range) => range.Information[WdInformation.wdActiveEndPageNumber];
        /// <summary>
        /// Обнаруживает первое вхождение символа из внутреннего списка.
        /// </summary>
        /// <param name="number">Строка с числом.</param>
        /// <returns>Индекс вхождения.</returns>
        /// <exception cref="Exception"></exception>
        int IndexOfEndOfNumber(string number)
        {
            string symbols = "),.?!:;\"\'>]} ";
            int index;
            foreach (var item in symbols)
            {
                index = number.IndexOf(item);
                if (index >= 0)
                    return index;
            }
            throw new Exception($"После номера идёт неизвестный символ: {number}");
        }
        void ShowError(string message) => MessageBox.Show(message, "Ошбика",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        void WriteError(string message, Range range) => 
            WriteToFile("ОШИБКА:\n" + message + "\nНомер страницы: " +
                $"{PageNumber(range)}.\n");
        bool WriteWarning(string message, Range range)
        {
            WriteToFile($"ПРЕДУПРЕЖДЕНИЕ:\n{message}\n" +
            $"Номер страницы: {PageNumber(range)}.\n");
            return false;
        }

        void WriteToFile(string toWrite)
        {
            using (StreamWriter writer = new StreamWriter("РезультатНормоконтроля.txt", true))
            {
                writer.Write(toWrite);
            }
        }
        #endregion
    }
}
