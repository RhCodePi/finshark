using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(){
            var commentModels = await _commentRepo.GetAllAsync();

            var commentDtos = commentModels.Select(s => s.ToCommentDto());

            if(commentDtos.Count() == 0){
                return NotFound();
            }

            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            
            var commentModel = await _commentRepo.GetByIdAsync(id);

            if(commentModel == null){
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!await _stockRepo.IsStockExsist(stockId))
            {
                return BadRequest("Stock does not exists");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel}, commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){

            var commentModel = await _commentRepo.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);

            if(commentModel == null){
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }
        

    }
}