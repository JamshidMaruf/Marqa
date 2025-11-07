using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Lessons.Models;
public class LessonModifyModel
{
    public string Name { get; set; }
    public int Id { get; set; }
    public HomeTaskStatus HomeTaskStatus { get; set; }
}
