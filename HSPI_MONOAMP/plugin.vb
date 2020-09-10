Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.Web
Imports System
Imports HomeSeerAPI
Imports Scheduler
Imports MPRSG6Z
Imports AmpApi


Public Class plugin
    Dim WithEvents amp As MPRSG6Z.Amp
    Dim ampserver As AmpApi.HttpServer

    '   Dim sConfigPage As String = "Sample_Config"
    '   Dim sStatusPage As String = "Sample_Status"
    '   Dim ConfigPage As New web_config(sConfigPage)
    '   Dim StatusPage As New web_status(sStatusPage)
    '   Dim WebPage As Object

    Dim actions As New hsCollection
    Dim action As New action
    Dim triggers As New hsCollection
    Dim trigger As New trigger

    '   Dim Commands As New hsCollection

    Const Pagename = "Events"


#Region "Device Interface"

    Public Function ConfigDevice(ref As Integer, user As String, userRights As Integer, newDevice As Boolean) As String
        Dim dv As Scheduler.Classes.DeviceClass = Nothing
        Dim stb As New StringBuilder
        Dim ddHouseCode As New clsJQuery.jqDropList("HouseCode", "", False)
        '     Dim ddUnitCode As New clsJQuery.jqDropList("DeviceCode", "", False)
        Dim bSave As New clsJQuery.jqButton("Save", "Done", "DeviceUtility", True)
        Dim HouseCode As String = ""
        '   Dim Devicecode As String = ""
        Dim Sample As ZoneClass

        dv = hs.GetDeviceByRef(ref)
        Log("ConfigDevice:" + dv.devString(hs))
        Return ""
        '''''''''''

        Dim PED As clsPlugExtraData = dv.PlugExtraData_Get(hs)

        Sample = PEDGet(PED, "Sample")

        If Sample Is Nothing Then
            ' Set the defaults
            Sample = New ZoneClass
            InitHSDevice(dv)
            Sample.Keypad = "1"
            '  Sample.DeviceCode = "1"
            PEDAdd(PED, "Sample", Sample)
            dv.PlugExtraData_Set(hs) = PED
        End If

        HouseCode = Sample.Keypad
        '  Devicecode = Sample.DeviceCode

        For Each l In "123456"
            ddHouseCode.AddItem(l, l, l = HouseCode)
        Next

        'For i = 1 To 16
        '    ddUnitCode.AddItem(i.ToString, i.ToString, i.ToString = Devicecode)
        'Next

        Try
            stb.Append("<form id='frmSample' name='SampleTab' method='Post'>")
            stb.Append(" <table border='0' cellpadding='0' cellspacing='0' width='610'>")
            stb.Append("  <tr><td colspan='4' align='Center' style='font-size:10pt; height:30px;' nowrap>Select a Housecode and Unitcode that matches one of the devices HomeSeer will be communicating with.</td></tr>")
            stb.Append("  <tr>")
            stb.Append("   <td nowrap class='tablecolumn' align='center' width='70'>House<br>Code</td>")
            stb.Append("   <td nowrap class='tablecolumn' align='center' width='70'>Unit<br>Code</td>")
            ' stb.Append("   <td nowrap class='tablecolumn' align='center' width='200'>&nbsp;</td>")
            stb.Append("  </tr>")
            stb.Append("  <tr>")
            stb.Append("   <td class='tablerowodd' align='center'>" & ddHouseCode.Build & "</td>")
            '          stb.Append("   <td class='tablerowodd' align='center'>" & ddUnitCode.Build & "</td>")
            stb.Append("   <td class='tablerowodd' align='left'>" & bSave.Build & "</td>")
            stb.Append("  </tr>")
            stb.Append(" </table>")
            stb.Append("</form>")
            Return stb.ToString
        Catch
            Return Err.Description
        End Try
    End Function

    Public Function ConfigDevicePost(ref As Integer, data As String, user As String, userRights As Integer) As Enums.ConfigDevicePostReturn
        Dim dv As Scheduler.Classes.DeviceClass = Nothing
        Dim parts As Collections.Specialized.NameValueCollection
        Dim PED As clsPlugExtraData
        Dim ReturnValue As Integer = Enums.ConfigDevicePostReturn.DoneAndCancel
        Dim Sample As ZoneClass

        dv = hs.GetDeviceByRef(ref)
        Log("ConfigDevicePost:" + dv.devString(hs) + "," + data)
        Return Enums.ConfigDevicePostReturn.DoneAndSave
        '''''''''''''''''

        Try
            parts = HttpUtility.ParseQueryString(data)

            dv = hs.GetDeviceByRef(ref)
            PED = dv.PlugExtraData_Get(hs)
            Sample = PEDGet(PED, "Sample")
            If Sample Is Nothing Then
                InitHSDevice(dv)
            End If

            Sample.Keypad = parts("HouseCode")
            ' Sample.DeviceCode = parts("DeviceCode")

            PED = dv.PlugExtraData_Get(hs)
            PEDAdd(PED, "Sample", Sample)
            dv.PlugExtraData_Set(hs) = PED
            hs.SaveEventsDevices()

            Return ReturnValue
        Catch ex As Exception

        End Try
        Return ReturnValue
    End Function

    'Homeseer has a Change from GUI, send to amp
    Public Sub SetIOMulti(colSend As System.Collections.Generic.List(Of HomeSeerAPI.CAPI.CAPIControl))
        Dim CC As CAPIControl
        '  Dim dv As Scheduler.Classes.DeviceClass = Nothing
        '   Dim PED As clsPlugExtraData
        '    Dim Sample As ZoneClass
        Dim Housecode As String = ""
        '//   Dim Devicecode As String = ""

        For Each CC In colSend

            '  PED = dv.PlugExtraData_Get(hs)
            '' Sample = PEDGet(PED, "Sample")

            hs.SetDeviceValueByRef(CC.Ref, CC.ControlValue, True)
            hs.SetDeviceString(CC.Ref, CC.ControlString, True)

            Dim dv As Scheduler.Classes.DeviceClass = hs.GetDeviceByRef(CC.Ref)
            Dim DT As DeviceTypeInfo
            DT = dv.DeviceType_Get(hs)
            Console.WriteLine("SetIOMulti set value: " & CC.ControlValue.ToString & "->ref:" & CC.Ref.ToString)
            Dim cmd As String = UCase(dv.Device_Type_String(hs))
            Dim newval As Integer = CC.ControlValue
            Dim da As String = dv.Address(hs)
            Dim params As String() = da.Split(":")
            Dim lb = UCase(CC.Label)
            If params(0) = "MONOAMP" Then
                'Dim zn As Integer
                'Integer.TryParse(params(3), zn) 
                Dim prop As String = params(1)
                Dim zn As String = params(3)
                Dim kp As KeyPad = amp.KeyPadID(zn)
                '    Dim kp As KeyPad = amp.Keypads(zn - 1)
                If amp IsNot Nothing Then
                    kp.Set_Value(newval, prop)

                    'Select Case True
                    '    Case cmd = "AMP SOURCE"
                    '        kp.CH = newval
                    '    Case cmd = "AMP BASS"
                    '        kp.BS = newval
                    '    Case cmd = "AMP TREBLE"
                    '        kp.TR = newval
                    '    Case cmd = "AMP BALANCE"
                    '        kp.BL = newval
                    '    Case cmd = "AMP VOLUME"
                    '        Select Case True
                    '            Case lb = "MUTE"
                    '                kp.MU = 1
                    '            Case lb = "UNMUTE"
                    '                kp.MU = 0
                    '            Case lb = "ON"
                    '                kp.PR = 1
                    '            Case lb = "OFF"
                    '                kp.PR = 0
                    '            Case lb = "VOLUME"
                    '                kp.VO = newval
                    '        End Select
                    'End Select
                End If

            End If

            '   SendCommand(Housecode, Devicecode, CC.ControlValue)
        Next
    End Sub

#End Region


#Region "Action Properties"

    Sub SetActions()
        Dim o As Object = Nothing
        If actions.Count = 0 Then
            actions.Add(o, "Send Command")
        End If
    End Sub

    Function ActionCount() As Integer
        SetActions()
        Return actions.Count
    End Function

    ReadOnly Property ActionName(ByVal ActionNumber As Integer) As String
        Get
            SetActions()
            If ActionNumber > 0 AndAlso ActionNumber <= actions.Count Then
                Return IFACE_NAME & ": " & actions.Keys(ActionNumber - 1)
            Else
                Return ""
            End If
        End Get
    End Property

#End Region

#Region "Trigger Proerties"

    Sub SetTriggers()
        Dim o As Object = Nothing
        If triggers.Count = 0 Then
            triggers.Add(o, "Recieve Command")
        End If
    End Sub

    Public ReadOnly Property HasTriggers() As Boolean
        Get
            SetTriggers()
            Return IIf(triggers.Count > 0, True, False)
        End Get
    End Property

    Public Function TriggerCount() As Integer
        SetTriggers()
        Return triggers.Count
    End Function

    Public ReadOnly Property SubTriggerCount(ByVal TriggerNumber As Integer) As Integer
        Get
            Dim trigger As trigger
            If ValidTrig(TriggerNumber) Then
                trigger = triggers(TriggerNumber)
                If Not (trigger Is Nothing) Then
                    Return trigger.Count
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property TriggerName(ByVal TriggerNumber As Integer) As String
        Get
            If Not ValidTrig(TriggerNumber) Then
                Return ""
            Else
                Return IFACE_NAME & ": " & triggers.Keys(TriggerNumber - 1)
            End If
        End Get
    End Property

    Public ReadOnly Property SubTriggerName(ByVal TriggerNumber As Integer, ByVal SubTriggerNumber As Integer) As String
        Get
            Dim trigger As trigger
            If ValidSubTrig(TriggerNumber, SubTriggerNumber) Then
                trigger = triggers(TriggerNumber)
                Return IFACE_NAME & ": " & trigger.Keys(SubTriggerNumber - 1)
            Else
                Return ""
            End If
        End Get
    End Property

    Friend Function ValidTrig(ByVal TrigIn As Integer) As Boolean
        SetTriggers()
        If TrigIn > 0 AndAlso TrigIn <= triggers.Count Then
            Return True
        End If
        Return False
    End Function

    Public Function ValidSubTrig(ByVal TrigIn As Integer, ByVal SubTrigIn As Integer) As Boolean
        Dim trigger As trigger = Nothing
        If TrigIn > 0 AndAlso TrigIn <= triggers.Count Then
            trigger = triggers(TrigIn)
            If Not (trigger Is Nothing) Then
                If SubTrigIn > 0 AndAlso SubTrigIn <= trigger.Count Then Return True
            End If
        End If
        Return False
    End Function

#End Region

#Region "Action Interface"

    Public Function HandleAction(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean
        Dim Housecode As String = ""
        Dim DeviceCode As String = ""
        Dim Command As String = ""
        Dim UID As String
        UID = ActInfo.UID.ToString

        Try
            If Not (ActInfo.DataIn Is Nothing) Then
                DeSerializeObject(ActInfo.DataIn, action)
            Else
                Return False
            End If
            For Each sKey In action.Keys
                Select Case True
                    Case InStr(sKey, "Keypads_" & UID) > 0
                        Housecode = action(sKey)
                    Case InStr(sKey, "Commands_" & UID) > 0
                        DeviceCode = action(sKey)
                    Case InStr(sKey, "Values_" & UID) > 0
                        Command = action(sKey)
                End Select
            Next
            Select Case Command
                Case "ValueUp"
                    amp.KeyPadID(Housecode).Set_ValueUp(DeviceCode)
                Case "ValueDown"
                    amp.KeyPadID(Housecode).Set_ValueDn(DeviceCode)
                Case Else
                    Dim U As String = "0"
                    Dim C As String = "0"
                    If Housecode <> "All" Then
                        U = Housecode.Substring(0, 1)
                        C = Housecode.Substring(1, 1)
                    End If
                    Dim value As String = Command
                    amp.SendCommand(U, C, DeviceCode, Command)
            End Select



            '    
            'SendCommand(Housecode, DeviceCode, Command)

        Catch ex As Exception
            hs.WriteLog(IFACE_NAME, "Error executing action: " & ex.Message)
        End Try
        Return True
    End Function

    Public Function ActionConfigured(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean
        Dim Configured As Boolean = False
        Dim sKey As String
        Dim itemsConfigured As Integer = 0
        Dim itemsToConfigure As Integer = 3
        Dim UID As String
        UID = ActInfo.UID.ToString

        If Not (ActInfo.DataIn Is Nothing) Then
            DeSerializeObject(ActInfo.DataIn, action)
            For Each sKey In action.Keys
                Select Case True
                    Case InStr(sKey, "Keypads_" & UID) > 0 AndAlso action(sKey) <> ""
                        itemsConfigured += 1
                    Case InStr(sKey, "Commands_" & UID) > 0 AndAlso action(sKey) <> ""
                        itemsConfigured += 1
                    Case InStr(sKey, "Values_" & UID) > 0 AndAlso action(sKey) <> ""
                        itemsConfigured += 1
                End Select
            Next
            If itemsConfigured = itemsToConfigure Then Configured = True
        End If
        Return Configured
    End Function

    Public Function ActionBuildUI(ByVal sUnique As String, ByVal ActInfo As HomeSeerAPI.IPlugInAPI.strTrigActInfo) As String
        Dim UID As String
        UID = ActInfo.UID.ToString
        Dim stb As New StringBuilder
        Dim Housecode As String = ""
        Dim DeviceCode As String = ""
        Dim Command As String = ""
        Dim dd As New clsJQuery.jqDropList("Keypads_" & UID & sUnique, Pagename, True)
        Dim dd1 As New clsJQuery.jqDropList("Commands_" & UID & sUnique, Pagename, True)
        Dim dd2 As New clsJQuery.jqDropList("Values_" & UID & sUnique, Pagename, True)
        Dim sKey As String


        dd.autoPostBack = True
        dd.AddItem("--Please Select--", "", False)
        dd1.autoPostBack = True
        dd1.AddItem("--Please Select--", "", False)
        dd2.autoPostBack = True
        dd2.AddItem("--Please Select--", "", False)

        If Not (ActInfo.DataIn Is Nothing) Then
            DeSerializeObject(ActInfo.DataIn, action)
        Else 'new event, so clean out the action object
            action = New action
        End If

        For Each sKey In action.Keys
            Select Case True
                Case InStr(sKey, "Keypads_" & UID) > 0
                    Housecode = action(sKey)
                Case InStr(sKey, "Commands_" & UID) > 0
                    DeviceCode = action(sKey)
                Case InStr(sKey, "Values_" & UID) > 0
                    Command = action(sKey)
            End Select
        Next

        'For Each C In "ABCDEFGHIJKLMNOP"
        '    dd.AddItem(C, C, (C = Housecode))
        'Next
        For Each C In amp.Keypads
            dd.AddItem(C.Name, C.ID.ToString(), (C.ID.ToString() = Housecode))
        Next
        dd.AddItem("All", "All", ("All" = Housecode))

        stb.Append("Select Keypad:")
        stb.Append(dd.Build)

        'For i = 1 To 16
        '    dd1.AddItem(i.ToString, i.ToString, (i.ToString = DeviceCode))
        'Next
        For Each cmd As MPRSG6Z.Command In System.Enum.GetValues(GetType(MPRSG6Z.Command))
            Dim code As String = StringEnum.GetCodeValue(cmd)
            dd1.AddItem(cmd.ToString, code, (code = DeviceCode))
        Next
        stb.Append("Select Command:")
        stb.Append(dd1.Build)

        'For Each item In Commands.Keys
        '    dd2.AddItem(Commands(item), item, (item = Command))
        'Next
        If "All" <> Housecode Then
            dd2.AddItem("ValueUp", "ValueUp", ("ValueUp" = Command))
            dd2.AddItem("ValueDown", "ValueDown", ("ValueDown" = Command))
        End If

        If Not String.IsNullOrWhiteSpace(DeviceCode) Then
            Dim min = MPRSG6Z.CommandValues.Min(DeviceCode)
            Dim max = MPRSG6Z.CommandValues.Max(DeviceCode)
            For x = min To max
                dd2.AddItem(x.ToString, x.ToString, (x.ToString = Command))
            Next
        End If

        stb.Append("Select Value:")
        stb.Append(dd2.Build)


        Return stb.ToString
    End Function

    Public Function ActionProcessPostUI(ByVal PostData As Collections.Specialized.NameValueCollection, _
                                        ByVal ActInfo As IPlugInAPI.strTrigActInfo) As IPlugInAPI.strMultiReturn

        Dim Ret As New HomeSeerAPI.IPlugInAPI.strMultiReturn
        Dim UID As String
        UID = ActInfo.UID.ToString

        Ret.sResult = ""
        ' We cannot be passed info ByRef from HomeSeer, so turn right around and return this same value so that if we want, 
        '   we can exit here by returning 'Ret', all ready to go.  If in this procedure we need to change DataOut or TrigInfo,
        '   we can still do that.
        Ret.DataOut = ActInfo.DataIn
        Ret.TrigActInfo = ActInfo

        If PostData Is Nothing Then Return Ret
        If PostData.Count < 1 Then Return Ret

        If Not (ActInfo.DataIn Is Nothing) Then
            DeSerializeObject(ActInfo.DataIn, action)
        End If

        Dim parts As Collections.Specialized.NameValueCollection

        Dim sKey As String

        parts = PostData

        Try
            For Each sKey In parts.Keys
                If sKey Is Nothing Then Continue For
                If String.IsNullOrEmpty(sKey.Trim) Then Continue For
                Select Case True
                    Case InStr(sKey, "Keypads_" & UID) > 0, InStr(sKey, "Commands_" & UID) > 0, InStr(sKey, "Values_" & UID) > 0
                        action.Add(CObj(parts(sKey)), sKey)
                End Select
            Next
            If Not SerializeObject(action, Ret.DataOut) Then
                Ret.sResult = IFACE_NAME & " Error, Serialization failed. Signal Action not added."
                Return Ret
            End If
        Catch ex As Exception
            Ret.sResult = "ERROR, Exception in Action UI of " & IFACE_NAME & ": " & ex.Message
            Return Ret
        End Try

        ' All OK
        Ret.sResult = ""
        Return Ret
    End Function

    Public Function ActionFormatUI(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As String
        Dim stb As New StringBuilder
        Dim sKey As String
        Dim Housecode As String = ""
        Dim DeviceCode As String = ""
        Dim Command As String = ""
        Dim UID As String
        UID = ActInfo.UID.ToString

        If Not (ActInfo.DataIn Is Nothing) Then
            DeSerializeObject(ActInfo.DataIn, action)
        End If

        For Each sKey In action.Keys
            Select Case True
                Case InStr(sKey, "Keypads_" & UID) > 0
                    Housecode = action(sKey)
                Case InStr(sKey, "Commands_" & UID) > 0
                    DeviceCode = action(sKey)
                Case InStr(sKey, "Values_" & UID) > 0
                    Command = action(sKey)
            End Select
        Next

        stb.Append(" the system will execute the " & Command & " command ")
        stb.Append("for  Command " & DeviceCode & " ")
        If Housecode = "ALL" Then
            stb.Append("for all Keypads")
        Else
            stb.Append("for Keypad " & Housecode)
        End If

        Return stb.ToString
    End Function

#End Region

#Region "Trigger Interface"

    Public ReadOnly Property TriggerConfigured(ByVal TrigInfo As HomeSeerAPI.IPlugInAPI.strTrigActInfo) As Boolean
        Get
            Dim Configured As Boolean = False
            Dim sKey As String
            Dim itemsConfigured As Integer = 0
            Dim itemsToConfigure As Integer = 3
            Dim UID As String
            UID = TrigInfo.UID.ToString

            If Not (TrigInfo.DataIn Is Nothing) Then
                DeSerializeObject(TrigInfo.DataIn, trigger)
                For Each sKey In trigger.Keys
                    Select Case True
                        Case InStr(sKey, "Keypads_" & UID) > 0 AndAlso trigger(sKey) <> ""
                            itemsConfigured += 1
                        Case InStr(sKey, "Commands_" & UID) > 0 AndAlso trigger(sKey) <> ""
                            itemsConfigured += 1
                        Case InStr(sKey, "Values_" & UID) > 0 AndAlso trigger(sKey) <> ""
                            itemsConfigured += 1
                    End Select
                Next
                If itemsConfigured = itemsToConfigure Then Configured = True
            End If
            Return Configured
        End Get
    End Property

    Public Function TriggerBuildUI(ByVal sUnique As String, ByVal TrigInfo As HomeSeerAPI.IPlugInAPI.strTrigActInfo) As String
        Dim UID As String
        UID = TrigInfo.UID.ToString
        Dim stb As New StringBuilder
        Dim Housecode As String = ""
        Dim DeviceCode As String = ""
        Dim Command As String = ""
        Dim dd As New clsJQuery.jqDropList("Keypads_" & UID & sUnique, Pagename, True)
        Dim dd1 As New clsJQuery.jqDropList("Commands_" & UID & sUnique, Pagename, True)
        Dim dd2 As New clsJQuery.jqDropList("Values_" & UID & sUnique, Pagename, True)
        Dim sKey As String

        dd.autoPostBack = True
        dd.AddItem("--Please Select--", "", False)
        dd1.autoPostBack = True
        dd1.AddItem("--Please Select--", "", False)
        dd2.autoPostBack = True
        dd2.AddItem("--Please Select--", "", False)

        If Not (TrigInfo.DataIn Is Nothing) Then
            DeSerializeObject(TrigInfo.DataIn, trigger)
        Else 'new event, so clean out the trigger object
            trigger = New trigger
        End If

        For Each sKey In trigger.Keys
            Select Case True
                Case InStr(sKey, "Keypads_" & UID) > 0
                    Housecode = trigger(sKey)
                Case InStr(sKey, "Commands_" & UID) > 0
                    DeviceCode = trigger(sKey)
                Case InStr(sKey, "Values_" & UID) > 0
                    Command = trigger(sKey)
            End Select
        Next

        'For Each C In "ABCDEFGHIJKLMNOP"
        '    dd.AddItem(C, C, (C = Housecode))
        'Next

        For Each C In amp.Keypads
            dd.AddItem(C.Name, C.ID.ToString(), (C.ID.ToString() = Housecode))
        Next
        dd.AddItem("Any", "Any", ("Any" = Housecode))

        stb.Append("Select Keypad:")
        stb.Append(dd.Build)

        'For i = 1 To 16
        '    dd1.AddItem(i.ToString, i.ToString, (i.ToString = DeviceCode))
        'Next
        For Each cmd As MPRSG6Z.Command In System.Enum.GetValues(GetType(MPRSG6Z.Command))
            Dim code As String = StringEnum.GetCodeValue(cmd)
            dd1.AddItem(cmd.ToString, code, (code = DeviceCode))
        Next
        'stb.Append("Select Command:")
        'stb.Append(dd.Build)

        'dd1.AddItem("All", "All", ("All" = DeviceCode))
        'For i = 1 To 16
        '    dd1.AddItem(i.ToString, i.ToString, (i.ToString = DeviceCode))
        'Next

        stb.Append("Select Unit Code:")
        stb.Append(dd1.Build)

        'For Each item In Commands.Keys
        '    dd2.AddItem(Commands(item), item, (item = Command))
        'Next
        ' If "Any" <> Housecode Then
        dd2.AddItem("ValueUp", "ValueUp", ("ValueUp" = Command))
        dd2.AddItem("ValueDown", "ValueDown", ("ValueDown" = Command))
        'End If

        If Not String.IsNullOrWhiteSpace(DeviceCode) Then
            Dim min = MPRSG6Z.CommandValues.Min(DeviceCode)
            Dim max = MPRSG6Z.CommandValues.Max(DeviceCode)
            For x = min To max
                dd2.AddItem(x.ToString, x.ToString, (x.ToString = Command))
            Next
        End If
        stb.Append("Select Value:")
        stb.Append(dd2.Build)


        Return stb.ToString
    End Function

    Public Function TriggerProcessPostUI(ByVal PostData As System.Collections.Specialized.NameValueCollection, _
                                                     ByVal TrigInfo As HomeSeerAPI.IPlugInAPI.strTrigActInfo) As HomeSeerAPI.IPlugInAPI.strMultiReturn
        Dim Ret As New HomeSeerAPI.IPlugInAPI.strMultiReturn
        Dim UID As String
        UID = TrigInfo.UID.ToString

        Ret.sResult = ""
        ' We cannot be passed info ByRef from HomeSeer, so turn right around and return this same value so that if we want, 
        '   we can exit here by returning 'Ret', all ready to go.  If in this procedure we need to change DataOut or TrigInfo,
        '   we can still do that.
        Ret.DataOut = TrigInfo.DataIn
        Ret.TrigActInfo = TrigInfo

        If PostData Is Nothing Then Return Ret
        If PostData.Count < 1 Then Return Ret

        If Not (TrigInfo.DataIn Is Nothing) Then
            DeSerializeObject(TrigInfo.DataIn, trigger)
        End If

        Dim parts As Collections.Specialized.NameValueCollection

        Dim sKey As String

        parts = PostData
        Try
            For Each sKey In parts.Keys
                If sKey Is Nothing Then Continue For
                If String.IsNullOrEmpty(sKey.Trim) Then Continue For
                Select Case True
                    Case InStr(sKey, "Keypads_" & UID) > 0, InStr(sKey, "Commands_" & UID) > 0, InStr(sKey, "Values_" & UID) > 0
                        trigger.Add(CObj(parts(sKey)), sKey)
                End Select
            Next
            If Not SerializeObject(trigger, Ret.DataOut) Then
                Ret.sResult = IFACE_NAME & " Error, Serialization failed. Signal Trigger not added."
                Return Ret
            End If
        Catch ex As Exception
            Ret.sResult = "ERROR, Exception in Trigger UI of " & IFACE_NAME & ": " & ex.Message
            Return Ret
        End Try

        ' All OK
        Ret.sResult = ""
        Return Ret
    End Function

    Public Function TriggerFormatUI(ByVal TrigInfo As HomeSeerAPI.IPlugInAPI.strTrigActInfo) As String
        Dim stb As New StringBuilder
        Dim sKey As String
        Dim Housecode As String = ""
        Dim DeviceCode As String = ""
        Dim Command As String = ""
        Dim UID As String
        UID = TrigInfo.UID.ToString

        If Not (TrigInfo.DataIn Is Nothing) Then
            DeSerializeObject(TrigInfo.DataIn, trigger)
        End If

        For Each sKey In trigger.Keys
            Select Case True
                Case InStr(sKey, "Keypads_" & UID) > 0
                    Housecode = trigger(sKey)
                Case InStr(sKey, "Commands_" & UID) > 0
                    DeviceCode = trigger(sKey)
                Case InStr(sKey, "Values_" & UID) > 0
                    Command = trigger(sKey)
            End Select
        Next

        stb.Append(" the system detected the " & Command & " command ")
        stb.Append("on Housecode " & Housecode & " ")
        If DeviceCode = "ALL" Then
            stb.Append("from a Unitcode")
        Else
            stb.Append("from Unitcode " & DeviceCode)
        End If

        Return stb.ToString
    End Function

#End Region

#Region "HomeSeer-Required Functions"

    Function name() As String
        name = IFACE_NAME
    End Function

    Public Function AccessLevel() As Integer
        AccessLevel = 1
    End Function

#End Region

#Region "Init"
    ' THIS FIRES WHEN KEYPADS ARE CHANGED, OR SOMBODY uses the Mixer app.
    Private Sub Amp_OnValueChanged(ByVal sender As Object, ByVal e As MPRSG6Z.Amp.State) Handles amp.OnValueChanged
        Console.WriteLine("Amp_OnValueChanged " + e.Keypad.Name + " " + e.Property + " " + e.NewValue.ToString)
        Log("Amp_OnValueChanged " + e.Keypad.Name + " " + e.Property + " " + e.NewValue.ToString)

        Dim dv As Scheduler.Classes.DeviceClass = Nothing
        Dim P As String = e.Property
        Dim addr = GetAddress(P, e.Keypad.ID.ToString())
        Dim i As Integer = hs.GetDeviceRef(addr)
        dv = hs.GetDeviceByRef(i)
        Dim ref As Integer = dv.Ref(Nothing)
        hs.SetDeviceValueByRef(ref, e.NewValue, True)



        Dim TrigsToCheck() As IAllRemoteAPI.strTrigActInfo = Nothing
        Dim TC As IAllRemoteAPI.strTrigActInfo = Nothing
        '   Dim Trig As strTrigger

        Try
            ' Step 1: Ask HomeSeer for any triggers that are for this plug-in and are Type 2, SubType 2
            ' (We did Type 2 SubType 1 up above)
            TrigsToCheck = Nothing
            TrigsToCheck = callback.TriggerMatches(IFACE_NAME, 1, -1)
        Catch ex As Exception
        End Try
        ' Step 2: If triggers were returned, we need to check them against our trigger value.
        Dim trig As trigger = New trigger
        If TrigsToCheck IsNot Nothing AndAlso TrigsToCheck.Count > 0 Then
            For Each TC In TrigsToCheck
                Dim UID As String
                UID = TC.UID.ToString
                Dim bRes = DeSerializeObject(TC.DataIn, trig)
                Dim Housecode As String = ""
                Dim DeviceCode As String = ""
                Dim Command As String = ""
                For Each sKey In trig.Keys
                    Select Case True
                        Case InStr(sKey, "Keypads_" & UID) > 0
                            Housecode = trig(sKey)
                        Case InStr(sKey, "Commands_" & UID) > 0
                            DeviceCode = trig(sKey)
                        Case InStr(sKey, "Values_" & UID) > 0
                            Command = trig(sKey)
                    End Select

                Next
                Select Case Command
                    Case "ValueUp"
                        If e.OldValue < e.NewValue And e.Property = DeviceCode And (Housecode = "Any" Or e.Keypad.ID.ToString = Housecode) Then
                            callback.TriggerFire(IFACE_NAME, TC)
                        End If
                    Case "ValueDown"
                        If e.OldValue > e.NewValue And e.Property = DeviceCode And (Housecode = "Any" Or e.Keypad.ID.ToString = Housecode) Then
                            callback.TriggerFire(IFACE_NAME, TC)
                        End If
                    Case Else
                        If e.Property = DeviceCode And (Housecode = "Any" Or e.Keypad.ID.ToString = Housecode) And Command = e.NewValue Then
                            callback.TriggerFire(IFACE_NAME, TC)
                        End If

                End Select
            Next
        End If

    End Sub



    Private Sub Amp_StatusChanged(ByVal sender As Object, ByVal e As MPRSG6Z.ControlerStatusChangedEventArgs) Handles amp.StatusChanged
        Log("Amp_StatusChanged " + e.State.ToString + " " + e.Status)
        Console.WriteLine("Amp_StatusChanged " + e.State.ToString + " " + e.Status)

    End Sub


    Public Function InitIO(ByVal port As String) As String
        Log("Monoprice AMP (c)2014 Mike Pisano")
        Log("Beta 3.O License Expires on 03/31/2017")
        Dim exp As Date = New Date(2017, 3, 31)
        If Date.Now > exp Then
            Throw New Exception("Beta Expired")
        End If

        Log("Creating Amp")


        Global.Global.CurrentConfig = New Config

        ' ConfigParameters c = new ConfigParameters();

        amp = New MPRSG6Z.Amp(Global.Global.CurrentConfig.Parameters)

        '     AmpApi.Global`.CurrentConfig = new Config();

        '        amp.PolledWait = Global.CurrentConfig.Parameters.PolledWait
        '        amp.Units = 2  ' Global.CurrentConfig.Parameters.Units
        '        amp.ComPort = "COM1" ' Global.CurrentConfig.Parameters.ComPort
        '        amp.PollMS = 1000 ' Global.CurrentConfig.Parameters.PollMS
        '        amp.QueueDupeElimination = True ' Global.CurrentConfig.Parameters.RemoveDupes
        '        amp.Sources = Global.CurrentConfig.Parameters.Sources


        amp.Start()
        Log("Amp Running on " + Global.Global.CurrentConfig.Parameters.ComPort)

        If Global.Global.CurrentConfig.Parameters.WebPort <> 0 Then
            Log("Starting Amp HTTP Server on " + Global.Global.CurrentConfig.Parameters.IPAddress + Global.Global.CurrentConfig.Parameters.WebPort.ToString)
            ampserver = New AmpApi.HttpServer()
            ampserver.Start(amp, Global.Global.CurrentConfig.Parameters.IPAddress, Global.Global.CurrentConfig.Parameters.WebPort)
            Log("Starting Amp HTTP Server Running")
            Console.WriteLine("Web amp Running on " + Global.Global.CurrentConfig.Parameters.WebAddress + ":" + Global.Global.CurrentConfig.Parameters.WebPort.ToString)
        End If
        'Dim o As Object = nothing
        ''    RegisterWebPage(sConfigPage)
        ''  RegisterWebPage(sStatusPage)
        'if more than 1 action/trigger is needed, or if subactions/triggers are needed, then add them all here
        'actions.Add(o, "Action1")
        'actions.Add(o, "Action2")
        'triggers.Add(o, "Trigger1")
        'triggers.Add(o, "Trigger2")
        Find_Create_Devices()
        callback.RegisterEventCB(Enums.HSEvent.VALUE_CHANGE, IFACE_NAME, "")
        '  hs.SaveINISetting("Settings", "test", Nothing, "hspi_HSTouch.ini")
        '   LoadCommands()

        Return ""
    End Function

    Public Sub ShutdownIO()
        Try
            Try
                hs.SaveEventsDevices()
                Log("Amp ShutdownIO")
                ampserver.Stop()
                amp.Stop()
                amp = Nothing
            Catch ex As Exception
                Log("could not save devices")
            End Try
            bShutDown = True
        Catch ex As Exception
            Log("Error ending " & IFACE_NAME & " Plug-In")
        End Try

    End Sub

    'Sub LoadCommands()
    '    Commands.Add(CObj("All Zones Off"), CStr(CInt(DEVICE_COMMAND.All_Zones_Off)))
    '    Commands.Add(CObj("All Zones  On"), CStr(CInt(DEVICE_COMMAND.All_Zones_On)))
    '    Commands.Add(CObj("Zone On"), CStr(CInt(DEVICE_COMMAND.Zone_On)))
    '    Commands.Add(CObj("Zone Off"), CStr(CInt(DEVICE_COMMAND.Zone_Off)))
    'End Sub

#End Region

    Private Function GetName(ByVal code As String) As String
        Dim Name As String = ""
        Select Case True
            Case code = "PA"
                Name = "Public Address"
            Case code = "PR"
                Name = "Power"
            Case code = "MU"
                Name = "Mute"
            Case code = "DT"
                Name = "Do Not Disturb"
            Case code = "VO"
                Name = "Volume"
            Case code = "TR"
                Name = "Treble"
            Case code = "BS"
                Name = "Bass"
            Case code = "BL"
                Name = "Balance"
            Case code = "CH"
                Name = "Source"
            Case code = "LS"
                Name = "Connected"
        End Select
        Return Name
    End Function
    'Private Function GetDeviceref(ByVal code As String, Zone As Integer) As String
    '    Return "Amp " + GetName(code) + " Zone " + Zone.ToString()
    'End Function
    Private Function GetAddress(Code As String, Unit As Integer, Keypad As Integer) As String
        Return "MONOAMP:" + Code + ":ZN:" + Unit.ToString() + Keypad.ToString()
    End Function

    Private Function GetAddress(Code As String, ID As String) As String
        Return "MONOAMP:" + Code + ":ZN:" + ID
    End Function

    'Private Function GetDevType(ByVal code) As String
    '    Return "Amp " + GetName(code)
    'End Function

    Friend Sub Find_Create_Devices()
        Dim col As New Collections.Generic.List(Of Scheduler.Classes.DeviceClass)
        Dim dv As Scheduler.Classes.DeviceClass
        Dim Found As Boolean = False


        'If Date.Now > New Date("2015,3,31") Then
        '    Throw New Exception("Beta Expired")
        '    Return
        'End If


        Try
            Dim EN As Scheduler.Classes.clsDeviceEnumeration
            EN = hs.GetDeviceEnumerator
            If EN Is Nothing Then Throw New Exception(IFACE_NAME & " failed to get a device enumerator from HomeSeer.")
            Do
                dv = EN.GetNext
                If dv Is Nothing Then Continue Do
                If dv.Interface(Nothing) IsNot Nothing Then
                    If dv.Interface(Nothing).Trim = IFACE_NAME Then
                        col.Add(dv)
                    End If
                End If
            Loop Until EN.Finished
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Enumerator: " & ex.Message)
        End Try

        Try
            Dim DT As DeviceTypeInfo = Nothing
            If col IsNot Nothing AndAlso col.Count > 0 Then
                For Each dv In col
                    If dv Is Nothing Then Continue For
                    If dv.Interface(hs) <> IFACE_NAME Then Continue For
                    DT = dv.DeviceType_Get(hs)
                    If DT IsNot Nothing Then
                        Found = True
                        hs.WriteLog(IFACE_NAME, "Found Root") 'DT.Device_Type + " " + DT.Device_SubType)
                        Exit For
                        'If DT.Device_API = DeviceTypeInfo.eDeviceAPI.Thermostat AndAlso DT.Device_Type = DeviceTypeInfo.eDeviceType_Thermostat.Temperature Then
                        '    ' this is our temp device
                        '    Found = True
                        '    MyTempDevice = dv.Ref(Nothing)
                        '    hs.SetDeviceValueByRef(dv.Ref(Nothing), 72, False)
                        'End If

                        'If DT.Device_API = DeviceTypeInfo.eDeviceAPI.Thermostat AndAlso DT.Device_Type = DeviceTypeInfo.eDeviceType_Thermostat.Setpoint Then
                        '    Found = True
                        '    If DT.Device_SubType = DeviceTypeInfo.eDeviceSubType_Setpoint.Heating_1 Then
                        '        hs.SetDeviceValueByRef(dv.Ref(Nothing), 68, False)
                        '    End If
                        'End If

                        'If DT.Device_API = DeviceTypeInfo.eDeviceAPI.Thermostat AndAlso DT.Device_Type = DeviceTypeInfo.eDeviceType_Thermostat.Setpoint Then
                        '    Found = True
                        '    If DT.Device_SubType = DeviceTypeInfo.eDeviceSubType_Setpoint.Cooling_1 Then
                        '        hs.SetDeviceValueByRef(dv.Ref(Nothing), 75, False)
                        '    End If
                        'End If

                        'If DT.Device_API = DeviceTypeInfo.eDeviceAPI.Plug_In AndAlso DT.Device_Type = 69 Then
                        '    Found = True
                        '    MyDevice = dv.Ref(Nothing)

                        '    ' Now (mostly for demonstration purposes) - work with the PlugExtraData object.
                        '    Dim EDO As HomeSeerAPI.clsPlugExtraData = Nothing
                        '    EDO = dv.PlugExtraData_Get(Nothing)
                        '    If EDO IsNot Nothing Then
                        '        Dim obj As Object = Nothing
                        '        obj = EDO.GetNamed("My Special Object")
                        '        If obj IsNot Nothing Then
                        '            Log("Plug-In Extra Data Object Retrieved = " & obj.ToString, LogLevel.Debug)
                        '        End If
                        '        obj = EDO.GetNamed("My Count")
                        '        Dim MC As Integer = 1
                        '        If obj Is Nothing Then
                        '            If Not EDO.AddNamed("My Count", MC) Then
                        '                Log("Error adding named data object to plug-in sample device!", LogLevel.Debug)
                        '                Exit For
                        '            End If
                        '            dv.PlugExtraData_Set(hs) = EDO
                        '            hs.SaveEventsDevices()
                        '        Else
                        '            Try
                        '                MC = Convert.ToInt32(obj)
                        '            Catch ex As Exception
                        '                MC = -1
                        '            End Try
                        '            If MC < 0 Then Exit For
                        '            Log("Retrieved count from plug-in sample device is: " & MC.ToString, LogLevel.Debug)
                        '            MC += 1
                        '            ' Now put it back - need to remove the old one first.
                        '            EDO.RemoveNamed("My Count")
                        '            EDO.AddNamed("My Count", MC)
                        '            dv.PlugExtraData_Set(hs) = EDO
                        '            hs.SaveEventsDevices()
                        '        End If
                        '    End If
                        'End If
                    End If
                Next
            End If
        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Find: " & ex.Message)
        End Try

        Try
            '      If Not Found Then
            Dim ref As Integer
            Dim GPair As VGPair = Nothing

            ' build a thermostat device group,all of the following thermostat devices are grouped under this root device
            '        gGlobalTempScaleF = Convert.ToBoolean(hs.GetINISetting("Settings", "gGlobalTempScaleF", "True").Trim)   ' get the F or C setting from HS setup
            Dim therm_root_dv As Scheduler.Classes.DeviceClass = Nothing


            ref = hs.GetDeviceRef("MONOAMP:ROOT")
            If ref = -1 Then
                hs.WriteLog(IFACE_NAME & " Error", "Missing Music Root")
                ref = hs.NewDeviceRef("Music Root")
                If ref > 0 Then
                    hs.WriteLog(IFACE_NAME & " Error", "Creating AMP Root")
                    dv = hs.GetDeviceByRef(ref)
                    therm_root_dv = dv
                    dv.Address(hs) = "MONOAMP:ROOT"
                    dv.Device_Type_String(hs) = "Monoprice Amp"     ' this device type is set up in the default HSTouch projects so we set it here so the default project displays
                    dv.Interface(hs) = IFACE_NAME
                    dv.InterfaceInstance(hs) = ""
                    dv.Location(hs) = IFACE_NAME
                    dv.Location2(hs) = ""

                    Dim DT As New DeviceTypeInfo
                    DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                    DT.Device_Type = DeviceTypeInfo.eDeviceType_SourceSwitch.Root
                    DT.Device_SubType = 0
                    DT.Device_SubType_Description = ""
                    dv.DeviceType_Set(hs) = DT
                    dv.MISC_Set(hs, Enums.dvMISC.STATUS_ONLY)
                    dv.Relationship(hs) = Enums.eRelationship.Parent_Root

                    hs.SaveEventsDevices()
                    hs.WriteLog(IFACE_NAME & " Error", "Created AMP Root")
                End If
            End If
            If ref > 0 Then
                dv = hs.GetDeviceByRef(ref)
                therm_root_dv = dv
            End If

            For UN = 1 To amp.Units
                For KP = 1 To 6
                    hs.WriteLog(IFACE_NAME & " Checking", "Amp " + UN.ToString() + " Keypad " + KP.ToString())

                    Dim ksuffix As String = amp.KeyPadID(UN, KP).Name
                    ref = hs.GetDeviceRef(GetAddress("PR", UN, KP))
                    If ref = -1 Then
                        hs.WriteLog(IFACE_NAME & " Error", "Missing Amp Power " + ksuffix)
                        ref = hs.NewDeviceRef(ksuffix + " Music Power")
                        If ref > 0 Then
                            hs.WriteLog(IFACE_NAME & " Error", "Creating Amp Power " + ksuffix)
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("PR", UN, KP)
                            dv.Device_Type_String(hs) = "Amp Power"
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Location(hs) = ksuffix
                            dv.Location2(hs) = ""

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            DT.Device_Type = DeviceTypeInfo.eDeviceType_SourceSwitch.Source
                            DT.Device_SubType = 0
                            DT.Device_SubType_Description = ""
                            dv.DeviceType_Set(hs) = DT
                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            ' add an ON button and value
                            Dim Pair As VSPair
                            Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.SingleValue
                            Pair.Value = 1
                            Pair.Status = "On"
                            Pair.Render = Enums.CAPIControlType.Button
                            hs.DeviceVSP_AddPair(ref, Pair)
                            GPair = New VGPair
                            GPair.PairType = VSVGPairType.SingleValue
                            Pair.ControlUse = ePairControlUse._On
                            GPair.Set_Value = 1
                            GPair.Graphic = "/images/HSPI_MONOAMP/power_on.png"
                            hs.DeviceVGP_AddPair(ref, GPair)

                            ' add an OFF button and value
                            Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.SingleValue
                            Pair.ControlUse = ePairControlUse._Off
                            Pair.Value = 0
                            Pair.Status = "Off"
                            Pair.Render = Enums.CAPIControlType.Button
                            hs.DeviceVSP_AddPair(ref, Pair)
                            GPair = New VGPair
                            GPair.PairType = VSVGPairType.SingleValue
                            GPair.Set_Value = 0
                            GPair.Graphic = "/images/HSPI_MONOAMP/power_off.png"
                            hs.DeviceVGP_AddPair(ref, GPair)

                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.Status_Support(hs) = True
                            hs.SaveEventsDevices()
                            hs.WriteLog(IFACE_NAME & " Error", "Created Amp Power " + ksuffix)
                        End If

                    End If
                    If ref > 0 Then
                        '       hs.WriteLog(IFACE_NAME & " Error", "Amp seting Power" + ksuffix)
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).PR, True)
                        ' hs.WriteLog(IFACE_NAME & " Error", "Amp set Power " + ksuffix)
                    End If


                    ref = hs.GetDeviceRef(GetAddress("MU", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix + " Music Mute")
                        If ref > 0 Then
                            hs.WriteLog(IFACE_NAME & " Error", "Creating Amp Mute " + ksuffix)
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("MU", UN, KP) '"ZONE:" + KP.ToString()
                            dv.Device_Type_String(hs) = "Amp Mute"
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            DT.Device_Type = DeviceTypeInfo.eDeviceType_SourceSwitch.Source
                            DT.Device_SubType = 0
                            DT.Device_SubType_Description = ""
                            dv.DeviceType_Set(hs) = DT
                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            ' add an ON button and value
                            Dim Pair As VSPair
                            Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.SingleValue
                            Pair.ControlUse = ePairControlUse._On
                            Pair.Value = 1
                            Pair.Status = "On"
                            Pair.Render = Enums.CAPIControlType.Button
                            hs.DeviceVSP_AddPair(ref, Pair)
                            GPair = New VGPair
                            GPair.PairType = VSVGPairType.SingleValue
                            GPair.Set_Value = 1
                            GPair.Graphic = "/images/HSPI_MONOAMP/mute_on.png"
                            hs.DeviceVGP_AddPair(ref, GPair)

                            ' add an OFF button and value
                            Pair = New VSPair(HomeSeerAPI.ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.SingleValue
                            Pair.Value = 0
                            Pair.ControlUse = ePairControlUse._Off
                            Pair.Status = "Off"
                            Pair.Render = Enums.CAPIControlType.Button
                            hs.DeviceVSP_AddPair(ref, Pair)
                            GPair = New VGPair
                            GPair.PairType = VSVGPairType.SingleValue
                            GPair.Set_Value = 0
                            GPair.Graphic = "/images/HSPI_MONOAMP/mute_off.png"
                            hs.DeviceVGP_AddPair(ref, GPair)
                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.Status_Support(hs) = True
                            hs.SaveEventsDevices()
                            hs.WriteLog(IFACE_NAME & " Error", "Created Amp Mute " + ksuffix)
                        End If
                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).MU, True)
                    End If

                    ref = hs.GetDeviceRef(GetAddress("VO", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix + " Music Volume")
                        If ref > 0 Then
                            hs.WriteLog(IFACE_NAME & " Error", "Creating Amp Volume" + ksuffix)
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("VO", UN, KP) ' "ZONE:" + KP.ToString()
                            'dv.Can_Dim(hs) = True
                            dv.Device_Type_String(hs) = "Amp Volume"
                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            DT.Device_Type = DeviceTypeInfo.eDeviceType_SourceSwitch.Source

                            DT.Device_SubType = 0
                            DT.Device_SubType_Description = ""

                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""
                            dv.DeviceType_Set(hs) = DT
                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            Dim Pair As VSPair
                            '' add values, will appear as a radio control and only allow one option to be selected at a time
                            'Pair = New VSPair(ePairStatusControl.Both)

                            'Pair.PairType = VSVGPairType.SingleValue
                            'Pair.Value = 0
                            'Pair.Status = "Off"
                            'Pair.Render = Enums.CAPIControlType.Button
                            'Pair.Render_Location.Row = 1
                            'Pair.Render_Location.Column = 1
                            'Pair.ControlUse = ePairControlUse._Off            ' set this for UI apps like HSTouch so they know this is for OFF
                            'hs.DeviceVSP_AddPair(ref, Pair)




                            ' add DIM values
                            Pair = New VSPair(ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.Range
                            Pair.ControlUse = ePairControlUse._Dim            ' set this for UI apps like HSTouch so they know this is for lighting control dimming
                            Pair.RangeStart = MPRSG6Z.CommandValues.Min("VO")
                            Pair.RangeEnd = MPRSG6Z.CommandValues.Max("VO")
                            Pair.RangeStatusPrefix = ""
                            Pair.RangeStatusSuffix = ""
                            Pair.Render = Enums.CAPIControlType.ValuesRangeSlider
                            Pair.Render_Location.Row = 2
                            Pair.Render_Location.Column = 1
                            Pair.Render_Location.ColumnSpan = 4
                            hs.DeviceVSP_AddPair(ref, Pair)

                            GPair = New VGPair
                            GPair.PairType = VSVGPairType.SingleValue
                            GPair.Set_Value = 0
                            GPair.Graphic = "/images/HSPI_MONOAMP/level_off.png"
                            hs.DeviceVGP_AddPair(ref, GPair)

                            Dim d As Decimal = Pair.RangeEnd / 14
                            Dim lm As Decimal = 1
                            For index = 1 To 14
                                ' add graphic pairs for the dim levels
                                GPair = New VGPair()
                                GPair.PairType = VSVGPairType.Range
                                GPair.RangeStart = lm ' Math.Round((index * d) - d)
                                GPair.RangeEnd = Math.Round((index * d))
                                Dim Lstr As String = index.ToString("00")
                                lm = GPair.RangeEnd + 1
                                GPair.Graphic = "/images/HSPI_MONOAMP/level_" + Lstr + ".png"
                                hs.DeviceVGP_AddPair(ref, GPair)
                            Next

                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                            'dv.MISC_Set(hs, Enums.dvMISC.STATUS_ONLY)      ' set this for a status only device, no controls, and do not include the DeviceVSP calls above
                            dv.Status_Support(hs) = True
                        End If
                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).VO, True)
                    End If

                    ref = hs.GetDeviceRef(GetAddress("CH", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix+" Music Source")
                        If ref > 0 Then
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("CH", UN, KP) '"ZONE:" + KP.ToString()
                            dv.Device_Type_String(hs) = "Amp Source"
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            DT.Device_Type = DeviceTypeInfo.eDeviceType_SourceSwitch.Source
                            DT.Device_SubType = 0
                            DT.Device_SubType_Description = ""
                            dv.DeviceType_Set(hs) = DT
                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            Dim Pair As VSPair

                            Pair = New VSPair(ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.SingleValue
                            Pair.Render = Enums.CAPIControlType.Radio_Option
                            Pair.Value = 1 : Pair.Status = "Source 1" : Pair.Render_Location.Column = 1 : Pair.Render_Location.Row = 1 : hs.DeviceVSP_AddPair(ref, Pair)
                            Pair.Value = 2 : Pair.Status = "Source 2" : Pair.Render_Location.Column = 2 : Pair.Render_Location.Row = 1 : hs.DeviceVSP_AddPair(ref, Pair)
                            Pair.Value = 3 : Pair.Status = "Source 3" : Pair.Render_Location.Column = 3 : Pair.Render_Location.Row = 1 : hs.DeviceVSP_AddPair(ref, Pair)
                            Pair.Value = 4 : Pair.Status = "Source 4" : Pair.Render_Location.Column = 1 : Pair.Render_Location.Row = 2 : hs.DeviceVSP_AddPair(ref, Pair)
                            Pair.Value = 5 : Pair.Status = "Source 5" : Pair.Render_Location.Column = 2 : Pair.Render_Location.Row = 2 : hs.DeviceVSP_AddPair(ref, Pair)
                            Pair.Value = 6 : Pair.Status = "Source 6" : Pair.Render_Location.Column = 3 : Pair.Render_Location.Row = 2 : hs.DeviceVSP_AddPair(ref, Pair)

                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.Status_Support(hs) = True
                            hs.SaveEventsDevices()
                        End If
                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).CH, True)
                    End If

                    ref = hs.GetDeviceRef(GetAddress("BS", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix + " Music Bass")
                        If ref > 0 Then
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("BS", UN, KP) '"ZONE:" + KP.ToString()
                            'dv.Can_Dim(hs) = True
                            dv.Device_Type_String(hs) = "Amp Bass"

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            ' DT.Device_Type = 76
                            dv.DeviceType_Set(hs) = DT
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""


                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            Dim Pair As VSPair
                            ' add DIM values
                            Pair = New VSPair(ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.Range
                            Pair.ControlUse = ePairControlUse._Dim            ' set this for UI apps like HSTouch so they know this is for lighting control dimming
                            Pair.RangeStart = MPRSG6Z.CommandValues.Min("BS")
                            Pair.RangeEnd = MPRSG6Z.CommandValues.Max("BS")
                            Pair.RangeStatusPrefix = "Bass "
                            Pair.RangeStatusSuffix = "" '"%"
                            Pair.Render = Enums.CAPIControlType.ValuesRangeSlider
                            Pair.Render_Location.Row = 2
                            Pair.Render_Location.Column = 1
                            Pair.Render_Location.ColumnSpan = 3
                            hs.DeviceVSP_AddPair(ref, Pair)
                            ' add graphic pairs for the dim levels
                            Dim d As Decimal = Pair.RangeEnd / 14
                            Dim lm As Decimal = 0
                            For index = 1 To 14
                                ' add graphic pairs for the dim levels
                                GPair = New VGPair()
                                GPair.PairType = VSVGPairType.Range
                                GPair.RangeStart = lm ' Math.Round((index * d) - d)
                                GPair.RangeEnd = Math.Round((index * d))
                                Dim Lstr As String = index.ToString("00")
                                lm = GPair.RangeEnd + 1
                                GPair.Graphic = "/images/HSPI_MONOAMP/level_" + Lstr + ".png"
                                hs.DeviceVGP_AddPair(ref, GPair)
                            Next

                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)

                            dv.Status_Support(hs) = True
                        End If
                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).BS, True)
                    End If

                    ref = hs.GetDeviceRef(GetAddress("TR", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix + " Music Treble")
                        If ref > 0 Then
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("TR", UN, KP) '"ZONE:" + KP.ToString()
                            'dv.Can_Dim(hs) = True
                            dv.Device_Type_String(hs) = "Amp Treble"

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            ' DT.Device_Type = 76
                            dv.DeviceType_Set(hs) = DT
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""


                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            Dim Pair As VSPair
                            'GPair = New VGPair
                            'GPair.PairType = VSVGPairType.SingleValue
                            'GPair.Set_Value = 0
                            'GPair.Graphic = "/images/HomeSeer/status/off.gif"
                            'hs.DeviceVGP_AddPair(ref, GPair)

                            ' add DIM values
                            Pair = New VSPair(ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.Range
                            Pair.ControlUse = ePairControlUse._Dim            ' set this for UI apps like HSTouch so they know this is for lighting control dimming
                            Pair.RangeStart = MPRSG6Z.CommandValues.Min("TR")
                            Pair.RangeEnd = MPRSG6Z.CommandValues.Max("TR")
                            Pair.RangeStatusPrefix = "Treble "
                            Pair.RangeStatusSuffix = "" '"%"
                            Pair.Render = Enums.CAPIControlType.ValuesRangeSlider
                            Pair.Render_Location.Row = 2
                            Pair.Render_Location.Column = 1
                            Pair.Render_Location.ColumnSpan = 3
                            hs.DeviceVSP_AddPair(ref, Pair)
                            '' add graphic pairs for the dim levels
                            Dim d As Decimal = Pair.RangeEnd / 14
                            Dim lm As Decimal = 0
                            For index = 1 To 14
                                ' add graphic pairs for the dim levels
                                GPair = New VGPair()
                                GPair.PairType = VSVGPairType.Range
                                GPair.RangeStart = lm ' Math.Round((index * d) - d)
                                GPair.RangeEnd = Math.Round((index * d))
                                Dim Lstr As String = index.ToString("00")
                                lm = GPair.RangeEnd + 1
                                GPair.Graphic = "/images/HSPI_MONOAMP/level_" + Lstr + ".png"
                                hs.DeviceVGP_AddPair(ref, GPair)
                            Next

                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)

                            dv.Status_Support(hs) = True
                        End If

                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).TR, True)
                    End If

                    ref = hs.GetDeviceRef(GetAddress("BL", UN, KP))
                    If ref = -1 Then
                        ref = hs.NewDeviceRef(ksuffix + " Music Balance")
                        If ref > 0 Then
                            dv = hs.GetDeviceByRef(ref)
                            dv.Address(hs) = GetAddress("BL", UN, KP) '"ZONE:" + KP.ToString()
                            'dv.Can_Dim(hs) = True
                            dv.Device_Type_String(hs) = "Amp Balance"

                            Dim DT As New DeviceTypeInfo
                            DT.Device_API = DeviceTypeInfo.eDeviceAPI.SourceSwitch
                            ' DT.Device_Type = 76
                            dv.DeviceType_Set(hs) = DT
                            dv.Interface(hs) = IFACE_NAME
                            dv.InterfaceInstance(hs) = ""
                            dv.Last_Change(hs) = #5/21/1929 11:00:00 AM#
                            dv.Location(hs) = IFACE_NAME
                            dv.Location2(hs) = ""


                            dv.Relationship(hs) = Enums.eRelationship.Child
                            If therm_root_dv IsNot Nothing Then
                                therm_root_dv.AssociatedDevice_Add(hs, ref)
                            End If
                            dv.AssociatedDevice_Add(hs, therm_root_dv.Ref(hs))

                            Dim Pair As VSPair
                            'GPair = New VGPair
                            'GPair.PairType = VSVGPairType.SingleValue
                            'GPair.Set_Value = 0
                            'GPair.Graphic = "/images/HomeSeer/status/off.gif"
                            'hs.DeviceVGP_AddPair(ref, GPair)

                            ' add DIM values
                            Pair = New VSPair(ePairStatusControl.Both)
                            Pair.PairType = VSVGPairType.Range
                            Pair.ControlUse = ePairControlUse._Dim            ' set this for UI apps like HSTouch so they know this is for lighting control dimming
                            Pair.RangeStart = MPRSG6Z.CommandValues.Min("BL")
                            Pair.RangeEnd = MPRSG6Z.CommandValues.Max("BL")
                            Pair.RangeStatusPrefix = "Balance "
                            Pair.RangeStatusSuffix = "" '"%"
                            Pair.Render = Enums.CAPIControlType.ValuesRangeSlider
                            Pair.Render_Location.Row = 2
                            Pair.Render_Location.Column = 1
                            Pair.Render_Location.ColumnSpan = 3
                            hs.DeviceVSP_AddPair(ref, Pair)
                            '' add graphic pairs for the dim levels
                            Dim d As Decimal = Pair.RangeEnd / 14
                            Dim lm As Decimal = 0
                            For index = 1 To 14
                                ' add graphic pairs for the dim levels
                                GPair = New VGPair()
                                GPair.PairType = VSVGPairType.Range
                                GPair.RangeStart = lm ' Math.Round((index * d) - d)
                                GPair.RangeEnd = Math.Round((index * d))
                                Dim Lstr As String = index.ToString("00")
                                lm = GPair.RangeEnd + 1
                                GPair.Graphic = "/images/HSPI_MONOAMP/level_" + Lstr + ".png"
                                hs.DeviceVGP_AddPair(ref, GPair)
                            Next
                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)
                            dv.MISC_Set(hs, Enums.dvMISC.SHOW_VALUES)
                            dv.MISC_Set(hs, Enums.dvMISC.NO_LOG)

                            dv.Status_Support(hs) = True
                        End If
                    End If
                    If ref > 0 Then
                        hs.SetDeviceValueByRef(ref, amp.KeyPadID(UN, KP).BL, True)
                    End If
                Next
            Next
            '    End If

        Catch ex As Exception
            hs.WriteLog(IFACE_NAME & " Error", "Exception in Find_Create_Devices/Create: " & ex.Message + " " + ex.StackTrace)
        End Try

    End Sub


    '#Region "Web Page Processing"

    '    Private Function SelectPage(ByVal pageName As String) As Object
    '        SelectPage = Nothing
    '        Select Case pageName
    '            Case ConfigPage.PageName
    '                SelectPage = ConfigPage
    '            Case StatusPage.PageName
    '                SelectPage = StatusPage
    '            Case Else
    '                SelectPage = ConfigPage
    '        End Select
    '    End Function

    '    Public Function postBackProc(page As String, data As String, user As String, userRights As Integer) As String
    '        WebPage = SelectPage(page)
    '        Return WebPage.postBackProc(page, data, user, userRights)
    '    End Function

    '    Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
    '        ' build and return the actual page
    '        WebPage = SelectPage(pageName)
    '        Return WebPage.GetPagePlugin(pageName, user, userRights, queryString)
    '    End Function

    '#End Region

End Class
