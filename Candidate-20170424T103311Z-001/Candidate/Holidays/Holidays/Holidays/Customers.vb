Imports System.IO
Public Class Customers
    Public Structure CustomerDetails
        Public ID As Integer
        Public FirstName As String
        Public LastName As String
        Public Phone As String
        Public Postcode As String
    End Structure
    Private Sub Customers_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim ID As Integer
        ID = CInt(txtCustomerID.Text)    'Converts the text in the textbox to an integer 
    End Sub
    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim FirstName, LastName, Phone, Postcode As String
        FirstName = txtFirstName.Text
        LastName = txtLastName.Text
        Phone = RTrim(txtPhone.Text)    'Removes any uneeded spaces so that the length check will work properally and therefore the user cant fill up the textbox with spaces 
        Phone = LTrim(txtPhone.Text)
        Postcode = txtPostcode.Text.ToUpper   'Converts the characters entered in the textbox to upper case so that the postcode is accurate when being saved 
        If txtFirstName.Text = "" Then
            MsgBox("Please enter your first name")   'Presence check to see if the user has entered their firstname so that when saving an error wont occur 
            Exit Sub
        ElseIf txtLastName.Text = "" Then
            MsgBox("Please enter your last name")     'Presence check to see if the user has entered their last name so that an error wont occur when trying to save values which arent there
        ElseIf txtPhone.Text = "" Then
            MsgBox(" Please enter your phone number")    'presence check to see if the user has entered their phoen number
            Exit Sub
        Else
            If IsNumeric(txtPhone.Text) = False Then
                MsgBox("Please enter a valid phone number")   'Data type check to see if the value entered by the user is a numerical value and if not then an error message is output
                Exit Sub
            ElseIf txtPhone.Text.Length < 11 Or txtPhone.Text.Length > 11 Then   'Length check to see if the users number entered is 11 characters long which is the length of a phone number
                MsgBox("Please enter a valid phone number")
                Exit Sub
            End If
        End If
        Try
            Dim CustomerData As New CustomerDetails                             'Declares  custoemr data as the values in the record structure CustomerDetails
            Dim sw As New System.IO.StreamWriter("CustoemrDetails.txt", True)
            CustomerData.ID = LSet(txtCustomerID.Text, 50)
            CustomerData.FirstName = LSet(txtFirstName.Text, 50)
            CustomerData.LastName = LSet(txtLastName.Text, 50)
            CustomerData.Phone = LSet(txtPhone.Text, 11)
            CustomerData.Postcode = LSet(txtPostcode.Text, 6)
            txtCustomerID.Text = txtCustomerID.Text + 1
            sw.WriteLine(CustomerData.ID & " " & CustomerData.FirstName & " " & CustomerData.LastName & " " & CustomerData.Phone & "" & CustomerData.Postcode)   'Writes all the custoemr details to the text file and then outputs a message sayingt that the file has saved 
            sw.Close()
            MsgBox("Details saved")
            txtFirstName.Clear()
            txtLastName.Clear()  'Clears all the textboxes except the Id textbox as that is going to be added by one 
            txtPhone.Clear()
            Dim CustomerID() As String = File.ReadAllLines("CustomerDetails.txt")
            For i = 1 To UBound(CustomerID)
                If Trim(Mid(CustomerID(i), 1, 50)) = txtCustomerID.Text Then   'If the ID in the customer ID textbox equals the ID in the record structure then the value in the textbox is incremented by one 
                    txtCustomerID.Text = Str(CustomerID(i)) + 1
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show("There was a problem saving the details please try again later")  'If an error occurs when saving the customer details then a message is output telling the user to try again 
        End Try
    End Sub
    Private Sub cmdRetrive_Click(sender As System.Object, e As System.EventArgs) Handles cmdRetrive.Click
        Dim CustomerData() As String = File.ReadAllLines("CustomerDetails.txt")
        For i = 1 To UBound(CustomerData)
            If Trim(Mid(CustomerData(i), 1, 50)) = txtCustomerID.Text Then   'If the ID in the text file is equal to the ID input then the details are retrived and added to the listbox 
                lbDetails.Items.AddRange(CustomerData)
            End If
        Next i
    End Sub
    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        lbDetails.Items.Clear()    'Clears the details in the listbox 
    End Sub
End Class