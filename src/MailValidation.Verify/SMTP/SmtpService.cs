using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MailValidation.Verify.Exceptions;

namespace MailValidation.Verify.SMTP
{
    internal class SmtpService
    {
        private readonly string _host;
        private readonly int _port;
        private SmtpResponse _response;

        public SmtpService(string host, int port = 25)
        {
            _host = host;
            _port = port;
            _response = new SmtpResponse(); 
        }

        public bool CheckMailboxExists(string email, out SmtpStatusCode result)
        {
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.SendTimeout = 1000;
                    tcpClient.ReceiveTimeout = 1000;

                    if (!tcpClient.ConnectAsync(_host, _port).Wait(1000))
                    {
                        throw new SmtpClientTimeoutException();
                    }

                    NetworkStream networkStream = tcpClient.GetStream();
                    StreamReader streamReader = new StreamReader(networkStream);

                    this.AcceptResponse(streamReader, SmtpStatusCode.ServiceReady);

                    string mailHost = (new MailAddress(email)).Host;

                    this.SendCommand(networkStream, streamReader, "HELO " + mailHost, SmtpStatusCode.Ok);
                    this.SendCommand(networkStream, streamReader, "MAIL FROM:<check@" + mailHost + ">", SmtpStatusCode.Ok);
                    _response = this.SendCommand(networkStream, streamReader, "RCPT TO:<" + email + ">");
                    this.SendCommand(networkStream, streamReader, "QUIT", SmtpStatusCode.ServiceClosingTransmissionChannel, SmtpStatusCode.MailboxUnavailable);

                    result = _response.Code;

                    return true;
                }
            }
            catch (IOException e)
            {
                // StreamReader problem
            }
            catch (SocketException e)
            {
                // TcpClient problem
            }

            result = SmtpStatusCode.GeneralFailure;
            return false;
        }

        private SmtpResponse SendCommand(NetworkStream networkStream, StreamReader streamReader, string command, params SmtpStatusCode[] goodReplys)
        {
            var dataBuffer = Encoding.ASCII.GetBytes(command + "\r\n");
            networkStream.Write(dataBuffer, 0, dataBuffer.Length);

            return this.AcceptResponse(streamReader, goodReplys);
        }

        private SmtpResponse AcceptResponse(StreamReader streamReader, params SmtpStatusCode[] goodReplys)
        {
            string response = streamReader.ReadLine();

            if (string.IsNullOrEmpty(response) || response.Length < 3)
            {
                throw new SmtpClientException("Invalid response");
            }

            SmtpStatusCode smtpStatusCode = this.GetResponseCode(response);

            if (goodReplys.Length > 0 && !goodReplys.Contains(smtpStatusCode))
            {
                throw new SmtpClientException(response);
            }

            return new SmtpResponse
            {
                Raw = response,
                Code = smtpStatusCode
            };
        }

        private SmtpStatusCode GetResponseCode(string response)
        {
            return (SmtpStatusCode)Enum.Parse(typeof(SmtpStatusCode), response.Substring(0, 3));
        }
    }

}
