using AutoMapper;

namespace Mbis.Cognito.Model.Actions
{
    public class AppSettingsAction<T, T2> : IMappingAction<T, T2>
    {
        public void Process(T source, T2 destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
