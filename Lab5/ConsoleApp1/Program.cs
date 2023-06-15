using Backups.Exception;
using Backups.InMemoryRepository;
using Directory = Backups.InMemoryRepository.Directory;
DateTime dateTime1 = new DateTime(2022,5,25);
DateTime dateTime2 = new DateTime(2022,6,25);
Directory directory = new Directory("hjhhj");
Console.WriteLine(directory.GetType()==typeof(Directory));
