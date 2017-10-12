using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LimitCalc
{
    public enum ChangesType
    {
        GENERAL_COFFICIENT_CHANGED,
        POWER_CHANGED,
        WINTER_MONTHES_IS_NULL
    }

    public class MeterStateChangedEventArgs : EventArgs
    {
        public ChangesType ChangedProperty { get; set; }
    }
}
