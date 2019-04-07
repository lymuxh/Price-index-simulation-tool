using System;

namespace ZI
{
    public class Record
    {
        private double payment;
        private long lenght;
        private string purchaseDate;
        private int number;

        public Record(double payment, long lenght, string purchaseDate, int number)
        {
            this.payment = payment;
            this.lenght = lenght;
            this.purchaseDate = purchaseDate;
            this.number = number;
        }

        public double Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public long Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        public string PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }
    public class BaseFormatter : IFormatProvider, ICustomFormatter
    {

        public object GetFormat(Type format)
        {
            if (format == typeof(ICustomFormatter)) return this;
            else return null;
        }

        public string Format(string format, object arg, IFormatProvider provider)
        {
            if (format == null)
            {
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, provider);
                else
                    return arg.ToString();
            }


            if (!format.StartsWith("B"))
            {
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, provider);
                else
                    return arg.ToString();
            }

            format = format.Trim(new char[] { 'B' });
            int b = Convert.ToInt32(format);
            return Convert.ToString((int)arg, b);
        }
    }
}
