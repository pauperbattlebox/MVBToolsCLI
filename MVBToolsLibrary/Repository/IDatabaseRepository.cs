using OpenQA.Selenium.DevTools.V110.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IDatabaseRepository
    {
        //GetAll
        Task GetAll();

        //Get
        Task Get();

        //Upsert
        Task Upsert();
    }
}
