using ElectroEcommerce.Contracts;

namespace ElectroEcommerce.Services.Abstracts;

public interface IFileService
{
	Task<string> UploadAsync(CustomUploadDirectories directories, IFormFile file, string folderPrefix);
	Task<List<string>> UploadAsync(CustomUploadDirectories directories, IFormFileCollection files, string folderPrefix);
	string ReadStaticFiles(string folderPrefix, CustomUploadDirectories directories, string physicalImageName);
	List<string> ReadStaticFiles(string folderPrefix, CustomUploadDirectories directories, List<string> physicalImageNames);
	void RemoveStaticFiles(string folderPrefix, CustomUploadDirectories directories, string pyshicalImageName); 
	void RemoveStaticFiles(string folderPrefix, CustomUploadDirectories directories, List<string> pyshicalImageNames);
}
