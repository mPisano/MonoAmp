using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using HttpNamespaceManager.Lib.AccessControl;

namespace HttpNamespaceManager.Lib
{
    public class HttpApi : IDisposable
    {
        public HttpApi()
        {
            HTTPAPI_VERSION version = new HTTPAPI_VERSION();
            version.HttpApiMajorVersion = 1;
            version.HttpApiMinorVersion = 0;

            HttpApi.HttpInitialize(version, HttpApi.HTTP_INITIALIZE_CONFIG, IntPtr.Zero);
        }

        ~HttpApi()
        {
            this.Dispose(false);
        }

        protected void Dispose(bool p)
        {
            HttpApi.HttpTerminate(HttpApi.HTTP_INITIALIZE_CONFIG, IntPtr.Zero);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        public Dictionary<string, SecurityDescriptor> QueryHttpNamespaceAcls()
        {
            Dictionary<string, SecurityDescriptor> nsTable = new Dictionary<string, SecurityDescriptor>();

            HTTP_SERVICE_CONFIG_URLACL_QUERY query = new HTTP_SERVICE_CONFIG_URLACL_QUERY();
            query.QueryDesc = HTTP_SERVICE_CONFIG_QUERY_TYPE.HttpServiceConfigQueryNext;

            IntPtr pQuery = Marshal.AllocHGlobal(Marshal.SizeOf(query));

            try
            {
                uint retval = NO_ERROR;
                for (query.dwToken = 0; true; query.dwToken++)
                {
                    Marshal.StructureToPtr(query, pQuery, false);

                    try
                    {
                        uint returnSize = 0;

                        // Get Size
                        retval = HttpQueryServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pQuery, (uint) Marshal.SizeOf(query), IntPtr.Zero, 0, ref returnSize, IntPtr.Zero);

                        if (retval == ERROR_NO_MORE_ITEMS)
                        {
                            break;
                        }
                        if (retval != ERROR_INSUFFICIENT_BUFFER)
                        {
                            throw new Exception("HttpQueryServiceConfiguration returned unexpected error code.");
                        }

                        IntPtr pConfig = Marshal.AllocHGlobal((IntPtr)returnSize);

                        try
                        {
                            retval = HttpApi.HttpQueryServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pQuery, (uint)Marshal.SizeOf(query), pConfig, returnSize, ref returnSize, IntPtr.Zero);

                            if (retval == NO_ERROR)
                            {
                                HTTP_SERVICE_CONFIG_URLACL_SET config = (HTTP_SERVICE_CONFIG_URLACL_SET)Marshal.PtrToStructure(pConfig, typeof(HTTP_SERVICE_CONFIG_URLACL_SET));

                                nsTable.Add(config.KeyDesc.pUrlPrefix, SecurityDescriptor.SecurityDescriptorFromString(config.ParamDesc.pStringSecurityDescriptor));
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(pConfig);
                        }
                    }
                    finally
                    {
                        Marshal.DestroyStructure(pQuery, typeof(HTTP_SERVICE_CONFIG_URLACL_QUERY));
                    }
                }

                if (retval != ERROR_NO_MORE_ITEMS)
                {
                    throw new Exception("HttpQueryServiceConfiguration returned unexpected error code.");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pQuery);
            }

            return nsTable;
        }

        public void SetHttpNamespaceAcl(string urlPrefix, SecurityDescriptor acl)
        {
            HTTP_SERVICE_CONFIG_URLACL_SET urlAclConfig = new HTTP_SERVICE_CONFIG_URLACL_SET();
            urlAclConfig.KeyDesc.pUrlPrefix = urlPrefix;
            urlAclConfig.ParamDesc.pStringSecurityDescriptor = acl.ToString();

            IntPtr pUrlAclConfig = Marshal.AllocHGlobal(Marshal.SizeOf(urlAclConfig));

            Marshal.StructureToPtr(urlAclConfig, pUrlAclConfig, false);

            try
            {
                uint retval = HttpApi.HttpSetServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pUrlAclConfig, (uint)Marshal.SizeOf(urlAclConfig), IntPtr.Zero);

                if (retval != 0)
                {
                    throw new ExternalException("Error Setting Configuration: " + Util.GetErrorMessage(retval));
                }
            }
            finally
            {
                if (pUrlAclConfig != IntPtr.Zero)
                {
                    Marshal.DestroyStructure(pUrlAclConfig, typeof(HTTP_SERVICE_CONFIG_URLACL_SET));
                    Marshal.FreeHGlobal(pUrlAclConfig); ;
                }
            }
        }

        public void RemoveHttpHamespaceAcl(string urlPrefix)
        {
            HTTP_SERVICE_CONFIG_URLACL_SET urlAclConfig = new HTTP_SERVICE_CONFIG_URLACL_SET();
            urlAclConfig.KeyDesc.pUrlPrefix = urlPrefix;
            //urlAclConfig.ParamDesc.pStringSecurityDescriptor = acl.ToString();

            IntPtr pUrlAclConfig = Marshal.AllocHGlobal(Marshal.SizeOf(urlAclConfig));

            Marshal.StructureToPtr(urlAclConfig, pUrlAclConfig, false);

            try
            {
                uint retval = HttpApi.HttpDeleteServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pUrlAclConfig, (uint)Marshal.SizeOf(urlAclConfig), IntPtr.Zero);

                if (retval != 0)
                {
                    throw new ExternalException("Error Setting Configuration: " + Util.GetErrorMessage(retval));
                }
            }
            finally
            {
                if (pUrlAclConfig != IntPtr.Zero)
                {
                    Marshal.DestroyStructure(pUrlAclConfig, typeof(HTTP_SERVICE_CONFIG_URLACL_SET));
                    Marshal.FreeHGlobal(pUrlAclConfig); ;
                }
            }
        }

        internal const uint ERROR_NO_MORE_ITEMS = 259;
        internal const uint ERROR_INSUFFICIENT_BUFFER = 122;
        internal const uint NO_ERROR = 0;
        internal const uint HTTP_INITIALIZE_CONFIG = 2;

        /*
         * ULONG HttpInitialize(
         *     HTTPAPI_VERSION Version,
         *     ULONG Flags,
         *     PVOID pReserved
         * );
         */
        [DllImport("Httpapi.dll")]
        internal static extern uint HttpInitialize(HTTPAPI_VERSION Version, uint Flags, IntPtr pReserved);

        /*
         * ULONG HttpTerminate(
         *     ULONG Flags,
         *     PVOID pReserved
         * );
         */
        [DllImport("Httpapi.dll")]
        internal static extern uint HttpTerminate(uint Flags, IntPtr pReserved);

        /*
         * ULONG HttpSetServiceConfiguration(
         *     HANDLE ServiceHandle,
         *     HTTP_SERVICE_CONFIG_ID ConfigId,
         *     PVOID pConfigInformation,
         *     ULONG ConfigInformationLength,
         *     LPOVERLAPPED pOverlapped
         * );
         */
        [DllImport("Httpapi.dll")]
        internal static extern uint HttpSetServiceConfiguration(IntPtr ServiceHandle, HTTP_SERVICE_CONFIG_ID ConfigId, IntPtr pConfigInformation, uint ConfigInformationLength, IntPtr pOverlapped);

        /*
         * ULONG HttpQueryServiceConfiguration(
         *     HANDLE ServiceHandle,
         *     HTTP_SERVICE_CONFIG_ID ConfigId,
         *     PVOID pInputConfigInfo,
         *     ULONG InputConfigInfoLength,
         *     PVOID pOutputConfigInfo,
         *     ULONG OutputConfigInfoLength,
         *     PULONG pReturnLength,
         *     LPOVERLAPPED pOverlapped
         * );
         */
        [DllImport("Httpapi.dll")]
        internal static extern uint HttpQueryServiceConfiguration(IntPtr ServiceHandle, HTTP_SERVICE_CONFIG_ID ConfigId, IntPtr pInputConfigInfo, uint InputConfigLength, IntPtr pOutputConfigInfo, uint OutputConfigInfoLength, ref uint pReturnLength, IntPtr pOverlapped);

        /*
         * ULONG HttpDeleteServiceConfiguration(
         *     HANDLE ServiceHandle,
         *     HTTP_SERVICE_CONFIG_ID ConfigId,
         *     PVOID pConfigInformation,
         *     ULONG ConfigInformationLength,
         *     LPOVERLAPPED pOverlapped
         * );
         */
        [DllImport("Httpapi.dll")]
        internal static extern uint HttpDeleteServiceConfiguration(IntPtr ServiceHandle, HTTP_SERVICE_CONFIG_ID ConfigId, IntPtr pConfigInformation, uint ConfigInformationLength, IntPtr pOverlapped);
    }

    /*
     * typedef struct _HTTPAPI_VERSION
     * {
     *     USHORT HttpApiMajorVersion;
     *     USHORT HttpApiMinorVersion;
     * } HTTPAPI_VERSION,  *PHTTPAPI_VERSION;
     */
    internal struct HTTPAPI_VERSION
    {
        public ushort HttpApiMajorVersion;
        public ushort HttpApiMinorVersion;
    }

    /*
     * typedef enum _HTTP_SERVICE_CONFIG_ID
     * {
     *     HttpServiceConfigIPListenList,
     *     HttpServiceConfigSSLCertInfo,
     *     HttpServiceConfigUrlAclInfo,
     *     HttpServiceConfigTimeout,
     *     HttpServiceConfigMax
     * }HTTP_SERVICE_CONFIG_ID,  *PHTTP_SERVICE_CONFIG_ID;
     */
    internal enum HTTP_SERVICE_CONFIG_ID
    {
        HttpServiceConfigIPListenList,
        HttpServiceConfigSSLCertInfo,
        HttpServiceConfigUrlAclInfo,
        HttpServiceConfigTimeout,
        HttpServiceConfigMax
    }

    /*
     * typedef struct _HTTP_SERVICE_CONFIG_URLACL_QUERY {
     *     HTTP_SERVICE_CONFIG_QUERY_TYPE QueryDesc;
     *     HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;
     *     DWORD dwToken;
     * } HTTP_SERVICE_CONFIG_URLACL_QUERY,  *PHTTP_SERVICE_CONFIG_URLACL_QUERY;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct HTTP_SERVICE_CONFIG_URLACL_QUERY
    {
        public HTTP_SERVICE_CONFIG_QUERY_TYPE QueryDesc;
        public HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;
        public uint dwToken;
    }

    /*
     * typedef enum _HTTP_SERVICE_CONFIG_QUERY_TYPE
     * {
     *     HttpServiceConfigQueryExact,
     *     HttpServiceConfigQueryNext,
     *     HttpServiceConfigQueryMax
     * } HTTP_SERVICE_CONFIG_QUERY_TYPE,  *PHTTP_SERVICE_CONFIG_QUERY_TYPE;
     */
    internal enum HTTP_SERVICE_CONFIG_QUERY_TYPE
    {
        HttpServiceConfigQueryExact,
        HttpServiceConfigQueryNext,
        HttpServiceConfigQueryMax
    }

    /*
     * typedef struct _HTTP_SERVICE_CONFIG_URLACL_KEY
     * {
     *     PWSTR pUrlPrefix;
     * } HTTP_SERVICE_CONFIG_URLACL_KEY, *PHTTP_SERVICE_CONFIG_URLACL_KEY;
     */
    internal struct HTTP_SERVICE_CONFIG_URLACL_KEY
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pUrlPrefix;
    }

    /*
     * typedef struct _HTTP_SERVICE_CONFIG_URLACL_SET
     * {
     *     HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;
     *     HTTP_SERVICE_CONFIG_URLACL_PARAM ParamDesc;
     * } HTTP_SERVICE_CONFIG_URLACL_SET,  *PHTTP_SERVICE_CONFIG_URLACL_SET;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal struct HTTP_SERVICE_CONFIG_URLACL_SET
    {
        public HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;
        public HTTP_SERVICE_CONFIG_URLACL_PARAM ParamDesc;
    }

    /*
     * typedef struct _HTTP_SERVICE_CONFIG_URLACL_PARAM
     * {
     *     PWSTR pStringSecurityDescriptor;
     * } HTTP_SERVICE_CONFIG_URLACL_PARAM,  *PHTTP_SERVICE_CONFIG_URLACL_PARAM;
     */
    internal struct HTTP_SERVICE_CONFIG_URLACL_PARAM
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pStringSecurityDescriptor;
    }
}
