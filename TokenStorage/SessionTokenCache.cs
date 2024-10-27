using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace boardroom_management.TokenStorage
{
    public class SessionTokenCache
    {
        private static ReaderWriterLockSlim sessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        string userId = string.Empty;
        string cacheId = string.Empty;
        HttpContext httpContext = null;

        ITokenCache tokenCache;

        public SessionTokenCache(string userId, HttpContext httpContext, IClientApplicationBase clientApp)
        {
            this.userId = userId;
            cacheId = userId + "_TokenCache";
            this.httpContext = httpContext;
            tokenCache = clientApp.UserTokenCache;
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
            Load();
        }

        public ITokenCache GetMsalCacheInstance()
        {
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
            Load();
            return tokenCache;
        }

        public bool HasData()
        {
            return (httpContext.Session.GetString(cacheId) != null && httpContext.Session.GetString(cacheId).Length > 0);
        }

        public void Clear()
        {
            httpContext.Session.Remove(cacheId);
        }

        private void Load(TokenCacheNotificationArgs args = null)
        {
            sessionLock.EnterReadLock();
            byte[] tokenCacheBytes = httpContext.Session.Get(cacheId);
            if (tokenCacheBytes != null)
            {
                tokenCache.DeserializeMsalV3(tokenCacheBytes);
            }
            sessionLock.ExitReadLock();
        }

        private void Persist(TokenCacheNotificationArgs args)
        {
            sessionLock.EnterWriteLock();
            // Optimistically set HasStateChanged to false. 
            // We need to do it early to avoid losing changes made by a concurrent thread.
            // Optimistically set HasStateChanged to false. 
            // We need to do it early to avoid losing changes made by a concurrent thread.
            // tokenCache.HasStateChanged = false; // Removed because ITokenCache does not have this property
            httpContext.Session.Set(cacheId, args.TokenCache.SerializeMsalV3());
            sessionLock.ExitWriteLock();
        }

        // Triggered right before MSAL needs to access the cache. 
        private void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            // Reload the cache from the persistent store in case it changed since the last access. 
            Load();
        }

        // Triggered right after MSAL accessed the cache.
        private void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                Persist(args);
            }
        }
    }
}