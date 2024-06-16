using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace WebApi_JWT.Utils
{
    public class RedisCon
    {

        static Lazy<ConnectionMultiplexer> _lazyCon;


        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyCon.Value;


            }
        }


        static RedisCon() {

            _lazyCon = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect("127.0.0.1")  
            );
        }


    }
}
