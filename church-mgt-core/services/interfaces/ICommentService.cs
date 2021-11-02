using church_mgt_dtos.CommentDto;
using church_mgt_dtos.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface ICommentService
    {
        Task<Response<string>> AddCommentAsync(string userId, string comment);
        Task<Response<string>> DeleteCommentById(string userId);
        Response<IEnumerable<CommentResponseDto>> GetAllComments();
        Task<Response<CommentResponseDto>> GetCommentById(string userId);
    }
}