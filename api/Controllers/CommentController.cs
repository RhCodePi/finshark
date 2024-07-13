using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var commentModels = await _commentRepo.GetAllAsync();

            var commentDtos = commentModels.Select(s => s.ToCommentDto());

            if(commentDtos.Count() == 0){
                return NotFound();
            }

            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){

            var commentModel = await _commentRepo.GetByIdAsync(id);

            if(commentModel == null){
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }
    }
}