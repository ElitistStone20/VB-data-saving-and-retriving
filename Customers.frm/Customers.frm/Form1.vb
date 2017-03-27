Imports System.IO
Public Class Form1
    Dim FirstName, LastName, Phone, Address As String
    Dim ClientID As Integer
    Private Structure CustomerDetails   'Record structure to store the values 
        Public ClientID As Integer
        Public FirstName As String
        Public LastName As String
        Public Address As String
        Public PhoneNumber As String
    End Structure
    Private Sub btnStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStore.Click
        FirstName = txtFirstName.Text
        LastName = txtLastName.Text
        Address = txtAddress.Text
        Phone = LTrim(txtPhone.Text)   'Removes spaces on the left and right side of the values in the phone number textbox
        Phone = RTrim(txtPhone.Text)
        If txtFirstName.Text = "" Or txtLastName.Text = "" Then
            MessageBox.Show("Please enter your first or last name")  'Checks to see if the users has entered their name and if not error message is outputed 
            txtFirstName.BackColor = Color.Red
            txtLastName.BackColor = Color.Red
            Exit Sub
        End If
        If txtAddress.Text = "" Then
            MessageBox.Show("Please enter your address")                     'Checks to see if the users has entered their age 
            txtAddress.BackColor = Color.Red
            Exit Sub
        End If
        If IsNumeric(txtPhone.Text) = False Then
            MessageBox.Show("Please enter a valid phone number")             'Checks to see if the value in the phone number textbox is a numerical value 
            txtPhone.BackColor = Color.Red
            Exit Sub
            If txtPhone.Text.Length > 11 Or txtPhone.Text.Length < 11 Then
                MessageBox.Show("Please enter a valid phone number")         'Checks if the value in the phone number textbox is greater or less than 11
                txtPhone.BackColor = Color.Red
                Exit Sub
            End If
        End If
        Dim CustomerDetails As New CustomerDetails
        Dim sw As New StreamWriter(Application.StartupPath & "\Leisure.txt", True)    'Stores customer details in the textboxes in a textfile
        CustomerDetails.FirstName = LSet(txtFirstName.Text, 30)
        CustomerDetails.LastName = LSet(txtLastName.Text, 30)
        CustomerDetails.Address = LSet(txtAddress.Text, 8)
        CustomerDetails.PhoneNumber = LSet(txtPhone.Text, 11)
        sw.WriteLine(CustomerDetails.FirstName & " " & CustomerDetails.LastName & " " & CustomerDetails.Address & " " & CustomerDetails.PhoneNumber)
        sw.Close()
        txtAddress.Clear()
        txtFirstName.Clear()                'Clears all textboxes and then displays a message saying that the users details were successfully stored
        txtLastName.Clear()
        txtPhone.Clear()
        MessageBox.Show("Your details have been stored")
        txtPhone.BackColor = Color.White
        txtAddress.BackColor = Color.White
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
    End Sub
    Private Sub btnRetrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetrieve.Click
        Dim lines() As String = IO.File.ReadAllLines(Dir$("Leisure.txt"))  'Retrives data from the text file and displays the data in a list box 
        lbCustomerDetails.Items.AddRange(lines)                            'Adds items to the list box
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lbCustomerDetails.Items.Clear()            'Clears the list box 
    End Sub
End Class
