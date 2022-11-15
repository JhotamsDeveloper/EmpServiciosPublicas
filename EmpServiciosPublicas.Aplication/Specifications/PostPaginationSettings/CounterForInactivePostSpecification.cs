using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Specifications.PostPaginationSettings
{
    public class CounterForInactivePostSpecification : BaseSpecification<Post>
    {
        public CounterForInactivePostSpecification(PostPaginationSettingsParams settingsParams) : 
            base(
                    x => !x.Availability &&
                            (string.IsNullOrEmpty(settingsParams.Search) ||
                            x.Title!.Contains(settingsParams.Search))
                )
        {
            AddIgnoreQueryFilters();
        }
    }
}
