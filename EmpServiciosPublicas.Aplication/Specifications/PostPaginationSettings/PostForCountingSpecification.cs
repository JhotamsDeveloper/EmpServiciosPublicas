using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Specifications.PostPaginationSettings
{
    public class PostForCountingSpecification : BaseSpecification<Post>
    {
        public PostForCountingSpecification(PostPaginationSettingsParams settingsParams)
            : base(
                    x => string.IsNullOrEmpty(settingsParams.Search) ||
                    x.Title!.Contains(settingsParams.Search)
                  )
        {

        }
    }
}
