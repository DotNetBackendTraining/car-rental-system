using AutoMapper;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;

namespace CarRentalSystem.Core.ApplicationUsers.Queries.CurrentUserQuery;

public class CurrentUserQueryHandler : IQueryHandler<CurrentUserQuery, CurrentUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserAccessorService _userAccessorService;

    public CurrentUserQueryHandler(
        IMapper mapper,
        IUserAccessorService userAccessorService)
    {
        _mapper = mapper;
        _userAccessorService = userAccessorService;
    }

    public async Task<Result<CurrentUserResponse>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userAccessorService.GetCurrentUserAsync();
        return _mapper.Map<CurrentUserResponse>(user);
    }
}