using Serilog;
using Server.DTOs.Book;
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
    internal class BookController: IController
    {
        Serilog.ILogger logger;

        public BookController()
        {
            logger = Log.Logger.ForContext<BookController>();
        }


        public async Task<ActionData> Get(long id)
        {
            return await Get(id, new BookService());
        }

        private async Task<ActionData> Get(long id, IBookService bookService)
        {
            try
            {
                var responseObj = await bookService.GetByIdAsync(id);
                if (responseObj == null)
                {
                    logger.Error($"Book with id: {id}, not found");
                    return ActionHelper.NotFound();
                }
                logger.Information($"Returned book with id: {id}");
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
            return await Get(new BookService());
        }

        private async Task<ActionData> Get(IBookService bookService)
        {
            try
            {
                var responseObj = await bookService.GetAllAsync();
                logger.Information($"Returned all books from database");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside GetAllAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Post(CreateBookRequest request)
        {
            return await Post(request, new BookService());
        }

        private async Task<ActionData> Post(CreateBookRequest request, IBookService bookService)
        {
            try
            {
                var responseObj = await bookService.CreateAsync(request);
                logger.Information($"Created Book object in DB");
                return ActionHelper.Ok(responseObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Something went wrong inside CreateAsync() action");
                return ActionHelper.InternalServerError(ex.Message);
            }
        }

        public async Task<ActionData> Put(long id, UpdateBookRequest request)
        {
            return await Put(id, request, new BookService());
        }

        private async Task<ActionData> Put(long id, UpdateBookRequest request, IBookService bookService)
        {
            try
            {
                var responseObj = await bookService.UpdateAsync(id, request);
                if (responseObj == null)
                {
                    return ActionHelper.NotFound();
                }
                logger.Information($"Updated Book object in DB with id: {id}");
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
            return await Delete(id, new BookService());
        }

        public async Task<ActionData> Delete(long id, IBookService bookService)
        {
            try
            {
                var result = await bookService.DeleteAsync(id);
                if (result.IsDeleted)
                {
                    logger.Information($"Deleted book with id: {id}");
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
