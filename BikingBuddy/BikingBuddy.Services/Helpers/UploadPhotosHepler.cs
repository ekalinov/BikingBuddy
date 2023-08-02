using Microsoft.AspNetCore.Http;

namespace BikingBuddy.Services.Helpers;

public static class UploadPhotosHepler
{
    
    public static async Task<string> UploadPhotoToLocalStorageAsync(string destinationPath, IFormFile file, string envWebRoot)
    {
        string fileName = file.FileName.Replace(' ','_');
        
        destinationPath +=  Guid.NewGuid().ToString() + '_' + fileName;
 
        string serverFolder = Path.Combine(envWebRoot, destinationPath);

        await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        return "/" + destinationPath;
    }



    
} 