using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeimheanCafePOS.Desktop.Services
{
    public class ImageCacheService
    {
        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        private readonly string _cacheDir;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(4);

        public ImageCacheService()
        {
            _cacheDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "KeimheanCafePOS", "ImageCache");
            Directory.CreateDirectory(_cacheDir);

            // Set a lightweight user-agent to avoid some CDNs rejecting requests
            if (!Http.DefaultRequestHeaders.UserAgent.Any())
            {
                Http.DefaultRequestHeaders.UserAgent.ParseAdd("KeimheanCafePOS/1.0");
            }
        }

        public async Task<string?> GetCachedPathAsync(string url, CancellationToken ct = default)
        {
            try
            {
                var ext = GetExtFromUrl(url) ?? ".img";
                var fileName = ComputeHash(url) + ext;
                var path = Path.Combine(_cacheDir, fileName);

                if (File.Exists(path))
                    return path;

                await _semaphore.WaitAsync(ct).ConfigureAwait(false);
                try
                {
                    using var resp = await Http.GetAsync(url, ct).ConfigureAwait(false);
                    if (!resp.IsSuccessStatusCode)
                        return null;

                    await using var fs = File.Create(path);
                    await resp.Content.CopyToAsync(fs, ct).ConfigureAwait(false);
                    return path;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch
            {
                return null;
            }
        }

        private static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
        }

        private static string? GetExtFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var last = uri.Segments.LastOrDefault();
                if (!string.IsNullOrEmpty(last))
                {
                    var ext = Path.GetExtension(last);
                    if (!string.IsNullOrEmpty(ext)) return ext;
                }
            }
            catch
            {
                // ignore
            }
            return null;
        }
    }
}
