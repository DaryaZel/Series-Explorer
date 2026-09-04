using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace Series.Api.Infrastructure
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            if (resolutionRoot == null)
            {
                throw new ArgumentNullException("resolutionRoot");
            }

            _resolutionRoot = resolutionRoot;
        }

        public object GetService(Type serviceType)
        {
            var request = CreateRequest(serviceType, isUnique: true);

            return _resolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = CreateRequest(serviceType, isUnique: false);

            return _resolutionRoot.Resolve(request).ToList();
        }

        private IRequest CreateRequest(Type serviceType, bool isUnique)
        {
            return _resolutionRoot.CreateRequest(
                serviceType,
                MatchesAnyBinding,
                Enumerable.Empty<IParameter>(),
                isOptional: true,
                isUnique: isUnique);
        }

        private static bool MatchesAnyBinding(IBindingMetadata bindingMetadata)
        {
            return true;
        }

        public void Dispose()
        {
            var disposable = _resolutionRoot as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
