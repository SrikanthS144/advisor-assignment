using Domain.Extend;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public partial class AdvisorContext : DbContext
    {
        public IHttpContextAccessor HttpContextAccessor;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleAuditEntries();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            HandleAuditEntries();

            return base.SaveChanges();
        }

        #region Private

        private void HandleAuditEntries()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var userName = GetClaim(_httpContextAccessor, "UserId");

            var userId = !string.IsNullOrEmpty(userName) ? int.Parse(userName) : 0;

            var now = DateTime.UtcNow;
            foreach (var added in addedAuditedEntities)
            {
                added.Created = now;
                added.Updated = now;
                added.CreatedBy = userId;
                added.UpdatedBy = userId;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.Updated = now;
                modified.UpdatedBy = userId;
            }
        }

        private static string GetClaim(IHttpContextAccessor httpUser, string claimName)
        {
            var myHttpUser = httpUser?.HttpContext?.User;
            var claim = myHttpUser?.FindFirst(x => x.Type == claimName);
            return claim?.Value ?? "";
        }
        #endregion
    }
}
