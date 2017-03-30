Imports System.IO
Public Class Form1
    Dim ClientID As Integer = 0
    Private Structure CustomerDetails
        Public CustomerID As Integer
        Public FirstName As String
        Public LastName As String
        Public Phone As String
        Public PostCode As String
    End Structure
    Private Sub btnStore_Click(sender As System.Object, e As System.EventArgs) Handles btnStore.Click
        Dim FirstName, LastName, Phone, PostCode As String
        txtPhone.Text = LTrim(txtPhone.Text)   'Removes uneeded spaces so when a length check is done the spaces wont be there to count as a character
        txtPhone.Text = RTrim(txtPhone.Text)
        ClientID = CInt(txtID.Text)
        FirstName = txtFirstName.Text
        LastName = txtLastName.Text
        Phone = txtPhone.Text
        PostCode = txtPostCode.Text
        If txtFirstName.Text = "" Then
            MessageBox.Show("Please enter your First name")   'If the user doesnt enter their first name then an eror message is output telling the user to enter their last name 
            Exit Sub
        ElseIf txtLastName.Text = "" Then
            MessageBox.Show("Please enter your last name")    'If the user doesnt enter their last naem then a mesage is output telling the user to enter their lastname 
            Exit Sub
        End If
        If txtPostCode.Text = "" Then
            MessageBox.Show("Please nete your postcode")     'If the user ahsnt entered a postcode then a message is output telling them to enter their postcode
            Exit Sub
        End If
        If txtPhone.Text = "" Then
            MessageBox.Show("Please enter your phone number")   ''If thye user doesnt enter their phone number then a message is output telling the user to enter their phone number 
            Exit Sub
        Else
            If IsNumeric(txtPhone.Text) = False Then
                MessageBox.Show("Pleade enter a valid phone number")    'checks if the value entered is numeric
                Exit Sub
            Else
                If txtPhone.Text.Length > 11 Or txtPhone.Text.Length < 11 Then
                    MessageBox.Show("Please enter a valid phone number")         'Lenght check tos ee if it is valid phone number
                    Exit Sub
                End If
            End If
        End If
        Dim CustomerDetails As New CustomerDetails
        Dim sw As New StreamWriter(Application.StartupPath & "Details.txt", True)
        CustomerDetails.CustomerID = LSet(txtID.Text, 7)
        CustomerDetails.FirstName = LSet(txtFirstName.Text, 30)   'Adds the new custoemr details to the text file 
        CustomerDetails.LastName = LSet(txtLastName.Text, 30)
        CustomerDetails.Phone = LSet(txtPhone.Text, 11)
        CustomerDetails.PostCode = LSet(txtPostCode.Text, 6)
        sw.WriteLine(CustomerDetails.CustomerID & " " & CustomerDetails.FirstName & " " & CustomerDetails.LastName & " " & CustomerDetails.Phone & " " & CustomerDetails.PostCode)
        sw.Close()
        txtFirstName.Clear()
        txtID.Clear()
        txtLastName.Clear()    'Clears all the textboxes in which the custoemr would ahve typed their details in 
        txtPhone.Clear()
        txtPostCode.Clear()
    End Sub

    Private Sub btnRetrive_Click(sender As System.Object, e As System.EventArgs) Handles btnRetrive.Click
        Try
            Dim lines() As String = IO.File.ReadAllLines(Dir$("Details.txt"))   'Retrives all data in the text file
            lbDetails.Items.AddRange(lines)                                     'Adds the details to the list box 
        Catch ex As Exception
            MessageBox.Show("there was an error when retriving customer details")  'Error message if anything goes wrong when retriving the customer details 
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        If txtID.Enabled = True Then
            Dim MaxID As Integer = 0
            ClientID = CInt(txtID.Text)
            Try
                Dim CustomerData() As String = File.ReadAllLines(Dir$("Details.txt"))
                For I = 1 To UBound(CustomerData)
                    lbDetails.Items.Clear()            'Clears all items in the list box 
                    If ClientID = Val(Mid(CustomerData(I), 1, 8)) Then
                        lbDetails.Items.AddRange(CustomerData)
                        MessageBox.Show("Customer details have been found and loaded")
                    Else
                        MessageBox.Show("Customer details were not found")
                    End If
                Next I
                txtID.Enabled = False
            Catch ex As Exception
                MessageBox.Show("Customer details were unable to load correctly")
            End Try
        Else
            txtID.Enabled = True  'Alloows the user to edit what value may be in the textbox
        End If
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        lbDetails.Items.Clear()   'Clears the list box of any items in it
    End Sub
End Class
