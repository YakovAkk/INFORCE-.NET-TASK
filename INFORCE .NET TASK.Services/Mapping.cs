using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model.ViewModel;
using Omu.ValueInjecter;

namespace INFORCE_.NET_TASK.Services
{
    public static class Mapping
    {
        private static readonly MapperInstance mapper;

        static Mapping()
        {
            mapper = new MapperInstance();

            mapper.AddMap<AlgoritmDescriptionEntity, DescriptionViewModel>((from) =>
            {
                var viewModel = new DescriptionViewModel().InjectFrom(from) as DescriptionViewModel;
                return viewModel;
            });

            mapper.AddMap<UserEntity, UserViewModel>((from) =>
            {
                var viewModel = new UserViewModel().InjectFrom(from) as UserViewModel;
                return viewModel;
            });

            mapper.AddMap<UrlEntity, UrlViewModel>((from) =>
            {
                var viewModel = new UrlViewModel().InjectFrom(from) as UrlViewModel;
                viewModel.CreatedBy = from.CreatedBy.MapTo<UserViewModel>();
                return viewModel;
            });
        }

        public static TDest MapTo<TDest>(this object source)
        {
            return mapper.Map<TDest>(source);
        }
    }
}
