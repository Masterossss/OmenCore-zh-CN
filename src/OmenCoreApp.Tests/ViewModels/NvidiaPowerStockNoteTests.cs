using System.Globalization;
using FluentAssertions;
using OmenCore.ViewModels;
using Xunit;

namespace OmenCoreApp.Tests.ViewModels
{
    public class NvidiaPowerStockNoteTests
    {
        [Theory]
        [InlineData("en", "Stock max read: 115 W", "expected: 140 W", "not an error by itself")]
        [InlineData("zh-CN", "115 W", "140 W", "不代表错误")]
        public void Mismatch_SaysBothNumbers_AndThatItIsNotAnErrorByItself(string culture, string first, string second, string explanation)
        {
            var original = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                var note = NvidiaPowerViewModel.StockMismatchNote(115, 140);
                note.Should().Contain(first).And.Contain(second).And.Contain(explanation);
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Theory]
        [InlineData(140.0, 140)]
        [InlineData(140.4, 140)]
        public void Agreement_SaysNothing(double live, int expected)
        {
            NvidiaPowerViewModel.StockMismatchNote(live, expected).Should().BeEmpty();
        }

        [Fact]
        public void UnknownValues_SayNothing()
        {
            NvidiaPowerViewModel.StockMismatchNote(null, 140).Should().BeEmpty();
            NvidiaPowerViewModel.StockMismatchNote(115, null).Should().BeEmpty();
        }
    }
}
