using Serilog;
using Server.DTOs.Genre;
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
    internal class GenreController : IController
    {
        Serilog.ILogger logger;

        public GenreController()
        {
            logger = Log.Logger.ForContext<GenreController>();
        }


        public async Task<ActionData> Get(long id)
        {
            return await Get(id, new GenreService());
        }

        private async Task<ActionData> Get(long id, IGenreService genreService)
        {
            try
            {
                var responseObj = await genreService.GetByIdAsync(id);
                if (responseObj == null)
                {
                    logger.Error($"Genre with id: {id}, not found");
                    return ActionHelper.NotFound();
                }
                logger.Information($"Returned genre with id: {id}");
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
            return await Get(new GenreService());
        }

        private async Task<ActionData> Get(IGenreService genreService)
        {
            try
            {
                var responseObj = await genreService.GetAllAsync();
                logger.Information($"Returned all genres from database");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetAllAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Post(CreateGenreRequest request)
        {
            return await Post(request, new GenreService());
        }

        private async Task<ActionData> Post(CreateGenreRequest request, IGenreService genreService)
        {
            try
            {
                var responseObj = await genreService.CreateAsync(request);
                logger.Information($"Created Genre object in DB");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside CreateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Put(long id, UpdateGenreRequest request)
        {
            return await Put(id, request, new GenreService());
        }

        private async Task<ActionData> Put(long id, UpdateGenreRequest request, IGenreService genreService)
        {
            try
            {
                var responseObj = await genreService.UpdateAsync(id, request);
                if (responseObj == null)
                {
                    return ActionHelper.NotFound();
                }
                logger.Information($"Updated Genre object in DB with id: {id}");
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
            return await Delete(id, new GenreService());
        }

        public async Task<ActionData> Delete(long id, IGenreService genreService)
        {
            try
            {
                var result = await genreService.DeleteAsync(id);
                if (result.IsDeleted)
                {
                    logger.Information($"Deleted genre with id: {id}");
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
