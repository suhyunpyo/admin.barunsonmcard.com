using Barunn.MobileInvitation.Common.Models.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Common.Helpers
{
    public interface IStaticContentHelper
    {

        /// <summary>
        /// 파일 업로드
        /// </summary>
        /// <param name="file">Form FIle</param>
        /// <param name="relativePath">상대 경로, 파일명 포함</param>
        /// <param name="overWrite">덥어쓰기 여부</param>
        /// <returns></returns>
        Task<(bool status, string message)> UploadFileAsync(IFormFile file, string relativePath, bool overWrite = false);
        Task<(bool status, string message)> UploadFileAsync(string imageData, string relativePath, bool overWrite = false);

        /// <summary>
        /// 파일 이동
        /// </summary>
        /// <param name="relativeFromPath">>원본 상대 경로, 파일명 포함</param>
        /// <param name="relativeToPath">>대상 상대 경로, 파일명 포함</param>
        /// <param name="overWrite">덥어쓰기 여부</param>
        /// <returns></returns>
        Task<(bool status, string message)> MoveToFileAsync(string relativeFromPath, string relativeToPath, bool overWrite = false);

        /// <summary>
        /// 파일 삭제
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<(bool status, string message)> DeleteFileAsync(string relativePath);

        /// <summary>
        /// File 존재 여부
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        bool ExistsFile(string relativePath);

        /// <summary>
        /// File 의 로컬 저장 경로 Full path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        string GetFileFullName(string relativePath);

        /// <summary>
        /// 콘텐츠 Full Url Local Config
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Uri AbsoluteUri(string relativePath);

        /// <summary>
        /// 콘텐츠 Full Url Default config
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Uri DefaultAbsoluteUri(string relativePath);
    }

    /// <summary>
    /// 로컬 파일 저장소
    /// </summary>
    public class StaticFileContentHelper : IStaticContentHelper
    {
        protected readonly BarunnConfig _barunnConfig;
        public StaticFileContentHelper(IOptions<BarunnConfig> barunnConfig)
        {
            _barunnConfig = barunnConfig.Value;
        }
        
        public async Task<(bool status, string message)> UploadFileAsync(IFormFile file, string relativePath, bool overWrite = false)
        {
            var path = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativePath.TrimStart('\\', '/'));

            var toFile = new FileInfo(path);
            if (!overWrite && toFile.Exists)
                return (false, "파일이 이미 있습니다.");

            var dir = toFile.Directory;
            if (!dir.Exists)
                dir.Create();

            using (var stream = toFile.Open(FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return (true, Path.Combine(relativePath.TrimStart('\\', '/')).Replace("\\", "/"));
        }
        public async Task<(bool status, string message)> UploadFileAsync(string imageData, string relativePath, bool overWrite = false)
        {
            var path = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativePath.TrimStart('\\', '/'));

            var toFile = new FileInfo(path);
            if (!overWrite && toFile.Exists)
                return (false, "파일이 이미 있습니다.");

            var dir = toFile.Directory;
            if (!dir.Exists)
                dir.Create();

            using (var stream = toFile.Open(FileMode.Create))
            {
                byte[] data = Convert.FromBase64String(imageData); //convert from base64
                await stream.WriteAsync(data, 0, data.Length);
            }
            return (true, Path.Combine(relativePath.TrimStart('\\', '/')).Replace("\\", "/"));
        }
        public async Task<(bool status, string message)> MoveToFileAsync(string relativeFromPath, string relativeToPath, bool overWrite = false)
        {
            var formPath = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativeFromPath.TrimStart('\\', '/'));
            var toPath = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativeToPath.TrimStart('\\', '/'));

            var fromFile = new FileInfo(formPath);
            var toFile = new FileInfo(toPath);

            if (!fromFile.Exists)
                return (false, "원본 파일이 없습니다.");
            if (!overWrite && toFile.Exists)
                return (false, "대상 파일이 이미 있습니다.");

            var dir = toFile.Directory;
            if (!dir.Exists)
                dir.Create();

            using (var sourceStream = fromFile.Open(FileMode.Open))
            using (var destinationStream = toFile.Open(FileMode.Create))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
            return (true, Path.Combine(relativeToPath.TrimStart('\\', '/')).Replace("\\", "/"));
        }

        public async Task<(bool status, string message)> DeleteFileAsync(string relativePath)
        {
            //파일 삭제시 Container 이름이 있는지 여부 검사. 서비스 정적 파일 삭제 방지
            if (relativePath.Replace("\\", "/").Contains(_barunnConfig.FileConfig.UploadContainer + "/"))
            {
                var path = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativePath.TrimStart('\\', '/'));
            
                var toFile = new FileInfo(path);
                if (toFile.Exists)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        toFile.Delete();
                        var dir = toFile.Directory;
                        if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0)
                        {
                            dir.Delete();
                        }
                    }).ConfigureAwait(false);
                }
            }
            return (true, "");
        }

        public bool ExistsFile(string relativePath)
        {
           var path = Path.Combine(_barunnConfig.FileConfig.UploadPath, relativePath.TrimStart('\\', '/'));
            return File.Exists(path);
        }
        public string GetFileFullName(string relativePath)
        {
            return Path.Combine(_barunnConfig.FileConfig.UploadPath, relativePath.TrimStart('\\', '/'));
        }

        public Uri AbsoluteUri(string relativePath)
        {
            return _barunnConfig.StaticContent.CDNUri.Combine(relativePath.TrimStart('\\', '/').Replace("\\", "/"));
        }

        public Uri DefaultAbsoluteUri(string relativePath)
        {
            return _barunnConfig.DefaultStaticContent.CDNUri.Combine(relativePath.TrimStart('\\', '/').Replace("\\", "/"));
        }
    }
}
