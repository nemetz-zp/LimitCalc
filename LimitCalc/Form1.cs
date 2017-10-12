using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LimitCalc
{
    public partial class Form1 : Form
    {
        private MetersList metersList = new MetersList();
        private bool programIsReady = false;

        #region Настройки программы

        // Точность цены за 1 кВт
        private const int PRICE_PRECISION = 3;

        // Точность суммы закупки
        private const int TENDER_SUM_PRECISION = 2;

        // Имя каталога с шаблонами
        private const string TEMPLATES_DIRECTORY_NAME = "templates";

        // Имя каталога с конфигурациями
        private const string CFG_DIRECTORY_NAME = "cfg";

        // Имя файла-шаблона
        private const string TEMPLATES_FILE_NAME = "limits.xlsx";

        // Имя файла в который будет произведено сохранение расчитаных лимитов
        private const string READY_FILE_NAME = "READY.xlsx";

        // Имя файла конфигурации по-умолчанию
        private const string DEFAULT_CFG_FILE_NAME = "default.xml";

        // Адрес первой ячейки столбца с именами ФАПов (здесь установлено как В8)
        private const string NAME_COLUMN_LETTER = "B";
        private const int NAME_COLUMN_INDEX = 9;
        
        // Буква столбца с мощностями 
        private const string POWER_COLUMN_LETTER = "C";

        // Буквы столбцов с месяцами
        private static string[] MONTHS_LETTERS = {   
                                                    "F",  // Январь
                                                    "H",  // Февраль
                                                    "J",  // Март
                                                    "N",  // Апрель
                                                    "P",  // Май
                                                    "R",  // Июнь
                                                    "V",  // Июль
                                                    "X",  // Август
                                                    "Z",  // Сентябрь
                                                    "AD", // Октябрь
                                                    "AF", // Ноабрь
                                                    "AH"  // Декабрь
                                                };

        // Буквы столбцов с итогами по кварталам
        private static string[] QUARTER_LETTERS = {
                                                    "D",  // 1 квартал
                                                    "L",  // 2 квартал
                                                    "T",  // 3 квартал
                                                    "AB"  // 4 квартал
                                                 };

        // Буква столбца с итогам по году
        private const string YEAR_LETTER = "AJ";

        // Номер строки с общими итогами по ФАПам
        private const int YEAR_BEGIN_INDEX = 8;

        #endregion

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            Visible = false;

            #region Проверка состояния файлов программы и загрузка данных из Excel
            LoadingProgramForm lf = new LoadingProgramForm(this);
            lf.Show();
            
            lf.ChangeLoadProgress("Перевірка файлової структури системи...", 0);

            // Проверяем наличие директории с шаблонами
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(getApplicationDirectory(), TEMPLATES_DIRECTORY_NAME)))
            {
                lf.ChangeLoadProgress("Створюємо директорію із шаблонами 'templates'...", 0);
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(getApplicationDirectory(), TEMPLATES_DIRECTORY_NAME));
                lf.ChangeLoadProgress("Директорія 'templates' створена!", 4);
            }
            else
            {
                lf.ChangeLoadProgress("Директорія 'templates' наявна...", 4);
            }

            // Проверяем наличие директории с конфигурациями
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(getApplicationDirectory(), CFG_DIRECTORY_NAME)))
            {
                lf.ChangeLoadProgress(
                    string.Format("Відсутня директорія із конфігураціями '{0}'. Створюємо її...", CFG_DIRECTORY_NAME), 
                    0);

                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(getApplicationDirectory(), CFG_DIRECTORY_NAME));
                
                lf.ChangeLoadProgress(
                    string.Format("Директорія із конфігураціями '{0}' успішно створена!", CFG_DIRECTORY_NAME), 
                    4);
            }
            else
            {
                lf.ChangeLoadProgress(string.Format("Директорія із конфігураціями '{0}' наявна...", CFG_DIRECTORY_NAME), 4);
                LoadCfgList();
            }

            if (!System.IO.File.Exists(System.IO.Path.Combine(getApplicationDirectory(), 
                TEMPLATES_DIRECTORY_NAME, 
                TEMPLATES_FILE_NAME)))
            {
                lf.ChangeLoadProgress("Створюємо файл-шаблон...", 0);
                using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(getApplicationDirectory(),
                TEMPLATES_DIRECTORY_NAME,
                TEMPLATES_FILE_NAME), System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    fs.Write(Properties.Resources.limits, 0, Properties.Resources.limits.Length);
                    fs.Flush();
                }
            }
            lf.ChangeLoadProgress(
                string.Format("Файл шаблону '{0}\\{1}' наявний...", TEMPLATES_DIRECTORY_NAME, TEMPLATES_FILE_NAME), 
                4);

            lf.ChangeLoadProgress("Завантажуємо шаблон...", 0);

            metersList = GetDataFromExelFile(System.IO.Path.Combine(getApplicationDirectory(), 
                TEMPLATES_DIRECTORY_NAME, TEMPLATES_FILE_NAME), 
                true);

            lf.ChangeLoadProgress("Данні завантажені!", 50);

            #endregion

            lf.ChangeLoadProgress("Налаштовуємо інтерфейс...", 0);

            metersList.IsInitialized = true;
            metersList.SetDefaultCoeffs();

            dataGridView1.DataSource = metersList;
            ShowCoeffs(metersList.First());

            lf.ChangeLoadProgress("Усе готово до роботи!", 0);
            lf.CompleteProcess();
            lf.Close();

            Visible = true;
            programIsReady = true;
        }

        private MetersList GetDataFromExelFile(string fileName, bool appStart)
        {
            MetersList mList = new MetersList();

            // запуск процесса Excel
            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                NotificationHelper.ShowError("Помилка при завантаженні екземпляру Microsoft Excel!");
                Environment.Exit(0);
            }

            // Загрузка книги
            Excel.Workbook wb = xlApp.Workbooks.Open(fileName);
            if (wb == null)
            {
                NotificationHelper.ShowError("Помилка при завантаженні книги Microsoft Excel!");
                xlApp.Quit();
                xlApp = null;
                Environment.Exit(0);
            }

            // Выбор листа с данными
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            if (ws == null)
            {
                NotificationHelper.ShowError("Помилка при завантаженні листа із книги Microsoft Excel!");
                xlApp.Quit();
                xlApp = null;
                Environment.Exit(0);
            }

            // Считывание данных с листа
            try
            {
                for (int i = 0; ; i++)
                {
                    string name = ws.get_Range(NAME_COLUMN_LETTER + (NAME_COLUMN_INDEX + i)).Value;
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    if (appStart)
                    {
                        mList.Add(
                            new EnergyMeter
                            {
                                Name = name,
                                IsHeating = (name.EndsWith("(опал.)") || name.EndsWith("(оп.)") || name.EndsWith("(опалення)")),
                                Number = (i + 1),
                                ConnectedPower = ws.get_Range(POWER_COLUMN_LETTER + (NAME_COLUMN_INDEX + i)).Value
                            });
                    }
                    else
                    {
                        double[] mounthLimits = new double[12];
                        for(int j = 0; j < MONTHS_LETTERS.Count(); j++)
                        {
                            mounthLimits[j] = ws.get_Range(MONTHS_LETTERS[j] + (NAME_COLUMN_INDEX + i)).Value;
                        }
                        EnergyMeter newMeter = new EnergyMeter(ws.get_Range(YEAR_LETTER + (NAME_COLUMN_INDEX + i)).Value,
                            mounthLimits);
                        newMeter.Name = name;
                        newMeter.Number = (i + 1);
                        newMeter.ConnectedPower = ws.get_Range(POWER_COLUMN_LETTER + (NAME_COLUMN_INDEX + i)).Value;
                        mList.Add(newMeter);
                    }
                }
            }
            catch(Exception ex)
            {
                NotificationHelper.ShowError("Помилка при завантаженні данних!");
                Environment.Exit(0);
            }
            finally
            {
                wb.Close(false);
                xlApp.Quit();
                xlApp = null;
                //System.GC.Collect();
            }
            
            return mList;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Не реагируем на изменение ячеек таблицы вызванное инициализацией элемента управления
            if(!programIsReady)
            {
                return;
            }
        }

        private string getApplicationDirectory()
        {
            System.IO.FileInfo currentExecutionFile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return currentExecutionFile.DirectoryName;
        }

        private void calcLimitesButton_Click(object sender, EventArgs e)
        {
            double volume = 0.0;
            try
            {
                volume = CalculateVolume();
            }
            catch(Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
                return;
            }

            //if (volume.EQ(0.0))
            //{
            //    if(NotificationHelper.AskYesNo("Об'єм закупівлі дорівнює нулю. Ви впевнені, що хочете продовжити?") == System.Windows.Forms.DialogResult.No)
            //    {
            //        return;
            //    }
            //}

            if (NotificationHelper.AskYesNo("Розпочати розрахунок?") == System.Windows.Forms.DialogResult.Yes)
            {
                if(!metersList.IsEverythingReady)
                {
                    if (NotificationHelper.AskYesNo(
                        "Для деяких ЛПЗ не встановлені коефіцієнти. Встановити для них значення за-умовчанням?") 
                        == System.Windows.Forms.DialogResult.Yes)
                    {

                        WaitForm wf = new WaitForm(this);
                        wf.Show();
                        wf.Refresh();
                        Enabled = false;

                        try
                        {
                            metersList.PrepareNotReadyMeters();
                        }
                        catch (Exception ex)
                        {
                            NotificationHelper.ShowError(ex.Message);
                        }
                        finally
                        {
                            wf.Close();
                            Enabled = true;
                        }

                        WriteLimitsToExcelFile();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                WriteLimitsToExcelFile();
            }
        }

        private void WriteLimitsToExcelFile()
        {
            try
            {
                metersList.SetSum(CalculateVolume());
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
                return;
            }

            HeavyProcessProgressForm hpForm = new HeavyProcessProgressForm(this);
            hpForm.Show();

            Enabled = false;

            Excel.Application xlApp = new Excel.Application();

            if(xlApp == null)
            {
                NotificationHelper.ShowError("Помилка при завантаженні Excel.");
                Environment.Exit(0);
            }
            hpForm.ChangeProgress(null, 2);

            Excel.Workbook wb = xlApp.Workbooks.Open(System.IO.Path.Combine(getApplicationDirectory(), TEMPLATES_DIRECTORY_NAME, TEMPLATES_FILE_NAME));

            if(wb == null)
            {
                xlApp.Quit();
                xlApp = null;
                NotificationHelper.ShowError("Помилка при завантаженні файлу шаблону.");
                Environment.Exit(0);
            }
            hpForm.ChangeProgress(null, 2);

            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            if(ws == null)
            {
                wb.Close(0);
                xlApp.Quit();
                xlApp = null;
                NotificationHelper.ShowError("Помилка при завантаженні файлу шаблону.");
                Environment.Exit(0);
            }
            hpForm.ChangeProgress(null, 2);

            int progrStep = (int)Math.Floor((double)(hpForm.ProgressRest / (metersList.Count() - 1)));
            try
            {
                // Массив итогов по месяцам
                double[] monthTotals = new double[12];
                // Массив итогов по кварталам
                double[] quartalsLimits = new double[QUARTER_LETTERS.Length];
                // Общая мощность
                double totalPower = 0.0;
                // Итог по году
                double yearTotal = 0.0;

                for(int i = 1; i < metersList.Count; i++)
                {
                    hpForm.ChangeProgress(string.Format("Заповнюємо ліміти по '{0}'...", metersList[i].Name), 
                        progrStep);

                    ws.get_Range(POWER_COLUMN_LETTER + (NAME_COLUMN_INDEX + (i - 1))).Value = metersList[i].ConnectedPower;
                    totalPower += metersList[i].ConnectedPower;
                    ws.get_Range(POWER_COLUMN_LETTER + YEAR_BEGIN_INDEX).Value = Math.Round(totalPower, EnergyMeter.LIMIT_PRECISION);

                    // Заполняем лимиты по месяцам
                    for (int j = 0; j < metersList[i].MonthsLimits.Length; j++)
                    {
                        ws.get_Range(MONTHS_LETTERS[j] + (NAME_COLUMN_INDEX + (i - 1))).Value = metersList[i].MonthsLimits[j];
                        monthTotals[j] += metersList[i].MonthsLimits[j];
                        ws.get_Range(MONTHS_LETTERS[j] + YEAR_BEGIN_INDEX).Value = Math.Round(monthTotals[j], EnergyMeter.LIMIT_PRECISION);
                    }

                    // Вычисляем и заполняем значения за кварталы
                    for(int j = 0; j < QUARTER_LETTERS.Length; j++)
                    {
                        double quartalValue = Math.Round(  (metersList[i].MonthsLimits[0 + (3 * j)] 
                                             + metersList[i].MonthsLimits[1 + (3 * j)]
                                             + metersList[i].MonthsLimits[2 + (3 * j)])
                                             , EnergyMeter.LIMIT_PRECISION);
                        ws.get_Range(QUARTER_LETTERS[j] + (NAME_COLUMN_INDEX + (i - 1))).Value = quartalValue;
                        quartalsLimits[j] += quartalValue;

                        // Если на шаблон на формулах, то комментим
                        ws.get_Range(QUARTER_LETTERS[j] + YEAR_BEGIN_INDEX).Value = Math.Round(quartalsLimits[j], EnergyMeter.LIMIT_PRECISION);
                    }

                    // ВСЁ ЧТО НИЖЕ МОЖНО ПРОСТО ПРОПИСАТЬ ФОРМУЛАМИ В ШАБЛОНЕ!!
                    double yearMeterTotal = Math.Round(metersList[i].MonthsLimits.Sum(), EnergyMeter.LIMIT_PRECISION);
                    ws.get_Range(YEAR_LETTER + (NAME_COLUMN_INDEX + (i - 1))).Value = yearMeterTotal;
                    yearTotal += yearMeterTotal;
                    ws.get_Range(YEAR_LETTER + YEAR_BEGIN_INDEX).Value = Math.Round(yearTotal, EnergyMeter.LIMIT_PRECISION);
                }

                hpForm.ChangeProgress("Зберігаємо зміни до файлу...", hpForm.ProgressRest);

                string fileNameToSave = System.IO.Path.Combine(getApplicationDirectory(), READY_FILE_NAME);
                string fName = System.IO.Path.GetFileNameWithoutExtension(fileNameToSave);
                int k = 0;
                while (System.IO.File.Exists(fileNameToSave))
                {
                    string fExtension = new System.IO.FileInfo(fileNameToSave).Extension;
                    string newFName = fName + "-" + (k++) + fExtension;
                    fileNameToSave = System.IO.Path.Combine(getApplicationDirectory(), newFName);
                }
                wb.SaveAs(fileNameToSave);

                NotificationHelper.ShowInfo("Все готово!");
            }
            catch
            {
                NotificationHelper.ShowError("Помилка при заповненні файлу! Скоріше за все файл зайнятий іншою программою");
                return;
            }
            finally
            {
                hpForm.Close();
                Enabled = true;

                // Закрываем всё!
                wb.Close(0);
                xlApp.Quit();
                xlApp = null;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.RowIndex == 0)
            {
                e.Cancel = true;
            }
        }

        private void ShowCoeffs(EnergyMeter selectedMeter)
        {
            for(int i = 1; i <= 12; i++)
            {
                if (selectedMeter.Number == null)
                {
                    Controls.Find("monthLimit" + i, true).First().Text = EnergyMeter.DEFAULT_COEFFICIENTS[i - 1].ToString("F3");
                }
                else
                {
                    Controls.Find("monthLimit" + i, true).First().Text = selectedMeter.MonthsCoefficients[i - 1].ToString("F3");
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!programIsReady)
            {
                return;
            }

            if (dataGridView1.SelectedCells.Count == 0 || dataGridView1.SelectedCells[0].RowIndex == 0)
            {
                selectedMeterName.Text = string.Empty;
                for (int i = 1; i <= 12; i++)
                {
                    Controls.Find("monthLabel" + i, true).First().Enabled = true;
                    Controls.Find("monthLimit" + i, true).First().Enabled = true;
                }
                ShowCoeffs(metersList[0]);
            }
            else
            {
                selectedMeterName.Text = metersList[dataGridView1.SelectedCells[0].RowIndex].Name;
                if(metersList[dataGridView1.SelectedCells[0].RowIndex].IsHeating)
                {
                    int[] allMonhes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    int[] nonWinterMonthes = allMonhes.Except(EnergyMeter.WINTER_MONTHES).ToArray();
                    for(int i = 0; i < nonWinterMonthes.Count(); i++)
                    {
                        Controls.Find("monthLabel" + nonWinterMonthes[i], true).First().Enabled = false;
                        Controls.Find("monthLimit" + nonWinterMonthes[i], true).First().Enabled = false;
                    }
                }
                else
                {
                    for(int i = 1; i <= 12; i++)
                    {
                        Controls.Find("monthLabel" + i, true).First().Enabled = true;
                        Controls.Find("monthLimit" + i, true).First().Enabled = true;
                    }
                }
                ShowCoeffs(metersList[dataGridView1.SelectedCells[0].RowIndex]);
            }

            CalculateCoeffsSum();
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            if (metersList[0].GeneralCoefficient.NE(100.0))
            {
                NotificationHelper.ShowError("Загальна сумма коефіцієнтів по закладам повинна бути рівна 100");
                return;
            }

            if(CalculateCoeffsSum().NE(100.0))
            {
                NotificationHelper.ShowError("Сумма коефіцієнтів повинная бути рівна 100");
                return;
            }

            if(dataGridView1.SelectedCells.Count == 0 || 
                ((dataGridView1.SelectedCells != null) && (dataGridView1.SelectedCells[0].RowIndex == 0)))
            {
                if(NotificationHelper.AskYesNo("Ви впевнені, що хочете встановити ці коефіцієнти для всіх ЛПЗ?") 
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    double[] coeffs = new double[12];
                    for (int i = 1; i <= 12; i++)
                    {
                        coeffs[i - 1] = Convert.ToDouble(Controls.Find("monthLimit" + i, true).First().Text);
                    }

                    WaitForm wf = new WaitForm(this);
                    wf.Show();
                    wf.Refresh();
                    Enabled = false;

                    try
                    {
                        metersList.SetDefaultCoeffs(coeffs);
                    }
                    catch(Exception ex)
                    {
                        NotificationHelper.ShowError(ex.Message);
                    }
                    finally
                    {
                        wf.Close();
                        Enabled = true;
                    }
                }
            }
            else
            {
                if (NotificationHelper.AskYesNo(
                                       string.Format("Встановити данні коефіцієнти для {0}", 
                                       metersList[dataGridView1.SelectedCells[0].RowIndex].Name)
                                       ) == System.Windows.Forms.DialogResult.Yes)
                {
                    double[] coeffs = new double[12];
                    for (int i = 1; i <= 12; i++)
                    {
                        coeffs[i - 1] = Convert.ToDouble(Controls.Find("monthLimit" + i, true).First().Text);
                    }

                    metersList[dataGridView1.SelectedCells[0].RowIndex].MonthsCoefficients = coeffs;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception.GetType() == typeof(System.FormatException))
            {
                NotificationHelper.ShowError("Для вводу дозволені лише числа!");
            }
            else
            {
                NotificationHelper.ShowError(e.Exception.Message);
            }

            dataGridView1.CancelEdit();
        }

        private void monthLimit_Leave(object sender, EventArgs e)
        {
            TextBox currentTB = (TextBox)sender;

            try
            {
                double num = Convert.ToDouble(currentTB.Text);
                currentTB.Text = string.Format("{0:F3}", num);
                if(num.LT(0.0))
                {
                    throw new ArgumentException("Коеффіцієнт не може бути менше нуля.");
                }
            }
            catch (FormatException)
            {
                if(string.IsNullOrWhiteSpace(currentTB.Text))
                {
                    currentTB.Text = string.Format("{0:F3}", 0.0);
                }
                else
                {
                    NotificationHelper.ShowError("Вводити можна лише числа!");
                    currentTB.Undo();
                }
            }
            catch(ArgumentException ex)
            {
                NotificationHelper.ShowError(ex.Message);
                currentTB.Undo();
            }

            CalculateCoeffsSum();
        }

        private double CalculateCoeffsSum()
        {
            // Суммируем значения коэффициентов
            double num = Math.Round(0.0, EnergyMeter.COEFF_PRECISION);
            for(int i = 1; i <= 12; i++)
            {
                string textBoxValue = Controls.Find("monthLimit" + i, true).First().Text;
                if(!string.IsNullOrEmpty(textBoxValue))
                {
                    num += Convert.ToDouble(textBoxValue);
                }
            }

            // Выводим сумму только если она не равна 100
            if (num.NE(100.0))
            {
                coeffsSumLabel.Text = string.Format("Сумма введиних коефіцієнтів: {0:F3}", num);
            }
            else
            {
                coeffsSumLabel.Text = string.Empty;
            }

            return num;
        }

        private double CalculateVolume()
        {
            double maxVolume = Math.Round((metersList[0].ConnectedPower * 8 * 251) / 1000, EnergyMeter.LIMIT_PRECISION);

            if (enterVolume.Checked)
            {
                double vol = Math.Round(0.0, EnergyMeter.LIMIT_PRECISION);
                try
                {
                    vol = Convert.ToDouble(tenderVolumeTextBox.Text);
                    vol = Math.Round(vol, EnergyMeter.LIMIT_PRECISION);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Неправильний формат об'єму");
                }

                if(vol.LT(0.0))
                {
                    tenderVolumeTextBox.Text = (0.0).ToString("F" + EnergyMeter.LIMIT_PRECISION);
                    throw new InvalidOperationException("Об'єм не може бути менше нуля");
                }

                if(vol.GT(maxVolume))
                {
                    string errorMsg = string.Format("Об'єм закупівлі перевищує максимально допустимий. Максимально допустимий об'є складає {0:F" + EnergyMeter.LIMIT_PRECISION + "} тис. кВт", maxVolume);
                    tenderVolumeTextBox.Text = (0.0).ToString("F" + EnergyMeter.LIMIT_PRECISION);
                    throw new InvalidOperationException(errorMsg);
                }

                return vol;
            }
            else
            {
                double price = 0.0;
                double sum = 0.0;
                try
                {
                    price = (!string.IsNullOrEmpty(priceTextBox.Text))
                    ? Math.Round(Convert.ToDouble(priceTextBox.Text), PRICE_PRECISION)
                    : Math.Round(0.0, PRICE_PRECISION);

                    if(price.LT(0.0))
                    {
                        tenderVolumeTextBox.Text = priceTextBox.Text = (0.0).ToString("F" + EnergyMeter.LIMIT_PRECISION);

                        throw new InvalidOperationException("Ціна не може бути менше нуля");
                    }

                    sum = (!string.IsNullOrEmpty(totalMoneySumTextBox.Text))
                        ? Math.Round(Convert.ToDouble(totalMoneySumTextBox.Text), TENDER_SUM_PRECISION)
                        : Math.Round(0.0, TENDER_SUM_PRECISION);

                    if (sum.LT(0.0))
                    {
                        tenderVolumeTextBox.Text = totalMoneySumTextBox.Text = priceTextBox.Text = (0.0).ToString("F" + EnergyMeter.LIMIT_PRECISION);

                        throw new InvalidOperationException("Сумма не може бути менше нуля");
                    }
                }
                catch (FormatException)
                {
                    throw new FormatException("В якості сумми та ціни можуть використовуватися лише числа.");
                }

                // Вычисляем объём закупки
                double vol = Math.Round(0.0, EnergyMeter.LIMIT_PRECISION);

                if (price.NE(0.0))
                {
                    vol = Math.Round((sum / price) / 1000, EnergyMeter.LIMIT_PRECISION);
                }

                if (vol.GT(maxVolume))
                {
                    string errorMsg = string.Format("Об'єм закупівлі перевищує максимально допустимий. Максимально допустимий об'є складає {0:F" + EnergyMeter.LIMIT_PRECISION + "} тис. кВт", maxVolume);
                    tenderVolumeTextBox.Text = (0.0).ToString("F" + EnergyMeter.LIMIT_PRECISION);
                    throw new InvalidOperationException(errorMsg);
                }

                tenderVolumeTextBox.Text = vol.ToString("F" + EnergyMeter.LIMIT_PRECISION);

                return vol;
            }
        }

        private void priceTextBox_Leave(object sender, EventArgs e)
        {
            double enteredPrice = Math.Round(0.0, PRICE_PRECISION);

            // Если пользователь ничего не ввёл, не выбрасывает никаких ошибок - будем считать что цена ноль
            if(string.IsNullOrEmpty(priceTextBox.Text))
            {
                CalculateVolume();
                return;
            }

            try
            {
                enteredPrice = Convert.ToDouble(priceTextBox.Text);
                priceTextBox.Text = enteredPrice.ToString("F" + PRICE_PRECISION);
            }
            catch
            {
                NotificationHelper.ShowError("Ціна повинна бути числовим значенням!");
                priceTextBox.Undo();
            }

            priceTextBox.Text = enteredPrice.ToString("F" + PRICE_PRECISION);

            try
            {
                CalculateVolume();
            }
            catch(Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
                return;
            }
        }

        private void totalMoneySumTextBox_Leave(object sender, EventArgs e)
        {
            double enteredTotalMoneySum = Math.Round(0.0, TENDER_SUM_PRECISION);

            // Если пользователь ничего не ввёл, не выбрасывает никаких ошибок - будем считать что сумма ноль
            if (string.IsNullOrEmpty(totalMoneySumTextBox.Text))
            {
                CalculateVolume();
                return;
            }

            try
            {
                enteredTotalMoneySum = Convert.ToDouble(totalMoneySumTextBox.Text);
            }
            catch
            {
                NotificationHelper.ShowError("Ціна повинна бути числовим значенням!");
                totalMoneySumTextBox.Undo();
            }

            totalMoneySumTextBox.Text = enteredTotalMoneySum.ToString("F" + TENDER_SUM_PRECISION);
            try
            {
                CalculateVolume();
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(NotificationHelper.AskYesNo("Завершити роботу?") == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // Загрузка списка конфигураций
        private void LoadCfgList()
        {
            System.IO.DirectoryInfo cfgDirectory = new System.IO.DirectoryInfo(
                System.IO.Path.Combine(getApplicationDirectory(), CFG_DIRECTORY_NAME));

            string[] cfgFiles = cfgDirectory.EnumerateFiles("*.xml")
                                            .OrderByDescending(t => t.LastAccessTime)
                                            .Select(t => t.Name).ToArray();
            
            for(int i = 0; i < cfgFiles.Length; i++)
            {
                System.IO.FileInfo cfgFile = new System.IO.FileInfo(cfgFiles[i]);
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = cfgFile.Name;
                item.Name = "programConfiguration" + i;
                item.Click += new EventHandler(onConfigurationSelect);
                openFileMenuItem.DropDownItems.Add(item);
            }
        }

        private void onConfigurationSelect(object sender, EventArgs e)
        {
            if(openFileMenuItem.DropDownItems.Count == 0)
            {
                return;
            }

            // Убираем флажки с других конфигураций
            foreach(var cfg in openFileMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)cfg).Checked = false;
            }

            string cfgFilePath = System.IO.Path.Combine(getApplicationDirectory(), CFG_DIRECTORY_NAME, ((ToolStripMenuItem)sender).Text);

            try
            {
                metersList.ReadFromXml(cfgFilePath);
                NotificationHelper.ShowInfo("Конфігурація успішно завантажена!");

                // Отмечаем в меню выбранную конфигурацию
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                item.Checked = true;
            }
            catch(Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
            }
        }

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "XML файл (*.xml)|*.xml";
            sd.InitialDirectory = System.IO.Path.Combine(getApplicationDirectory(), CFG_DIRECTORY_NAME);
            sd.ShowDialog();

            try
            {
                if (!string.IsNullOrWhiteSpace(sd.FileName))
                {
                    metersList.WriteToXml(sd.FileName);
                    NotificationHelper.ShowInfo("Зміни успішно збережені!"); 
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
            }
        }

        void sleeper()
        {
            Debug.WriteLine(string.Format("sleeper thread is '{0}'", Thread.CurrentThread.ManagedThreadId));
            Thread.Sleep(10000);
        }

        private void WaitForProcess(object state)
        {
            CancellationToken token = (CancellationToken)state;
            if (token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();

            WaitForm wf = new WaitForm(this);
        }

        private class Waiter
        {
            CancellationTokenSource tSource = new CancellationTokenSource();
            CancellationToken token;
            WaitForm wf;
            Task asyncWaitTask;

            public Waiter(Form owner)
            {
                wf = new WaitForm(owner);
                token = tSource.Token;
            }

            private void WaitingTask(object state)
            {
                CancellationToken token = (CancellationToken)state;
                if(token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                wf.Show();
                while (!token.IsCancellationRequested) wf.Refresh();
                wf.Close();

                token.ThrowIfCancellationRequested();
            }

            public void BeginWait()
            {
                asyncWaitTask = new Task(WaitingTask, token);
                asyncWaitTask.Start();
            }

            public void EndWait()
            {
                tSource.Cancel();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Waiter w = new Waiter(this);
            w.BeginWait();

            Enabled = false;

            try
            {
                metersList.CalculateGeneralCoeffs();
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowError(ex.Message);
            }
            finally
            {
                Enabled = true;
                w.EndWait();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramForm ap = new AboutProgramForm(this);
            ap.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(enterVolume.Checked)
            {
                tenderVolumeTextBox.Enabled = true;
                priceTextBox.Enabled = totalMoneySumTextBox.Enabled = false;
            }
            else
            {
                tenderVolumeTextBox.Enabled = false;
                priceTextBox.Enabled = totalMoneySumTextBox.Enabled = true;
            }
        }

        private void importDataMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog importDataDialog = new OpenFileDialog();
            importDataDialog.Filter = "Файл Excel (*.xlsx)|*.xlsx";
            importDataDialog.ShowDialog();

            string fileName = importDataDialog.FileName;

            if (string.IsNullOrWhiteSpace(fileName))
                return;

            this.Enabled = false;
            metersList = GetDataFromExelFile(fileName, false);

            metersList.IsInitialized = true;

            dataGridView1.DataSource = metersList;
            ShowCoeffs(metersList.First());
            NotificationHelper.ShowInfo("Данні успішно завантажені");
            this.Enabled = true;
        }
    }
}
