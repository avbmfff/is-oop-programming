using System.Net.Mail;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Security;

namespace DataAccessLayer.Entities;

public class Boss : Employee
{
    private List<Employee> _workers;
    private SecureString _password;
    public Boss(string name, MailAddress mailAddress, SecureString password) : base(name, mailAddress)
    {
        if (password == null)
            throw new MessageException("null reference");
        _workers = new List<Employee>();
        _password = password;
    }

    public IReadOnlyCollection<Employee> Workers => _workers;
    
    public void AddWorker(Employee employee)
    {
        if (employee == null)
            throw new MessageException("null reference of worker");
        _workers.Add(employee);
    }

    public SecureString GetPassword()
    {
        return _password;
    }

    public bool WorkerExist(Employee employee)
    {
        if (employee == null)
            throw new MessageException("null reference");
        return _workers.Any(value => value.MailAddress == employee.MailAddress);
    }
    public void GetReport(string path)
    {
        Word._Application wordApp = new Word.ApplicationClass();
        wordApp.Visible = true;
        object missing = Type.Missing;

        Word._Document wordDoc = wordApp.Documents.Add (ref missing, ref missing, ref missing, ref missing);
        foreach (var worker in _workers)
        {
            Word.Paragraph para = wordDoc.Paragraphs.Add (ref missing);
            object styleName = worker.GetName() + " review " + worker.AmountMessages;
            para.Range.set_Style (ref styleName);
            para.Range.InsertParagraphAfter ();
        }

        object filename = Path.GetFullPath(path);

        wordDoc.SaveAs(ref filename, ref missing, ref missing,

            ref missing, ref missing, ref missing, ref missing,

            ref missing, ref missing, ref missing, ref missing,

            ref missing, ref missing, ref missing, ref missing,

            ref missing);
    }
}