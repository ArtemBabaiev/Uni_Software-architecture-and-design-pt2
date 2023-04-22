using Serilog;
using Server.DTOs.Publisher;
using Server.Services.Interfaces;
using Server.Services;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers.Interfaces;

namespace Server.Controllers
{
    internal class PublisherController : IController
    {
        ILogger logger;

        public PublisherController()
        {
            logger = Log.Logger.ForContext<PublisherController>();
        }


        public async Task<ActionData> Get(long id)
        {
            return await Get(id, new PublisherService());
        }

        private async Task<ActionData> Get(long id, IPublisherService publisherService)
        {
            try
            {
                var responseObj = await publisherService.GetByIdAsync(id);
                if (responseObj == null)
                {
                    logger.Error($"Publisher with id: {id}, not found");
                    return ActionHelper.NotFound();
                }
                logger.Information($"Returned publisher with id: {id}");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetByIdAsync action with {id}");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Get()
        {
            return await Get(new PublisherService());
        }

        private async Task<ActionData> Get(IPublisherService publisherService)
        {
            try
            {
                var responseObj = await publisherService.GetAllAsync();
                logger.Information($"Returned all publishers from database");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetAllAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Post(CreatePublisherRequest request)
        {
            return await Post(request, new PublisherService());
        }

        private async Task<ActionData> Post(CreatePublisherRequest request, IPublisherService publisherService)
        {
            try
            {
                var responseObj = await publisherService.CreateAsync(request);
                logger.Information($"Created Publisher object in DB");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside CreateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Put(long id, UpdatePublisherRequest request)
        {
            return await Put(id, request, new PublisherService());
        }

        private async Task<ActionData> Put(long id, UpdatePublisherRequest request, IPublisherService publisherService)
        {
            try
            {
                var responseObj = await publisherService.UpdateAsync(id, request);
                if (responseObj == null)
                {
                    return ActionHelper.NotFound();
                }
                logger.Information($"Updated Publisher object in DB with id: {id}");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside UpdateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Delete(long id)
        {
            return await Delete(id, new PublisherService());
        }

        public async Task<ActionData> Delete(long id, IPublisherService publisherService)
        {
            try
            {
                var result = await publisherService.DeleteAsync(id);
                if (result.IsDeleted)
                {
                    logger.Information($"Deleted publisher with id: {id}");
                }
                return ActionHelper.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside DeleteAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }
    }
}
