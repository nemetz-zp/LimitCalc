using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace LimitCalc
{
    /// <summary>
    /// Список узлов учёта
    /// </summary>
    class MetersList : BindingList<EnergyMeter>
    {
        // Заполнен ли список данными из Excel файла
        private bool isReady = false;

        // Обработчик события изменений в элементах списка
        private void OnElementsChange(object sender, MeterStateChangedEventArgs e)
        {
            if(!IsInitialized)
            {
                return;
            }

            if((e != null) && (e.ChangedProperty == ChangesType.POWER_CHANGED))
            {
                AddUpdateTotals();
                ResetBindings();
            }
            else if ((e != null) && (e.ChangedProperty == ChangesType.GENERAL_COFFICIENT_CHANGED))
            {
                AddUpdateTotals();
                ResetBindings();
            }
            else if((e != null) && (e.ChangedProperty == ChangesType.WINTER_MONTHES_IS_NULL))
            {
                CalculateGeneralCoeffs(true);
                AddUpdateTotals();
                ResetBindings();
            }
            else
            {
                AddUpdateTotals();
            }
        }

        // Добавляем итоги как первую запись списка
        private void AddUpdateTotals()
        {
            if (Count == 0)
            {
                return;
            }

            if (this.First().Number == null)
            {
                this.First().GeneralCoefficient = this.Where(t => (t.Number != null)).Sum(t => t.GeneralCoefficient);
                this.First().ConnectedPower = this.Where(t => (t.Number != null)).Sum(t => t.ConnectedPower);
            }
            else
            {
                this.Insert(0,
                    new EnergyMeter
                    {
                        Name = "--- УСІ ЗАКЛАДИ ---",
                        ConnectedPower = this.Where(t => (t.Number != null)).Sum(t => t.ConnectedPower),
                        GeneralCoefficient = this.Where(t => (t.Number != null)).Sum(t => t.GeneralCoefficient)
                    }
                );
            }
        }

        public void CalculateGeneralCoeffs(bool dontTouchNullCoeffs = false, bool calcThroughLimits = false)
        {
            double totalPower = this.Where(t => (t.Number != null)).Sum(t => t.ConnectedPower);
            double totalLimit = this.Where(t => (t.Number != null)).Sum(t => t.TotalLimit);

            // Список узлов учёта для корректировки
            IEnumerable<EnergyMeter> metersToCalculate = this.Where(t => (t.Number != null));

            if(dontTouchNullCoeffs)
            {
                IEnumerable<EnergyMeter> nullMeters = metersToCalculate.Where(t => t.GeneralCoefficient.EQ(0.0));
                metersToCalculate = metersToCalculate.Except(nullMeters, new EnergyMeterComparer());
            }

            // Узлы учёта с фиксированными коэффициентами
            IEnumerable<EnergyMeter> metersWithFixedGeneralCoeffs = metersToCalculate.Where(t => t.IsFixedGeneralCoefficient);
            if (metersWithFixedGeneralCoeffs.Count() > 0)
            {
                if(metersWithFixedGeneralCoeffs.Sum(t => t.GeneralCoefficient).GT(100.0))
                {
                    throw new ArgumentException("Сумма фіксованих коеффіцієнтів перевищує 100");
                }

                double coeffsSumWithoutFixed = Math.Round(100 - metersWithFixedGeneralCoeffs.Sum(t => t.GeneralCoefficient), EnergyMeter.COEFF_PRECISION);
                IEnumerable<EnergyMeter> metersWithNonFixedCoeffs = this.Where(t => (t.Number != null)).Except(metersWithFixedGeneralCoeffs);
                double powerOfFixedMeters = Math.Round(metersWithFixedGeneralCoeffs.Sum(t => t.ConnectedPower), EnergyMeter.COEFF_PRECISION);

                foreach (var item in metersWithNonFixedCoeffs)
                {
                    if (calcThroughLimits)
                    {
                        item.GeneralCoefficient = Math.Round(
                            (item.TotalLimit / totalLimit)
                            * 100, EnergyMeter.COEFF_PRECISION);
                    }
                    else
                    {
                        double meterCoeff = Math.Round(
                            (item.ConnectedPower / (totalPower - powerOfFixedMeters))
                            * 100, EnergyMeter.COEFF_PRECISION);
                        item.GeneralCoefficient = Math.Round((meterCoeff * coeffsSumWithoutFixed)
                            / 100, EnergyMeter.COEFF_PRECISION);
                    }
                }
                double rest = Math.Round(coeffsSumWithoutFixed - metersWithNonFixedCoeffs.Sum(t => t.GeneralCoefficient),
                    EnergyMeter.COEFF_PRECISION);

                if (rest.LT(0.0))
                {
                    double maxCoeff = metersWithNonFixedCoeffs.Max(t => t.GeneralCoefficient);
                    metersWithNonFixedCoeffs.Where(t => t.GeneralCoefficient == maxCoeff).First().GeneralCoefficient += rest;
                }
                else if (rest.GT(0.0))
                {
                    double minCoeff = metersWithNonFixedCoeffs.Min(t => t.GeneralCoefficient);
                    metersWithNonFixedCoeffs.Where(t => t.GeneralCoefficient == minCoeff).First().GeneralCoefficient += rest;
                }
            }

            else
            {
                foreach (var m in metersToCalculate)
                {
                    if (calcThroughLimits)
                    {
                        m.GeneralCoefficient = Math.Round(
                            (m.TotalLimit / totalLimit)
                            * 100, EnergyMeter.COEFF_PRECISION);
                    }
                    else
                    {
                        m.GeneralCoefficient = Math.Round((m.ConnectedPower / totalPower) * 100, EnergyMeter.COEFF_PRECISION);
                    }
                }

                // Корректируем коэфициенты
                double totalCoeffsSum = metersToCalculate.Sum(t => t.GeneralCoefficient);
                double rest = Math.Round((100 - totalCoeffsSum), EnergyMeter.COEFF_PRECISION);
                if (rest.NE(0.0))
                {
                    if (rest.LT(0.0))
                    {
                        double minCoeff = metersToCalculate.Min(t => t.GeneralCoefficient);
                        metersToCalculate.Where(t => (t.GeneralCoefficient == minCoeff)).First().GeneralCoefficient += rest;
                    }
                    else
                    {
                        double maxCoeff = metersToCalculate.Max(t => t.GeneralCoefficient);
                        metersToCalculate.Where(t => (t.GeneralCoefficient == maxCoeff)).First().GeneralCoefficient += rest;
                    }
                }
            }
        }

        /// <summary>
        /// Готовы ли все узлы учёта
        /// </summary>
        public bool IsEverythingReady
        {
            get
            {
                return this.Where(t => (t.Number != null)).Where(t => !t.IsReadyToUse()).Count() == 0;
            }
        }

        /// <summary>
        /// Приводим в порядок все неготовые узлы, устанавливая для них коэффициенты по-умолчанию
        /// </summary>
        public void PrepareNotReadyMeters()
        {
            foreach(var item in this.Where(t => (t.Number != null) && (!t.IsReadyToUse())))
            {
                item.MonthsCoefficients = EnergyMeter.DEFAULT_COEFFICIENTS;
            }
        }

        public new void Add(EnergyMeter item)
        {
            base.Add(item);
            if (item.TotalLimit.GT(0.0))
            {
                CalculateGeneralCoeffs(false, true);
            }
            else
            {
                CalculateGeneralCoeffs();
            }
            AddUpdateTotals();
            item.StateChanged += OnElementsChange;
        }

        public new bool Remove(EnergyMeter item)
        {
            bool result = base.Remove(item);

            if(Count > 0) AddUpdateTotals();
            
            return result;
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (Count > 0) AddUpdateTotals();
        }

        public new void Insert(int index, EnergyMeter item)
        {
            base.Insert(index, item);
            if (Count > 0) AddUpdateTotals();
        }

        public void SetSum(double sum)
        {
            if(this[0].GeneralCoefficient.NE(100.0))
            {
                throw new InvalidOperationException("Сумма коеффіцієнтів по закладах повинна дорівнювати 100");
            }

            for(int i = 1; i < Count; i++)
            {
                this[i].TotalLimit = Math.Round(((sum * this[i].GeneralCoefficient) / 100), EnergyMeter.LIMIT_PRECISION);
            }

            IEnumerable<EnergyMeter> metersToSetLimits = this.Where(t => (t.Number != null));
            IEnumerable<EnergyMeter> nullCoeffsMonthes = metersToSetLimits.Where(t => t.GeneralCoefficient.EQ(0.0));
            metersToSetLimits = metersToSetLimits.Except(nullCoeffsMonthes, new EnergyMeterComparer());

            double newSum = Math.Round(metersToSetLimits.Sum(t => t.TotalLimit), EnergyMeter.LIMIT_PRECISION);
            double rest = Math.Round((sum - newSum), EnergyMeter.LIMIT_PRECISION);
            if(rest.NE(0.0))
            {
                if(rest.LT(0.0))
                {
                    this.Where(t => (t.TotalLimit == metersToSetLimits.Max(m => m.TotalLimit))).First().TotalLimit += rest;
                }
                else
                {
                    this.Where(t => (t.TotalLimit == metersToSetLimits.Min(m => m.TotalLimit))).First().TotalLimit += rest;
                }
            }
        }

        public void SetDefaultCoeffs(double[] coeffs = null)
        {
            if(coeffs != null)
            {
                EnergyMeter.DEFAULT_COEFFICIENTS = coeffs;
            }

            for (int i = 1; i < Count; i++)
                this[i].MonthsCoefficients = EnergyMeter.DEFAULT_COEFFICIENTS;
        }

        public bool IsInitialized
        { 
            get
            {
                return isReady;
            }
            set
            {
                isReady = value;
            }
        }

        public void WriteToXml(string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xDecl = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement rootElement = xDoc.CreateElement("configuration");
            xDoc.AppendChild(rootElement);
            xDoc.InsertBefore(xDecl, rootElement);

            // Основные элементы документа
            XmlElement defaultCoeffsSection = xDoc.CreateElement("defaultCoefficients");
            XmlElement metersList = xDoc.CreateElement("meters");
            XmlElement meterElement = xDoc.CreateElement("meter");
            XmlElement coeffsList = xDoc.CreateElement("coefficients");
            XmlElement monthElement = xDoc.CreateElement("month");

            // Атрибуты элементов
            XmlAttribute meterName = xDoc.CreateAttribute("name");
            XmlAttribute monthNumber = xDoc.CreateAttribute("number");
            XmlAttribute coeffAttrib = xDoc.CreateAttribute("coeff");
            XmlAttribute meterCoeffAttrib = xDoc.CreateAttribute("coeff");
            XmlAttribute meterPower = xDoc.CreateAttribute("power");
            XmlAttribute meterIsFixedAttrib = xDoc.CreateAttribute("isFixed");

            for (int i = 0; i < EnergyMeter.DEFAULT_COEFFICIENTS.Length; i++)
            {
                monthNumber.Value = i.ToString();
                coeffAttrib.Value = EnergyMeter.DEFAULT_COEFFICIENTS[i].ToString("F" + EnergyMeter.COEFF_PRECISION);
                
                monthElement.Attributes.Append(monthNumber);
                monthElement.Attributes.Append(coeffAttrib);

                coeffsList.AppendChild(monthElement.Clone());
                monthElement.RemoveAll();
            }

            defaultCoeffsSection.AppendChild(coeffsList.Clone());
            rootElement.AppendChild(defaultCoeffsSection);
            coeffsList.RemoveAll();

            for (int i = 1; i < Count; i++)
            {
                meterName.Value = this[i].Name;
                meterPower.Value = this[i].ConnectedPower.ToString("F" + EnergyMeter.LIMIT_PRECISION);
                meterCoeffAttrib.Value = this[i].GeneralCoefficient.ToString("F" + EnergyMeter.COEFF_PRECISION);
                meterIsFixedAttrib.Value = this[i].IsFixedGeneralCoefficient.ToString();

                // Записываем коэффициенты
                for (int j = 0; j < this[i].MonthsCoefficients.Length; j++)
                {
                    monthNumber.Value = j.ToString();
                    coeffAttrib.Value = this[i].MonthsCoefficients[j].ToString("F" + EnergyMeter.COEFF_PRECISION);
                    monthElement.Attributes.RemoveAll();
                    monthElement.Attributes.Append(monthNumber);
                    monthElement.Attributes.Append(coeffAttrib);
                    coeffsList.AppendChild(monthElement.Clone());
                }

                meterElement.Attributes.Append(meterName);
                meterElement.Attributes.Append(meterPower);
                meterElement.Attributes.Append(meterCoeffAttrib);
                meterElement.Attributes.Append(meterIsFixedAttrib);

                meterElement.AppendChild(coeffsList.Clone());
                metersList.AppendChild(meterElement.Clone());

                meterElement.RemoveAll();
                coeffsList.RemoveAll();
            }

            rootElement.AppendChild(metersList);

            xDoc.Save(filename);
        }

        public void ReadFromXml(string filename) 
        {
            string wrongXmlFileError = "Файл конфігурації пошкоджено або не данний файл не є файлом конфігурації";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            XmlElement rootElement = xDoc.DocumentElement;
            if(rootElement.Name != "configuration")
            {
                throw new XmlException(wrongXmlFileError);
            }
            else if(!rootElement.HasChildNodes)
            {
                throw new XmlException(wrongXmlFileError);
            }

            XmlNode defaultCoeffs = rootElement.FirstChild;
            XmlNode defaultCoeffsMonthesRoot = defaultCoeffs.FirstChild;

            if (defaultCoeffsMonthesRoot == null)
            {
                throw new XmlException(wrongXmlFileError);
            }
            else if (defaultCoeffsMonthesRoot.Name != "coefficients")
            {
                throw new XmlException(wrongXmlFileError);
            }

            if (defaultCoeffsMonthesRoot.ChildNodes.Count != 12)
            {
                throw new XmlException(wrongXmlFileError);
            }

            // Читаем коэффициенты по-умолчанию
            double[] defMonthCoeffs = new double[12];
            foreach (XmlNode month in defaultCoeffsMonthesRoot.ChildNodes)
            {
                // Аттрибуты месяца
                XmlNode monthNumAttrib = month.Attributes.GetNamedItem("number");
                XmlNode monthCoeff = month.Attributes.GetNamedItem("coeff");

                if(monthNumAttrib == null || monthCoeff == null)
                {
                    throw new XmlException(wrongXmlFileError);
                }
                else if (string.IsNullOrWhiteSpace(monthNumAttrib.Value) || string.IsNullOrWhiteSpace(monthCoeff.Value))
                {
                    throw new XmlException(wrongXmlFileError);
                }

                int index = Convert.ToInt32(monthNumAttrib.Value);
                double coeff = Convert.ToDouble(monthCoeff.Value);
                defMonthCoeffs[index] = coeff;
            }

            List<EnergyMeter> meters = new List<EnergyMeter>();

            if(rootElement.SelectNodes("meters").Count != 1)
            {
                throw new XmlException(wrongXmlFileError);
            }

            XmlNodeList metersList = rootElement.SelectSingleNode("meters").ChildNodes;
            if(metersList.Count == 0)
            {
                throw new XmlException(wrongXmlFileError);
            }

            int metersNumber = 1;
            foreach(XmlNode meter in metersList)
            {
                EnergyMeter newMeter = new EnergyMeter();

                // Аттрибуты узла учёта
                XmlNode nameAttrib = meter.Attributes.GetNamedItem("name");
                XmlNode powerAttrib = meter.Attributes.GetNamedItem("power");
                XmlNode generalCoeffAttrib = meter.Attributes.GetNamedItem("coeff");
                XmlNode meterIsFixedAttrib = meter.Attributes.GetNamedItem("isFixed");

                if (nameAttrib == null || powerAttrib == null || generalCoeffAttrib == null || meterIsFixedAttrib == null)
                {
                    throw new XmlException(wrongXmlFileError);
                }
                else if (string.IsNullOrWhiteSpace(nameAttrib.Value) || string.IsNullOrWhiteSpace(powerAttrib.Value)
                                                                     || string.IsNullOrWhiteSpace(generalCoeffAttrib.Value)
                                                                     || string.IsNullOrWhiteSpace(meterIsFixedAttrib.Value))
                {
                    throw new XmlException(wrongXmlFileError);
                }

                newMeter.Name = nameAttrib.Value;
                newMeter.ConnectedPower = Convert.ToDouble(powerAttrib.Value);
                newMeter.GeneralCoefficient = Convert.ToDouble(generalCoeffAttrib.Value);
                newMeter.IsFixedGeneralCoefficient = Convert.ToBoolean(meterIsFixedAttrib.Value);
                newMeter.Number = metersNumber++;

                XmlNode monthesRootElement = meter.SelectSingleNode("coefficients");
                if(monthesRootElement == null)
                {
                    throw new XmlException(wrongXmlFileError);
                }

                XmlNodeList monthesList = monthesRootElement.SelectNodes("month");

                if(monthesList.Count != 12)
                {
                    throw new XmlException(wrongXmlFileError);
                }

                double[] monthesCoeffs = new double[12];
                foreach(XmlNode month in monthesList)
                {
                    XmlNode monthNumAttrib = month.Attributes.GetNamedItem("number");
                    XmlNode monthCoeff = month.Attributes.GetNamedItem("coeff");

                    if (monthNumAttrib == null || monthCoeff == null)
                    {
                        throw new XmlException(wrongXmlFileError);
                    }
                    else if (string.IsNullOrWhiteSpace(monthNumAttrib.Value) || string.IsNullOrWhiteSpace(monthCoeff.Value))
                    {
                        throw new XmlException(wrongXmlFileError);
                    }

                    int index = Convert.ToInt32(monthNumAttrib.Value);
                    double coeff = Convert.ToDouble(monthCoeff.Value);
                    monthesCoeffs[index] = coeff;
                }
                newMeter.MonthsCoefficients = monthesCoeffs;

                meters.Add(newMeter);
            }

            // Сравниваем список узлов из шаблона и полученный список из файла конфигурации
            if((Count - 1) != meters.Count)
            {
                throw new InvalidOperationException("Кількість записів файлу конфігурації відрізняється від кількості записів у файлі-шаблоні");
            }

            if(this.Where(t => (t.Number != null)).Except(meters, new EnergyMeterComparer()).Count() != 0)
            {
                throw new InvalidOperationException("Записи файлу конфігурації відрізняється від записів у файлі-шаблоні");
            }

            EnergyMeter.IsReportAboutChanges = false;

            // Обнуляем зафиксированные коєффициенты для их изменения
            foreach (var item in this.Where(t => (t.Number != null)))
            {
                item.IsFixedGeneralCoefficient = false;
            }

            try
            {
                meters = meters.OrderBy(t => t.Name).ToList();
                IEnumerable<EnergyMeter> orderedList = this.Where(t => (t.Number != null)).OrderBy(t => t.Name);
                int meterIndex = 0;
                foreach (var item in orderedList)
                {
                    item.ConnectedPower = meters[meterIndex].ConnectedPower;
                    item.GeneralCoefficient = meters[meterIndex].GeneralCoefficient;
                    item.IsFixedGeneralCoefficient = meters[meterIndex].IsFixedGeneralCoefficient;
                    item.MonthsCoefficients = meters[meterIndex].MonthsCoefficients;
                    meterIndex++;
                }
                EnergyMeter.DEFAULT_COEFFICIENTS = defMonthCoeffs;
                AddUpdateTotals();
                ResetBindings();
            }
            catch
            {
                throw;
            }
            finally
            {
                EnergyMeter.IsReportAboutChanges = true;
            }
        }

        private class EnergyMeterComparer : IEqualityComparer<EnergyMeter>
        {

            public bool Equals(EnergyMeter x, EnergyMeter y)
            {
                if(x == null || y == null)
                {
                    return false;
                }

                return x.Name == y.Name;
            }

            public int GetHashCode(EnergyMeter obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        public void GetCoeffsFromAnotherList(MetersList anotherList)
        {
            if(!IsValidListToReplace())
            {
                return;
            }

            // Назначаем коэфициенты по ФАПам
            for(int i = 1; i < Count; i++)
            {
                this[i].GeneralCoefficient = Math.Round(anotherList[i].GeneralCoefficient / anotherList[0].GeneralCoefficient, EnergyMeter.COEFF_PRECISION);
            }

            // Корректируем коэффициенты по ФАПам
            double rest = Math.Round(this.Sum(t => t.GeneralCoefficient), EnergyMeter.COEFF_PRECISION);
            if (rest.NE(100.0))
            {
                if(rest.LT(0.0))
                {
                    this.Where(t => (t.GeneralCoefficient == this.Max(k => k.GeneralCoefficient))).First().GeneralCoefficient += rest;
                }
                else
                {
                    this.Where(t => (t.GeneralCoefficient == this.Min(k => k.GeneralCoefficient))).First().GeneralCoefficient += rest;
                }
            }
            

            // Устанавливаем и корректируем коэффициенты месяцев
            for(int i = 1; i < anotherList.Count; i++)
            {
                for(int j = 0; j < anotherList[i].MonthsLimits.Count(); j++)
                {
                    this[i].MonthsCoefficients[j] = Math.Round(anotherList[i].MonthsLimits[j] / anotherList[i].TotalLimit, EnergyMeter.COEFF_PRECISION);
                }

                rest = Math.Round(this[i].MonthsCoefficients.Sum(), EnergyMeter.COEFF_PRECISION);
                if(rest.NE(100.0))
                {
                    if(rest.LT(0.0))
                    {
                        int maxElemIndex = 0;
                        double maxElem = this[i].MonthsCoefficients.Max();
                        for(int k = 0; k < this[i].MonthsCoefficients.Count(); k++)
                        {
                            if(this[i].MonthsCoefficients[k].EQ(maxElem))
                            {
                                maxElemIndex = k;
                                break;
                            }
                        }
                        this[i].MonthsCoefficients[maxElemIndex] += rest;
                    }
                    else
                    {
                        int minElemIndex = 0;
                        double minElem = this[i].MonthsCoefficients.Max();
                        for (int k = 0; k < this[i].MonthsCoefficients.Count(); k++)
                        {
                            if (this[i].MonthsCoefficients[k].EQ(minElem))
                            {
                                minElemIndex = k;
                                break;
                            }
                        }
                        this[i].MonthsCoefficients[minElemIndex] += rest;
                    }
                }
            }
        }

        private bool IsValidListToReplace()
        {
            return true;
        }
    }
}
