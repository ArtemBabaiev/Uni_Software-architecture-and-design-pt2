using Serilog;
using Server.Controllers.Interfaces;
using Server.DTOs.Author;
using Server.ObjectManagers;
using Server.Services;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Controllers
{
    internal class AuthorController : IController
    {
        Serilog.ILogger logger;

        public AuthorController()
        {
            logger = Log.Logger.ForContext<AuthorController>();
        }


        public async Task<ActionData> Get(long id)
        {
            return await Get(id, new AuthorService());
        }

        private async Task<ActionData> Get(long id, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.GetByIdAsync(id);
                if (responseObj == null)
                {
                    logger.Error($"Author with id: {id}, not found");
                    return ActionHelper.NotFound();
                }
                logger.Information($"Returned author with id: {id}");
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
            return await Get(new AuthorService());
        }

        private async Task<ActionData> Get(IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.GetAllAsync();
                logger.Information($"Returned all authors from database");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetAllAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Post(AuthorCreateRequest request)
        {
            return await Post(request, new AuthorService());
        }

        private async Task<ActionData> Post(AuthorCreateRequest request, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.CreateAsync(request);
                logger.Information($"Created Author object in DB");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside CreateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Put(long id, AuthorUpdateRequest request)
        {
            return await Put(id, request, new AuthorService());
        }

        private async Task<ActionData> Put(long id, AuthorUpdateRequest request, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.UpdateAsync(id, request);
                if (responseObj == null)
                {
                    return ActionHelper.NotFound();
                }
                logger.Information($"Updated Author object in DB with id: {id}");
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
            return await Delete(id, new AuthorService());
        }

        public async Task<ActionData> Delete(long id, IAuthorService authorService)
        {
            try
            {
                var result = await authorService.DeleteAsync(id);
                if (result.IsDeleted)
                {
                    logger.Information($"Deleted author with id: {id}");
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
