﻿//-----------------------------------------------------------------------------
// Filename: SIPSoftPhoneState.cs
//
// Description: A helper class to load the application's settings and to hold some application wide variables. 
// 
// History:
// 27 Mar 2012	Aaron Clauson	Refactored.
//
// License: 
// This software is licensed under the BSD License http://www.opensource.org/licenses/bsd-license.php
//
// Copyright (c) 2006-2012 Aaron Clauson (aaron@sipsorcery.com), SIP Sorcery PTY LTD, Hobart, Australia (www.sipsorcery.com)
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that 
// the following conditions are met:
//
// Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following 
// disclaimer in the documentation and/or other materials provided with the distribution. Neither the name of SIPSorcery Ltd. 
// nor the names of its contributors may be used to endorse or promote products derived from this software without specific 
// prior written permission. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
// BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//-----------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Net;
using System.Xml;
using SIPSorcery.Sys;
using log4net;

namespace SIPSorcery.SoftPhone
{
    public class SIPSoftPhoneState
    {
        private const string SIPSOFTPHONE_CONFIGNODE_NAME = "sipsoftphone";
        private const string SIPSOCKETS_CONFIGNODE_NAME = "sipsockets";
        private const string STUN_SERVER_KEY = "STUNServerHostname";
        private const string VIDEO_DEVICE_INDEX_KEY = "VideoDeviceIndex";
        private const int DEFAULT_VIDEO_DEVICE_INDEX = 0;

        private static ILog logger = AppState.logger;

        private static readonly XmlNode m_sipSoftPhoneConfigNode;
        public static readonly XmlNode SIPSocketsNode;
        public static readonly string STUNServerHostname;

        public static readonly string SIPUsername = ConfigurationManager.AppSettings["SIPUsername"];    // Get the SIP username from the config file.
        public static readonly string SIPPassword = ConfigurationManager.AppSettings["SIPPassword"];    // Get the SIP password from the config file.
        public static readonly string SIPServer = ConfigurationManager.AppSettings["SIPServer"];        // Get the SIP server from the config file.
        public static readonly string SIPFromName = ConfigurationManager.AppSettings["SIPFromName"];    // Get the SIP From display name from the config file.
        public static readonly string DnsServer = ConfigurationManager.AppSettings["DnsServer"];        // Get the optional DNS server from the config file.

        public static IPAddress DefaultLocalAddress;

        static SIPSoftPhoneState()
        {
            try
            {
                if (ConfigurationManager.GetSection(SIPSOFTPHONE_CONFIGNODE_NAME) != null)
                {
                    m_sipSoftPhoneConfigNode = (XmlNode)ConfigurationManager.GetSection(SIPSOFTPHONE_CONFIGNODE_NAME);
                }

                if (m_sipSoftPhoneConfigNode != null)
                {
                    SIPSocketsNode = m_sipSoftPhoneConfigNode.SelectSingleNode(SIPSOCKETS_CONFIGNODE_NAME);
                }

                STUNServerHostname = ConfigurationManager.AppSettings[STUN_SERVER_KEY];

                DefaultLocalAddress = LocalIPConfig.GetDefaultIPv4Address();
                logger.Debug("Default local IPv4 address determined as " + DefaultLocalAddress + ".");
            }
            catch (Exception excp)
            {
                logger.Error("Exception SIPSoftPhoneState. " + excp.Message);
                throw;
            }
        }
    }
}
