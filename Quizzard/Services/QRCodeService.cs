using System;
using System.Threading.Tasks;
using QRCoder;
namespace Quizzard.Services
{
    // Services/IQrCodeService.cs
    public interface IQrCodeService
    {
        /// <summary>
        /// Generates a PNG Data‑URL for the given absolute URL.
        /// </summary>
        /// <param name="text">The text or URI to encode.</param>
        /// <param name="pixelsPerModule">Size of each QR “pixel”.</param>
        Task<string> GenerateQrCodeDataUrlAsync(string text, int pixelsPerModule = 20);
    }


    public class QrCodeService : IQrCodeService
    {
        public Task<string> GenerateQrCodeDataUrlAsync(string text, int pixelsPerModule = 20)
        {
            // 1) Create the QR code data
            using var qrGenerator = new QRCodeGenerator();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            // 2) Render to PNG byte[]
            using var pngQr = new PngByteQRCode(qrCodeData);
            byte[] qrBytes = pngQr.GetGraphic(pixelsPerModule);

            // 3) Convert to Base64 data‑URL
            string base64 = Convert.ToBase64String(qrBytes);
            string dataUrl = $"data:image/png;base64,{base64}";
            return Task.FromResult(dataUrl);
        }
    }

}
