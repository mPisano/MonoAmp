Imports System.IO
Imports System.Runtime.Serialization.Formatters
Imports HomeSeerAPI

Module utils
    Public plugin As New HSPI
    Public IFACE_NAME As String = "Monoprice Amp"
    Public callback As HomeSeerAPI.IAppCallbackAPI
    Public hs As HomeSeerAPI.IHSApplication
    Public Instance As String = ""
    Public InterfaceVersion As Integer
    Public bShutDown As Boolean = False
    Public gEXEPath As String = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)
    Public Const INIFILE As String = "MonopriceAmp.ini"
    Public CurrentPage As Object

    Public Structure pair
        Dim name As String
        Dim value As String
    End Structure

    Enum LogLevel As Integer
        Normal = 1
        Debug = 2
    End Enum

    Enum MessageType
        Normal = 0
        Warning = 1
        Error_ = 2
    End Enum

    'Public Enum DEVICE_COMMAND
    '    All_Zones_Off = 0
    '    All_Zones_On = 1
    '    All_Zones_Mute = 2
    '    All_Zones_UnMute = 3
    '    Zone_On = 4
    '    Zone_Off = 5
    '    Zone_Mute = 6
    '    Zone_UnMute = 7

    'End Enum

    Sub PEDAdd(ByRef PED As clsPlugExtraData, ByVal PEDName As String, ByVal PEDValue As Object)
        Dim ByteObject() As Byte = Nothing
        If PED Is Nothing Then PED = New clsPlugExtraData
        SerializeObject(PEDValue, ByteObject)
        If Not PED.AddNamed(PEDName, ByteObject) Then
            PED.RemoveNamed(PEDName)
            PED.AddNamed(PEDName, ByteObject)
        End If
    End Sub

    Function PEDGet(ByRef PED As clsPlugExtraData, ByVal PEDName As String) As Object
        Dim ByteObject() As Byte
        Dim ReturnValue As New Object
        ByteObject = PED.GetNamed(PEDName)
        If ByteObject Is Nothing Then Return Nothing
        DeSerializeObject(ByteObject, ReturnValue)
        Return ReturnValue
    End Function

    Public Function SerializeObject(ByRef ObjIn As Object, ByRef bteOut() As Byte) As Boolean
        If ObjIn Is Nothing Then Return False
        Dim str As New MemoryStream
        Dim sf As New Binary.BinaryFormatter

        Try
            sf.Serialize(str, ObjIn)
            ReDim bteOut(CInt(str.Length - 1))
            bteOut = str.ToArray
            Return True
        Catch ex As Exception
            Log(LogLevel.Debug, IFACE_NAME & " Error: Serializing object " & ObjIn.ToString & " :" & ex.Message)
            Return False
        End Try

    End Function

    Public Function DeSerializeObject(ByRef bteIn() As Byte, ByRef ObjOut As Object) As Boolean
        ' Almost immediately there is a test to see if ObjOut is NOTHING.  The reason for this
        '   when the ObjOut is suppose to be where the deserialized object is stored, is that 
        '   I could find no way to test to see if the deserialized object and the variable to 
        '   hold it was of the same type.  If you try to get the type of a null object, you get
        '   only a null reference exception!  If I do not test the object type beforehand and 
        '   there is a difference, then the InvalidCastException is thrown back in the CALLING
        '   procedure, not here, because the cast is made when the ByRef object is cast when this
        '   procedure returns, not earlier.  In order to prevent a cast exception in the calling
        '   procedure that may or may not be handled, I made it so that you have to at least 
        '   provide an initialized ObjOut when you call this - ObjOut is set to nothing after it 
        '   is typed.
        If bteIn Is Nothing Then Return False
        If bteIn.Length < 1 Then Return False
        If ObjOut Is Nothing Then Return False
        Dim str As MemoryStream
        Dim sf As New Binary.BinaryFormatter
        Dim ObjTest As Object
        Dim TType As System.Type
        Dim OType As System.Type
        Try
            OType = ObjOut.GetType
            ObjOut = Nothing
            str = New MemoryStream(bteIn)
            ObjTest = sf.Deserialize(str)
            If ObjTest Is Nothing Then Return False
            TType = ObjTest.GetType
            'If Not TType.Equals(OType) Then Return False
            ObjOut = ObjTest
            If ObjOut Is Nothing Then Return False
            Return True
        Catch exIC As InvalidCastException
            Return False
        Catch ex As Exception
            Log(LogLevel.Debug, IFACE_NAME & " Error: DeSerializing object: " & ex.Message)
            Return False
        End Try

    End Function

    Private Sub DeleteDevices()
        Dim en As Object
        Dim dv As Object

        Try
            en = hs.GetDeviceEnumerator
            Do While Not en.Finished
                dv = en.GetNext
                If dv IsNot Nothing Then
                    If dv.interface = IFACE_NAME Then
                        Try
                            hs.DeleteDevice(dv.ref)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Loop
            hs.SaveEventsDevices()
        Catch ex As Exception
        End Try
    End Sub

    Sub DeleteModule(ByVal n As Integer)
        Dim i As Integer
        Log("Module to Delete is " & n)
        For i = 1 To 16
            hs.DeleteDevice(hs.GetINISetting("Module " & n, "ref-" & i.ToString, "", INIFILE))
        Next

        hs.ClearINISection("Module " & n.ToString, INIFILE)

        Log("Finished deleting module.")
    End Sub

    Function InitDevice(ByVal PName As String, ByVal modNum As Integer, ByVal counter As Integer, Optional ByVal ref As Integer = 0) As Boolean
        Dim dv As Scheduler.Classes.DeviceClass = Nothing
        Log("Initiating Device " & PName, LogLevel.Debug)

        Try
            If Not hs.DeviceExistsRef(ref) Then
                ref = hs.NewDeviceRef(PName)

                hs.SaveINISetting("Module " & modNum, "ref-" & counter.ToString, ref, INIFILE)
                Try
                    dv = hs.GetDeviceByRef(ref)
                    InitHSDevice(dv, PName)
                    Return True
                Catch ex As Exception
                    Log("Error initializing device " & PName & ": " & ex.Message)
                    Return False
                End Try
            End If
        Catch ex As Exception
            Log("Error getting RefID from DeviceCode within InitDevice. " & ex.Message)
        End Try
        Return False
    End Function

    Sub InitHSDevice(ByRef dv As Scheduler.Classes.DeviceClass, Optional ByVal Name As String = "Sample")
        Dim test As Object = Nothing
        Log(LogLevel.Normal, IFACE_NAME & " InitHSDevice:" & Name & " :" & dv.Address(hs))
        dv.Address(hs) = "HOME"
        Dim DT As New DeviceTypeInfo
        DT.Device_Type = DeviceTypeInfo.eDeviceAPI.Plug_In
        dv.DeviceType_Set(hs) = DT
        dv.Interface(hs) = IFACE_NAME
        dv.InterfaceInstance(hs) = Instance
        dv.Last_Change(hs) = Now
        dv.Name(hs) = Name
        dv.Location(hs) = "Sample"
        dv.Device_Type_String(hs) = "Sample"
        dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
        dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
        dv.Status_Support(hs) = False
    End Sub


    'Dim ref As Long = hs.GetDeviceRefByName("House House Heat")
    'hs.writelog("", "Device Reference: " & ref)
    'Dim Pair As VSPair
    'Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
    'Pair.PairType = VSVGPairType.Range
    'Pair.RangeStart = 5
    'Pair.RangeEnd = 35
    'Pair.RangeStatusPrefix = "Temp "
    'Pair.RangeStatusSuffix = "C"
    'Pair.Render = Enums.CAPIControlType.ValuesRangeSlider
    'hs.DeviceVSP_AddPair(ref, Pair)

    '  Public Sub SendCommand(ByVal Housecode As String, ByVal Devicecode As String, ByVal Action As Integer)
    'Send a command somewhere
    ' End Sub

    Public Sub RegisterCallback(ByRef frm As Object)
        ' call back into HS and get a reference to the HomeSeer ActiveX interface
        ' this can be used make calls back into HS like hs.SetDeviceValue, etc.
        ' The callback object is a different interface reserved for plug-ins.
        callback = frm
        hs = frm.GetHSIface
        If hs Is Nothing Then
            MsgBox("Unable to access HS interface", MsgBoxStyle.Critical)
        Else
            Log("Register callback completed", LogLevel.Debug)
            InterfaceVersion = hs.InterfaceVersion
        End If
    End Sub

    Public Sub RegisterWebPage(ByVal link As String, Optional linktext As String = "", Optional page_title As String = "")
        Try
            hs.RegisterPage(link, IFACE_NAME, Instance)
            If linktext = "" Then linktext = link
            linktext = linktext.Replace("_", " ")
            If page_title = "" Then page_title = linktext
            Dim wpd As New HomeSeerAPI.WebPageDesc
            wpd.plugInName = IFACE_NAME
            wpd.link = link
            wpd.linktext = linktext & Instance
            wpd.page_title = page_title & Instance
            callback.RegisterLink(wpd)
        Catch ex As Exception
            Log(LogLevel.Debug, "Error - Registering Web Links: " & ex.Message)
        End Try
    End Sub


    Public Sub Log(ByVal Message As String, Optional ByVal Log_Level As LogLevel = LogLevel.Normal)
        If Log_Level = LogLevel.Normal Then
            hs.WriteLog(IFACE_NAME, Message)
        End If
        If Log_Level = LogLevel.Debug Then
            If IO.Directory.Exists(gEXEPath & "\Debug Logs") Then
                IO.File.AppendAllText(gEXEPath & "\Debug Logs\CurrentCost.log", Now.ToString & " ~ " & Message & vbCrLf)
            ElseIf IO.Directory.Exists(gEXEPath & "\Logs") Then
                IO.File.AppendAllText(gEXEPath & "\Logs\CurrentCost.log", Now.ToString & " ~ " & Message & vbCrLf)
            Else
                IO.File.AppendAllText(gEXEPath & "\CurrentCost.log", Now.ToString & " ~ " & Message & vbCrLf)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public gGlobalTempScaleF As Boolean = True
    Public MyDevice As Integer = -1




    Private Sub Default_VS_Pairs_AddUpdateUtil(ByVal dvRef As Integer, ByRef Pair As VSPair)
        If Pair Is Nothing Then Exit Sub
        If dvRef < 1 Then Exit Sub
        If Not hs.DeviceExistsRef(dvRef) Then Exit Sub

        Dim Existing As VSPair = Nothing

        ' The purpose of this procedure is to add the protected, default VS/VG pairs WITHOUT overwriting any user added
        '   pairs unless absolutely necessary (because they conflict).

        Try
            Existing = hs.DeviceVSP_Get(dvRef, Pair.Value, Pair.ControlStatus) 'VSPairs.GetPairByValue(Pair.Value, Pair.ControlStatus)

            If Existing IsNot Nothing Then

                ' This is unprotected, so it is a user's value/status pair.
                If Existing.ControlStatus = HomeSeerAPI.ePairStatusControl.Both And Pair.ControlStatus <> HomeSeerAPI.ePairStatusControl.Both Then
                    ' The existing one is for BOTH, so try changing it to the opposite of what we are adding and then add it.
                    If Pair.ControlStatus = HomeSeerAPI.ePairStatusControl.Status Then
                        If Not hs.DeviceVSP_ChangePair(dvRef, Existing, HomeSeerAPI.ePairStatusControl.Control) Then
                            hs.DeviceVSP_ClearBoth(dvRef, Pair.Value)
                            hs.DeviceVSP_AddPair(dvRef, Pair)
                        Else
                            hs.DeviceVSP_AddPair(dvRef, Pair)
                        End If
                    Else
                        If Not hs.DeviceVSP_ChangePair(dvRef, Existing, HomeSeerAPI.ePairStatusControl.Status) Then
                            hs.DeviceVSP_ClearBoth(dvRef, Pair.Value)
                            hs.DeviceVSP_AddPair(dvRef, Pair)
                        Else
                            hs.DeviceVSP_AddPair(dvRef, Pair)
                        End If
                    End If
                ElseIf Existing.ControlStatus = HomeSeerAPI.ePairStatusControl.Control Then
                    ' There is an existing one that is STATUS or CONTROL - remove it if ours is protected.
                    hs.DeviceVSP_ClearControl(dvRef, Pair.Value)
                    hs.DeviceVSP_AddPair(dvRef, Pair)

                ElseIf Existing.ControlStatus = HomeSeerAPI.ePairStatusControl.Status Then
                    ' There is an existing one that is STATUS or CONTROL - remove it if ours is protected.
                    hs.DeviceVSP_ClearStatus(dvRef, Pair.Value)
                    hs.DeviceVSP_AddPair(dvRef, Pair)

                End If

            Else
                ' There is not a pair existing, so just add it.
                hs.DeviceVSP_AddPair(dvRef, Pair)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Default_VG_Pairs_AddUpdateUtil(ByVal dvRef As Integer, ByRef Pair As VGPair)
        If Pair Is Nothing Then Exit Sub
        If dvRef < 1 Then Exit Sub
        If Not hs.DeviceExistsRef(dvRef) Then Exit Sub

        Dim Existing As VGPair = Nothing

        ' The purpose of this procedure is to add the protected, default VS/VG pairs WITHOUT overwriting any user added
        '   pairs unless absolutely necessary (because they conflict).

        Try
            Existing = hs.DeviceVGP_Get(dvRef, Pair.Value) 'VGPairs.GetPairByValue(Pair.Value)

            If Existing IsNot Nothing Then
                hs.DeviceVGP_Clear(dvRef, Pair.Value)
                hs.DeviceVGP_AddPair(dvRef, Pair)
            Else
                ' There is not a pair existing, so just add it.
                hs.DeviceVGP_AddPair(dvRef, Pair)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Module
