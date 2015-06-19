using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class UploadHelper
    {
        public async Task<bool> PostClueResponse(MobileServiceClient ms, string clueId, double lat, double lng, byte[] dataArray)
        {
            var parameters = new Dictionary<string, string>();

            var requestDto = new PostClueRequestDto
            {
                ClueId = clueId,
                Data = "photo.jpg",
                Latitude = lat,
                Longitude = lng
            };

            PostClueResponseDto responseDto = await ms.InvokeApiAsync<PostClueRequestDto, PostClueResponseDto>("clue", requestDto);
            if (responseDto == null)
            {
                // invalid response object - guard
                return false;
            }

            // Get the URI generated that contains the SAS 
            // and extract the storage credentials.
            var cred = new StorageCredentials(responseDto.SasQueryString);
            var imageUri = new Uri(responseDto.ImageUri);

            // Instantiate a Blob store container based on the info in the returned item.
            var container = new CloudBlobContainer(new Uri(string.Format("https://{0}/{1}", imageUri.Host, responseDto.ContainerName)), cred);

            // Upload the new image as a BLOB from the stream.
            var blobFromSASCredential = container.GetBlockBlobReference(responseDto.ResourceName);
            //await blobFromSASCredential.UploadFromStreamAsync(data);
            await blobFromSASCredential.UploadFromByteArrayAsync(dataArray, 0, dataArray.Length);

            return true;
        }


        private class PostClueRequestDto
        {
            public string ClueId { get; set; }
            public string Data { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        private class PostClueResponseDto
        {
            public string ImageUri { get; set; }
            public string ContainerName { get; set; }
            public string ResourceName { get; set; }
            public string SasQueryString { get; set; }
        }
    }
}
