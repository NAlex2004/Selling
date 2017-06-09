using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace NAlex.Selling.Service
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		static void Main(string[] args)
		{           
            // запуск как служба
			if (!Environment.UserInteractive)
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new SellingService()
				};
				ServiceBase.Run(ServicesToRun);
			}
			else // консольное
			{
                Console.WriteLine("Any key to stop.");
                using (SellingService service = new SellingService())
                {
                    service.StartInteractive(args);
                    Console.ReadKey();
                    service.StopInteractive();
                }
			}
			
		}

	}
}
