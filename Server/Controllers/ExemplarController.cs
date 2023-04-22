using Serilog;
using Server.Controllers.Interfaces;
using Server.DTOs.Exemplar;
using Server.Services;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Controllers
{
    internal class ExemplarController : IController
    {
        Serilog.ILogger logger;

        public ExemplarController()
        {
            logger = Log.Logger.ForContext<ExemplarController>();
        }


        public async Task<ActionData> Get(long id)
        {
            return await Get(id, new ExemplarService());
        }

        private async Task<ActionData> Get(long id, IExemplarService exemplarService)
        {
            try
            {
                var responseObj = await exemplarService.GetByIdAsync(id);
                if (responseObj == null)
                {
                    logger.Error($"Exemplar with id: {id}, not found");
                    return ActionHelper.NotFound();
                }
                logger.Information($"Returned exemplar with id: {id}");
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
            return await Get(new ExemplarService());
        }

        private async Task<ActionData> Get(IExemplarService exemplarService)
        {
            try
            {
                var responseObj = await exemplarService.GetAllAsync();
                logger.Information($"Returned all exemplars from database");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetAllAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Post(CreateExemplarRequest request)
        {
            return await Post(request, new ExemplarService());
        }

        private async Task<ActionData> Post(CreateExemplarRequest request, IExemplarService exemplarService)
        {
            try
            {
                var responseObj = await exemplarService.CreateAsync(request);
                logger.Information($"Created Exemplar object in DB");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside CreateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Put(long id, UpdateExemplarRequest request)
        {
            return await Put(id, request, new ExemplarService());
        }

        private async Task<ActionData> Put(long id, UpdateExemplarRequest request, IExemplarService exemplarService)
        {
            try
            {
                var responseObj = await exemplarService.UpdateAsync(id, request);
                if (responseObj == null)
                {
                    return ActionHelper.NotFound();
                }
                logger.Information($"Updated Exemplar object in DB with id: {id}");
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
            return await Delete(id, new ExemplarService());
        }

        public async Task<ActionData> Delete(long id, IExemplarService exemplarService)
        {
            try
            {
                var result = await exemplarService.DeleteAsync(id);
                if (result.IsDeleted)
                {
                    logger.Information($"Deleted exemplar with id: {id}");
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
