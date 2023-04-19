using Server.Controllers.Interfaces;
using Server.DTOs;
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
            logger = SingletonPool.Logger.ForContext<AuthorController>();
        }


        public async Task<ActionResponse> Get(long id)
        {
            return await Get(id, new AuthorService());
        }

        private async Task<ActionResponse> Get(long id, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.GetAuthorById(id);
                if (responseObj == null)
                {
                    logger.Information($"Not Found GetAuthorById with id={id}");
                    return ResponseHelper.NotFound();
                }
                logger.Information($"Successful GetAuthorById with id={id}");
                return ResponseHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error($"Execption during GetAuthorById with id={id}. Exception: {ex.Message}");
                return ResponseHelper.InternalServerError();
            }
        }

        public async Task<ActionResponse> Get()
        {
            return await Get(new AuthorService());
        }

        private async Task<ActionResponse> Get(IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.GetAllAuthors();
                logger.Information($"Successful GetAllAuthors");
                return ResponseHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error($"Execption during GetAllAuthors. Exception: {ex.Message}");
                return ResponseHelper.InternalServerError();
            }
        }

        public async Task<ActionResponse> Post(AuthorCreateRequest request)
        {
            return await Post(request, new AuthorService());
        }

        private async Task<ActionResponse> Post(AuthorCreateRequest request, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.CreateAuthor(request);
                logger.Information($"Successful CreateAuthor");
                return ResponseHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Execption during CreateAuthor");
                return ResponseHelper.InternalServerError();
            }
        }

        public async Task<ActionResponse> Put(long id, AuthorUpdateRequest request)
        {
            return await Put(id, request, new AuthorService());
        }

        private async Task<ActionResponse> Put(long id, AuthorUpdateRequest request, IAuthorService authorService)
        {
            try
            {
                var responseObj = await authorService.UpdateAuthor(id, request);
                logger.Information($"Successful UpdateAuthor");
                return ResponseHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Execption during UpdateAuthor");
                return ResponseHelper.InternalServerError();
            }
        }

        public async Task<ActionResponse> Delete(long id)
        {
            return await Delete(id, new AuthorService());
        }

        public async Task<ActionResponse> Delete(long id, IAuthorService authorService)
        {
            try
            {
                var result = await authorService.DeleteAuthor(id);
                logger.Information($"Successful DeleteAuthor with id={id}");
                return ResponseHelper.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Execption during DeleteAuthor with id={id}.");
                return ResponseHelper.InternalServerError();
            }
        }


    }
}
