using Isu.Entities;
using Isu.Exception;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isuService = new Services.IsuService();
        var newgroup = new GroupName("M3204");
        Group group = isuService.AddGroup(newgroup);
        Student newstudent = isuService.AddStudent(group, "Anton");
        Assert.Contains(newstudent, group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuService = new Services.IsuService();
        Assert.Throws<IsuException>(() =>
        {
            Group newGroup = isuService.AddGroup(new GroupName("M3203"));
            Student newStudent = isuService.AddStudent(newGroup, "Anton");
            for (int i = 0; i < Group.MaxStudentsCount + 1; ++i)
            {
                newGroup.AddStudent(newStudent);
            }

            newGroup.AddStudent(newStudent);
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var service = new Services.IsuService();
        Assert.Throws<IsuException>(() =>
        {
            Group group = service.AddGroup(new GroupName("V45000"));
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var service = new Services.IsuService();
        Group oldGroup = service.AddGroup(new GroupName("M3100"));
        Group newGroup = service.AddGroup(new GroupName("M3115"));
        Student newStudent = service.AddStudent(oldGroup, "Haha");
        service.AddStudent(oldGroup, "Haha");
        service.ChangeStudentGroup(newStudent, newGroup);
        Assert.Contains(newStudent, newGroup.Students);
    }
}