using ArnoldVinkCode;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using static ArnoldVinkCode.ArnoldVinkSockets;
using static ArnoldVinkCode.AVClassConverters;
using static FpsOverlayer.AppVariables;
using static LibraryShared.Classes;

namespace FpsOverlayer
{
    partial class SocketHandlers
    {
        //Handle received socket data
        public static void ReceivedSocketHandler(TcpClient tcpClient, UdpEndPointDetails endPoint, byte[] receivedBytes)
        {
            try
            {
                async Task TaskAction()
                {
                    try
                    {
                        if (tcpClient != null)
                        {
                            //await ReceivedTcpSocketHandlerThread(tcpClient, receivedBytes);
                        }
                        else
                        {
                            await ReceivedUdpSocketHandlerThread(endPoint, receivedBytes);
                        }
                    }
                    catch { }
                }
                AVActions.TaskStartBackground(TaskAction);
            }
            catch { }
        }

        private static async Task ReceivedUdpSocketHandlerThread(UdpEndPointDetails endPoint, byte[] receivedBytes)
        {
            try
            {
                //Get the source server ip and port
                //Debug.WriteLine("Received udp socket from: " + endPoint.IPEndPoint.Address.ToString() + ":" + endPoint.IPEndPoint.Port + "/" + receivedBytes.Length + "bytes");

                //Deserialize the received bytes
                if (DeserializeBytesToObject(receivedBytes, out SocketSendContainer deserializedBytes))
                {
                    Type objectType = Type.GetType(deserializedBytes.SendType);
                    if (objectType == typeof(KeypadSize))
                    {
                        KeypadSize receivedKeypadSize = deserializedBytes.GetObjectAsType<KeypadSize>();

                        //Set the window keypad margin
                        vKeypadAdjustMargin = receivedKeypadSize.Height;

                        //Update fps overlay position and visibility
                        vWindowStats.UpdateFpsOverlayPositionVisibility(vTargetProcess.ExeNameNoExt);
                    }
                    else if (objectType == typeof(string))
                    {
                        string receivedString = (string)deserializedBytes.SendObject;
                        Debug.WriteLine("Received socket string: " + receivedString);
                        if (receivedString == "ApplicationExit")
                        {
                            await AppExit.Exit();
                        }
                        else if (receivedString == "SwitchFpsOverlayVisibility")
                        {
                            vWindowStats.SwitchFpsOverlayVisibility();
                        }
                        else if (receivedString == "SwitchToolsOverlayVisibility")
                        {
                            vWindowTools.SwitchToolsVisibility();
                        }
                        else if (receivedString == "SwitchCrosshairOverlayVisibility")
                        {
                            vWindowCrosshair.SwitchCrosshairVisibility(true);
                        }
                        else if (receivedString == "ChangeFpsOverlayPosition")
                        {
                            vWindowStats.ChangeFpsOverlayPosition();
                        }
                    }
                }
            }
            catch { }
        }
    }
}