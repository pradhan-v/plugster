//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace MadPiranha.Plugster.Base.Test
{
    public class RemoteMethod
    {
        public static int TheMethod(String parameter)
        {
            Console.WriteLine(parameter + " : " + DateTime.Today + " : " + Process.GetCurrentProcess());
            //TcpChannel channel = new TcpChannel(8080);
            //ChannelServices.RegisterChannel(channel);
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteMethod), "HelloWorld", WellKnownObjectMode.Singleton);
            return 1;
        }
    }

}
