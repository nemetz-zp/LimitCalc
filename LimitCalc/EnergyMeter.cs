using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LimitCalc
{
    // Класс ассоциированный с узлом учёта
    public class EnergyMeter
    {
        private static int[] _winterMonthes = { 1, 2, 3, 4, 10, 11, 12 };

        public static bool IsReportAboutChanges = true;

        private double _totalLimit = Math.Round(0.0, COEFF_PRECISION);

        public event EventHandler<MeterStateChangedEventArgs> StateChanged;
        public static event EventHandler WinterMonthesListIsChanged;

        public const int COEFF_PRECISION = 3;
        public const int LIMIT_PRECISION = 3;

        private static double[] _defCoeffs = {
                                                8.33, // Январь
                                                8.33, // Февраль
                                                8.33, // Март
                                                8.33, // Апрель
                                                8.33, // Май
                                                8.33, // Июнь
                                                8.33, // Июль
                                                8.33, // Август
                                                8.33, // Сентябрь
                                                8.33, // Октябрь
                                                8.33, // Ноябрь
                                                8.37  // Декабрь
                                            };

        // Помесячные коэффициенты
        public static double[] DEFAULT_COEFFICIENTS
        {
            get
            {
                return (double[])_defCoeffs.Clone();
            }
            set
            {
                if(value.Where(t => t.LT(0.0)).Count() != 0)
                {
                    throw new ArgumentException("Коеффіцієнти не можеть бути меньші за 0");
                }
                if(value.Sum().NE(100.0))
                {
                    throw new ArgumentException("Сумма введених коефіцієнтів повинна дорівнювати 100");
                }
                else
                {
                    _defCoeffs = (double[])value.Clone();
                }
            }
        }

        private void OnChange(MeterStateChangedEventArgs e)
        {
            if (StateChanged != null && IsReportAboutChanges)
            {
                StateChanged.Invoke(this, e);
            }
        }

        /// <summary>
        /// Когда меняется список зимних месяцев
        /// </summary>
        /// <param name="e"></param>
        private static void OnWinterMonthesListChanged(EventArgs e)
        {
            if(WinterMonthesListIsChanged != null)
            {
                WinterMonthesListIsChanged.Invoke(null, e);
            }
        }

        private double[] _monthsCoefficients = new double[12];

        /// <summary>
        /// Присоеденённіая мощность
        /// </summary>
        public double ConnectedPower
        {
            get
            {
                return connectedPower;
            }
            set
            {
                if(value.LT(0.0) || value.EQ(0.0))
                {
                    throw new ArgumentException("Приєднана потужніть повинна бути більша за 0");
                }
                connectedPower = value;
                OnChange(new MeterStateChangedEventArgs { ChangedProperty = ChangesType.POWER_CHANGED });
            }
        }

        private void updateWinterCoeffs(object sender, EventArgs e)
        {
            if(!isCorrectHeatingCoeffs(_monthsCoefficients))
            {
                CalculateCoeffs(_monthsCoefficients);
            }
        }

        public EnergyMeter()
        {
            EnergyMeter.WinterMonthesListIsChanged += updateWinterCoeffs;
        }

        public EnergyMeter(double totalLimit, double[] mounthLimits)
        {
            if(mounthLimits.Length != 12)
            {
                throw new ArgumentException("Невірний переданий массив лімітів до конструктора EnergyMeter");
            }

            this._monthsLimits = mounthLimits;
            this._totalLimit = totalLimit;

            CalculateCoeffsThroughLimits();
        }

        /// <summary>
        /// Расчёт коэффициентов по установленным лимитам
        /// </summary>
        private void CalculateCoeffsThroughLimits()
        {
            for(int i = 0; i < this._monthsLimits.Length; i++)
            {
                _monthsCoefficients[i] = Math.Round(((_monthsLimits[i] / _totalLimit) * 100), COEFF_PRECISION);
            }

            // Месяца с ненулевыми коэффициентами
            double[] notNullMonthes = _monthsCoefficients.Where(t => t.NE(0.0)).ToArray();

            // Распределяем остаток мощности между ненулевыми месяцами
            double freeRest = Math.Round((100 - _monthsCoefficients.Sum()), COEFF_PRECISION);
            if (freeRest.NE(0.0))
            {
                if (freeRest.LT(0.0))
                {
                    int indexOfMaxElement = _monthsCoefficients.ToList().IndexOf(notNullMonthes.Max());
                    _monthsCoefficients[indexOfMaxElement] += freeRest;
                }
                else
                {
                    int indexOfMinElement = _monthsCoefficients.ToList().IndexOf(notNullMonthes.Min());
                    _monthsCoefficients[indexOfMinElement] += freeRest;
                }
            }
        }

        // Помесячные лимиты
        private double[] _monthsLimits = new double[12];

        // Месяца, когда работают счётчики на отопление (отопительный период)
        public static int[] WINTER_MONTHES
        {
            get
            {
                return (int[])_winterMonthes;
            }
            set
            {
                if(value.Length > 12)
                {
                    throw new ArgumentException("Кількість місяців не може бути більшою за 12");
                }
                if(value.Where(t => (t < 1) || (t > 12)).Count() != 0)
                {
                    throw new ArgumentException("Значення місяця може бути від 1 до 12.");
                }

                _winterMonthes = (int[])value.Clone();

                // Информируем всех об изменении списка зимних месяцев
                OnWinterMonthesListChanged(null);
            }
        }

        /// <summary>
        /// Проверка правильности расстановки коэффициентов на узлах учёта для отопления - незимние месяца должны быть с нулевыми коэффициентами. 
        /// </summary>
        /// <param name="coeffs">Массив номеров месяцев являющихся "зимними". 1 - Январь, 12 - Декабрь</param>
        /// <returns>true - если все незимние месяцы имеют нулевые коэффициенты</returns>
        private bool isCorrectHeatingCoeffs(double[] coeffs)
        {
            int[] allMonthes = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            int[] nonWinterMonthes = allMonthes.Except(_winterMonthes).ToArray();
            for(int i = 0; i < nonWinterMonthes.Length; i++)
            {
                if (coeffs[nonWinterMonthes[i] - 1].NE(0.0))
                {
                    return false;
                }
            }

            return true;
        }

        private double connectedPower;
        private double generalCoefficient;

        // Это счётчик на отопление?
        public bool IsHeating { get; set; }

        public int? Number { get; set; }

        /// <summary>
        /// Имя узла учёта (пример: Бабурская амбулатория)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коэффициент узла учёта от общей мощности 
        /// </summary>
        public double GeneralCoefficient 
        { 
            get
            {
                return generalCoefficient;
            }
            set
            {
                if(isFixedGeneralCoefficient)
                {
                    throw new InvalidOperationException("Неможливо змінити зафіксований коефіцієнт");
                }

                if(value.LT(0.0))
                {
                    throw new ArgumentException("Коеффіцієнт не може бути менше нуля");
                }

                generalCoefficient = Math.Round(value, COEFF_PRECISION);

                OnChange(new MeterStateChangedEventArgs { ChangedProperty = ChangesType.GENERAL_COFFICIENT_CHANGED });
            }
        }

        /// <summary>
        /// Лимиты на каждый месяц
        /// </summary>
        public double[] MonthsLimits
        {
            get
            {
                return (double[])_monthsLimits.Clone();
            }
        }

        /// <summary>
        /// Запуск расчёта лимитов на каждый месяц
        /// </summary>
        private void CalculateLimits()
        {
            // Определяем помысячные лимиты просто умножая на помесячные коэффициенты
            for(int i = 0; i < _monthsLimits.Length; i++)
            {
                _monthsLimits[i] = Math.Round(((_totalLimit * _monthsCoefficients[i]) / 100), LIMIT_PRECISION);
            }

            // Месяца с ненулевыми лимитами
            double[] notNullMonthes = _monthsLimits.Where(t => t.NE(0.0)).ToArray();

            // Распределяем остаток мощности между ненулевыми месяцами
            double freeRest = Math.Round((TotalLimit - _monthsLimits.Sum()), LIMIT_PRECISION);
            if (freeRest.NE(0.0))
            {
                if (freeRest.LT(0.0))
                {
                    int indexOfMaxElement = _monthsLimits.ToList().IndexOf(notNullMonthes.Max());
                    _monthsLimits[indexOfMaxElement] += freeRest;
                }
                else
                {
                    int indexOfMinElement = _monthsLimits.ToList().IndexOf(notNullMonthes.Min());
                    _monthsLimits[indexOfMinElement] += freeRest;
                }
            }
        }

        /// <summary>
        /// Расчитываем помесячные коэффициенты
        /// </summary>
        /// <param name="coeffs"></param>
        private void CalculateCoeffs(double[] coeffs)
        {
            _monthsCoefficients = coeffs;

            // Если узёл учёта для отопления
            if(IsHeating)
            {
                // Количество неотопительных месяцев
                int nonWinterMonthesCount = 12 - EnergyMeter.WINTER_MONTHES.Count();

                // Определяем массив незимних месяцев
                int[] nonWinterMonthes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                // Количество месяцев с нулевыми коэффициентами
                int nullCoeffsMonthesCount = EnergyMeter.DEFAULT_COEFFICIENTS.Where(t => (t.EQ(0.0))).Count();
                int[] winterMonthes = EnergyMeter.WINTER_MONTHES;

                // Выбрасываем месяца с нулевыми коэффициентами из списка зимних месяцев
                if (nullCoeffsMonthesCount > 0)
                {
                    int[] nullCoeffsMonthes = new int[nullCoeffsMonthesCount];
                    
                    for (int j = 0, i = 0; j < EnergyMeter.DEFAULT_COEFFICIENTS.Length; j++)
                    {
                        if(EnergyMeter.DEFAULT_COEFFICIENTS[j].EQ(0.0))
                        {
                            nullCoeffsMonthes[i++] = j + 1;
                        }
                    }

                    winterMonthes = EnergyMeter.WINTER_MONTHES.Except(nullCoeffsMonthes).ToArray();
                }

                nonWinterMonthes = nonWinterMonthes.Except(EnergyMeter.WINTER_MONTHES).ToArray();

                // Обнуляем значения незимних месяцев, и записываем освободившуюся сумму коэффициентов
                double freeCoeffSum = 0.000;
                for (int j = 0; j < nonWinterMonthes.Count(); j++)
                {
                    freeCoeffSum += _monthsCoefficients[nonWinterMonthes[j] - 1];
                    _monthsCoefficients[nonWinterMonthes[j] - 1] = Math.Round(0.0, COEFF_PRECISION);
                }

                // Если все зимние месяца нулевые, то и распределять нечего
                if (winterMonthes.Count() == 0)
                {
                    generalCoefficient = 0.0;
                    OnChange(new MeterStateChangedEventArgs { ChangedProperty = ChangesType.WINTER_MONTHES_IS_NULL });
                    return;
                }

                // Распределяем освободившуюся сумму между зимними месяцами
                double coeffToAdd = Math.Round((freeCoeffSum / winterMonthes.Count()), COEFF_PRECISION);
                for (int j = 0; j < winterMonthes.Count(); j++)
                {
                    _monthsCoefficients[winterMonthes[j] - 1] += coeffToAdd;
                }

                // Остаток от деления плюсуюем к наибольшему или наименьшему элементу
                double freeRest = Math.Round(100 - _monthsCoefficients.Sum(), COEFF_PRECISION);
                if (freeRest.NE(0.0))
                {
                    if (freeRest.LT(0.0))
                    {
                        double[] aa = _monthsCoefficients.Where(t => (t.NE(0.0))).ToArray();
                        int indexOfMaxElement = _monthsCoefficients.ToList().IndexOf(_monthsCoefficients.Where(t => (t.NE(0.0))).Max());
                        _monthsCoefficients[indexOfMaxElement] += freeRest;
                    }
                    else
                    {
                        double[] aa = _monthsCoefficients.Where(t => (t.NE(0.0))).ToArray();
                        int indexOfMinElement = _monthsCoefficients.ToList().IndexOf(_monthsCoefficients.Where(t => (t.NE(0.0))).Min());
                        _monthsCoefficients[indexOfMinElement] += freeRest;
                    }
                }
            }
        }

        private bool isFixedGeneralCoefficient = false;

        /// <summary>
        /// Определяет возможность изменения общего коэффициента узла
        /// </summary>
        public bool IsFixedGeneralCoefficient
        {
            get { return isFixedGeneralCoefficient; }
            set { isFixedGeneralCoefficient = value; }
        }

        public double[] MonthsCoefficients
        { 
            get
            {
                return (double[])_monthsCoefficients.Clone();
            }

            set
            {
                double[] coeffs = (double[])value.Clone();

                if (coeffs.Sum().NE(100.0) && generalCoefficient.NE(0.0))
                {
                    throw new ArgumentException("Сумма введених коефіцієнтів повинна дорівнювати 100");
                }
                else if(coeffs.Sum().EQ(0.0) && generalCoefficient.NE(0.0))
                {
                    throw new ArgumentException("Сумма введених коефіцієнтів повинна дорівнювати 100");
                }
                else if (coeffs.Length != 12)
                {
                    throw new ArgumentException("Массив із коефіцієнтами має розмірність меньше 12");
                }
                else if (coeffs.Where(t => t.LT(0.0)).Count() > 0)
                {
                    throw new ArgumentException("Коефіцієнти не можуть бути менше нуля");
                }
                else
                {
                    if (isCorrectHeatingCoeffs(coeffs))
                    {
                        _monthsCoefficients = coeffs;
                    }
                    else
                    {
                        CalculateCoeffs(coeffs);
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет вожможность расчёта лимитов (установлены ли все помесячные коэффициенты)
        /// </summary>
        /// <returns></returns>
        public bool IsReadyToUse()
        {
            double monthesCoeffsSum = _monthsCoefficients.Sum();

            //if (monthesCoeffsSum.EQ(generalCoefficient))
            //{
            //    return true;
            //}

            if (monthesCoeffsSum.NE(100.0)) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Годовой лимит узла учёта
        /// </summary>
        public double TotalLimit
        {
            get
            {
                return _totalLimit;
            }
            set
            {
                _totalLimit = value;
                if(IsReadyToUse())
                {
                    CalculateLimits();
                }
            }
        }
    }
}
