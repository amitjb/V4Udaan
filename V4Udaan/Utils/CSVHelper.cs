using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Utils
{
    class CSVHelper
    {
    }

    public abstract class CsvBase<T>
    {
        private static readonly char[] trimEnd = new[] { ' ', ',' };
        private readonly IEnumerable<T> values;
        private readonly Func<T, object> getItem;
        protected CsvBase(IEnumerable<T> values, Func<T, object> getItem)
        {
            this.values = values;
            this.getItem = getItem;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in
                from element in this.values.Select(this.getItem)
                where element != null
                select element.ToString())
            {
                this.Build(builder, item).Append(", ");
            }
            return builder.ToString().TrimEnd(trimEnd);
        }
        protected abstract StringBuilder Build(StringBuilder builder, string item);
    }

    public class CsvBare<T> : CsvBase<T>
    {
        public CsvBare(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }
        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            return builder.Append(item);
        }
    }

    public sealed class CsvTrimBare<T> : CsvBare<T>
    {
        public CsvTrimBare(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }
        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            return base.Build(builder, item.Trim());
        }
    }

    public class CsvRfc4180<T> : CsvBase<T>
    {
        private static readonly char[] csvChars = new[] { ',', '"', ' ', '\n', '\r' };
        public CsvRfc4180(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }

        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            item = item.Replace("\"", "\"\"");
            return item.IndexOfAny(csvChars) >= 0
                ? builder.Append("\"").Append(item).Append("\"")
                : builder.Append(item);
        }
    }

    public sealed class CsvTrimRfc4180<T> : CsvRfc4180<T>
    {
        public CsvTrimRfc4180(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            return base.Build(builder, item.Trim());
        }
    }
    public class CsvAlwaysQuote<T> : CsvBare<T>
    {
        public CsvAlwaysQuote(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }
        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            return builder.Append("\"").Append(item.Replace("\"", "\"\"")).Append("\"");
        }
    }

    public sealed class CsvTrimAlwaysQuote<T> : CsvAlwaysQuote<T>
    {
        public CsvTrimAlwaysQuote(IEnumerable<T> values, Func<T, object> getItem) : base(values, getItem)
        {
        }
        protected override StringBuilder Build(StringBuilder builder, string item)
        {
            return base.Build(builder, item.Trim());
        }
    }

    public static class CsvExtensions
    {
        public static string ToCsv<T>(this IEnumerable<T> source, Func<T, object> getItem, Type csvProcessorType)
        {
            if ((source == null)
                || (getItem == null)
                || (csvProcessorType == null)
                || !csvProcessorType.IsSubclassOf(typeof(CsvBase<T>)))
            {
                return string.Empty;
            }

            return csvProcessorType
                .GetConstructor(new[] { source.GetType(), getItem.GetType() })
                .Invoke(new object[] { source, getItem })
                .ToString();
        }

        //private static void Main()
        //{
        //    var words = new[] { ",this", "   is   ", "a", "test", "Super, \"luxurious\" truck" };

        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvAlwaysQuote<string>)));
        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvRfc4180<string>)));
        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvBare<string>)));
        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvTrimAlwaysQuote<string>)));
        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvTrimRfc4180<string>)));
        //    Console.WriteLine(words.ToCsv(x => x, typeof(CsvTrimBare<string>)));
        //    Console.ReadLine();
        //}
    }
}
