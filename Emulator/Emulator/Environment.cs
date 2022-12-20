using Emulator.Logging;
using Emulator.Network;
using Emulator.Messages;
using Emulator.Game.Database;
using Emulator.Network.Session;
using Emulator.Game.Navigator;
using Emulator.Game.Rooms;

public class Environment
{

    private ServerBootStrap m_server_bootstrap = null;
    private MessageHandler m_message_handler = null;
    private DatabaseManager m_database_manager = null;
    private SessionManager m_session_manager = null;
    private NavigatorManager m_navigator_manager = null;
    private RoomManager m_room_manager = null;

    public MessageHandler return_message_handler()
    {
        return m_message_handler;
    }

    public DatabaseManager return_database_manager()
    {
        return m_database_manager;
    }

    public SessionManager return_session_manager()
    {
        return m_session_manager;
    }

    public NavigatorManager return_navigator_manager()
    {
        return m_navigator_manager;
    }

    public RoomManager return_room_manager()
    {
        return m_room_manager;
    }

    public Environment()
    {
       Logging.m_Logger.Info("A server environment has been intialized successfully.");
        m_server_bootstrap = new ServerBootStrap();
        Logging.m_Logger.Info("A server bootstrap has been intialized successfully.");
        m_server_bootstrap.beginListening("10.0.0.23", 30000);

        try
        {
            m_message_handler = new MessageHandler();
            m_database_manager = new DatabaseManager();
            m_session_manager = new SessionManager();
           m_navigator_manager = new NavigatorManager();
           m_room_manager = new RoomManager();
          

            m_navigator_manager.LoadNavigator();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}