using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Specifications.PostPaginationSettings
{
    public class CounterForActivePostSpecification : BaseSpecification<Post>
    {
        public CounterForActivePostSpecification(PostPaginationSettingsParams settingsParams)
            : base(
                    x => string.IsNullOrEmpty(settingsParams.Search) ||
                    x.Title!.Contains(settingsParams.Search)
                  )
        {

        }
    }
}
