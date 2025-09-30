using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Service.Services.HomeTask.Models;

namespace Marqa.Service.Services.HomeTask;
public interface IHomeTaskService
{
    Task CreateHomeTaskAsync(HomeTaskCreateModel model);
    Task DeleteHomeTaskAsync(int lessonid);
    Task UpdateHomeTaskAsync(HomeTaskUpdateModel model);
    Task<HomeTaskGetModel> GetHomeTaskAsync(int lessonid);
}
