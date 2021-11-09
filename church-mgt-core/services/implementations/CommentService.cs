using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos.CommentDto;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<string>> AddCommentAsync(string userId, string comment)
        {
            AppUser user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return Response<string>.Fail("User not found");


            var userComment = new Comment { Id = Guid.NewGuid().ToString(), Comments = comment, CreatedAt = DateTime.UtcNow, AppUserId = userId };
            await _unitOfWork.Comment.AddAsync(userComment);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Comment added!", comment, StatusCodes.Status201Created);
        }

        public Response<IEnumerable<CommentResponseDto>> GetAllComments()
        {
            var users = _unitOfWork.Comment.GetAll();

            var response = _mapper.Map<IEnumerable<CommentResponseDto>>(users);
            return Response<IEnumerable<CommentResponseDto>>.Success("Success", response);
        }

        public async Task<Response<CommentResponseDto>> GetCommentById(string userId)
        {
            var user = await _unitOfWork.Comment.GetAsync(userId);
            if (user == null)
                return Response<CommentResponseDto>.Fail("Comment not found");

            var response = _mapper.Map<CommentResponseDto>(user);
            return Response<CommentResponseDto>.Success("Success", response);
        }

        public async Task<Response<string>> DeleteCommentById(string userId)
        {
            var user = await _unitOfWork.Comment.GetAsync(userId);
            if (user == null)
                return Response<string>.Fail("Comment not found");

            _unitOfWork.Comment.Remove(user);
            await _unitOfWork.CompleteAsync();
            return Response<string>.Success("Success", $"comment with id {userId} deleted", StatusCodes.Status200OK);
        }
    }
}
