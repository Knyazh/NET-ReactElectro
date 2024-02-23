using ElectroEcommerce.Contracts;
using ElectroEcommerce.Services.Abstracts;

namespace ElectroEcommerce.Services.Concretes;

public class FileService : IFileService
{
	private readonly IWebHostEnvironment _env;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly string _directory;
	private readonly string _filePath;
	private readonly string _hostURL;

	public FileService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
	{
		_env = env;
		_httpContextAccessor = httpContextAccessor;
		_directory = Directory.GetCurrentDirectory();
		_filePath = _env.WebRootPath;
		_hostURL = Path.Combine($"{_httpContextAccessor.HttpContext!.Request.Scheme}:",
								$"{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}");
	}


	private string StaticFilesDirectory(CustomUploadDirectories customUploadDirectories, string folderPrefix)
	{
		return $"{Path.Combine(_directory, _filePath, "Upload", "images", customUploadDirectories.ToString(), folderPrefix)}";
	}

	public async Task<string> UploadAsync(CustomUploadDirectories directories, IFormFile file, string folderPrefix)
	{
		string fileDirectory = StaticFilesDirectory(directories, folderPrefix), uniquefileName = string.Empty;

		try
		{
			if (!Directory.Exists(fileDirectory))
			{
				Directory.CreateDirectory(fileDirectory);
			}

			uniquefileName = await UploadAsync(directories, fileDirectory, file);

		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
		return uniquefileName;
	}
	public async Task<List<string>> UploadAsync(CustomUploadDirectories directories, IFormFileCollection files, string folderPrefix)
	{
		string fileDirectory = StaticFilesDirectory(directories, folderPrefix);
		List<string> filesNames = new List<string>();


		try
		{
			if (!System.IO.Directory.Exists(fileDirectory))
			{
				System.IO.Directory.CreateDirectory(fileDirectory);
			}
			foreach (var file in files)
			{

				filesNames.Add(await UploadAsync(directories, fileDirectory, file));
			}

		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
		return filesNames;
	}
	private async Task<string> UploadAsync(CustomUploadDirectories directories, string fileDirectory, IFormFile file)
	{
		string uniqueFileName = $"{directories}-{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";
		string fullPath = Path.Combine(fileDirectory, uniqueFileName);
		try
		{
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}
			using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
			{
				await file.CopyToAsync(fs);
				await fs.FlushAsync();
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
		return uniqueFileName;
	}
	public string ReadStaticFiles(string folderPrefix, CustomUploadDirectories directories, string physicalImageName)
	{
		string fileUrl = string.Empty;

		try
		{
			if (Directory.Exists(StaticFilesDirectory(directories, folderPrefix)))
			{
				DirectoryInfo info = new DirectoryInfo(StaticFilesDirectory(directories, folderPrefix));
				FileInfo[] files = info.GetFiles();
				var file = files.SingleOrDefault(f => f.Name.Equals(physicalImageName));
				if (file != null)
				{
					string imgPath = Path.Combine(StaticFilesDirectory(directories, folderPrefix), file.Name);
					if (File.Exists(imgPath))
					{
						string imgURL = _hostURL + $"/Upload/images/{directories}/" + folderPrefix + "/" + file.Name;
						fileUrl = imgURL;
					}
					else
					{
						throw new FileNotFoundException("File cant found " + imgPath);
					}
				}
			}
			else
			{
				throw new DirectoryNotFoundException("Directory cant found" + StaticFilesDirectory(directories, folderPrefix));
			}

		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
		return fileUrl;
	}
		
	public List<string> ReadStaticFiles(string folderPrefix, CustomUploadDirectories directories, List<string> physicalImageNames)
	{
		List<string> fileUrls = new();

		try
		{
			if (Directory.Exists(StaticFilesDirectory(directories, folderPrefix)))
			{
				DirectoryInfo info = new DirectoryInfo(StaticFilesDirectory(directories, folderPrefix));
				FileInfo[] files = info.GetFiles();
				foreach (string physicalImageName in physicalImageNames)
				{
					var file = files.SingleOrDefault(f => f.Name.Equals(physicalImageName));
					if (file != null)
					{
						string imgPath = Path.Combine(StaticFilesDirectory(directories, folderPrefix), file.Name);
						if (File.Exists(imgPath))
						{
							string imgURL = _hostURL + $"/Upload/images/{directories}/" + folderPrefix + "/" + file.Name;
							fileUrls.Add(imgURL);
						}
						else
						{
							throw new FileNotFoundException("File cant found " + imgPath);
						}
					}
				}
			}
			else
			{
				throw new DirectoryNotFoundException("Directory cant found" + StaticFilesDirectory(directories, folderPrefix));
			}

		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
		return fileUrls;
	}

	public void RemoveStaticFiles(string folderPrefix, CustomUploadDirectories directories, string pyshicalImageName)
	{
		string filePath = StaticFilesDirectory(directories, folderPrefix);
		try
		{
			if (Directory.Exists(filePath))
			{
				DirectoryInfo dirInfo = new DirectoryInfo(filePath);
				FileInfo[] fileInfos = dirInfo.GetFiles();
				var file = fileInfos.SingleOrDefault(fi => fi.Equals(pyshicalImageName));
				if (file != null)
				{
					file.Delete();
				}

			}
			else { throw new DirectoryNotFoundException($"Directory cant find, {filePath}"); }
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}
	public void RemoveStaticFiles(string folderPrefix, CustomUploadDirectories directories, List<string> pyshicalImageNames)
	{
		string filePath = StaticFilesDirectory(directories, folderPrefix);
		try
		{
			if (Directory.Exists(filePath))
			{
				DirectoryInfo dirInfo = new DirectoryInfo(filePath);
				FileInfo[] fileInfos = dirInfo.GetFiles();
				foreach(var pyshicalImageName in pyshicalImageNames)
				{

				var file = fileInfos.SingleOrDefault(fi => fi.Equals(pyshicalImageName));
				file?.Delete();
				}

			}
			else { throw new DirectoryNotFoundException($"Directory cant find, {filePath}"); }
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}
}
