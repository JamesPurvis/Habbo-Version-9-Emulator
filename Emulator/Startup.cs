using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
public static class Startup
{

    private static Environment m_singleton = null;
    public static void Main(String[] args)
    {
        Console.WriteLine("The server is starting...");

        try
        {
            m_singleton = new Environment();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.InnerException.Message);
        }
        finally
        {
            
            Console.ReadLine();
        }
    }


    public static Environment return_environment()
    {
        if (m_singleton == null)
        {
            m_singleton = new Environment();
        }

        return m_singleton;
    }
}