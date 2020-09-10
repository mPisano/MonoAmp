using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HttpNamespaceManager.Lib.AccessControl
{
    /// <summary>
    /// Security Identity
    /// </summary>
    /// <remarks>The SecurityIdentity class is a read only representation of a 
    /// SID. The class has no public constructors, instead use the static 
    /// SecurityIdentityFrom* methods to instantiate it.</remarks>
    public class SecurityIdentity
    {
        private string name;
        private string sid;
        private WELL_KNOWN_SID_TYPE wellKnownSidType = WELL_KNOWN_SID_TYPE.None;

        /// <summary>
        /// Gets the name of to security object represented by the SID
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the SID string of the security object
        /// </summary>
        public string SID
        {
            get { return this.sid; }
        }

        /// <summary>
        /// Gets a value indicating whether or not the SID is a well known SID or not
        /// </summary>
        public bool IsWellKnownSid
        {
            get { return this.wellKnownSidType != WELL_KNOWN_SID_TYPE.None; }
        }

        /// <summary>
        /// Gets the type of well known SID
        /// </summary>
        public WELL_KNOWN_SID_TYPE WellKnownSidType
        {
            get { return this.wellKnownSidType; }
        }
        
        /// <summary>
        /// Creates a blank Security Identity
        /// </summary>
        private SecurityIdentity()
        {
            // Do Nothing
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is SecurityIdentity)
            {
                SecurityIdentity sd = (SecurityIdentity)obj;

                return (String.Compare(this.sid, sd.sid, true) == 0);
            }
            else return false;
        }

        public static bool operator==(SecurityIdentity obj1, SecurityIdentity obj2)
        {
            if (Object.ReferenceEquals(obj1, null) && Object.ReferenceEquals(obj2, null)) return true;
            else if (Object.ReferenceEquals(obj1, null) || Object.ReferenceEquals(obj2, null)) return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(SecurityIdentity obj1, SecurityIdentity obj2)
        {
            if (Object.ReferenceEquals(obj1, null) && Object.ReferenceEquals(obj2, null)) return false;
            else if (Object.ReferenceEquals(obj1, null) || Object.ReferenceEquals(obj2, null)) return true;
            return !obj1.Equals(obj2);
        }

        public override int GetHashCode()
        {
            return this.sid != null ? this.sid.GetHashCode() : base.GetHashCode();
        }

        /// <summary>
        /// Renders the Security Identity as a SDDL SID string or abbreviation
        /// </summary>
        /// <returns>An SDDL SID string or abbreviation</returns>
        public override string ToString()
        {
            if (this.IsWellKnownSid && !String.IsNullOrEmpty(SecurityIdentity.wellKnownSidAbbreviations[(int)this.wellKnownSidType]))
            {
                return SecurityIdentity.wellKnownSidAbbreviations[(int)this.wellKnownSidType];
            }
            else return this.sid;
        }

        /// <summary>
        /// Table of well known SID strings
        /// </summary>
        /// <remarks>The table indicies correspond to <see cref="WELL_KNOWN_SID_TYPE"/>s</remarks>
        private static readonly string[] wellKnownSids = new string[] {
            "S-1-0-0", // NULL SID
            "S-1-1-0", // Everyone
            "S-1-2-0", // LOCAL
            "S-1-3-0", // CREATOR OWNER
            "S-1-3-1", // CREATOR GROUP
            "S-1-3-2", // CREATOR OWNER SERVER
            "S-1-3-3", // CREATOR GROUP SERVER
            "S-1-5", // NT Pseudo Domain\NT Pseudo Domain
            "S-1-5-1", // NT AUTHORITY\DIALUP
            "S-1-5-2", // NT AUTHORITY\NETWORK
            "S-1-5-3", // NT AUTHORITY\BATCH
            "S-1-5-4", // NT AUTHORITY\INTERACTIVE
            "S-1-5-6", // NT AUTHORITY\SERVICE
            "S-1-5-7", // NT AUTHORITY\ANONYMOUS LOGON
            "S-1-5-8", // NT AUTHORITY\PROXY
            "S-1-5-9", // NT AUTHORITY\ENTERPRISE DOMAIN CONTROLLERS
            "S-1-5-10", // NT AUTHORITY\SELF
            "S-1-5-11", // NT AUTHORITY\Authenticated Users
            "S-1-5-12", // NT AUTHORITY\RESTRICTED
            "S-1-5-13", // NT AUTHORITY\TERMINAL SERVER USER
            "S-1-5-14", // NT AUTHORITY\REMOTE INTERACTIVE LOGON
            "", // Unknown
            "S-1-5-18", // NT AUTHORITY\SYSTEM
            "S-1-5-19", // NT AUTHORITY\LOCAL SERVICE
            "S-1-5-20", // NT AUTHORITY\NETWORK SERVICE
            "S-1-5-32", // BUILTIN\BUILTIN
            "S-1-5-32-544", // BUILTIN\Administrators
            "S-1-5-32-545", // BUILTIN\Users
            "S-1-5-32-546", // BUILTIN\Guests
            "S-1-5-32-547", // BUILTIN\Power Users
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "S-1-5-32-551", // BUILTIN\Backup Operators
            "S-1-5-32-552", // BUILTIN\Replicator
            "", // Unknown
            "S-1-5-32-555", // BUILTIN\Remote Desktop Users
            "S-1-5-32-556", // BUILTIN\Network Configuration Operators
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "", // Unknown
            "S-1-5-64-10", // NT AUTHORITY\NTLM Authentication
            "S-1-5-64-21", // NT AUTHORITY\Digest Authentication
            "S-1-5-64-14", // NT AUTHORITY\SChannel Authentication
            "S-1-5-15", // NT AUTHORITY\This Organization
            "S-1-5-1000", // NT AUTHORITY\Other Organization
            "", // Unknown
            "S-1-5-32-558", // BUILTIN\Performance Monitor Users
            "S-1-5-32-559", // BUILTIN\Performance Log Users
            "", // Unknown
            "", // Unknown
            "S-1-5-32-562", // BUILTIN\Distributed COM Users
            "S-1-5-32-568", // BUILTIN\IIS_IUSRS
            "S-1-5-17", // NT AUTHORITY\IUSR
            "S-1-5-32-569", // BUILTIN\Cryptographic Operators
            "S-1-16-0", // Mandatory Label\Untrusted Mandatory Level
            "S-1-16-4096", // Mandatory Label\Low Mandatory Level
            "S-1-16-8192", // Mandatory Label\Medium Mandatory Level
            "S-1-16-12288", // Mandatory Label\High Mandatory Level
            "S-1-16-16384", // Mandatory Label\System Mandatory Level
            "S-1-5-33", // NT AUTHORITY\WRITE RESTRICTED
            "S-1-3-4", // OWNER RIGHTS
            "", // Unknown
            "", // Unknown
            "S-1-5-22", // NT AUTHORITY\ENTERPRISE READ-ONLY DOMAIN CONTROLLERS BETA
            "", // Unknown
            "S-1-5-32-573" // BUILTIN\Event Log Readers
        };

        /// <summary>
        /// Creates a Security Identity from a SID string
        /// </summary>
        /// <param name="sid">A SID string (Format: S-1-1-...) or well known SID abbreviation (e.g. DA)</param>
        /// <returns>A populated Security Identity</returns>
        public static SecurityIdentity SecurityIdentityFromString(string sid)
        {
            if (sid == null)
            {
                throw new ArgumentNullException("sid");
            }

            if (sid == "")
            {
                throw new ArgumentException("Argument 'sid' cannot be the empty string.", "sid");
            }

            if (!sid.StartsWith("S-"))
            {
                // If the string is not a SID string (S-1-n-...) assume it is a SDDL abbreviation
                return SecurityIdentity.SecurityIdentityFromWellKnownSid(SecurityIdentity.GetWellKnownSidTypeFromSddlAbbreviation(sid));
            }

            SecurityIdentity secId = new SecurityIdentity();
            secId.sid = sid;

            // Check if the SID is a well known SID
            secId.wellKnownSidType = (WELL_KNOWN_SID_TYPE)Array.IndexOf<string>(SecurityIdentity.wellKnownSids, secId.sid);

            IntPtr sidStruct;

            // Convert the SID string to a SID structure
            if (!SecurityIdentity.ConvertStringSidToSid(sid, out sidStruct))
            {
                throw new ExternalException(String.Format("Error Converting SID String to SID Structur: {0}", Util.GetErrorMessage(Util.GetLastError())));
            }

            try
            {
                uint nameLen = 0;
                uint domainLen = 0;

                SID_NAME_USE nameUse;

                // Get the lengths of the object and domain names
                SecurityIdentity.LookupAccountSid(null, sidStruct, IntPtr.Zero, ref nameLen, IntPtr.Zero, ref domainLen, out nameUse);

                if (nameLen == 0) throw new ExternalException("Unable to Find SID");

                IntPtr accountName = Marshal.AllocHGlobal((IntPtr)nameLen);
                IntPtr domainName = domainLen > 0 ? Marshal.AllocHGlobal((IntPtr)domainLen) : IntPtr.Zero;

                try
                {
                    // Get the object and domain names
                    if (!SecurityIdentity.LookupAccountSid(null, sidStruct, accountName, ref nameLen, domainName, ref domainLen, out nameUse))
                    {
                        throw new ExternalException("Unable to Find SID");
                    }

                    // Marshal and store the object name
                    secId.name = String.Format("{0}{1}{2}", domainLen > 1 ? Marshal.PtrToStringAnsi(domainName) : "", domainLen > 1 ? "\\" : "", Marshal.PtrToStringAnsi(accountName));
                }
                finally
                {
                    if (accountName != IntPtr.Zero) Marshal.FreeHGlobal(accountName);
                    if (domainName != IntPtr.Zero) Marshal.FreeHGlobal(domainName);
                }
            }
            finally
            {
                if (sidStruct != IntPtr.Zero) Util.LocalFree(sidStruct);
            }
            
            return secId;
        }

        /// <summary>
        /// Table of SDDL SID abbreviations
        /// </summary>
        /// <remarks>The table indicies correspond to <see cref="WELL_KNOWN_SID_TYPE"/>s</remarks>
        private static readonly string[] wellKnownSidAbbreviations = new string[] {
            "",
            "WD",
            "",
            "CO",
            "CG",
            "",
            "",
            "",
            "",
            "NU",
            "",
            "IU",
            "SU",
            "AN",
            "",
            "EC",
            "PS",
            "AU",
            "RC",
            "",
            "",
            "",
            "SY",
            "LS",
            "NS",
            "",
            "BA",
            "BU",
            "BG",
            "PU",
            "AO",
            "SO",
            "PO",
            "BO",
            "RE",
            "RU",
            "RD",
            "NO",
            "LA",
            "LG",
            "",
            "DA",
            "DU",
            "DG",
            "DC",
            "DD",
            "CA",
            "SA",
            "EA",
            "PA",
            "RS",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };

        /// <summary>
        /// Gets the Well Known SID Type for an SDDL abbreviation
        /// </summary>
        /// <param name="abbreviation">The SDDL abbreviation</param>
        /// <returns>The Well Known SID Type that corresponds to the abbreviation</returns>
        public static WELL_KNOWN_SID_TYPE GetWellKnownSidTypeFromSddlAbbreviation(string abbreviation)
        {
            if (abbreviation == null)
            {
                throw new ArgumentNullException("abbreviation");
            }

            if (abbreviation == "")
            {
                throw new ArgumentException("Argument 'abbreviation' cannot be the empty string.", "abbreviation");
            }

            return (WELL_KNOWN_SID_TYPE)Array.IndexOf<string>(SecurityIdentity.wellKnownSidAbbreviations, abbreviation);
        }

        /// <summary>
        /// Creates a Security Identity from an object name (e.g. DOMAIN\AccountName)
        /// </summary>
        /// <param name="name">A security object name (i.e. a Computer, Account, or Group)</param>
        /// <returns>A populated Security Identity</returns>
        public static SecurityIdentity SecurityIdentityFromName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name == "")
            {
                throw new ArgumentException("Argument 'name' cannot be the empty string.", "name");
            }

            LSA_OBJECT_ATTRIBUTES attribs = new LSA_OBJECT_ATTRIBUTES();
            attribs.Attributes = 0;
            attribs.ObjectName = IntPtr.Zero;
            attribs.RootDirectory = IntPtr.Zero;
            attribs.SecurityDescriptor = IntPtr.Zero;
            attribs.SecurityQualityOfService = IntPtr.Zero;
            attribs.Length = (uint)Marshal.SizeOf(attribs);

            IntPtr handle;

            int status = SecurityIdentity.LsaOpenPolicy(IntPtr.Zero, ref attribs, ACCESS_MASK.POLICY_LOOKUP_NAMES, out handle);

            if(status != 0)
            {
                throw new ExternalException("Unable to Find Object: " + Util.GetErrorMessage(SecurityIdentity.LsaNtStatusToWinError(status)));
            }

            try
            {
                LSA_UNICODE_STRING nameString = new LSA_UNICODE_STRING();
                nameString.Buffer = name;
                nameString.Length = (ushort)(name.Length * UnicodeEncoding.CharSize);
                nameString.MaxLength = (ushort)(name.Length * UnicodeEncoding.CharSize + UnicodeEncoding.CharSize);

                IntPtr domains;
                IntPtr sids;

                status = SecurityIdentity.LsaLookupNames2(handle, 0, 1, new LSA_UNICODE_STRING[] { nameString }, out domains, out sids);
                
                if(status != 0)
                {
                    throw new ExternalException("Unable to Find Object: " + Util.GetErrorMessage(SecurityIdentity.LsaNtStatusToWinError(status)));
                }

                try
                {
                    SecurityIdentity secId = new SecurityIdentity();

                    LSA_TRANSLATED_SID2 lsaSid = (LSA_TRANSLATED_SID2)Marshal.PtrToStructure(sids, typeof(LSA_TRANSLATED_SID2));

                    IntPtr sidStruct = lsaSid.Sid;

                    IntPtr sidString = IntPtr.Zero;

                    // Get the SID string
                    if (!SecurityIdentity.ConvertSidToStringSid(sidStruct, out sidString))
                    {
                        throw new ExternalException("Unable to Find Object: " + Util.GetErrorMessage(Util.GetLastError()));
                    }

                    try
                    {
                        // Marshal and store the SID string
                        secId.sid = Marshal.PtrToStringAnsi(sidString);
                    }
                    finally
                    {
                        if (sidString != IntPtr.Zero) Util.LocalFree(sidString);
                    }

                    // Check if the SID is a well known SID
                    secId.wellKnownSidType = (WELL_KNOWN_SID_TYPE)Array.IndexOf<string>(SecurityIdentity.wellKnownSids, secId.sid);

                    SID_NAME_USE nameUse;

                    uint nameLen = 0;
                    uint domainLen = 0;

                    // Get the lengths for the object and domain names
                    SecurityIdentity.LookupAccountSid(null, sidStruct, IntPtr.Zero, ref nameLen, IntPtr.Zero, ref domainLen, out nameUse);

                    if (nameLen == 0)
                    {
                        throw new ExternalException("Unable to Find SID: " + Util.GetErrorMessage(Util.GetLastError()));
                    }

                    IntPtr accountName = Marshal.AllocHGlobal((IntPtr)nameLen);
                    IntPtr domainName = domainLen > 0 ? Marshal.AllocHGlobal((IntPtr)domainLen) : IntPtr.Zero;

                    try
                    {
                        // Get the object and domain names
                        if (!SecurityIdentity.LookupAccountSid(null, sidStruct, accountName, ref nameLen, domainName, ref domainLen, out nameUse))
                        {
                            throw new ExternalException("Unable to Find SID: " + Util.GetErrorMessage(Util.GetLastError()));
                        }

                        // Marshal and store the object name
                        secId.name = String.Format("{0}{1}{2}", domainLen > 1 ? Marshal.PtrToStringAnsi(domainName) : "", domainLen > 1 ? "\\" : "", Marshal.PtrToStringAnsi(accountName));

                        return secId;
                    }
                    finally
                    {
                        if (accountName != IntPtr.Zero) Marshal.FreeHGlobal(accountName);
                        if (domainName != IntPtr.Zero) Marshal.FreeHGlobal(domainName);
                    }
                }
                finally
                {
                    if (domains != IntPtr.Zero) SecurityIdentity.LsaFreeMemory(domains);
                    if (sids != IntPtr.Zero) SecurityIdentity.LsaFreeMemory(sids);
                }
            }
            finally
            {
                if (handle != IntPtr.Zero) SecurityIdentity.LsaClose(handle);
            }
        }

        /// <summary>
        /// Creates a Security Identity for a well known SID (such as LOCAL SYSTEM)
        /// </summary>
        /// <param name="sidType">The type of well known SID</param>
        /// <returns>A populated Security Identity</returns>
        public static SecurityIdentity SecurityIdentityFromWellKnownSid(WELL_KNOWN_SID_TYPE sidType)
        {
            if (sidType == WELL_KNOWN_SID_TYPE.None)
            {
                throw new ExternalException("Unable to Get Well Known SID");
            }

            SecurityIdentity secId = new SecurityIdentity();

            secId.wellKnownSidType = sidType;

            // Get the size required for the SID
            uint size = SecurityIdentity.GetSidLengthRequired(SecurityIdentity.SID_MAX_SUB_AUTHORITIES); ;

            IntPtr sidStruct = Marshal.AllocHGlobal((IntPtr)size);

            try
            {
                // Get the SID struct from the well known SID type
                if (!SecurityIdentity.CreateWellKnownSid(sidType, IntPtr.Zero, sidStruct, ref size))
                {
                    throw new ExternalException("Unable to Get Well Known SID");
                }

                IntPtr sidString = IntPtr.Zero;

                // Convert the SID structure to a SID string
                SecurityIdentity.ConvertSidToStringSid(sidStruct, out sidString);

                try
                {
                    // Marshal and store the SID string
                    secId.sid = Marshal.PtrToStringAnsi(sidString);
                }
                finally
                {
                    if (sidString != IntPtr.Zero) Util.LocalFree(sidString);
                }

                uint nameLen = 0;
                uint domainLen = 0;

                SID_NAME_USE nameUse;

                // Get the lengths of the object and domain names
                SecurityIdentity.LookupAccountSid(null, sidStruct, IntPtr.Zero, ref nameLen, IntPtr.Zero, ref domainLen, out nameUse);

                if (nameLen == 0)
                {
                    throw new ExternalException("Unable to Find SID");
                }

                IntPtr accountName = Marshal.AllocHGlobal((IntPtr)nameLen);
                IntPtr domainName = domainLen > 0 ? Marshal.AllocHGlobal((IntPtr)domainLen) : IntPtr.Zero;

                try
                {
                    // Get the object and domain names
                    if (!SecurityIdentity.LookupAccountSid(null, sidStruct, accountName, ref nameLen, domainName, ref domainLen, out nameUse))
                    {
                        throw new ExternalException("Unable to Find SID");
                    }

                    // Marshal and store the object name
                    secId.name = String.Format("{0}{1}{2}", domainLen > 1 ? Marshal.PtrToStringAnsi(domainName) : "", domainLen > 1 ? "\\" : "", Marshal.PtrToStringAnsi(accountName));
                }
                finally
                {
                    if (accountName != IntPtr.Zero) Marshal.FreeHGlobal(accountName);
                    if (domainName != IntPtr.Zero) Marshal.FreeHGlobal(domainName);
                }
            }
            finally
            {
                if (sidStruct != IntPtr.Zero) Marshal.FreeHGlobal(sidStruct);
            }

            return secId;
        }

        private const ushort SID_MAX_SUB_AUTHORITIES = 15;

        /*
         * BOOL ConvertStringSidToSid(
         *     LPCTSTR StringSid,
         *     PSID* Sid
         * );
         */
        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern bool ConvertStringSidToSid([MarshalAs(UnmanagedType.LPWStr)]string StringSid,
                                                          out IntPtr Sid);

        /*
         * BOOL ConvertSidToStringSid(
         *     PSID Sid,
         *     LPTSTR* StringSid
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern bool ConvertSidToStringSid(IntPtr Sid, out IntPtr StringSid);

        /*
         * DWORD GetSidLengthRequired(
         *     UCHAR nSubAuthorityCount
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern uint GetSidLengthRequired(ushort nSubAuthorityCount);

        /*
         * BOOL CreateWellKnownSid(
         *     WELL_KNOWN_SID_TYPE WellKnownSidType,
         *     PSID DomainSid,
         *     PSID pSid,
         *     DWORD* cbSid
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern bool CreateWellKnownSid(WELL_KNOWN_SID_TYPE WellKnownSidType, IntPtr DomainSid, IntPtr pSid, ref uint cbSid);

        /*
         * NTSTATUS LsaOpenPolicy(
         *     PLSA_UNICODE_STRING SystemName,
         *     PLSA_OBJECT_ATTRIBUTES ObjectAttributes,
         *     ACCESS_MASK DesiredAccess,
         *     PLSA_HANDLE PolicyHandle
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern int LsaOpenPolicy(IntPtr SystemName,
                                                ref LSA_OBJECT_ATTRIBUTES ObjectAttributes,
                                                ACCESS_MASK DesiredAccess,
                                                out IntPtr PolicyHandle);

        /*
         * NTSTATUS LsaClose(
         *     LSA_HANDLE ObjectHandle
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern int LsaClose(IntPtr ObjectHandle);

        /*
         * NTSTATUS LsaLookupNames2(
         *     LSA_HANDLE PolicyHandle,
         *     ULONG Flags,
         *     ULONG Count,
         *     PLSA_UNICODE_STRING Names,
         *     PLSA_REFERENCED_DOMAIN_LIST* ReferencedDomains,
         *     PLSA_TRANSLATED_SID2* Sids
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern int LsaLookupNames2(IntPtr PolicyHandle,
                                                   uint Flags,
                                                   uint Count,
                                                   LSA_UNICODE_STRING[] Names,
                                                   out IntPtr ReferencedDomains,
                                                   out IntPtr Sids);

        /*
         * ULONG LsaNtStatusToWinError(
         *     NTSTATUS Status
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern uint LsaNtStatusToWinError(int Status);


        /*
         * NTSTATUS LsaFreeMemory(
         *     PVOID Buffer
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern int LsaFreeMemory(IntPtr Buffer);

        /*
         * BOOL LookupAccountSid(
         *     LPCTSTR lpSystemName,
         *     PSID lpSid,
         *     LPTSTR lpName,
         *     LPDWORD cchName,
         *     LPTSTR lpReferencedDomainName,
         *     LPDWORD cchReferencedDomainName,
         *     PSID_NAME_USE peUse
         * );
         */
        [DllImport("Advapi32.dll")]
        private static extern bool LookupAccountSid([MarshalAs(UnmanagedType.LPTStr)]string lpSystemName,
                                                     IntPtr lpSid,
                                                     IntPtr lpName,
                                                     ref uint cchName,
                                                     IntPtr lpReferencedDomainName,
                                                     ref uint cchReferencedDomainName,
                                                     out SID_NAME_USE peUse);
    }

    /*
     * typedef enum {
     *     WinNullSid                                  = 0,
     *     WinWorldSid                                 = 1,
     *     WinLocalSid                                 = 2,
     *     WinCreatorOwnerSid                          = 3,
     *     WinCreatorGroupSid                          = 4,
     *     WinCreatorOwnerServerSid                    = 5,
     *     WinCreatorGroupServerSid                    = 6,
     *     WinNtAuthoritySid                           = 7,
     *     WinDialupSid                                = 8,
     *     WinNetworkSid                               = 9,
     *     WinBatchSid                                 = 10,
     *     WinInteractiveSid                           = 11,
     *     WinServiceSid                               = 12,
     *     WinAnonymousSid                             = 13,
     *     WinProxySid                                 = 14,
     *     WinEnterpriseControllersSid                 = 15,
     *     WinSelfSid                                  = 16,
     *     WinAuthenticatedUserSid                     = 17,
     *     WinRestrictedCodeSid                        = 18,
     *     WinTerminalServerSid                        = 19,
     *     WinRemoteLogonIdSid                         = 20,
     *     WinLogonIdsSid                              = 21,
     *     WinLocalSystemSid                           = 22,
     *     WinLocalServiceSid                          = 23,
     *     WinNetworkServiceSid                        = 24,
     *     WinBuiltinDomainSid                         = 25,
     *     WinBuiltinAdministratorsSid                 = 26,
     *     WinBuiltinUsersSid                          = 27,
     *     WinBuiltinGuestsSid                         = 28,
     *     WinBuiltinPowerUsersSid                     = 29,
     *     WinBuiltinAccountOperatorsSid               = 30,
     *     WinBuiltinSystemOperatorsSid                = 31,
     *     WinBuiltinPrintOperatorsSid                 = 32,
     *     WinBuiltinBackupOperatorsSid                = 33,
     *     WinBuiltinReplicatorSid                     = 34,
     *     WinBuiltinPreWindows2000CompatibleAccessSid = 35,
     *     WinBuiltinRemoteDesktopUsersSid             = 36,
     *     WinBuiltinNetworkConfigurationOperatorsSid  = 37,
     *     WinAccountAdministratorSid                  = 38,
     *     WinAccountGuestSid                          = 39,
     *     WinAccountKrbtgtSid                         = 40,
     *     WinAccountDomainAdminsSid                   = 41,
     *     WinAccountDomainUsersSid                    = 42,
     *     WinAccountDomainGuestsSid                   = 43,
     *     WinAccountComputersSid                      = 44,
     *     WinAccountControllersSid                    = 45,
     *     WinAccountCertAdminsSid                     = 46,
     *     WinAccountSchemaAdminsSid                   = 47,
     *     WinAccountEnterpriseAdminsSid               = 48,
     *     WinAccountPolicyAdminsSid                   = 49,
     *     WinAccountRasAndIasServersSid               = 50,
     *     WinNTLMAuthenticationSid                    = 51,
     *     WinDigestAuthenticationSid                  = 52,
     *     WinSChannelAuthenticationSid                = 53,
     *     WinThisOrganizationSid                      = 54,
     *     WinOtherOrganizationSid                     = 55,
     *     WinBuiltinIncomingForestTrustBuildersSid    = 56,
     *     WinBuiltinPerfMonitoringUsersSid            = 57,
     *     WinBuiltinPerfLoggingUsersSid               = 58,
     *     WinBuiltinAuthorizationAccessSid            = 59,
     *     WinBuiltinTerminalServerLicenseServersSid   = 60,
     *     WinBuiltinDCOMUsersSid                      = 61,
     *     WinBuiltinIUsersSid                         = 62,
     *     WinIUserSid                                 = 63,
     *     WinBuiltinCryptoOperatorsSid                = 64,
     *     WinUntrustedLabelSid                        = 65,
     *     WinLowLabelSid                              = 66,
     *     WinMediumLabelSid                           = 67,
     *     WinHighLabelSid                             = 68,
     *     WinSystemLabelSid                           = 69,
     *     WinWriteRestrictedCodeSid                   = 70,
     *     WinCreatorOwnerRightsSid                    = 71,
     *     WinCacheablePrincipalsGroupSid              = 72,
     *     WinNonCacheablePrincipalsGroupSid           = 73,
     *     WinEnterpriseReadonlyControllersSid         = 74,
     *     WinAccountReadonlyControllersSid            = 75,
     *     WinBuiltinEventLogReadersGroup              = 76,
     * } WELL_KNOWN_SID_TYPE;
     */
    public enum WELL_KNOWN_SID_TYPE
    {
        None = -1,
        WinNullSid = 0,
        WinWorldSid = 1,
        WinLocalSid = 2,
        WinCreatorOwnerSid = 3,
        WinCreatorGroupSid = 4,
        WinCreatorOwnerServerSid = 5,
        WinCreatorGroupServerSid = 6,
        WinNtAuthoritySid = 7,
        WinDialupSid = 8,
        WinNetworkSid = 9,
        WinBatchSid = 10,
        WinInteractiveSid = 11,
        WinServiceSid = 12,
        WinAnonymousSid = 13,
        WinProxySid = 14,
        WinEnterpriseControllersSid = 15,
        WinSelfSid = 16,
        WinAuthenticatedUserSid = 17,
        WinRestrictedCodeSid = 18,
        WinTerminalServerSid = 19,
        WinRemoteLogonIdSid = 20,
        WinLogonIdsSid = 21,
        WinLocalSystemSid = 22,
        WinLocalServiceSid = 23,
        WinNetworkServiceSid = 24,
        WinBuiltinDomainSid = 25,
        WinBuiltinAdministratorsSid = 26,
        WinBuiltinUsersSid = 27,
        WinBuiltinGuestsSid = 28,
        WinBuiltinPowerUsersSid = 29,
        WinBuiltinAccountOperatorsSid = 30,
        WinBuiltinSystemOperatorsSid = 31,
        WinBuiltinPrintOperatorsSid = 32,
        WinBuiltinBackupOperatorsSid = 33,
        WinBuiltinReplicatorSid = 34,
        WinBuiltinPreWindows2000CompatibleAccessSid = 35,
        WinBuiltinRemoteDesktopUsersSid = 36,
        WinBuiltinNetworkConfigurationOperatorsSid = 37,
        WinAccountAdministratorSid = 38,
        WinAccountGuestSid = 39,
        WinAccountKrbtgtSid = 40,
        WinAccountDomainAdminsSid = 41,
        WinAccountDomainUsersSid = 42,
        WinAccountDomainGuestsSid = 43,
        WinAccountComputersSid = 44,
        WinAccountControllersSid = 45,
        WinAccountCertAdminsSid = 46,
        WinAccountSchemaAdminsSid = 47,
        WinAccountEnterpriseAdminsSid = 48,
        WinAccountPolicyAdminsSid = 49,
        WinAccountRasAndIasServersSid = 50,
        WinNTLMAuthenticationSid = 51,
        WinDigestAuthenticationSid = 52,
        WinSChannelAuthenticationSid = 53,
        WinThisOrganizationSid = 54,
        WinOtherOrganizationSid = 55,
        WinBuiltinIncomingForestTrustBuildersSid = 56,
        WinBuiltinPerfMonitoringUsersSid = 57,
        WinBuiltinPerfLoggingUsersSid = 58,
        WinBuiltinAuthorizationAccessSid = 59,
        WinBuiltinTerminalServerLicenseServersSid = 60,
        WinBuiltinDCOMUsersSid = 61,
        WinBuiltinIUsersSid = 62,
        WinIUserSid = 63,
        WinBuiltinCryptoOperatorsSid = 64,
        WinUntrustedLabelSid = 65,
        WinLowLabelSid = 66,
        WinMediumLabelSid = 67,
        WinHighLabelSid = 68,
        WinSystemLabelSid = 69,
        WinWriteRestrictedCodeSid = 70,
        WinCreatorOwnerRightsSid = 71,
        WinCacheablePrincipalsGroupSid = 72,
        WinNonCacheablePrincipalsGroupSid = 73,
        WinEnterpriseReadonlyControllersSid = 74,
        WinAccountReadonlyControllersSid = 75,
        WinBuiltinEventLogReadersGroup = 76
    }

    /*
     * typedef enum _SID_NAME_USE
     * {
     *     SidTypeUser = 1,
     *     SidTypeGroup,
     *     SidTypeDomain,
     *     SidTypeAlias,
     *     SidTypeWellKnownGroup,
     *     SidTypeDeletedAccount,
     *     SidTypeInvalid,
     *     SidTypeUnknown,
     *     SidTypeComputer
     * } SID_NAME_USE,  *PSID_NAME_USE;
     */
    internal enum SID_NAME_USE
    {
        SidTypeUser = 1,
        SidTypeGroup,
        SidTypeDomain,
        SidTypeAlias,
        SidTypeWellKnownGroup,
        SidTypeDeletedAccount,
        SidTypeInvalid,
        SidTypeUnknown,
        SidTypeComputer
    }

    /*
     * typedef struct _LSA_UNICODE_STRING {
     *     USHORT Length;
     *     USHORT MaximumLength;
     *     PWSTR Buffer;
     * } LSA_UNICODE_STRING,  *PLSA_UNICODE_STRING;
     */
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    internal struct LSA_UNICODE_STRING
    {
        public ushort Length;
        public ushort MaxLength;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Buffer;
    }

    /*
     * typedef struct _LSA_REFERENCED_DOMAIN_LIST {
     *     ULONG Entries;
     *     PLSA_TRUST_INFORMATION Domains;
     * } LSA_REFERENCED_DOMAIN_LIST,  *PLSA_REFERENCED_DOMAIN_LIST;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct LSA_REFERENCED_DOMAIN_LIST
    {
        public uint Entries;
        public IntPtr Domains;
    }
    
    /*
     * typedef struct _LSA_TRUST_INFORMATION {
     *     LSA_UNICODE_STRING Name;
     *     PSID Sid;
     * } LSA_TRUST_INFORMATION,  *PLSA_TRUST_INFORMATION;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct LSA_TRUST_INFORMATION
    {
        public LSA_UNICODE_STRING Name;
        public IntPtr Sid;
    }

    /*
     * typedef struct _LSA_TRANSLATED_SID2 {
     *     SID_NAME_USE Use;
     *     PSID Sid;
     *     LONG DomainIndex;
     *     ULONG Flags;
     * } LSA_TRANSLATED_SID2,  *PLSA_TRANSLATED_SID2;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct LSA_TRANSLATED_SID2
    {
        public SID_NAME_USE Use;
        public IntPtr Sid;
        public int DomainIndex;
        public uint Flags;
    }

    /*
     * typedef struct _LSA_OBJECT_ATTRIBUTES {
     *     ULONG Length;
     *     HANDLE RootDirectory;
     *     PLSA_UNICODE_STRING ObjectName;
     *     ULONG Attributes;
     *     PVOID SecurityDescriptor;
     *     PVOID SecurityQualityOfService;
     * } LSA_OBJECT_ATTRIBUTES,  *PLSA_OBJECT_ATTRIBUTES;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct LSA_OBJECT_ATTRIBUTES
    {
        public uint Length;
        public IntPtr RootDirectory;
        public IntPtr ObjectName;
        public uint Attributes;
        public IntPtr SecurityDescriptor;
        public IntPtr SecurityQualityOfService;
    }

    [Flags]
    internal enum ACCESS_MASK
    {
        POLICY_VIEW_LOCAL_INFORMATION = 0x0001,
        POLICY_VIEW_AUDIT_INFORMATION = 0x0002,
        POLICY_GET_PRIVATE_INFORMATION = 0x0004,
        POLICY_TRUST_ADMIN = 0x0008,
        POLICY_CREATE_ACCOUNT = 0x0010,
        POLICY_CREATE_SECRET = 0x0020,
        POLICY_CREATE_PRIVILEGE = 0x0040,
        POLICY_SET_DEFAULT_QUOTA_LIMITS = 0x0080,
        POLICY_SET_AUDIT_REQUIREMENTS = 0x0100,
        POLICY_AUDIT_LOG_ADMIN = 0x0200,
        POLICY_SERVER_ADMIN = 0x0400,
        POLICY_LOOKUP_NAMES = 0x0800
    }
}
