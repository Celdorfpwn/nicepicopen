using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountManager.Abstract;
using AccountManager.Concrete;
using Community.Abstract;
using Community.Concrete;
using Contests.Abstract;
using Contests.Concrete;
using Explorer.Abstract;
using Explorer.Concrete;
using Friends.Abstract;
using Friends.Concrete;
using MessMeLib.Abstract;
using MessMeLib.Concrete;
using Ninject;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Repositories;
using PicturesProvider.Abstract;
using PicturesProvider.Concrete;
using PortofolioManager.Abstract;
using PortofolioManager.Concrete;
using Workbench.Abstract;
using Workbench.Concrete;

namespace p.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver {
           private IKernel kernel;

           public NinjectDependencyResolver(IKernel kernelParam) {
               kernel = kernelParam;
               AddBindings();
           }
           public object GetService(Type serviceType) {
               return kernel.TryGet(serviceType);
           }
           public IEnumerable<object> GetServices(Type serviceType) {
               return kernel.GetAll(serviceType);
           }
          
           private void AddBindings() {
               kernel.Bind<AbstractConversationsRepository>().To<ConversationsRepository>();
               kernel.Bind<AbstractFriendsRepository>().To<FriendsRepository>();
               kernel.Bind<AbstractCommunityRepository>().To<CommunityRepository>();
               kernel.Bind<AbstractExplorerRepository>().To<ExplorerRepository>();
               kernel.Bind<AbstractImagesRepository>().To<ImagesRepository>();
               kernel.Bind<AbstractPortofolioRepository>().To<PortofolioRepository>();

               WorkbenchBinds();
               ExplorerBinds();
               AccountBinds();
               ContestsBinds();
               CommunityBinds();

               kernel.Bind<IMessMeContext>().To<MessMeContext>();
               kernel.Bind<IFriendsZoneContext>().To<FriendsZoneContext>();
               kernel.Bind<ICommunityContext>().To<CommunityContext>();
               kernel.Bind<IExplorerContext>().To<ExplorerContext>();
               kernel.Bind<IImageProviderContext>().To<ImageProviderContext>();
               kernel.Bind<IResizerContext>().To<ResizerContext>();
               kernel.Bind<IPortofolioContext>().To<PortofolioContext>();
           }

           private void CommunityBinds()
           {
               kernel.Bind<AbstractDiscussionsRepository>().To<DiscussionRepository>();
               kernel.Bind<AbstractDiscussionsContext>().To<DiscussionsContext>();
               kernel.Bind<AbstractDiscussionPostsRepository>().To<DiscussionPostsRepository>();
               kernel.Bind<AbstractDiscussionPostsContext>().To<DiscussionPostsContext>();
           }

           private void ContestsBinds()
           {
               kernel.Bind<AbstractDailyContestsRepository>().To<DailyContestsRepository>();
               kernel.Bind<AbstractDailyContestsContext>().To<DailyContestsContext>();
           }

           private void WorkbenchBinds()
           {
               kernel.Bind<AbstractWorkbenchProfileRepository>().To<WorkbenchProfileRepository>();
               kernel.Bind<AbstractWorkbenchAlbumsRepository>().To<WorkbenchAlbumsRepository>();
               kernel.Bind<AbstractWorkbenchPicturesRepository>().To<WorkbenchPicturesRepository>();
               kernel.Bind<AbstractWorkbenchWatermarksRepository>().To<WorkbenchWatermarksRepository>();
               kernel.Bind<AbstractWorkbenchQuotesRepository>().To<WorkbenchQuotesRepository>();
               kernel.Bind<AbstractWorkbenchCamerasRepository>().To<WorkbenchCamerasRepository>();

               kernel.Bind<AbstractWorkbenchProfileContext>().To<WorkbenchProfileContext>();
               kernel.Bind<AbstractWorkbenchAlbumsContext>().To<WorkbenchAlbumsContext>();
               kernel.Bind<AbstractWorkbenchPicturesContext>().To<WorkbenchPicturesContext>();
               kernel.Bind<AbstractWorkbenchWatermarksContext>().To<WorkbenchWatermarksContext>();
               kernel.Bind<AbstractQuotesContext>().To<WorkbenchQuotesContext>();
               kernel.Bind<AbstractWorkbenchCameraContext>().To<WorkbenchCameraContext>();
           }

           private void ExplorerBinds()
           {
               kernel.Bind<AbstractExplorerPortofolioRepository>().To<ExplorerPortofolioRepository>();
               kernel.Bind<AbstractExplorerPortofolioContext>().To<ExplorerPortofolioContext>();
               kernel.Bind<AbstractExplorerUpdatesRepository>().To<ExplorerUpdatesRepository>();
               kernel.Bind<AbstractExplorerUpdatesContext>().To<ExplorerUpdatesContext>();
           }

           private void AccountBinds()
           {
               kernel.Bind<AbstractAccountRepository>().To<AccountRepository>();
               kernel.Bind<AbstractAccountContext>().To<AccountContext>();
           }
    }
}