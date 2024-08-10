using Microsoft.OData.ModelBuilder;
using Microsoft.OData.Edm;

namespace Advisor.Config
{
    public static class EdmModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Domain.Models.Advisor>(nameof(Advisor)).EntityType.HasKey(x => x.AdvisorId);

            return builder.GetEdmModel();
        }
    }
}
