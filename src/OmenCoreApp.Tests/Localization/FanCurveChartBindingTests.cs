using System.Collections.ObjectModel;
using System.ComponentModel;
using FluentAssertions;
using OmenCore.Controls;
using OmenCore.Models;
using Xunit;

namespace OmenCoreApp.Tests.Localization
{
    public class FanCurveChartBindingTests
    {
        [Fact]
        public void PointsIsARegisteredBindableDependencyProperty()
        {
            var property = DependencyPropertyDescriptor.FromProperty(FanCurveChart.PointsProperty, typeof(FanCurveChart));
            property.Should().NotBeNull();
            property!.PropertyType.Should().Be(typeof(ObservableCollection<FanCurvePoint>));
            property.IsReadOnly.Should().BeFalse();
        }
    }
}
