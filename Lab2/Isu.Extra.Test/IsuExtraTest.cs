using Isu.Entities;
using Isu.Exception;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Service;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private IsuExtraService isuExtra = new IsuExtraService();
    [Fact]
    public void AddOgnpGroup()
    {
        var groupname = new GroupName("CIB-1");
        var lesson = new Lesson("Ciber", DayOfWeek.Monday, new TimeOnly(17, 0), groupname);
        var ognpgroup = new OgnpGroup('K', "Ciber", groupname, lesson);
        isuExtra.AddNewOgnpGroup(ognpgroup);
        Assert.Contains(ognpgroup, isuExtra.GetListOgnpGroup());
    }

    [Fact]
    public void CheckClassOgnpGroups()
    {
        var ognpname1 = new GroupName("cib-1");
        var ognpname2 = new GroupName("cib-2");
        var ognpname3 = new GroupName("cib-3");
        var ognpname4 = new GroupName("cib-4");
        var lesson = new Lesson("Ciber", DayOfWeek.Monday, new TimeOnly(17, 0), ognpname1);
        var lesson1 = new Lesson("Ciber", DayOfWeek.Monday, new TimeOnly(17, 0), ognpname2);
        var lesson2 = new Lesson("Ciber", DayOfWeek.Monday, new TimeOnly(17, 0), ognpname3);
        var lesson3 = new Lesson("Ciber", DayOfWeek.Monday, new TimeOnly(17, 0), ognpname4);
        var ognpgroup = new OgnpGroup('K', "Ciber", ognpname1, lesson);
        var ognpgroup1 = new OgnpGroup('K', "Ciber", ognpname2, lesson1);
        var ognpgroup2 = new OgnpGroup('K', "Ciber", ognpname3, lesson2);
        var ognpgroup3 = new OgnpGroup('K', "Ciber", ognpname4, lesson3);
        isuExtra.AddNewOgnpGroup(ognpgroup);
        isuExtra.AddNewOgnpGroup(ognpgroup1);
        isuExtra.AddNewOgnpGroup(ognpgroup2);
        isuExtra.AddNewOgnpGroup(ognpgroup3);
        Assert.Contains(ognpgroup1, isuExtra.GetClassOgnpGroups("Ciber"));
        Assert.Contains(ognpgroup, isuExtra.GetClassOgnpGroups("Ciber"));
        Assert.Contains(ognpgroup2, isuExtra.GetClassOgnpGroups("Ciber"));
        Assert.Contains(ognpgroup3, isuExtra.GetClassOgnpGroups("Ciber"));
    }
}