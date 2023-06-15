using System.Net.Mail;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using Microsoft.Office.Interop.Word;

namespace BusinessLayer;

public interface IBossService
{
    Employee CreateWorker(MailAddress mailAddress, string name);
    void AddWorker(Employee employee);
    void GetReport(string path);
}