using Backups.InMemoryRepository;
using Backups.InMemoryRepository.Tools;
using Xunit;
using Directory = Backups.InMemoryRepository.Directory;
using File = Backups.InMemoryRepository.File;

namespace Backups.Test;

public class BackupTest
{
    [Fact]
    public void Test1()
    {
        var rep = new RepositoryComponent("C://");
        var backupTask = new BackupTask(new SplitTools(), "BackupTask");
        var file = new File("Hello.cs");
        var folder = new Directory("Folder/");
        var tosave = new Directory("For Restore points/");
        rep.Add(tosave);
        rep.Add(file);
        rep.Add(folder);
        var bo1 = new BackupObject(file);
        var bo2 = new BackupObject(folder);
        backupTask.AddBackupObject(bo1);
        backupTask.AddBackupObject(bo2);
        backupTask.Save(tosave, "1", DateTime.Now);
        backupTask.RemoveBackupObject(bo2);
        backupTask.Save(tosave, "2");
        Assert.True(backupTask.RestorePoints.Count == 2);
        Assert.True(backupTask.Storages.Count == 3);
    }
}