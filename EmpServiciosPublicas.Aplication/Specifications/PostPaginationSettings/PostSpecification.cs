﻿using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Specifications.PostPaginationSettings
{
    public class PostSpecification : BaseSpecification<Post>
    {
        public PostSpecification(PostPaginationSettingsParams settingsParams) :
            base(
                    x => string.IsNullOrEmpty(settingsParams.Search) ||
                    x.Title!.Contains(settingsParams.Search)
                )
        {
            ApplyPaging(settingsParams.PageSize * (settingsParams.PageIndex - 1), settingsParams.PageSize);

            if (!string.IsNullOrEmpty(settingsParams.Sort))
            {
                switch (settingsParams.Sort)
                {
                    case "titleAsc":
                        AddOrderBy(x => x.Title!);
                        break;

                    case "titleDesc":
                        AddOrderByDesc(x => x.Title!);
                        break;

                    case "categoryIdAsc":
                        AddOrderBy(x => x.CategoryId);
                        break;

                    case "categoryIdDesc":
                        AddOrderByDesc(x => x.CategoryId!);
                        break;

                    default:
                        AddOrderBy(x => x.Title!);
                        break;
                }
            }
        }
    }
}
