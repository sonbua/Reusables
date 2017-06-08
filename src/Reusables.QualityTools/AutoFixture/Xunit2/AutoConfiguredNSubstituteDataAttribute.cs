using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace Reusables.QualityTools.AutoFixture.Xunit2
{
    public class AutoConfiguredNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoConfiguredNSubstituteDataAttribute()
            : base(new Fixture().Customize(new AutoConfiguredNSubstituteCustomization()))
        {
        }
    }
}