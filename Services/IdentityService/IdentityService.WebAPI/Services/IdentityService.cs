using Identity.gRPC.Contracts;
using Identity.gRPC.Contracts.Enums;
using Identity.gRPC.Contracts.Replies;
using Identity.gRPC.Contracts.Requests;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces;
using ProtoBuf.Grpc;

namespace IdentityService.WebAPI.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserService _userService;

    public IdentityService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<GetUserFirstNameByIdReply> GetUserFirstNameAsync(GetUserFirstNameByIdRequest request, CallContext context)
    {
        var reply = new GetUserFirstNameByIdReply();
        
        try
        {
            var user = await _userService.GetUserByIdAsync(request.UserId, context.CancellationToken);

            reply.FirstName = user.FirstName;
        }
        catch (NotExistsException e)
        {
            reply.Error = Error.UserNotFound;
            reply.ErrorMessage = e.Message;
        }

        return reply;
    }
}
