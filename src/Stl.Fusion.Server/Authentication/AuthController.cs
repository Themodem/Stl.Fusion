using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stl.Fusion.Authentication;
using Stl.Fusion.Bridge;

namespace Stl.Fusion.Server.Authentication
{
    [Route("fusion/auth")]
    [ApiController]
    public class AuthController : FusionController, IAuthService
    {
        protected IAuthService AuthService { get; }
        protected ISessionResolver SessionResolver { get; }

        public AuthController(IPublisher publisher, IAuthService authService, ISessionResolver sessionResolver)
            : base(publisher)
        {
            AuthService = authService;
            SessionResolver = sessionResolver;
        }

        [HttpGet("signOut")]
        public Task SignOutAsync(bool force, Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return AuthService.SignOutAsync(force, session, cancellationToken);
        }

        [HttpGet("saveSessionInfo")]
        public Task SaveSessionInfoAsync(SessionInfo sessionInfo, Session? session = null,
            CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return AuthService.SaveSessionInfoAsync(sessionInfo, session, cancellationToken);
        }

        [HttpGet("updatePresence")]
        public Task UpdatePresenceAsync(Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return AuthService.UpdatePresenceAsync(session, cancellationToken);
        }

        // Compute methods

        [HttpGet("isSignOutForced")]
        public Task<bool> IsSignOutForcedAsync(Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return PublishAsync(ct => AuthService.IsSignOutForcedAsync(session, ct), cancellationToken);
        }

        [HttpGet("getUser")]
        public Task<User> GetUserAsync(Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return PublishAsync(ct => AuthService.GetUserAsync(session, ct), cancellationToken);
        }

        [HttpGet("getSessionInfo")]
        public Task<SessionInfo> GetSessionInfoAsync(Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return PublishAsync(ct => AuthService.GetSessionInfoAsync(session, ct), cancellationToken);
        }

        [HttpGet("getUserSessions")]
        public Task<SessionInfo[]> GetUserSessions(Session? session = null, CancellationToken cancellationToken = default)
        {
            session ??= SessionResolver.Session;
            return PublishAsync(ct => AuthService.GetUserSessions(session, ct), cancellationToken);
        }
    }
}
